using Godot;
using System.Collections.Generic;

[Tool]
public abstract partial class SpawnPoint : Marker2D
{
    [Signal] public delegate void SpawnedEntityChangedEventHandler(PackedScene spawnedEntity);

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
            EmitSignal(SignalName.SpawnedEntityChanged, _spawnedEntity);
#if(DEBUG)
            if (Engine.IsEditorHint())
            {
                UpdateConfigurationWarnings();
                UpdateGizmo();
            }
#endif
        }
    }

    public override void _Ready()
    {
#if(DEBUG)
        if (Engine.IsEditorHint())
        {
            UpdateConfigurationWarnings();
            return;
        }
#endif

        GameManager.Instance.GameResetting += Spawn;
    }

    public abstract void Spawn();

    public override string[] _GetConfigurationWarnings()
    {
        var warnings = new List<string>();

        if (SpawedEntity == null)
            warnings.Add("Spawned entity should be set");

        return warnings.ToArray();
    }

    private void UpdateGizmo()
    {
        if (_gizmo != null)
        {
            _gizmo.QueueFree();
            _gizmo = null;
        }

        if (_spawnedEntity == null)
            return;

        var gizmo = _spawnedEntity.Instantiate<Node2D>();
        AddChild(gizmo);
        _gizmo = gizmo;
    }

    private PackedScene _spawnedEntity = null;

#if(DEBUG)
    private Node2D _gizmo = null;
#endif
}
