using UnityEngine;

namespace Farm._Scripts.Items
{
    [CreateAssetMenu(menuName = "Farm/NewFood")]
    public class FoodData : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public float GrowDuration;
        public FoodLogic FoodPrefab;
        public int Experience;

        public int GetExperience()
        {
            return (int) GrowDuration * Experience;
        }
    }
}