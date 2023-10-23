using System;
using Godot;
public partial class Projectile : Area2D
{
	public float Angle
	{
		get => _angle; set
		{
			_angle = value;
			vector = new Vector2((float)Math.Cos(value), (float)Math.Sin(value)).Normalized();
		}
	}

	public float Speed { get; set; } = 200f;
	public int Damage { get; set; } = 20;
	public DamageableKind Target { get; set; } = DamageableKind.Friendly;


	private float _angle;
	private Vector2 vector;

	public override void _PhysicsProcess(double delta)
	{
		Position += vector * Speed * (float)delta;
	}

	public void _OnBodyEntered(Node2D body)
	{
		if (body is TileMap)
			QueueFree();

		if (body is IDamageable damageable && damageable.DamageableKind == Target)
		{
			damageable.TakeDamage(Damage, vector);
			QueueFree();
		}
	}
}
