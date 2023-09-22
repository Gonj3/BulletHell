using Godot;
using System;

public partial class Enemy : Area2D
{
	private Vector2 velocity = new Vector2();
	public float speed = 4f;
	private float fireCount = 0;

	public override void _Ready()
	{
		// pick a random direction to move in
		Random random = new Random();
		velocity = new Vector2((float)random.NextDouble() * 2 - 1, (float)random.NextDouble() * 2 - 1);
		velocity = velocity.Normalized() * speed;
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += velocity * (float)delta * speed;
	}

	public void _OnFireTimerTimeout()
	{
		var projectile = GD.Load<PackedScene>("res://Game/Entities/Projectile.tscn");
		var projInstance = projectile.Instantiate();
		// rotate the projectiles direction by 5degrees increasing for each fire
		projInstance.Set("velocity", velocity.Rotated((float)Math.PI / 180 * 5 * fireCount));
		projInstance.Set("position", Position);
		GetParent().AddChild(projInstance);
		fireCount++;
	}

	public void _OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup("Bounds"))
		{
			QueueFree();
		}
		else if (body.Name == "Player")
		{
			// TODO: afflict/inform player
			QueueFree();
		}
	}
}
