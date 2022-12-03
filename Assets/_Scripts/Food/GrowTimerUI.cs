using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Farm._Scripts
{
    public class GrowTimerUI : MonoBehaviour
    {
        [SerializeField] private Transform _background;
        [SerializeField] private Image _progressBar;
        [SerializeField] private TextMeshProUGUI _timerLabel;
        [SerializeField] private Gradient _progressGradient;

        private float _duration;
        private float _timer;

        private void Update()
        {
            if (_timer > Mathf.Epsilon)
            {
                _timer -= Time.deltaTime;
                _progressBar.fillAmount = _timer / _duration;
                _progressBar.color = _progressGradient.Evaluate(_timer / _duration);
                _timerLabel.SetText(_timer.ToString("0.0"));
            }
        }

        public void Show()
        {
            _background.localScale = Vector3.zero;
            _background
                .DOScale(Vector3.one, 0.2f)
                .SetEase(Ease.OutCubic);
        }

        public void Hide()
        {
            _background
                .DOScale(Vector3.zero, 0.2f)
                .SetEase(Ease.OutCubic);
        }

        public void SetDuration(float duration)
        {
            _duration = duration;
            _timer = duration;
        }
    }
}