using Godot;

public partial class LifeControl : Node
{
    [Signal]
    public delegate void KilledEventHandler();
    [Signal]
    public delegate void IsInvincibleChangedEventHandler(bool isInvincible);

    public override void _Ready()
    {
        foreach (Node node in GetChildren())
        {
            if (node is Timer timer)
                damageCooldown = timer;
        }
    }

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

    public void OnItemCollected(Item item)
    {
        if (item.Type != "Shield")
            return;

        shield += 1;
    }

    public void OnDamaged()
    {
        if (IsInvincible)
            return;

        IsInvincible = true;
        damageCooldown.Start();

        if (shield != 0)
        {
            shield -= 1;
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
