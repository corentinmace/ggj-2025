using Godot;
using System;

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
        value.Text = velocity.ToString();
    }
}