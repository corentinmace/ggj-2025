using Godot;

public partial class Collectible : Area2D
{
    [Export]
    public Item Item { get; private set; }

    [Signal]
    public delegate void CollectedEventHandler();

    public void Collect()
    {
        EmitSignal(SignalName.Collected);
        QueueFree();
    }
}
