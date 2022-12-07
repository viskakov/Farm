using System;
using UnityEngine;

namespace GameStat
{
    public class GameStatManager : MonoBehaviour
    {
        public static Action<int, int> OnCarrotChange;
        public static Action<int, int> OnExperienceChange;

        private static int _carrot;
        private static int _experience;

        public static void AddCarrot()
        {
            var prevValue = _carrot;
            _carrot++;
            OnCarrotChange?.Invoke(prevValue, _carrot);
        }

        public static void AddExperience(int value)
        {
            var prevValue = _experience;
            _experience += value;
            OnExperienceChange?.Invoke(prevValue, _experience);
        }
    }
}