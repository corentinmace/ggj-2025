using Godot;

[GlobalClass]
public partial class Item : Resource
{
    [Export]
    public string Type { get; private set; }
}
