namespace World
{
    /// <summary>Represents ONE directed, weighted connection set in the Inspector.</summary>
    [System.Serializable]
    public class VehicleWireConfig
    {
        public enum SourceType { Sensor, ThresholdUnit }
        public enum TargetType { ThresholdUnit, Motor }

        public SourceType sourceKind;
        public TargetType targetKind;

        public WorldSensor sensor;   // used if sourceKind == Sensor
        public ThresholdUnit2D unitSrc;  // used if sourceKind == ThresholdUnit

        public ThresholdUnit2D unitDst;  // used if targetKind == ThresholdUnit
        public WheelMotor2D motorDst; // used if targetKind == Motor

        public float gain = 1f;
    }
}
