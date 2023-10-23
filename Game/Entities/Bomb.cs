using Godot;

public partial class Bomb : RigidBody2D, IDamageable
{
	public DamageableKind DamageableKind { get; } = DamageableKind.Enemy;
	private Timer timer;
	private int Damage = 50;
	private float Timeout = 5f;
	
	public override void _Ready()
	{
		timer = GetNode<Timer>("ExplosionTimer");
		timer.WaitTime = Timeout;
		timer.Start();
	}

	private void _OnExplosionTimerTimeout()
	{
		var area = GetNode<Area2D>("ExplosionRadius");
		foreach (var body in area.GetOverlappingBodies())
		{
			if (body is Enemy enemy)
			{
				enemy.TakeDamage(Damage - (int)(GlobalPosition.DistanceTo(enemy.GlobalPosition)/10), GlobalPosition);
			}
			if (body is Player player)
			{
				player.TakeDamage(Damage - (int)(GlobalPosition.DistanceTo(player.GlobalPosition)/10), GlobalPosition);
			}
		}
		foreach (var proj in area.GetOverlappingAreas())
		{
			proj.QueueFree();
		}
		QueueFree();
	}
	public void TakeDamage(int damage, Vector2 direction)
	{
		ApplyImpulse(direction * 40);
	}
}
