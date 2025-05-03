using UnityEngine;

namespace World
{
    public class SensorViewToggle : MonoBehaviour
    {
        [Tooltip("Key to toggle sensor rays")]
        public KeyCode toggleKey = KeyCode.L;

        private void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                LightSensor2D.showRays = !LightSensor2D.showRays;
                Debug.Log($"Sensor rays {(LightSensor2D.showRays ? "ON" : "OFF")}");
            }
        }
    }
}
