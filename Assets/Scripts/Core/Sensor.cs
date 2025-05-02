using System;

namespace Core
{
    public sealed class Sensor
    {
        private readonly Func<float> _sensingLogic;
        public float Value { get; private set; }

        public Sensor(Func<float> sensingLogic)
        {
            _sensingLogic = sensingLogic;
        }

        public float Sense()
        {
            Value = _sensingLogic();

            return Value;
        }
    }
}
