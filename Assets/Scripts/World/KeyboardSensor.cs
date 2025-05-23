using Core;
using UnityEngine;

namespace World
{
    [DisallowMultipleComponent]
    public class KeyboardSensor : WorldSensor
    {
        public KeyCode key = KeyCode.B;

        void Awake() => CoreSensor = new Sensor(() => Input.GetKey(key) ? 1f : 0f);
    }
}
