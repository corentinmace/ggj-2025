using Godot;
using System;

public partial class StyleManager : Node
{
    private struct ShaderMasks
    {
        public const byte Shield = (1 << 0);
        public const byte Invincible = (1 << 1);
    }

    [Export]
    public ShaderMaterial ShieldedShader { get; set; }
    [Export]
    public ShaderMaterial InvincibleShader { get; set; }

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

    private void UpdateShaders()
    {
        if (InvincibleShaderEnabled)
            sprite.Material = InvincibleShader;
        else if (ShieldShaderEnabled)
            sprite.Material = ShieldedShader;
        else
            sprite.Material = null;
    }

    private AnimatedSprite2D sprite;
    private byte shaderMask = 0;
}
