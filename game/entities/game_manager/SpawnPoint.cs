using Godot;

public partial class SpawnPoint : Marker2D
{
	public override void _Ready() {
		GameManager.Instance.RegisterSpawnPoint(this);
	}
}
