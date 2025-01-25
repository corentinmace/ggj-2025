using Godot;
using GGJ2025.entities;

public partial class Droplet : RigidBody2D
{
	
	private AnimatedSprite2D Sprite;
	private AnimationPlayer Animator;
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
	public void OnCollide(Node2D body)
	{
		CallDeferred("Disable");
		Animator.Play("splat");
		if (body is IHittable hittable)
		{
			hittable.Hit();
		}
	}

	public void OnDetachAnimationFinished()
	{
		Enable();
		Animator.Play("fall");
	}

	public void OnSplatAnimationFinished()
	{
		QueueFree();
	}

	public void Enable()
	{
		Sleeping = false;
		Freeze = false;
	}

	public void Disable()
	{
		Sleeping = true;
		Freeze = true;
	}
	
	
}
