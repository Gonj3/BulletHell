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

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		projectileScene = GD.Load<PackedScene>("res://Game/Entities/Projectile.tscn");
		InitTimers();
	}

	private void InitTimers()
	{
		FireTimer = GetNode<Timer>("FireTimer");
		FireTimer.WaitTime = 0.7f;
		FireTimer.Start();

		AltFireTimer = GetNode<Timer>("AltFireTimer");
		AltFireTimer.WaitTime = 10f;
		AltFireTimer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
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
    var angleStep = Mathf.Pi * 2 / ProjectileCount;
    var angleOffset = fireCount * angleStep / 2;
    for (var i = 0; i < ProjectileCount; i++)
    {
        Projectile projectile = (Projectile)projectileScene.Instantiate();
        projectile.Position = Position;
        projectile.Angle = angleStep * i + angleOffset + Rotation;
        projectile.Speed = ProjectileSpeed;
        GetTree().Root.AddChild(projectile);
    }
    fireCount++;
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