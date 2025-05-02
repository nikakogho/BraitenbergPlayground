namespace Core
{
    public sealed class Motor
    {
        public float Power { get; private set; }

        public void SetPower(float power)
        {
            Power = power;
        }
    }
}
