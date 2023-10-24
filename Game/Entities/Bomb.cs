using Godot;

public partial class Bomb : RigidBody2D, IDamageable
{
	private int BaseDamage = 50;
	private void _OnExplosionTimerTimeout()
	{
		var area = GetNode<Area2D>("ExplosionRadius");
		foreach (var body in area.GetOverlappingBodies())
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
		foreach (var proj in area.GetOverlappingAreas())
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
