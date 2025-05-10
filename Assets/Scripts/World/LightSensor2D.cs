using UnityEngine;
using Core;

namespace World
{
    [DisallowMultipleComponent]
    public sealed class LightSensor2D : MonoBehaviour
    {
        public static bool showRays = false;

        [Header("Optics")]
        [Tooltip("Cone half-angle in degrees")]
        [Range(0, 90)] public float halfFOV = 20f;
        [Tooltip("How many rays across the cone")]
        [Range(1, 25)] public int rays = 5;
        [Tooltip("Maximum sensing distance")]
        public float sampleRange = 5f;
        [Tooltip("Layer(s) treated as light emitters")]
        public LayerMask lightLayer = ~0;

        [Header("Gizmo")]
        public Color gizmoColor = Color.yellow;

        public Sensor CoreSensor { get; private set; }

        // Allocated once for perf
        private readonly RaycastHit2D[] _hits = new RaycastHit2D[1];

        private void Awake() => CoreSensor = new Sensor(SampleLight);

        private float SampleLight()
        {
            float best = 0f;                                   // keep the brightest ray
            const float epsilon = 0.001f;                      // avoids divide-by-0
            float bestDist = float.MaxValue;

            for (int i = 0; i < rays; i++)
            {
                float t = (rays == 1) ? 0f : (float)i / (rays - 1);
                float angle = Mathf.Lerp(-halfFOV, +halfFOV, t);
                Vector2 dir = Quaternion.Euler(0, 0, angle) * transform.up;

                if (showRays)
                    Debug.DrawRay(transform.position, dir * sampleRange, gizmoColor, 0f, false);

                int hitCount = Physics2D.RaycastNonAlloc(transform.position,
                                                         dir,
                                                         _hits,
                                                         sampleRange,
                                                         lightLayer);
                if (hitCount == 0) continue;

                float d = Mathf.Max(epsilon, _hits[0].distance);     // 0 → ε so intensity = 1

                float intrinsic = 1f;
                if (_hits[0].collider.TryGetComponent(out LightEmitter2D emitter))
                    intrinsic = emitter.brightness;

                // linear fall-off: 1 at d = 0, 0 at d = sampleRange

                float intensity = intrinsic * Mathf.Clamp01(1f - d / sampleRange);

                if (d < bestDist) bestDist = d;

                best = Mathf.Max(best, intensity);
            }

            return best;      // 0 (no light) … 1 (lamp right on the sensor)
        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = gizmoColor;
            Vector3 origin = transform.position;

            for (int i = 0; i < rays; i++)
            {
                float t = (rays == 1) ? 0f : (float)i / (rays - 1);
                float angle = Mathf.Lerp(-halfFOV, +halfFOV, t);
                Vector3 dir = Quaternion.Euler(0, 0, angle) * transform.up;

                Gizmos.DrawLine(origin, origin + dir * sampleRange);
            }

            // Outline the cone edges for clarity
            Gizmos.DrawLine(origin,
                            origin + Quaternion.Euler(0, 0, -halfFOV) * transform.up * sampleRange);
            Gizmos.DrawLine(origin,
                            origin + Quaternion.Euler(0, 0, +halfFOV) * transform.up * sampleRange);
        }
#endif
    }
}
