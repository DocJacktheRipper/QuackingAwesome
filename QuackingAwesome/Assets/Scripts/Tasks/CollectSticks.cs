using Inventory;

public class CollectSticks : Task
{
    private StickInventory _stickInventory;

    public CollectSticks(StickInventory stickInventory)
    {
        description = "Collect sticks";
        
        _stickInventory = stickInventory;
    }

    public override bool Update()
    {
        progression = _stickInventory.GetNumberOfSticks();
        return IsCompleted();
    }
}
