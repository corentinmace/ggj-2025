using Godot;

public partial class HairDryer : StaticBody2D
{
    public RayCast2D Ray { get; private set; }
    public Area2D EffectArea { get; private set; }

    public override void _Ready()
    {
        InitChildrenRefs();
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
}
