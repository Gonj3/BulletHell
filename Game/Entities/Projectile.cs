using Godot;
public partial class Projectile : Area2D
{
	public Vector2 velocity = new Vector2();
	public float speed = 30f;
	public float duration = 40;

	public override void _PhysicsProcess(double delta)
	{
		Position += velocity * (float)delta * speed;
		// ensure maximum duration
		duration -= (float)delta;
		if (duration <= 0)
		{
			QueueFree();
		}
	}

	public void _OnBodyEntered(Node2D body)
	{
		QueueFree();
	}
}
