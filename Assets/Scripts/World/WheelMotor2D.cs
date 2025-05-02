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

        public void Init(Rigidbody2D vehicleRb)
        {
            _vehicleRb = vehicleRb;
            CoreMotor = new Motor();
        }

        private void FixedUpdate()
        {
            if (_vehicleRb == null) return; // not initialised yet

            var worldDir = transform.TransformDirection(localForceDir).normalized;
            var force = worldDir * (CoreMotor.Power * maxForce);
            _vehicleRb.AddForceAtPosition(force, transform.position, ForceMode2D.Force);
        }
    }
}
