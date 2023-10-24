using Godot;

public partial class World : Node2D
{
	[Export]
	private PackedScene projectileScene;
	[Export]
	private PackedScene bombScene;

	public void SpawnProjectile(Vector2 pos, float angle, DamageableKind target, Projectile.Type type)
	{
		var projInstance = (Projectile)projectileScene.Instantiate();

		projInstance.Position = pos;
		projInstance.SetAngle(angle);
		projInstance.Target = target;
		projInstance.SetType(type);

		GetParent().AddChild(projInstance);
	}
	// no target because a bomb should hit everything.
	public void ThrowBomb(Vector2 pos, float angle)
	{
		var bombInstance = (Bomb)bombScene.Instantiate();
		bombInstance.Position = pos;
		bombInstance.SetAngle(angle);

		GetParent().AddChild(bombInstance);
		bombInstance.ApplyImpulse(bombInstance.vector * 40);
	}
}
