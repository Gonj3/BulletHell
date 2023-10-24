using Godot;
using System;

public partial class Enemy : RigidBody2D, IDamageable
{
	[Export]
	private AnimationPlayer spriteAnim;

	public float Speed = 50f;

	[Export]
	private Player player;
	[Export]
	private World world;

	public DamageableKind DamageableKind { get; } = DamageableKind.Enemy;
	public int Health = 30;

	[Export]
	private Timer fireTimer;

	public float ProjectileSpeed;
	public int ProjectileCount;
	private float fireCount = 0;
	public float FireTimeout;
	public enum FiringStyle
	{
		Spin,
		Spike,
		Spread
	}
	public FiringStyle CurrentFiringStyle;

	public override void _Ready()
	{
		InitializeRandomValues();
		InitializeFiringStyle();
	}

	private void InitializeFiringStyle()
	{
		switch (CurrentFiringStyle)
		{
			case FiringStyle.Spin:
				fireTimer.WaitTime = 0.2f;
				ProjectileCount = 1;
				ProjectileSpeed = 10f;
				break;
			case FiringStyle.Spike:
				fireTimer.WaitTime = 1f;
				ProjectileCount = 8;
				ProjectileSpeed = 5f;
				break;
			case FiringStyle.Spread:
				fireTimer.WaitTime = 2.0f;
				ProjectileCount = 15;
				ProjectileSpeed = 3f;
				break;
		}
	}
	private void InitializeRandomValues()
	{
		Random random = new Random();
		CurrentFiringStyle = (FiringStyle)random.Next(0, 3);
	}

	public override void _PhysicsProcess(double delta)
	{
		ConstantForce = Position.DirectionTo(player.Position) * Speed;
	}

	public void _OnFireTimerTimeout()
	{
		switch (CurrentFiringStyle)
		{
			case FiringStyle.Spin:
				FireSpin();
				break;
			case FiringStyle.Spike:
				FireSpike();
				break;
			case FiringStyle.Spread:
				FireSpread();
				break;
		}
	}

	private void FireSpin()
	{
		float angleOffset = (float)Math.PI / 180 * 20 * fireCount;
		FireProjectile(angleOffset);
		fireCount++;
	}
	private void FireSpike()
	{
		float coneAngle = (float)Math.PI / 4.0f;
		float startAngle = -coneAngle / 2.0f;

		for (int i = 0; i < ProjectileCount; i++)
		{
			float angleOffset = startAngle + coneAngle / (ProjectileCount - 1) * i;
			FireProjectile(angleOffset);
		}
	}
	private void FireSpread()
	{
		for (int i = 0; i < ProjectileCount; i++)
		{
			float angleOffset = (float)(2 * Math.PI / ProjectileCount * i);
			FireProjectile(angleOffset);
		}
	}
	private void FireProjectile(float angleOffset)
	{
		float angle = Position.AngleToPoint(player.Position) + angleOffset;
		world.SpawnProjectile(Position, angle, DamageableKind.Friendly, Projectile.Type.Normal);
	}

	public void TakeDamage(int damage, Vector2 direction)
	{
		Health -= damage;

		spriteAnim.Play("damage_flash");
		ApplyImpulse(direction * 40);

		if (Health <= 0)
			QueueFree();
	}
}
