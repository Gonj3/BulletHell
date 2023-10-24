using Godot;

public partial class Bomb : RigidBody2D, IDamageable
{
	private int BaseDamage = 50;
	[Export]
	private Area2D ExplosionRadius;
	private void _OnExplosionTimerTimeout()
	{
		foreach (var body in ExplosionRadius.GetOverlappingBodies())
		{
			if (body is Enemy enemy)
			{
				var damage = BaseDamage - (GlobalPosition.DistanceTo(enemy.GlobalPosition) / 10);
				enemy.TakeDamage((int)damage, GlobalPosition);
			}
			else if (body is Player player)
			{
				var damage = BaseDamage - (GlobalPosition.DistanceTo(player.GlobalPosition) / 10);
				player.TakeDamage((int)damage, GlobalPosition);
			}
		}
		foreach (var proj in ExplosionRadius.GetOverlappingAreas())
		{
			if (proj is Projectile)
			{
				proj.QueueFree();
			}
		}
		QueueFree();
	}

	public DamageableKind DamageableKind { get; } = DamageableKind.Enemy;
	public void TakeDamage(int damage, Vector2 direction)
	{
		ApplyImpulse(direction * 40);
	}
}
