using Food;
using UnityEngine;

public class Grass : FoodBase
{
    private void Start()
    {
        FoodKind = FoodKind.Grass;
    }

    public override void Interact()
    {
        Debug.Log("Grass cut down");
        Destroy(gameObject);
    }
}