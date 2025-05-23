using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class VehicleBody : MonoBehaviour
    {
        [Header("Regular 'dead' wires")]
        public List<VehicleWireConfig> wires = new();

        [Header("Plastic (Mnemotrix) wires")]
        public List<MnemotrixConfig> mnemotrixWires = new();

        public Vehicle Vehicle { get; private set; }
        private Rigidbody2D _rb;

        private void Awake() => _rb = GetComponent<Rigidbody2D>();

        private void Start()
        {
            Vehicle = new Vehicle();

            SetupRegularWires();
            SetupMnemotrixWires();
        }

        private void SetupRegularWires()
        {
            // First pass: register sensors & units, and init motors
            foreach (var cfg in wires)
            {
                if (cfg.sourceKind == VehicleWireConfig.SourceType.Sensor)
                    Vehicle.AddSensor(cfg.sensor.CoreSensor);

                if (cfg.sourceKind == VehicleWireConfig.SourceType.ThresholdUnit)
                    Vehicle.AddUnit(cfg.unitSrc.CoreUnit);

                if (cfg.targetKind == VehicleWireConfig.TargetType.ThresholdUnit)
                    Vehicle.AddUnit(cfg.unitDst.CoreUnit);

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

                Vehicle.AddWire(wire);
            }
        }

        private void SetupMnemotrixWires()
        {
            foreach (var mnemotrix in mnemotrixWires)
            {
                // ----- pick the pre-value -------
                Func<float> rawInput = mnemotrix.sourceKind switch
                {
                    VehicleWireConfig.SourceType.Sensor => () => mnemotrix.sensor.CoreSensor.Value,
                    VehicleWireConfig.SourceType.ThresholdUnit => () => mnemotrix.unitSrc.CoreUnit.Output,
                    _ => () => 0f
                };

                Func<float> read = () => rawInput() * mnemotrix.weight.value;

                // ----- pick the post-write ------
                Action<float> write = mnemotrix.targetKind switch
                {
                    VehicleWireConfig.TargetType.ThresholdUnit => v => mnemotrix.unitDst.CoreUnit.Accumulate(v),
                    VehicleWireConfig.TargetType.Motor => v => mnemotrix.motorDst.CoreMotor.SetPower(v),
                    _ => _ => { }
                };

                Vehicle.AddWire(new Wire(read, write, 1f));

                //-- registration so Tick handles endpoints
                if (mnemotrix.sourceKind == VehicleWireConfig.SourceType.Sensor)
                    Vehicle.AddSensor(mnemotrix.sensor.CoreSensor);
                else
                    Vehicle.AddUnit(mnemotrix.unitSrc.CoreUnit);

                if (mnemotrix.targetKind == VehicleWireConfig.TargetType.ThresholdUnit)
                    Vehicle.AddUnit(mnemotrix.unitDst.CoreUnit);
            }
        }

        private void MnemotrixMemoryTick()
        {
            const float forgetPerSecond = 0.05f; // 5 %/s  → ~20 s half-life
            float forgetAmount = forgetPerSecond * Time.deltaTime;

            foreach (var mnemotrix in mnemotrixWires)
            {
                mnemotrix.Learn();
                mnemotrix.PassiveForget(forgetAmount);
            }
        }

        private void Update()
        {
            Vehicle.Tick();
            MnemotrixMemoryTick();
        }
    }
}
