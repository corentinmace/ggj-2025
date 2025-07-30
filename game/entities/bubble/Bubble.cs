using Godot;

public partial class Bubble : CharacterBody2D
{
	[Export] private float Speed { get; set; } = 300.0f;
	[Export] public float Friction { get; set; } = 100.0f;
	[Export] public float VelocityThresh { get; private set; } = 0.1f;

	[Signal]
	public delegate void VelocityChangedEventHandler(Vector2 velocity);
	[Signal]
	public delegate void SpawnedEventHandler();
	[Signal]
	public delegate void KilledEventHandler();

	public Vector2 Acceleration { get; set; }

	public new Vector2 Velocity
	{
		get { return base.Velocity; }

		set
		{
			base.Velocity = value;
			EmitSignal(SignalName.VelocityChanged, base.Velocity);
		}
	}

	public Bubble()
	{
		ChildEnteredTree += CollectChild;
	}

	public override void _Ready()
	{
		GameManager.Instance.RegisterBubble(this);
		Spawn(Position);
	}

	public override void _Process(double delta)
	{
		KinematicCollision2D moveAndCollide = MoveAndCollide(Velocity * (float)delta);
		if (moveAndCollide == null)
			return;
		Velocity = Velocity.Bounce(moveAndCollide.GetNormal());
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 newVelocity = Velocity + Acceleration * (float)delta;
		newVelocity = newVelocity.Length() < VelocityThresh ? Vector2.Zero : newVelocity.LimitLength(Speed);
		Velocity = newVelocity;

		Vector2 dir = Velocity.Normalized();
		Acceleration = -dir * Friction;
	}

	public void OnPlayerImpulse(Vector2 direction, float strength)
	{
		Vector2 impulse = direction * strength;

		Velocity += impulse * Speed;
	}

	public void Hit()
	{
		Velocity = Vector2.Zero;
		Animator.Play("pop");
	}

	public void Spawn(Vector2 newPos)
	{
		Translate(newPos - GlobalPosition);
		Animator.Play("spawn");
	}

	public void OnPopAnimationFinished()
	{
		EmitSignal(SignalName.Killed);
	}

	public void OnSpawnAnimationFinished()
	{
		EmitSignal(SignalName.Spawned);
	}

	private void CollectChild(Node child)
	{
		if (child is AnimatedSprite2D animatedSprite)
			Sprite = animatedSprite;
		else if (child is AnimationPlayer animationPlayer)
			Animator = animationPlayer;
	}

	private AnimatedSprite2D Sprite;
	private AnimationPlayer Animator;
}
