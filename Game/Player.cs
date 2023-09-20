using Godot;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public int Health = 100;
	public int Lives = 3;

	
	
	public override void _PhysicsProcess(double delta)
	{
		UpdateHealth();
		Vector2 velocity = Velocity;
		// Get the input direction and handle the movement/deceleration.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity = direction.Normalized() * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
	
	//takes health from player based on int damage
	public void TakeDamage(int damage)
	{
		Health = Health - damage;
	}
	
	//Heals Player by int amount, makes sure health never goes over 100
	public void Heal(int amount)
	{
		if(Health + amount > 100)
		{
			Health = 100;
		}
		else
		{
			Health = Health + amount;
		}
	}
	
	//updates the HealthBar value to the current players Health
	public void UpdateHealth()
	{
		GetNode<ProgressBar>("HealthBar").Value = Health;
	}
	
	//Checks if the Player is 0 or less than 0 health, if so removes life and resets health
	public void _Process(float delta)
	{
		if(Health <= 0)
		{
			if(Lives <= 0)
			{
				//Player is dead and has lost all lives
				//death screen / restart level
			}
			else
			{
				Lives--;
				Health = 100;
			}
		}
	}
}
