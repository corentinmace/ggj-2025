using Godot;
using System;

public partial class GameManager : Node
{
    [Signal] public delegate void GameResettingEventHandler();
    [Signal] public delegate void BubbleRegisteredEventHandler(Bubble bubble);

    public class MultipleBubbleError : Exception
    {
        public MultipleBubbleError() : base("Multiple bubble registered") { }
    }

    public static GameManager Instance { get; private set; }

    public void RegisterBubble(Bubble value)
    {
        if (value == null)
            throw new NullReferenceException();
        if (_bubble != null)
            throw new MultipleBubbleError();

        _bubble = value;
        _bubble.Connect(Bubble.SignalName.Killed, Callable.From(OnBubbleKilled), (uint)(ConnectFlags.Deferred));

        EmitSignal(SignalName.BubbleRegistered, _bubble);
    }

    public override void _Ready()
    {
        GameManager.Instance = this;

        GetTree().CurrentScene.Connect(Node.SignalName.Ready, Callable.From(ResetGame), (uint)(ConnectFlags.Deferred | ConnectFlags.OneShot));
    }

    private void OnBubbleKilled()
    {
        _bubble.QueueFree();
        _bubble = null;

        ResetGame();
    }

    private void ResetGame()
    {
        EmitSignal(SignalName.GameResetting);
    }

    private Bubble _bubble = null;
}
