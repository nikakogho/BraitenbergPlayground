using UnityEngine;
using UnityEngine.SceneManagement;

namespace Simulation
{
    public class ReturnerToMenu : MonoBehaviour
    {
        public KeyCode returnKey = KeyCode.Escape;
        public string menuName = "Main Menu";

        private void Update()
        {
            if (Input.GetKeyDown(returnKey)) SceneManager.LoadScene(menuName);
        }
    }
}
