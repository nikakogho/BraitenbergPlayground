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
            foreach (var row in wires)
            {
                _vehicle.AddSensor(row.sensor.CoreSensor);
                row.motor.Init(_rb);

                var w = new Wire(row.sensor.CoreSensor,
                                 row.motor.CoreMotor,
                                 row.gain);

                _vehicle.AddWire(w);
            }
        }

        private void Update() => _vehicle.Tick();
    }
}
