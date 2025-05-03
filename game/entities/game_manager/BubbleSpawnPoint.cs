using Godot;

[Tool]
public partial class BubbleSpawnPoint : SpawnPoint
{
    public override void Spawn()
    {
        var entity = SpawedEntity.Instantiate<Node2D>();
        GetTree().CurrentScene.AddChild(entity);
        entity.Position = GlobalPosition;
    }
}
