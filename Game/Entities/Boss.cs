using Godot;
using System;

public partial class Boss : RigidBody2D, IDamageable
{
	public float Speed = 10f;
	public int Health = 200;
	private float ProjectileSpeed = 50f;
	private int ProjectileCount = 8;
	private int fireCount = 0;
	private PackedScene projectileScene;
	private Timer FireTimer;
	private Timer AltFireTimer;
	[Export]
	private Player player;
	public DamageableKind DamageableKind { get; } = DamageableKind.Enemy;

	public override void _Ready()
	{
		projectileScene = GD.Load<PackedScene>("res://Game/Entities/Projectile.tscn");
		FireTimer = GetNode<Timer>("FireTimer");
		FireTimer.WaitTime = 0.7f;
		FireTimer.Start();

		AltFireTimer = GetNode<Timer>("AltFireTimer");
		AltFireTimer.WaitTime = 10f;
		AltFireTimer.Start();
	}

	public override void _PhysicsProcess(double delta)
	{
		LinearVelocity = Position.DirectionTo(player.Position) * Speed;
	}
	
	public void _OnFireTimerTimeout()
	{
		Fire();
	}

	public void _OnAltFireTimerTimeout()
	{ 
		// Todo: adaptable projectile types.
	}

	private void Fire()
	{
    	for (int i = 0; i < ProjectileCount; i++)
		{
			float angleOffset = (float)(2 * Math.PI / ProjectileCount * i) + fireCount * 1000;
			FireProjectile(angleOffset);
		}
		fireCount = (fireCount + 1) % ProjectileCount;
	}
	private void FireProjectile(float angleOffset)
	{
		var projInstance = (Projectile)projectileScene.Instantiate();

		projInstance.Position = Position;
		projInstance.Angle = angleOffset;

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