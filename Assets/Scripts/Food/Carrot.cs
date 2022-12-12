using Food;
using GameStat;

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

        GameStatManager.AddCarrot();
        Destroy(gameObject);
        return true;
    }
}