using Godot;

public partial class Bomb : RigidBody2D, IDamageable
{
	[Export]
	private AnimatedSprite2D idle;
	[Export]
	private AnimatedSprite2D explosion;
	[Export]
	private Area2D ExplosionRadius;

	private int BaseDamage = 50;

	public Vector2 vector { get; private set; }

	public DamageableKind DamageableKind { get; } = DamageableKind.Enemy;

	public void SetAngle(float angle)
	{
		vector = Vector2.FromAngle(angle).Normalized();
	}
	public override void _Ready()
	{
		explosion.Hide();
		idle.Play("idle");
	}
	private async void _OnExplosionTimerTimeout()
	{
		Freeze = true;
		Rotation = 0;

		idle.Hide();
		explosion.Show();
		explosion.Play("explode");
		foreach (var body in ExplosionRadius.GetOverlappingBodies())
		{
			if (body is Player player)
			{
				var damage = BaseDamage - (Position.DistanceTo(player.Position) / 10);
				player.TakeDamage((int)damage, 0, 0);
			}
			else if (body is IDamageable enemy)
			{
				var damage = BaseDamage - (Position.DistanceTo(enemy.Position) / 10);
				var angle = vector.AngleTo(enemy.Position);
				enemy.TakeDamage((int)damage, angle, 40);
			}
		}
		foreach (var proj in ExplosionRadius.GetOverlappingAreas())
		{
			if (proj is Projectile p && p.Target != DamageableKind)
			{
				proj.QueueFree();
			}
		}
		await ToSignal(explosion, "animation_finished");
		QueueFree();

		this.GetAudioManager().PlaySound("BombSFX");
	}

	public void TakeDamage(int damage, float angle, int force)
	{
		// take "knockback"
		Vector2 impulse = new Vector2(force, 0).Rotated(angle);
		ApplyImpulse(impulse);
	}
}
