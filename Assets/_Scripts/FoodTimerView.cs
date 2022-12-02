using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Farm._Scripts
{
    public class FoodTimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerLabel;
        [SerializeField] private Image _progressCircle;

        private float _duration;
        private float _timer;

        private void Update()
        {
            _timer -= Time.deltaTime;
            _progressCircle.fillAmount = _timer / _duration;
            _timerLabel.SetText(_timer.ToString("0.0"));
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetDuration(float duration)
        {
            _duration = duration;
            _timer = duration;
        }
    }
}