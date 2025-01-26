using Godot;

public partial class VelocityDebug : Control
{
    private Label value;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        foreach (var child in GetChildren())
        {
            if (child is Label label && label.Name == "VelocityValue")
            {
                value = label;
            }
        }
    }

    public void OnVelocityChanged(Vector2 velocity)
    {
        value.Text = $"({Mathf.Snapped(velocity.X, 0.01)}, {Mathf.Snapped(velocity.Y, 0.01)})";
    }
}
