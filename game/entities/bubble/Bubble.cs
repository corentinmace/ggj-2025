using Godot;
using System;
using GGJ2025.entities;

public partial class Bubble : CharacterBody2D, IHittable
{
    [Export] private float Speed { get; set; } = 300.0f;
    private AnimatedSprite2D Sprite;
    private AnimationPlayer Animator;

    [Signal]
    public delegate void VelocityChangedEventHandler(Vector2 velocity);

    public override void _Ready()
    {
        foreach (var child in GetChildren())
        {
            if (child is AnimatedSprite2D animatedSprite)
            {
                Sprite = animatedSprite;
            }
            else if (child is AnimationPlayer animationPlayer)
            {
                Animator = animationPlayer;
            }
        }
    }

    public override void _Process(double delta)
    {
        if (Velocity.Length() > Speed)
        {
            Velocity = Velocity.Normalized() * Speed;
        }

        KinematicCollision2D moveAndCollide = MoveAndCollide(Velocity * (float)delta);
        if (moveAndCollide == null)
            return;
        Velocity = Velocity.Bounce(moveAndCollide.GetNormal());
    }

    public void OnPlayerImpulse(Vector2 direction, float strength)
    {
        Vector2 impulse = direction * strength;

        Velocity += impulse * Speed;
        EmitSignal(SignalName.VelocityChanged, Velocity);
    }

    public void Hit()
    {
        Velocity = Vector2.Zero;
        Animator.Play("pop");
    }

    public void OnPopAnimationFinished()
    {
        QueueFree();
    }
}