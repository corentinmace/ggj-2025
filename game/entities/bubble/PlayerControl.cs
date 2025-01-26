using Godot;

public partial class PlayerControl : Node
{
    public struct Inputs
    {
        public const string Left = "left_dir";
        public const string Right = "right_dir";
        public const string Up = "up_dir";
        public const string Down = "down_dir";
        public const string Impulse = "impulse";
    }

    [Export] public float ThreshTime { get; set; } = 0.5f;

    [Signal]
    public delegate void PlayerImpulseEventHandler(Vector2 direction, float strength);
    
    [Signal]
    public delegate void CanImpulseChangedEventHandler(bool state);

    public Timer CooldownTimer { get; private set; }

    public bool CanImpulse
    {
        get
        {
            return canImpulse;
        }
        set
        {
            canImpulse = value;
            EmitSignal(SignalName.CanImpulseChanged, value);
        }
    }

    public override void _Ready()
    {
        foreach (Node child in GetChildren())
        {
            if (child is Timer timer)
                CooldownTimer = timer;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!canImpulse)
            return;

        if (Input.IsActionPressed(Inputs.Impulse))
        {
            impulseDuration += (float)delta;
            return;
        }

        Vector2 direction = Input.GetVector(Inputs.Left, Inputs.Right, Inputs.Up, Inputs.Down);
        if (!direction.IsZeroApprox() && Input.IsActionJustReleased(Inputs.Impulse))
        {
            float strength = impulseDuration > ThreshTime ? 1.0f : impulseDuration / ThreshTime;
            impulseDuration = 0.0f;
            CanImpulse = false;
            CooldownTimer.Start();
            
            EmitSignal(SignalName.PlayerImpulse, direction, strength);
        }
    }

    public void OnImpulseCouldownTimeout()
    {
        CanImpulse = true;
    }

    private float impulseDuration = 0.0f;
    private bool canImpulse = true;
}
