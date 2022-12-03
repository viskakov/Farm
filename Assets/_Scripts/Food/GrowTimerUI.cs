using System;
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

        public void SetDuration(float duration)
        {
            _duration = duration;
            _timer = duration;
        }

        private void Update()
        {
            if (_timer > Mathf.Epsilon)
            {
                _timer -= Time.deltaTime;

                UpdateProgressBar();
                UpdateTimer();
            }
        }

        private void UpdateProgressBar()
        {
            var normalizedTimer = _timer / _duration;
            _progressBar.fillAmount = normalizedTimer;
            _progressBar.color = _progressGradient.Evaluate(normalizedTimer);
        }

        private void UpdateTimer()
        {
            var correctedTimer = _timer + 1f;
            var timeSpan = TimeSpan.FromSeconds(correctedTimer);
            _timerLabel.SetText(timeSpan.ToString("m':'ss"));
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
    }
}