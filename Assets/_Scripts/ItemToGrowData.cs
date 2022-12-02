using UnityEngine;

namespace Farm._Scripts.Items
{
    [CreateAssetMenu(menuName = "Farm/NewItemToGrow")]
    public class ItemToGrowData : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public float GrowDuration;
    }
}