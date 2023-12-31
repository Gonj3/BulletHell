using Godot;
using System;

public partial class Boss : RigidBody2D, IDamageable
{
	[Export]
	public World World { get; set; }

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
		ConstantForce = Position.DirectionTo(World.Player.Position) * Speed;
	}

	public void _OnFireTimerTimeout()
	{
		for (int i = 0; i < projectileCount; i++)
		{
			float angleOffset = (float)(2 * Math.PI / projectileCount * i) + fireCount * 10000;
			float angle = Position.AngleToPoint(World.Player.Position) + angleOffset;
			World.SpawnProjectile(Position, angle, DamageableKind.Friendly, Projectile.Type.Normal);
		}
		fireCount++;
	}

	public void _OnAltFireTimerTimeout()
	{
		body.Play("alt");
		for (int i = 0; i < projectileCount; i++)
		{
			float angleOffset = (float)(2 * Math.PI / projectileCount * i);
			float angle = Position.AngleToPoint(World.Player.Position) + angleOffset;
			World.SpawnProjectile(Position, angle, DamageableKind.Friendly, Projectile.Type.Alt);
		}
	}
	public void _OnBombTimerTimeout()
	{
		body.Play("bomb");
		World.ThrowBomb(Position, Position.AngleToPoint(World.Player.Position), 120);
	}

	public void TakeDamage(int damage, float angle, int force)
	{
		// take "knockback"
		Vector2 impulse = new Vector2(force, 0).Rotated(angle);
		ApplyImpulse(impulse);

		this.GetAudioManager().PlaySound("HitSFX");

		// take damage
		Health -= damage;
		if (Health <= 0)
		{
			World.SpawnPowerup(Position);
			this.GetAudioManager().PlaySound("DeathSFX");
			EmitSignal(SignalName.Death);
			QueueFree();
		}
	}
}
