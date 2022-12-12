using System;
using System.Collections;
using GameStat;
using TMPro;
using UnityEngine;

namespace Farm.UI
{
    public class GameStatView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _carrotLabel;
        [SerializeField] private TextMeshProUGUI _expLabel;

        private void OnEnable()
        {
            GameDataManager.OnCarrotChange += OnCarrotChange;
            GameDataManager.OnExperienceChange += OnExperienceChange;
        }

        private void OnDisable()
        {
            GameDataManager.OnCarrotChange -= OnCarrotChange;
            GameDataManager.OnExperienceChange -= OnExperienceChange;
        }

        private void OnCarrotChange(int prevValue, int newValue)
        {
            StartCoroutine(GradualChangeValue(prevValue, newValue, UpdateCarrotLabel));
        }

        private void UpdateCarrotLabel(float value)
        {
            _carrotLabel.SetText($"Carrot: {value:0}");
        }

        private void OnExperienceChange(int prevValue, int newValue)
        {
            StartCoroutine(GradualChangeValue(prevValue, newValue, UpdateExperienceLabel));
        }

        private void UpdateExperienceLabel(float value)
        {
            _expLabel.SetText($"Exp: {value:0}");
        }

        private IEnumerator GradualChangeValue(float startValue, float endValue, Action<float> action)
        {
            var elapsedTime = 0f;
            var duration = 0.3f;
            while (elapsedTime < duration)
            {
                var t = elapsedTime / duration;
                var result = Mathf.SmoothStep(startValue, endValue, t);
                elapsedTime += Time.deltaTime;
                action?.Invoke(result);

                yield return null;

                action?.Invoke(endValue);
            }
        }
    }
}