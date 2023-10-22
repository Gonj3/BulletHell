using Godot;

public partial class Player : CharacterBody2D
{
	[Signal]
	public delegate void DeathEventHandler();

	[Signal]
	public delegate void KillEventHandler();

	public float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public int Health = 100;
	public int Lives = 3;
	public bool NoHealthMode = false;
	public int[] Items = {0, 0, 0, 0};
	
	public override void _PhysicsProcess(double delta)
	{
		UpdateHealth();
		UpdateLives();
		CheckLostLives();
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
		if(NoHealthMode == true)
		{
			Lives--;
		}
		else
		{
			Health = Health - damage;
		}
	}

	//Heals Player by int amount, makes sure health never goes over 100
	public void Heal(int amount)
	{
		if (Health + amount > 100)
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

	public void UpdateLives()
	{
		GetNode<Label>("LivesLabel").Text = Lives + "Live(s)";
	}

	//Checks if the Player is 0 or less than 0 health, if so removes life and resets health
	public void CheckLostLives()
	{
		if (Health <= 0)
		{
			if (Lives <= 0)
			{
				EmitSignal(SignalName.Death);
			}
			else
			{
				Lives--;
				Health = 100;
			}
		}
		if(NoHealthMode == true)
		{
			if(Lives <= 0)
			{
				EmitSignal(SignalName.Death);
			}
		}
	}

	public void _on_area_2d_area_entered(Area2D area)
	{
		if (!(area is Projectile) && !(area is Enemy) && !(area is PowerUp))
		{
			return;
		}

		if (area is Projectile)
		{
			TakeDamage(20);
		}

		if (area is Enemy)
		{
			EmitSignal(SignalName.Kill);
		}

		if (area is PowerUp)
		{
			GD.Print("PowerUp type" + (((PowerUp)area).type));
			switch((((PowerUp)area).type))
			{
				case 0:
					Heal((((PowerUp)area).healthIncrease));
					Items[0]++;
					break;
				case 1:
					this.Lives += (((PowerUp)area).lifeIncrease);
					Items[1]++;
					break;
				case 2:
					this.Speed += (((PowerUp)area).speedIncrease);;
					Items[2]++;
					break;
				case 3:
					Items[3]++;
					break;
			}
			
		}

		area.QueueFree();
	}
}
