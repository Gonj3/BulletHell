using Godot;
using System;

public partial class Enemy : RigidBody2D, IDamageable
{
	[Signal]
	public delegate void DeathEventHandler();

	public float Speed = 50f;

	[Export]
	private AnimationPlayer spriteAnim;

	[Export]
	public World World { get; set; }

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

	[Export]
	private AnimationPlayer healthBarAnim;

	public override void _Ready()
	{
		InitializeRandomValues();
		InitializeFiringStyle();
		spriteAnim.Play("Idle");
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
		ConstantForce = Position.DirectionTo(World.Player.Position) * Speed;
		UpdateHealth();
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
		spriteAnim.Play("Attack");
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
		float angle = Position.AngleToPoint(World.Player.Position) + angleOffset;
		World.SpawnProjectile(Position, angle, DamageableKind.Friendly, Projectile.Type.Normal);
	}

	public void TakeDamage(int damage, Vector2 direction)
	{
		Health -= damage;

		spriteAnim.Play("damage_flash");
		ApplyImpulse(direction * 40);

		this.GetAudioManager().PlaySound("HitSFX");

		if (Health <= 0)
		{
			this.GetAudioManager().PlaySound("KillSFX");
			EmitSignal(SignalName.Death);
			QueueFree();
		}
	}

	private void UpdateHealth()
	{
		healthBarAnim.Play("Health" + Mathf.RoundToInt(Health / 30 * 100));
	}
}
