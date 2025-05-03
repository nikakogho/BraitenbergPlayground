using Core;
using UnityEngine;

namespace World
{
    [DisallowMultipleComponent]
    public sealed class WheelMotor2D : MonoBehaviour
    {
        [Tooltip("Maximum linear force when CoreMotor power == 1")]
        public float maxForce = 3f;

        [Tooltip("Direction of push in wheel's local space (usually +Y or +X)")]
        public Vector2 localForceDir = Vector2.up;

        // Assigned by VehicleBody at runtime
        internal Motor CoreMotor { get; private set; }
        private Rigidbody2D _vehicleRb;

        private bool _isInit;

        public void Init(Rigidbody2D vehicleRb)
        {
            if (_isInit) return;

            _isInit = true;

            _vehicleRb = vehicleRb;
            CoreMotor = new Motor();
        }

        private void FixedUpdate()
        {
            if (!_isInit) return;

            var worldDir = transform.TransformDirection(localForceDir).normalized;
            var velocity = worldDir * (CoreMotor.Power * maxForce);

            _vehicleRb.velocity = velocity;
        }
    }
}
