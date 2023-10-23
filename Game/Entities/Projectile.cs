using System;
using Godot;
public partial class Projectile : Area2D
{
	public enum Type
	{
		Normal,
		Alt,
		Player
	}
	public float Speed;
	public int Damage;
	public float Angle
	{
		get => _angle; set
		{
			_angle = value;
			vector = new Vector2((float)Math.Cos(value), (float)Math.Sin(value)).Normalized();
		}
	}
	private float _angle;
	private Vector2 vector;
	public DamageableKind Target { get; set; } = DamageableKind.Friendly;


	public void SetType(Type type)
	{
		var sprite = GetNode<Sprite2D>("Sprite");
		var collisionShape = GetNode<CollisionShape2D>("HitBox");
		switch (type)
		{
			case Type.Normal:
				sprite.Texture = GD.Load<Texture2D>("res://Textures/Projectiles/ProjectileSprite.png");
				sprite.Scale = new Vector2(1f, 1f);
				collisionShape.Shape = new CircleShape2D { Radius = 8f };
				Damage = 20;
				Speed = 200f;
				break;
			case Type.Alt:
				sprite.Texture = GD.Load<Texture2D>("res://Textures/Projectiles/AltProjectileSprite.png");
				sprite.Scale = new Vector2(2f, 2f);
				collisionShape.Shape = new CircleShape2D { Radius = 16f };
				Damage = 40;
				Speed = 100f;
				break;
			case Type.Player:
				sprite.Texture = GD.Load<Texture2D>("res://Textures/Projectiles/ProjectileSprite.png");
				sprite.Scale = new Vector2(1f, 1f);
				collisionShape.Shape = new CircleShape2D { Radius = 8f };
				Damage = 30;
				Speed = 1000f;
				break;
		}
	}
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
