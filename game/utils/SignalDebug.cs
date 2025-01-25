using Godot;
using System;

public partial class SignalDebug : Node
{
    public void PrintMessage(Vector2 direction, float strength, string message)
    {
        GD.Print($"{message}: {direction}, {strength}");
    }
}
