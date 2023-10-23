using System;
using Godot;
public partial class Projectile : Area2D
{
	public enum ProjectileType
	{
		Normal,
		Alt
	}
    public float Speed { get; set; } = 200f;
    public int Damage { get; set; } = 20;
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


	public void SetType(ProjectileType type)
    {
		var sprite = GetNode<Sprite2D>("Sprite");
		var collisionShape = GetNode<CollisionShape2D>("HitBox");
        switch (type)
        {
            case ProjectileType.Normal:
                sprite.Texture = GD.Load<Texture2D>("res://Textures/Projectiles/ProjectileSprite.png");
				sprite.Scale = new Vector2(0.5f, 0.5f);
				collisionShape.Shape = new CircleShape2D { Radius = 8f };
				break;
            case ProjectileType.Alt:
				sprite.Texture = GD.Load<Texture2D>("res://Textures/Projectiles/AltProjectileSprite.png");
				sprite.Scale = new Vector2(1f, 1f);
				collisionShape.Shape = new CircleShape2D { Radius = 16f };
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
