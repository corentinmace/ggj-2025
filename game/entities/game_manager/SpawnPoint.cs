using Godot;
using System.Collections.Generic;

public abstract partial class SpawnPoint : Marker2D
{
    [Export]
    public PackedScene SpawedEntity
    {
        get
        {
            return _spawnedEntity;
        }
        set
        {
            _spawnedEntity = value;
            UpdateConfigurationWarnings();
        }
    }

    public override void _Ready()
    {
        GameManager.Instance.GameResetting += Spawn;

        UpdateConfigurationWarnings();
    }

    public abstract void Spawn();

    public override string[] _GetConfigurationWarnings()
    {
        var warnings = new List<string>();

        if (SpawedEntity == null)
            warnings.Add("Spawned entity should be set");

        return warnings.ToArray();
    }

    private PackedScene _spawnedEntity = null;
}
