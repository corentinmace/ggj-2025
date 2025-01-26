using Godot;

public partial class LifeControl : Node
{
    [Signal]
    public delegate void KilledEventHandler();

    public void OnDamaged()
    {
        EmitSignal(SignalName.Killed);
    }
}
