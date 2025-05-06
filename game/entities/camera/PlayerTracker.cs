using Godot;

using ParentType = Godot.Node2D;

public partial class PlayerTracker : Node
{
    public override void _Ready()
    {
        GameManager.Instance.BubbleRegistered += (bubble) => _player = bubble;
        _parent = GetParent<ParentType>();
    }

    public override void _Process(double delta)
    {
        if (_player == null)
            return;

        var diff = _player.GlobalPosition - _parent.GlobalPosition;
        _parent.Translate(diff);
    }

    private Node2D _player = null;
    private Node2D _parent = null;
}
