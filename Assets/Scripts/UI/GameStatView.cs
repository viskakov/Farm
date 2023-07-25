using System;
using System.Collections;
using GameData;
using TMPro;
using UnityEngine;

namespace Farm.UI
{
    public class GameStatView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _carrotLabel;
        [SerializeField] private TextMeshProUGUI _expLabel;

        private float _minDuration = 0.33f;
        private float _maxDuration = 0.66f;

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

        private IEnumerator GradualChangeValue(float startValue, float endValue, Action<float> callback)
        {
            var diff = Mathf.Abs(endValue - startValue);
            var elapsedTime = 0f;
            var dynamicDuration = Mathf.Lerp(_minDuration, _maxDuration, 1f / Mathf.Max(diff, 1f));

            while (elapsedTime < dynamicDuration)
            {
                var t = elapsedTime / dynamicDuration;
                elapsedTime += Time.deltaTime;
                var result = Mathf.SmoothStep(startValue, endValue, t);
                callback?.Invoke(result);

                yield return null;
            }

            callback?.Invoke(endValue);
        }
    }
}