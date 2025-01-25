using Godot;
using System;

public partial class Bubble : CharacterBody2D
{
	[Export] private float Speed { get; set; } = 300.0f;
	
	[Signal]
	public delegate void VelocityChangedEventHandler(Vector2 velocity);
	
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
	
}
