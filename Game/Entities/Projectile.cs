using System;
using Godot;
public partial class Projectile : Area2D
{
	[Export]
	private Sprite2D sprite;
	[Export]
	private CollisionShape2D hitBox;
	[Export]
	private Texture2D ProjTexture;
	[Export]
	private Texture2D AltProjTexture;
	public enum Type
	{
		Normal,
		Alt,
		Player
	}
	public float Speed;
	public int Damage;
	private Vector2 vector;
	public void SetAngle(float angle)
	{
		vector = Vector2.FromAngle(angle).Normalized();
	}
	public DamageableKind Target { get; set; } = DamageableKind.Friendly;

	public void SetType(Type type)
	{
		switch (type)
		{
			case Type.Normal:
				sprite.Texture = ProjTexture;
				sprite.Scale = new Vector2(1f, 1f);
				hitBox.Shape = new CircleShape2D { Radius = 8f };
				Damage = 20;
				Speed = 200f;
				break;
			case Type.Alt:
				sprite.Texture = AltProjTexture;
				sprite.Scale = new Vector2(2f, 2f);
				hitBox.Shape = new CircleShape2D { Radius = 16f };
				Damage = 40;
				Speed = 100f;
				break;
			case Type.Player:
				sprite.Texture = ProjTexture;
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
