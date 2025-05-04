using UnityEngine;

namespace Simulation
{
    [RequireComponent(typeof(Collider2D))]
    public class DragLamp : MonoBehaviour
    {
        private bool _dragging;
        private Camera _cam;

        void Awake() => _cam = Camera.main;

        void OnMouseDown() => _dragging = true;
        void OnMouseUp() => _dragging = false;

        void Update()
        {
            if (!_dragging) return;

            Vector3 mouse = _cam.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = transform.position.z;
            transform.position = mouse;
        }
    }
}
