using Godot;
using System;

public partial class Boss : RigidBody2D, IDamageable
{
	[Export]
	private Player player;

	[Export]
	private World world;

	[Export]
	private AnimatedSprite2D body;

	[Signal]
	public delegate void DeathEventHandler();

	public DamageableKind DamageableKind { get; } = DamageableKind.Enemy;
	public int Health = 200;
	public float Speed = 10f;

	private int projectileCount = 8;
	private int fireCount = 0;

	public override void _Ready()
	{
		body.Play("idle");
	}
	public override void _PhysicsProcess(double delta)
	{
		ConstantForce = Position.DirectionTo(player.Position) * Speed;
	}

	public void _OnFireTimerTimeout()
	{
		for (int i = 0; i < projectileCount; i++)
		{
			float angleOffset = (float)(2 * Math.PI / projectileCount * i) + fireCount * 10000;
			float angle = Position.AngleToPoint(player.Position) + angleOffset;
			world.SpawnProjectile(Position, angle, DamageableKind.Friendly, Projectile.Type.Normal);
		}
		fireCount++;
	}

	public void _OnAltFireTimerTimeout()
	{
		body.Play("alt");
		for (int i = 0; i < projectileCount; i++)
		{
			float angleOffset = (float)(2 * Math.PI / projectileCount * i);
			float angle = Position.AngleToPoint(player.Position) + angleOffset;
			world.SpawnProjectile(Position, angle, DamageableKind.Friendly, Projectile.Type.Alt);
		}
	}
	public void _OnBombTimerTimeout()
	{
		body.Play("bomb");
		world.ThrowBomb(Position, Position.AngleToPoint(player.Position));
	}

	public void TakeDamage(int damage, Vector2 direction)
	{
		Health -= damage;

		ApplyImpulse(direction * 40);
		if (Health <= 0)
		{
			EmitSignal(SignalName.Death);
			QueueFree();
		}
	}
}
