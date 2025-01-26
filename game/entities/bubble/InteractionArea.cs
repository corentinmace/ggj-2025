using Godot;

public partial class InteractionArea : Area2D
{
    [Signal]
    public delegate void ItemCollectedEventHandler(Item item);

    public override void _Ready()
    {
        BodyEntered += OnObjectEntered;
        AreaEntered += OnObjectEntered;
    }

    public void OnObjectEntered(Node2D body)
    {
        if (body is Collectible collectible)
        {
            Item item = collectible.Item;
            EmitSignal(SignalName.ItemCollected, item);
            collectible.Collect();
        }
    }
}
