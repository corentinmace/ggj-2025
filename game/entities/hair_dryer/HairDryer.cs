using Godot;

public partial class HairDryer : StaticBody2D
{
    [Export]
    public float Strength { get; set; }

    public RayCast2D Ray { get; private set; }
    public Area2D EffectArea { get; private set; }

    public override void _Ready()
    {
        InitChildrenRefs();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (bubbleRef != null)
        {
            bubbleRef.Acceleration += Ray.GlobalTransform.Y * Strength;
        }
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body is Bubble bubble)
        {
            bubbleRef = bubble;
        }
    }

    public void OnBodyExited(Node2D body)
    {
        if (body.GetInstanceId() == bubbleRef.GetInstanceId())
        {
            bubbleRef = null;
        }
    }

    private void InitChildrenRefs()
    {
        foreach (var node in GetChildren())
        {
            if (node is RayCast2D ray)
            {
                Ray = ray;
            }
            else if (node is Area2D area)
            {
                EffectArea = area;
            }
        }
    }

    private Bubble bubbleRef;
}
