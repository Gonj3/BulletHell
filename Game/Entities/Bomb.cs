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
		idle.Hide();
		explosion.Show();
		explosion.Play("explode");
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
		await ToSignal(explosion, "animation_finished");
		QueueFree();

		this.GetAudioManager().PlaySound("BombSFX");
	}

	public void TakeDamage(int damage, Vector2 direction)
	{
		ApplyImpulse(direction * 40);
	}
}
