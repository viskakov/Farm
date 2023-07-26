using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    public class Timer : MonoBehaviour
    {
        private Coroutine _timerCoroutine;

        public void StartTimer(float duration, Action<float> onTimerProgress, Action onTimerComplete = null)
        {
            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);

            _timerCoroutine = StartCoroutine(CountdownTimer(duration, onTimerProgress, onTimerComplete));
        }

        public void StopTimer()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
                _timerCoroutine = null;
            }
        }

        private IEnumerator CountdownTimer(float duration, Action<float> onTimerProgress, Action onTimerComplete)
        {
            var timer = duration;
            while (timer > Mathf.Epsilon)
            {
                timer -= Time.deltaTime;

                yield return null;

                onTimerProgress?.Invoke(timer);
            }

            onTimerComplete?.Invoke();
            _timerCoroutine = null;
        }
    }
}