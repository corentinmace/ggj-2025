using Godot;

public partial class ItemSpawnPoint : SpawnPoint
{
    public override void Spawn()
    {
        if (_entityInstance != null)
            return;

        var entity = SpawedEntity.Instantiate<Node2D>();
        GetTree().CurrentScene.AddChild(entity);
        entity.Position = GlobalPosition;

        entity.TreeExited += OnEntityDestroyed;

        _entityInstance = entity;
    }

    private void OnEntityDestroyed()
    {
        _entityInstance = null;
    }

    private Node2D _entityInstance = null;
}
