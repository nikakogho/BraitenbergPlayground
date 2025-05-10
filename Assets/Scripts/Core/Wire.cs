using System;

namespace Core
{
    public sealed class Wire
    {
        private readonly Func<float> _input;
        private readonly Action<float> _output;
        private readonly float _weight;

        public Wire(Func<float> input, Action<float> output, float weight)
        {
            _input = input;
            _output = output;
            _weight = weight;
        }

        public void TransmitPower()
        {
            var power = _input() * _weight;

            _output(power);
        }
    }
}
