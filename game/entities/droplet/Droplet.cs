using Godot;
using System;
using GGJ2025.entities;

public partial class Droplet : CharacterBody2D
{
    private AnimatedSprite2D Sprite;
    private AnimationPlayer Animator;

    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
    private bool Enabled = false;

    [Signal]
    public delegate void BodyEnteredEventHandler(Node2D body);

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
        if (!Enabled)
            return;

        Velocity += Vector2.Down * gravity * (float)delta;
        var collision = MoveAndCollide(Velocity * (float)delta);

        if (collision != null)
        {
            EmitSignal(SignalName.BodyEntered, collision.GetCollider());
        }
    }

    public void OnCollide(Node2D body)
    {
        if (body.GetInstanceId() == GetInstanceId())
            return;
        
        Enabled = false;
        Animator.Play("splat");
        if (body is IHittable hittable)
        {
            hittable.Hit();
        }
    }

    public void OnDetachAnimationFinished()
    {
        Animator.Play("fall");
        Enabled = true;
    }

    public void OnSplatAnimationFinished()
    {
        QueueFree();
    }
}