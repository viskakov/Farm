using Food;

public class Tree : FoodBase
{
    private void Start()
    {
        FoodKind = FoodKind.Tree;
    }

    public override bool Interact()
    {
        if (!IsRipe)
        {
            return false;
        }

        // Do nothing

        return true;
    }
}