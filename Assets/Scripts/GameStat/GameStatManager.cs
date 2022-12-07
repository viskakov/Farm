using System;
using UnityEngine;

namespace GameStat
{
    public class GameStatManager : MonoBehaviour
    {
        public static Action<int> OnCarrotChange;
        public static Action<int> OnExperienceChange;

        private static int _carrot;
        private static int _experience;

        public static void IncreasedCarrot()
        {
            _carrot++;
            OnCarrotChange?.Invoke(_carrot);
        }

        public static void AddExperience(int value)
        {
            _experience += value;
            OnExperienceChange?.Invoke(_experience);
        }
    }
}