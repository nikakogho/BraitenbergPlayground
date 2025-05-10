using Core;
using UnityEngine;

namespace World
{
    [DisallowMultipleComponent]
    public class ThresholdUnit2D : MonoBehaviour
    {
        [Range(0f, 1f)] public float threshold = 0.7f;

        public ThresholdUnit CoreUnit { get; private set; }

        private void Awake() => CoreUnit = new ThresholdUnit(threshold);
    }
}
