using Godot;
using System;

public partial class Boss : RigidBody2D, IDamageable
{
	public float Speed = 10f;
	public int Health = 200;
	private float ProjectileSpeed = 2f;
	private int ProjectileCount = 10;
	private PackedScene projectileScene;
	private Timer FireTimer;
	private Timer AltFireTimer;
	[Export]
	private Player player;
	public DamageableKind DamageableKind { get; } = DamageableKind.Boss;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		projectileScene = GD.Load<PackedScene>("res://Game/Entities/Projectile.tscn");
		InitTimers();
	}

	private void InitTimers()
	{
		FireTimer = GetNode<Timer>("FireTimer");
		FireTimer.WaitTime = 1f;
		FireTimer.Start();

		AltFireTimer = GetNode<Timer>("AltFireTimer");
		AltFireTimer.WaitTime = 10f;
		AltFireTimer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		LinearVelocity = Position.DirectionTo(player.Position) * Speed;
	}
	
	public void _OnFireTimerTimeout()
	{
		FireSpin();
	}

	public void _OnAltFireTimerTimeout()
	{
		FireSpin();	
	}

	// a fire method similar to the enemys the spread, that slowly spins around the boss
	private void FireSpin()
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
		Health -= damage;
		if (Health <= 0)
		{
			QueueFree();
		}
	}
}