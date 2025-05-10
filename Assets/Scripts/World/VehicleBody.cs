using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class VehicleBody : MonoBehaviour
    {
        [Header("Wiring table (add rows in Inspector)")]
        public List<VehicleWireConfig> wires = new();

        private Vehicle _vehicle;
        private Rigidbody2D _rb;

        private void Awake() => _rb = GetComponent<Rigidbody2D>();

        private void Start()
        {
            _vehicle = new Vehicle();

            // First pass: register sensors & units, and init motors
            foreach (var cfg in wires)
            {
                if (cfg.sourceKind == VehicleWireConfig.SourceType.Sensor)
                    _vehicle.AddSensor(cfg.sensor.CoreSensor);

                if (cfg.sourceKind == VehicleWireConfig.SourceType.ThresholdUnit)
                    _vehicle.AddUnit(cfg.unitSrc.CoreUnit);

                if (cfg.targetKind == VehicleWireConfig.TargetType.ThresholdUnit)
                    _vehicle.AddUnit(cfg.unitDst.CoreUnit);

                if (cfg.targetKind == VehicleWireConfig.TargetType.Motor)
                    cfg.motorDst.Init(_rb);
            }

            // Second pass: build wires
            foreach (var cfg in wires)
            {
                Func<float> read;
                Action<float> write;

                // source delegate
                read = cfg.sourceKind switch
                {
                    VehicleWireConfig.SourceType.Sensor => () => cfg.sensor.CoreSensor.Value,
                    VehicleWireConfig.SourceType.ThresholdUnit => () => cfg.unitSrc.CoreUnit.Output,
                    _ => throw new Exception("Unknown sourceKind")
                };

                // target delegate
                write = cfg.targetKind switch
                {
                    VehicleWireConfig.TargetType.ThresholdUnit => val => cfg.unitDst.CoreUnit.Accumulate(val),
                    VehicleWireConfig.TargetType.Motor => val => cfg.motorDst.CoreMotor.SetPower(val),
                    _ => throw new Exception("Unknown targetKind")
                };

                var wire = new Wire(read, write, cfg.gain);

                _vehicle.AddWire(wire);
            }
        }

        private void Update() => _vehicle.Tick();
    }
}
