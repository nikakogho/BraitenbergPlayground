namespace Core
{
    public sealed class Motor
    {
        public float Baseline { get; } // tonic drive
        public float Power { get; private set; }

        public Motor(float baseline = 0.1f) // default tiny hum
        {
            Baseline = baseline;
        }

        public void SetPower(float powerFromWires)
        {
            Power = Baseline + powerFromWires; // never below baseline
        }
    }
}
