using System.Collections.Generic;

namespace Core
{
    public sealed class Vehicle
    {
        // ── containers ──────────────────────────────────────────────────────────
        private readonly List<Sensor> _sensors = new();
        private readonly List<ThresholdUnit> _units = new();
        private readonly List<Wire> _wires = new();

        // ── builder API ─────────────────────────────────────────────────────────
        public void AddSensor(Sensor s) => _sensors.Add(s);
        public void AddUnit(ThresholdUnit u) => _units.Add(u);
        public void AddWire(Wire w) => _wires.Add(w);

        // ── simulation step ─────────────────────────────────────────────────────
        public void Tick()
        {
            // 1. Sensors read the world
            foreach (var s in _sensors) s.Sense();

            // 2. Reset accumulators on all logic units
            foreach (var u in _units) u.Reset();

            // 3. Transmit along EVERY wire once
            foreach (var w in _wires) w.TransmitPower();

            // 4. Apply thresholds, storing new outputs for the next tick
            foreach (var u in _units) u.ApplyThreshold();
        }
    }
}