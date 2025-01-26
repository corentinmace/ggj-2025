using Godot;

public partial class StyleManager : Node
{
    private struct ShaderMasks
    {
        public const byte Shield = (1 << 0);
        public const byte Invincible = (1 << 1);
        public const byte JumpCooldown = (1 << 2);
    }

    [Export]
    public ShaderMaterial ShieldedShader { get; set; }
    [Export]
    public ShaderMaterial InvincibleShader { get; set; }
    [Export]
    public ShaderMaterial JumpCooldownShader { get; set; }

    public bool ShieldShaderEnabled
    {
        get
        {
            return (shaderMask & ShaderMasks.Shield) != 0;
        }
        set
        {
            if (value)
                shaderMask |= ShaderMasks.Shield;
            else
                shaderMask &= unchecked((byte)(~ShaderMasks.Shield));
        }
    }

    public bool InvincibleShaderEnabled
    {
        get
        {
            return (shaderMask & ShaderMasks.Invincible) != 0;
        }
        set
        {
            if (value)
                shaderMask |= ShaderMasks.Invincible;
            else
                shaderMask &= unchecked((byte)(~ShaderMasks.Invincible));
        }
    }

    public bool JumpCooldownShaderEnabled
    {
        get
        {
            return (shaderMask & ShaderMasks.JumpCooldown) != 0;
        }
        set
        {
            if (value)
                shaderMask |= ShaderMasks.JumpCooldown;
            else
                shaderMask &= unchecked((byte)(~ShaderMasks.JumpCooldown));
        }
    }

    public override void _Ready()
    {
        foreach (Node node in GetParent().GetChildren())
        {
            if (node is AnimatedSprite2D sprite2d)
                sprite = sprite2d;
        }
    }

    public void OnShieldChanged(uint shield)
    {
        ShieldShaderEnabled = shield > 0;
        UpdateShaders();
    }

    public void OnInvincibleChanged(bool isInvincible)
    {
        InvincibleShaderEnabled = isInvincible;
        UpdateShaders();
    }

    public void OnCanImpulseChanged(bool canImpulse)
    {
        JumpCooldownShaderEnabled = !canImpulse;
        UpdateShaders();
    }

    private void UpdateShaders()
    {
        if (JumpCooldownShaderEnabled)
            sprite.Material = JumpCooldownShader;
        else if (InvincibleShaderEnabled)
            sprite.Material = InvincibleShader;
        else if (ShieldShaderEnabled)
            sprite.Material = ShieldedShader;
        else
            sprite.Material = null;
    }


    private AnimatedSprite2D sprite;
    private byte shaderMask = 0;
}
