using System;

namespace Core
{
    public sealed class Sensor
    {
        public readonly Func<float> SensingLogic;
        public float Value { get; private set; }

        public Sensor(Func<float> sensingLogic)
        {
            SensingLogic = sensingLogic;
        }

        public float Sense()
        {
            Value = SensingLogic();

            return Value;
        }
    }
}
