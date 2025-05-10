namespace Core
{
    public sealed class ThresholdUnit
    {
        private float _accum;          // summed weighted input
        public float Output { get; private set; }  // 0 or 1

        public float Threshold { get; }

        public ThresholdUnit(float threshold = 0.7f) => Threshold = threshold;

        public void Reset() => _accum = 0f;
        public void Accumulate(float val) => _accum += val;

        public void ApplyThreshold() =>
            Output = _accum >= Threshold ? 1f : 0f;
    }
}
