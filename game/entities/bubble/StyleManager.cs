using Godot;

public partial class StyleManager : Node
{
    [Export]
    public ShaderMaterial JumpCooldownShader { get; set; }

    public override void _Ready()
    {
        foreach (Node node in GetParent().GetChildren())
        {
            if (node is AnimatedSprite2D sprite2d)
                sprite = sprite2d;
        }
    }

    public void OnCanImpulseChanged(bool canImpulse)
    {
        if (!canImpulse)
            sprite.Material = JumpCooldownShader;
        else if (sprite.Material?.GetInstanceId() == JumpCooldownShader.GetInstanceId())
            sprite.Material = null;
    }

    private AnimatedSprite2D sprite;
}
