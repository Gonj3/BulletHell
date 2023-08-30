using Godot;
using System;

public partial class Player : Godot.CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		// Get the input direction and handle the movement/deceleration.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			//direction.Normalized();
			//velocity.X = direction.X * Speed;
			//velocity.Y = direction.Y * Speed;
			velocity = direction.Normalized() * Speed;
			GD.Print(velocity.Length());
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
			GD.Print("not moving");
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
