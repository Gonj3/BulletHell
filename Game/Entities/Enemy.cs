using Godot;
using System;

public partial class Enemy : Area2D
{
	private Vector2 velocity = new Vector2();
	public float speed = 4.0f;
	public float ProjectileSpeed { get; set; } = 10.0f;
	public int ProjectileCount { get; set; } = 1;
	public float FireTimeout { get; set; } = 1.0f;
	private float fireCount = 0;

	[Export]
	private AnimationPlayer animPlayer;

	public enum FiringStyle
	{
		Spin,
		Spike,
		Spread
	}

	public FiringStyle CurrentFiringStyle { get; set; } = FiringStyle.Spin;

	private Timer fireTimer;
	private PackedScene projectileScene;

	public override void _Ready()
	{
		projectileScene = GD.Load<PackedScene>("res://Game/Entities/Projectile.tscn");
		InitializeRandomValues();
		InitializeFiringStyle();
		InitializeFireTimer();
		animPlayer.Play("Idle");
	}

	private void InitializeRandomValues()
	{
		Random random = new Random();
		CurrentFiringStyle = (FiringStyle)random.Next(0, 3);
		velocity = new Vector2((float)(2 * random.NextDouble()), (float)(2 * random.NextDouble())).Normalized() * speed;
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
		Position += velocity * (float)delta * speed;
		
		// Reset animation
		if(!animPlayer.IsPlaying()) 
		{
			animPlayer.Play("Idle");
		}
	}

	public void _OnFireTimerTimeout()
	{
		// Shoot Animation
		animPlayer.Play("Shoot");

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
		var projInstance = projectileScene.Instantiate();
		if (projInstance != null)
		{
			Vector2 rotatedVelocity = velocity.Rotated(angleOffset);
			projInstance.Set("velocity", rotatedVelocity.Normalized() * ProjectileSpeed);
			projInstance.Set("position", Position);
			GetParent().AddChild(projInstance);
		}
	}

	// Player collision may be removed entirely in favor of player side detection
	public void _OnBodyEntered(Node2D body)
	{
		if (body.Name == "BoundsBody")
		{
			// remove self
			QueueFree();
		}
	}
}
