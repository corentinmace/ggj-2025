using Godot;
using System;

public partial class Tap : StaticBody2D
{

	[Export(PropertyHint.Range, "0.5, 100, 0.5")] public double SpawnInterval = 2.0;
	[Export(PropertyHint.Range, "0, 100, 0.5")] public double SpawnDelay = 0.5;
	[Export(PropertyHint.File, ".tscn")] public PackedScene DropletScene;

	private static Marker2D SpawnPosition;
	private static Timer SpawnTimer;
	private static Timer DelayTimer;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (var child in GetChildren())
		{
			if (child is Marker2D marker)
			{
				SpawnPosition = marker;
			} else if (child is Timer timer && timer.Name == "SpawnTimer")
			{
				SpawnTimer = timer;
			} else if (child is Timer delayTimer && delayTimer.Name == "SpawnDelay")
			{
				DelayTimer = delayTimer;
			}
		}
		
		SpawnTimer.WaitTime = SpawnInterval;
		if (SpawnDelay == 0)
		{
			SpawnTimer.Start();
			SpawnDroplet();
		}
		else
		{
			DelayTimer.WaitTime = SpawnDelay;
			DelayTimer.Start();
		}
	}

	public void SpawnDroplet()
	{
		Node2D newDroplet = DropletScene.Instantiate<Node2D>();
		newDroplet.Position = SpawnPosition.Position;
		AddChild(newDroplet);
		
	}
	
}
