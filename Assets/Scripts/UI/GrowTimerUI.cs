using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Farm.UI
{
    public class GrowTimerUI : MonoBehaviour
    {
        [SerializeField] private Transform _background;
        [SerializeField] private Image _progressBar;
        [SerializeField] private TextMeshProUGUI _timerLabel;
        [SerializeField] private Gradient _progressGradient;

        private Timer _timer;
        private float _duration;
        private float _scaleDuration = 0.2f;

        public void Init(float duration)
        {
            _timer = GetComponent<Timer>();
            _duration = duration;
            _timer.StartTimer(duration, OnTimerProgress, OnTimerComplete);
            Show();
        }

        private void OnTimerProgress(float currentTime)
        {
            UpdateProgressBar(currentTime);
            UpdateTimer(currentTime);
        }

        private void OnTimerComplete()
        {
            Hide();
        }

        private void UpdateProgressBar(float currentTime)
        {
            var normalizedTimer = currentTime / _duration;
            _progressBar.fillAmount = normalizedTimer;
            _progressBar.color = _progressGradient.Evaluate(normalizedTimer);
        }

        private void UpdateTimer(float currentTime)
        {
            var correctedTimer = currentTime + 1f;
            var timeSpan = TimeSpan.FromSeconds(correctedTimer);
            _timerLabel.SetText(timeSpan.ToString("m':'ss"));
        }

        private void Show()
        {
            _background.localScale = Vector3.zero;
            _background
                .DOScale(Vector3.one, _scaleDuration)
                .SetEase(Ease.OutCubic);
        }

        private void Hide()
        {
            _background
                .DOScale(Vector3.zero, _scaleDuration)
                .SetEase(Ease.OutCubic);
        }
    }
}