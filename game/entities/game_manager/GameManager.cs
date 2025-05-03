using Godot;
using System;

public partial class GameManager : Node
{
    public class MultipleSpawnPointError : Exception
    {
        public MultipleSpawnPointError() : base("Multiple spawn point registered") { }
    }

    public class NoSpawnPointError : NullReferenceException
    {
        public NoSpawnPointError() : base("No spawn point registered") { }
    }

    public class NoBubbleError : NullReferenceException
    {
        public NoBubbleError() : base("No bubble registered") { }
    }

    public class MultipleBubbleError : Exception
    {
        public MultipleBubbleError() : base("Multiple bubble registered") { }
    }

    public static GameManager Instance { get; private set; }

    public void RegisterSpawnPoint(Marker2D value)
    {
        if (value == null)
            throw new NullReferenceException();
        if (spawnPoint != null)
            throw new MultipleSpawnPointError();

        spawnPoint = value;
    }

    public void RegisterBubble(Bubble value)
    {
        if (value == null)
            throw new NullReferenceException();
        if (bubble != null)
            throw new MultipleBubbleError();

        bubble = value;
        bubble.Connect(Bubble.SignalName.Killed, Callable.From(OnBubbleKilled), (uint)(ConnectFlags.Deferred));
    }

    public override void _Ready()
    {
        GameManager.Instance = this;
    }

    public override void _Process(double delta)
    {
        time += delta;
    }

    private void OnBubbleKilled()
    {
        if (spawnPoint == null)
            throw new NoSpawnPointError();
        if (bubble == null)
            throw new NoBubbleError();

        GD.Print("Respawning bubble");

        bubble.Spawn(spawnPoint.GlobalPosition);
        time = 0.0;
    }

    private double time;
    private Marker2D spawnPoint = null;
    private Bubble bubble = null;
}
