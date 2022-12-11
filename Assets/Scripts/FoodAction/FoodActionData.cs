using Food;
using UnityEngine;

namespace FoodAction
{
    [CreateAssetMenu(fileName = "Farm/NewAction")]
    public class ActionData : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public FoodKind FoodKind;
    }
}