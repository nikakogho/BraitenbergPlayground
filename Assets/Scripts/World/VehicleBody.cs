using Core;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class VehicleBody : MonoBehaviour
    {
        [Header("Wire list = one row per physical wire")]
        public List<VehicleWireConfig> wires = new();

        private Vehicle _vehicle;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _vehicle = new Vehicle();

            // Build Core object graph
            foreach (var wireConfig in wires)
            {
                _vehicle.AddSensor(wireConfig.sensor.CoreSensor);
                wireConfig.motor.Init(_rb);

                var wire = new Wire(wireConfig.sensor.CoreSensor,
                                 wireConfig.motor.CoreMotor,
                                 wireConfig.gain);

                _vehicle.AddWire(wire);
            }
        }

        private void Update() => _vehicle.Tick();
    }
}
