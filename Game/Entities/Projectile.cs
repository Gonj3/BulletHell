using Godot;
public partial class Projectile : Area2D
{
	public Vector2 velocity = new Vector2();
	public float speed = 30f;
	public float duration = 40;

	// Private variable to improve performance
	private Sprite2D sprite;

    public override void _Ready()
    {
        sprite = GetNode<Sprite2D>("Sprite");
    }

    public override void _PhysicsProcess(double delta)
	{
		Position += velocity * (float)delta * speed;
		// ensure maximum duration
		duration -= (float)delta;
		if (duration <= 0)
		{
			QueueFree();
		}

		// Set the rotation of the sprite to match the direction of the projectile
		sprite.Rotation = velocity.Angle();
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
