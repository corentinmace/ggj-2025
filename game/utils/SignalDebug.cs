using Godot;
using System;

public partial class SignalDebug : Node
{
    public void PrintMessage()
    {
        GD.Print("Signal triggered");
    }
}
