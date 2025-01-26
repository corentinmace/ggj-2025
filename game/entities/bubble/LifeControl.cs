using Godot;

public partial class LifeControl : Node
{
    [Signal]
    public delegate void KilledEventHandler();
    [Signal]
    public delegate void IsInvincibleChangedEventHandler(bool isInvincible);
    [Signal]
    public delegate void ShieldChangedEventHandler(uint shield);

    public bool IsInvincible
    {
        get
        {
            return isInvincible;
        }

        set
        {
            isInvincible = value;
            EmitSignal(SignalName.IsInvincibleChanged, isInvincible);
        }
    }
    public uint Shield
    {
        get
        {
            return shield;
        }

        set
        {
            shield = value;
            EmitSignal(SignalName.ShieldChanged, shield);
        }
    }

    public override void _Ready()
    {
        foreach (Node node in GetChildren())
        {
            if (node is Timer timer)
                damageCooldown = timer;
        }
    }


    public void OnItemCollected(Item item)
    {
        if (item.Type != "Shield")
            return;

        Shield += 1;
    }

    public void OnDamaged()
    {
        if (IsInvincible)
            return;

        IsInvincible = true;
        damageCooldown.Start();

        if (Shield != 0)
        {
            Shield -= 1;
            return;
        }

        EmitSignal(SignalName.Killed);
    }

    public void OnHitCooldownTimeout()
    {
        IsInvincible = false;
    }

    private bool isInvincible = false;
    private uint shield = 0;
    private Timer damageCooldown;
}
