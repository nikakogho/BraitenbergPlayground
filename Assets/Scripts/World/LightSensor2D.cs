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
            float best = 0f;
            const float bias = 0.02f;

            for (int i = 0; i < rays; i++)
            {
                float t = (rays == 1) ? 0f : (float)i / (rays - 1);
                float angle = Mathf.Lerp(-halfFOV, +halfFOV, t);
                Vector2 dir = Quaternion.Euler(0, 0, angle) * transform.up;

                if (showRays)
                {
                    Debug.DrawLine(transform.position,
                                   transform.position + (Vector3)(dir * sampleRange),
                                   gizmoColor,         // yellow, or your field
                                   0f,                 // duration, 0 = 1 frame
                                   false);             // don't depth-test
                }

                Vector2 origin = (Vector2)transform.position + dir * bias;

                int n = Physics2D.RaycastNonAlloc(origin,
                                                  dir,
                                                  _hits,
                                                  sampleRange,
                                                  lightLayer);
                if (n == 0) continue;

                var hit = _hits[0];
                float d = hit.distance;
                float intrinsic = 1f; // default if no script

                var emitter = hit.collider.GetComponent<LightEmitter2D>();
                if (emitter) intrinsic = emitter.brightness;

                // linear fall-off, clamp 0..1
                float intensity = intrinsic * (1f - Mathf.Clamp01(d / sampleRange));
                best = Mathf.Max(best, intensity);
            }

            return best;
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
