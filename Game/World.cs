using Godot;

public partial class World : Node2D
{
	private PackedScene projectileScene;

	public override void _Ready()
	{
		projectileScene = GD.Load<PackedScene>("res://Game/Entities/Projectile.tscn");
	}

	public void SpawnProjectile(Vector2 pos, float angle, float speed, DamageableKind target, Projectile.Type type)
	{
		var projInstance = (Projectile)projectileScene.Instantiate();

		projInstance.Position = pos;
		projInstance.Angle = angle;
		projInstance.Speed = speed;
		projInstance.Target = target;
		projInstance.SetType(type);

		GetParent().AddChild(projInstance);
	}
}
