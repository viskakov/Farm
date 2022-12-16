using Farm.Food;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FoodData))]
public class FoodDataEditor : Editor
{
    private FoodData _foodData;

    private void OnEnable()
    {
        _foodData = target as FoodData;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (_foodData.Icon == null)
        {
            return;
        }

        var texture = AssetPreview.GetAssetPreview(_foodData.Icon);
        GUILayout.Label(string.Empty, GUILayout.Height(94), GUILayout.Width(94));
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
    }
}