using UnityEngine;

namespace Simulation
{
    public class LightSourceResetter : MonoBehaviour
    {
        private Vector3[] originalPositions;

        private void Awake()
        {
            originalPositions = new Vector3[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                originalPositions[i] = child.position;
            }
        }

        public void ResetLight()
        {
            for (int i = 0; i < originalPositions.Length; i++)
            {
                var child = transform.GetChild(i);
                child.position = originalPositions[i];
            }
        }
    }
}
