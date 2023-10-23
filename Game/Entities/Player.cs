using Godot;

public partial class Player : CharacterBody2D, IDamageable
{
	[Signal]
	public delegate void DeathEventHandler();

	[Signal]
	public delegate void KillEventHandler();

	public float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public int Health = 100;
	public int Lives = 3;
	public bool CanDash = true;
	public bool Dashing = false;
	public int DashCounter = 10;

	public DamageableKind DamageableKind { get; } = DamageableKind.Friendly;

	private bool takenDamageThisTick = false;

	public override void _PhysicsProcess(double delta)
	{
		takenDamageThisTick = false;
		UpdateHealth();
		UpdateLives();
		CheckLostLives();
		CheckIfCanDash();
		
		if(Input.IsActionPressed("ui_select"))
		{
			if(CanDash == true)
			{
				Speed = 800.0f;
				Dashing = true;
				CanDash = false;
				DashCounter = 150;
			}
		}
		
		if(DashCounter != 0)
		{
			GD.Print(DashCounter);
			DashCounter--;
		}
		
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

	public void CheckIfCanDash()
	{
		if(DashCounter == 0)
		{
			CanDash = true;
		}
		if(DashCounter == 120)
		{
			//GD.Print("stopped dashing");
			Dashing = false;
			Speed = 300.0f;
		}
	}

	//takes health from player based on int damage
	public void TakeDamage(int damage)
	{
		if (!takenDamageThisTick && Dashing == false)
		{
			takenDamageThisTick = true;
			Health -= damage;
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
	}
}
