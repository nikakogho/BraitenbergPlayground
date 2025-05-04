using Core;
using UnityEngine;

namespace World
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider2D))]
    public sealed class WheelMotor2D : MonoBehaviour
    {
        [Tooltip("Maximum thrust when CoreMotor.Power == 1")]
        public float maxForce = 3f;

        [Tooltip("Local direction of push (usually +Y)")]
        public Vector2 localForceDir = Vector2.up;

        [Tooltip("Tonic drive")]
        public float baseline = 0.2f;

        internal Motor CoreMotor { get; private set; }
        private Rigidbody2D _vehicleRb;
        private bool _isInit;

        public void Init(Rigidbody2D vehicleRb)
        {
            if (_isInit) return;
            _isInit = true;
            _vehicleRb = vehicleRb;
            CoreMotor = new Motor(baseline);
        }

        private void FixedUpdate()
        {
            if (!_isInit) return;

            // Where this wheel sits in world space
            Vector2 contactPoint = transform.position;

            // Thrust vector in world space
            Vector2 worldDir = (Vector2)(transform.rotation * localForceDir).normalized;
            Vector2 force = worldDir * (CoreMotor.Power * maxForce);

            // Add force at the wheel’s spot — produces translation + torque
            _vehicleRb.AddForceAtPosition(force, contactPoint, ForceMode2D.Force);
        }
    }
}
