using Food;
using GameData;

public class Carrot : FoodBase
{
    private void Start()
    {
        FoodKind = FoodKind.Carrot;
    }

    public override bool Interact()
    {
        if (!IsRipe)
        {
            return false;
        }

        GameDataManager.AddCarrot();
        Destroy(gameObject);
        return true;
    }
}