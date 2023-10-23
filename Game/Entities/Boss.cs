using Godot;
using System;

public partial class Boss : RigidBody2D, IDamageable
{
	[Export]
	private Player player;
	[Export]
	private World world;

	private Timer FireTimer;
	private Timer AltFireTimer;
	public override void _Ready()
	{
		FireTimer = GetNode<Timer>("FireTimer");
		FireTimer.WaitTime = 0.3f;
		FireTimer.Start();

		AltFireTimer = GetNode<Timer>("AltFireTimer");
		AltFireTimer.WaitTime = 3f;
		AltFireTimer.Start();
	}

	public float Speed = 10f;
	public override void _PhysicsProcess(double delta)
	{
		ConstantForce = Position.DirectionTo(player.Position) * Speed;
	}

	private int ProjectileCount = 8;
	private int fireCount = 0;
	public void _OnFireTimerTimeout()
	{
		for (int i = 0; i < ProjectileCount; i++)
		{
			float angleOffset = (float)(2 * Math.PI / ProjectileCount * i) + fireCount * 10000;
			float angle = Position.AngleToPoint(player.Position) + angleOffset;
			world.SpawnProjectile(Position, angle, DamageableKind.Friendly, Projectile.Type.Normal);
		}
		fireCount++;
	}

	public void _OnAltFireTimerTimeout()
	{
		for (int i = 0; i < ProjectileCount; i++)
		{
			float angleOffset = (float)(2 * Math.PI / ProjectileCount * i);
			float angle = Position.AngleToPoint(player.Position) + angleOffset;
			world.SpawnProjectile(Position, angle, DamageableKind.Friendly, Projectile.Type.Alt);
		}
	}

	public int Health = 200;
	public DamageableKind DamageableKind { get; } = DamageableKind.Enemy;
	public void TakeDamage(int damage, Vector2 direction)
	{
		Health -= damage;

		ApplyImpulse(direction * 40);
		if (Health <= 0)
		{
			QueueFree();
		}
	}
}