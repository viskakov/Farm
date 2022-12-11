using Food;
using GameStat;
using UnityEngine;

public class Carrot : FoodBase
{
    private void Start()
    {
        FoodKind = FoodKind.Carrot;
    }

    public override void Interact()
    {
        Debug.Log("Carrot pickup");
        GameStatManager.AddCarrot();
        Destroy(gameObject);
    }
}