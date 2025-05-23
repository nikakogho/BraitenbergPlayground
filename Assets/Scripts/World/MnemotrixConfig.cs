using Core;
using UnityEngine;

namespace World
{
    [System.Serializable]
    public sealed class MnemotrixConfig : VehicleWireConfig
    {
        // Plastic fields
        public PlasticWeight weight = new();        // mutable gain
        [Range(0, 1)] public float learnThreshold = 0.7f;
        [Range(0, 1)] public float forgetThreshold = 0.1f;
        public float learnedGain = 1f;
        [HideInInspector] public bool learned;

        public void Learn()
        {
            if (learned) return;

            float preVal = (sourceKind == SourceType.Sensor)
                           ? sensor.CoreSensor.Value
                           : unitSrc.CoreUnit.Output;

            float postVal = (targetKind == TargetType.ThresholdUnit)
                            ? unitDst.CoreUnit.Output
                            : motorDst.CoreMotor.Power;      // or 0 if you skip

            if (preVal > learnThreshold && postVal > learnThreshold)
            {
                weight.value = learnedGain;
                learned = true;
            }
        }

        public void PassiveForget(float forgetAmount)
        {
            weight.value = Mathf.MoveTowards(
                weight.value,
                0f,
                forgetAmount);

            if (weight.value < forgetThreshold) learned = false;
        }
    }
}
