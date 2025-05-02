using Core;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
    public sealed class VehicleBody : MonoBehaviour
    {
        [Header("Wire list = one row per physical wire")]
        public List<VehicleWireConfig> wires = new();

        private Vehicle _vehicle;

        private void Awake()
        {
            _vehicle = new Vehicle();

            // Build Core object graph
            foreach (var row in wires)
            {
                _vehicle.AddSensor(row.sensor.CoreSensor);
                var w = new Wire(row.sensor.CoreSensor,
                                 row.motor.CoreMotor,
                                 row.gain);
                _vehicle.AddWire(w);
            }
        }

        private void Update() => _vehicle.Tick();
    }
}
