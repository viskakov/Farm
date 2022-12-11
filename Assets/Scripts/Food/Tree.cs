using Food;

public class Tree : FoodBase
{
    private void Start()
    {
        FoodKind = FoodKind.Tree;
    }

    public override void Interact()
    {
        // Do nothing
    }
}