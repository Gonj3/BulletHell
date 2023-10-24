using System;
using Godot;
public partial class Projectile : Area2D
{
	[Export]
	private Sprite2D sprite;

	[Export]
	private AnimationPlayer animator;

	[Export]
	private CollisionShape2D hitBox;

	[Export]
	private Texture2D projTexture;

	[Export]
	private Texture2D altProjTexture;

	public enum Type
	{
		Normal,
		Alt,
		Player
	}
	public float Speed;
	public int Damage;
	private Vector2 vector;
	public DamageableKind Target { get; set; } = DamageableKind.Friendly;

	public void SetAngle(float angle)
	{
		vector = Vector2.FromAngle(angle).Normalized();
	}

	public void SetType(Type type)
	{
		switch (type)
		{
			case Type.Normal:
				//sprite.Texture = projTexture;
				animator.Play("Shoot");
				sprite.Scale = new Vector2(1f, 1f);
				hitBox.Shape = new CircleShape2D { Radius = 8f };
				Damage = 20;
				Speed = 200f;
				break;
			case Type.Alt:
				//sprite.Texture = altProjTexture;
				animator.Play("ShootAlt");
				sprite.Scale = new Vector2(2f, 2f);
				hitBox.Shape = new CircleShape2D { Radius = 16f };
				Damage = 40;
				Speed = 100f;
				break;
			case Type.Player:
				//sprite.Texture = projTexture;
				animator.Play("ShootPlayer");
				sprite.Scale = new Vector2(1f, 1f);
				hitBox.Shape = new CircleShape2D { Radius = 8f };
				Damage = 30;
				Speed = 1000f;
				break;
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		Position += vector * Speed * (float)delta;
		sprite.Rotation = vector.Angle();
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
