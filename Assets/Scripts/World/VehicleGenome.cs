using UnityEngine;

namespace World
{
    [System.Serializable]
    public struct VehicleGenome
    {
        public float[] gains;
        public VehicleGenome(float[] src) => gains = src;

        public VehicleGenome Mutate(float rate = 0.1f)
        {
            float[] m = new float[gains.Length];
            for (int i = 0; i < gains.Length; i++)
                m[i] = gains[i] * (1f + Random.Range(-rate, rate));
            return new VehicleGenome(m);
        }
    }
}
