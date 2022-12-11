using Food;
using UnityEngine;

namespace Farm.Food
{
    [CreateAssetMenu(menuName = "Farm/NewFood")]
    public class FoodData : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public float GrowTime;
        public FoodBase FoodPrefab;
        public int Experience;

        public int GetExperience()
        {
            return (int) GrowTime * Experience;
        }
    }
}