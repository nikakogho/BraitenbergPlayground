using System.Collections.Generic;

namespace Core
{
    public sealed class Vehicle
    {
        private readonly List<Sensor> _sensors = new();
        private readonly List<Wire> _wires = new();

        public IReadOnlyList<Sensor> Sensors => _sensors;
        public IReadOnlyList<Wire> Wires => _wires;

        public void AddSensor(Sensor s) => _sensors.Add(s);
        public void AddWire(Wire w) => _wires.Add(w);

        public void Tick()
        {
            foreach (var s in _sensors) s.Sense();
            foreach (var w in _wires) w.TransmitPower();
        }
    }
}