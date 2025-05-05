using TMPro;
using UnityEngine;

namespace World.UI
{
    public class HUDDisplayFade : MonoBehaviour
    {
        public float holdSeconds = 3f;
        public float fadeSeconds = 1f;
        private TextMeshProUGUI _tmp; float _t;

        void Awake() => _tmp = GetComponent<TextMeshProUGUI>();

        void Update()
        {
            _t += Time.deltaTime;
            if (_t > holdSeconds)
            {
                float k = 1 - (_t - holdSeconds) / fadeSeconds;
                _tmp.alpha = Mathf.Clamp01(k);
                if (k <= 0) gameObject.SetActive(false);
            }
        }

        public void ResetHUD()
        {
            _t = 0;
            _tmp.alpha = 1;
            gameObject.SetActive(true);
        }
    }
}
