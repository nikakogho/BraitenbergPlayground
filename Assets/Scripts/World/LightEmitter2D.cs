using UnityEngine;

namespace World
{
    public class LightEmitter2D : MonoBehaviour
    {
        [Tooltip("Intrinsic brightness 0-1")]
        [Range(0f, 1f)] public float brightness = 1f;
    }
}
