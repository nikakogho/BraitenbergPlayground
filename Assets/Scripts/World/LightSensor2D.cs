using UnityEngine;
using Core;

namespace World
{
    [DisallowMultipleComponent]
    public sealed class LightSensor2D : MonoBehaviour
    {
        [Tooltip("Layer(s) treated as light emitters")]
        public LayerMask lightLayer;

        [Tooltip("Distance for raycast sample")]
        public float sampleRange = 5f;

        [Header("Gizmo")]
        public Color gizmoColor = Color.yellow;

        public Sensor CoreSensor { get; private set; }

        private void Awake() => CoreSensor = new Sensor(SampleLight);

        private float SampleLight()
        {
            // Raycast straight ahead
            var hit = Physics2D.Raycast(transform.position,
                                        transform.up,
                                        sampleRange,
                                        lightLayer);
            if (!hit) return 0f;

            // Simple inverse-square falloff
            return 1f / (1f + hit.distance * hit.distance);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawLine(transform.position,
                            transform.position + transform.up * sampleRange);
            Gizmos.DrawSphere(transform.position + transform.up * sampleRange, 0.05f);
        }
#endif
    }
}
