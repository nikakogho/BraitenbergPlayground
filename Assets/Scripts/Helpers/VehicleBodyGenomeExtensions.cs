using World;

namespace Helpers
{
    public static class VehicleBodyGenomeExtensions
    {
        public static float[] GetGains(this VehicleBody vb)
        {
            int n = vb.wires.Count;
            float[] g = new float[n];
            for (int i = 0; i < n; i++) g[i] = vb.wires[i].gain;
            return g;
        }

        public static void ApplyGenome(this VehicleBody vb, VehicleGenome genome)
        {
            for (int i = 0; i < vb.wires.Count; i++)
                vb.wires[i].gain = genome.gains[i];
        }
    }
}
