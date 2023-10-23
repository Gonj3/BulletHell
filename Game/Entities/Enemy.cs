using Godot;
using System;

public partial class Enemy : RigidBody2D, IDamageable
{
	public float Speed = 50f;
	public float ProjectileSpeed { get; set; } = 10.0f;
	public int ProjectileCount { get; set; } = 1;
	public float FireTimeout { get; set; } = 1.0f;

	public enum FiringStyle
	{
		Spin,
		Spike,
		Spread
	}

	public FiringStyle CurrentFiringStyle { get; set; } = FiringStyle.Spin;

	public DamageableKind DamageableKind { get; } = DamageableKind.Enemy;

	private float fireCount = 0;
	private PackedScene projectileScene;
	private Timer fireTimer;

	[Export]
	private Player player;

	public override void _Ready()
	{
		projectileScene = GD.Load<PackedScene>("res://Game/Entities/Projectile.tscn");
		InitializeRandomValues();
		InitializeFiringStyle();
		InitializeFireTimer();
	}

	private void InitializeRandomValues()
	{
		Random random = new Random();
		CurrentFiringStyle = (FiringStyle)random.Next(0, 3);
	}

	private void InitializeFiringStyle()
	{
		switch (CurrentFiringStyle)
		{
			case FiringStyle.Spin:
				FireTimeout = 0.2f;
				break;
			case FiringStyle.Spike:
				ProjectileCount = 8;
				break;
			case FiringStyle.Spread:
				FireTimeout = 2.0f;
				ProjectileCount = 15;
				break;
		}
	}

	private void InitializeFireTimer()
	{
		fireTimer = GetNode<Timer>("FireTimer");
		fireTimer.WaitTime = FireTimeout;
		fireTimer.Start();
	}

	public override void _PhysicsProcess(double delta)
	{
		LinearVelocity = Position.DirectionTo(player.Position) * Speed;
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
		FireProjectiles(angleOffset);
		fireCount++;
	}

	private void FireSpike()
	{
		float coneAngle = (float)Math.PI / 4.0f;
		float startAngle = -coneAngle / 2.0f;

		for (int i = 0; i < ProjectileCount; i++)
		{
			float angleOffset = startAngle + coneAngle / (ProjectileCount - 1) * i;
			FireProjectiles(angleOffset);
		}
	}

	private void FireSpread()
	{
		for (int i = 0; i < ProjectileCount; i++)
		{
			float angleOffset = (float)(2 * Math.PI / ProjectileCount * i);
			FireProjectiles(angleOffset);
		}
	}

	private void FireProjectiles(float angleOffset)
	{
		var projInstance = (Projectile)projectileScene.Instantiate();

		projInstance.Angle = Position.AngleToPoint(player.Position) + angleOffset;
		projInstance.Position = Position;

		GetParent().AddChild(projInstance);
	}

	public void TakeDamage(int damage)
	{
		throw new NotImplementedException();
	}

}
