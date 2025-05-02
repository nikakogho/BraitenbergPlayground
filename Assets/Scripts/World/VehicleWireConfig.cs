namespace World
{
    [System.Serializable]
    public sealed class VehicleWireConfig
    {
        public LightSensor2D sensor;
        public WheelMotor2D motor;
        public float gain = 1f;
    }
}
