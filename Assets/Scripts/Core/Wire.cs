namespace Core
{
    public sealed class Wire
    {
        private readonly Sensor _from;
        private readonly Motor _to;
        private readonly float _gain;

        public Wire(Sensor from, Motor to, float gain = 1f)
        {
            _from = from;
            _to = to;
            _gain = gain;
        }

        public void TransmitPower()
        {
            _to.SetPower(_from.Value * _gain);
        }
    }
}
