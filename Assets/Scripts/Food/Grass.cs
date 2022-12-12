using Food;

public class Grass : FoodBase
{
    private void Start()
    {
        FoodKind = FoodKind.Grass;
    }

    public override bool Interact()
    {
        if (!IsRipe)
        {
            return false;
        }

        Destroy(gameObject);
        return true;
    }
}