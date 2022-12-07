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
            GameStatManager.OnCarrotChange += OnCarrotChange;
            GameStatManager.OnExperienceChange += OnExperienceChange;
        }

        private void OnDisable()
        {
            GameStatManager.OnCarrotChange -= OnCarrotChange;
            GameStatManager.OnExperienceChange -= OnExperienceChange;
        }

        private void OnCarrotChange(int value)
        {
            _carrotLabel.SetText($"Carrot: {value}");
        }

        private void OnExperienceChange(int value)
        {
            _expLabel.SetText($"Exp: {value}");
        }
    }
}