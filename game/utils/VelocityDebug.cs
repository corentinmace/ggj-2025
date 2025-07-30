using Godot;

public partial class VelocityDebug : Control
{
    public VelocityDebug()
    {
        ChildEnteredTree += RegisterChild;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GameManager.Instance.BubbleRegistered += OnBubbleRegistered;
    }

    public void OnVelocityChanged(Vector2 velocity)
    {
        _velocityLabel.Text = $"({Mathf.Snapped(velocity.X, 0.01)}, {Mathf.Snapped(velocity.Y, 0.01)})";
    }

    private void RegisterChild(Node child)
    {
        if (child is Label label && label.Name == "VelocityValue")
            _velocityLabel = label;
    }

    private void OnBubbleRegistered(Bubble bubble)
    {
        bubble.VelocityChanged += OnVelocityChanged;
    }

    private Label _velocityLabel;
}
