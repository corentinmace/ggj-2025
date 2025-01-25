using Godot;
using System;

public partial class PlayerControl : Node
{
    [Export] public float ThreshTime { get; set; } = 0.5f;

    private float _impulseDuration = 0.0f;

    public struct Inputs
    {
        public const string Left = "left_dir";
        public const string Right = "right_dir";
        public const string Up = "up_dir";
        public const string Down = "down_dir";
        public const string Impulse = "impulse";
    }

    [Signal]
    public delegate void PlayerImpulseEventHandler(Vector2 direction, float strength);

    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = Input.GetVector(Inputs.Left, Inputs.Right, Inputs.Up, Inputs.Down);

        if (Input.IsActionPressed(Inputs.Impulse))
        {
            _impulseDuration += (float)delta;
        }
        else if (!direction.IsZeroApprox() && Input.IsActionJustReleased(Inputs.Impulse))
        {
            float strength = _impulseDuration > ThreshTime ? 1.0f : _impulseDuration / ThreshTime;
            _impulseDuration = 0.0f;
            
            EmitSignal(SignalName.PlayerImpulse, direction, strength);
        }
    }
}