using Farm._Scripts.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Farm._Scripts
{
    public class ButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _duration;
        [SerializeField] private Image _icon;

        public void Init(FoodData foodData)
        {
            _name.SetText(foodData.Name);
            _duration.SetText(foodData.GrowDuration.ToString("0:00"));
            _icon.sprite = foodData.Icon;
        }

        public void ClickTest()
        {
            Debug.Log("ClickTest");
        }
    }
}