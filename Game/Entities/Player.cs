using Godot;

public partial class Player : CharacterBody2D, IDamageable
{
	[Signal]
	public delegate void DeathEventHandler();

	[Signal]
	public delegate void KillEventHandler();

	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public int Health = 100;
	public int Lives = 3;

	public DamageableKind DamageableKind { get; } = DamageableKind.Friendly;

	private bool takenDamageThisTick = false;

	[Export]
	private World world;

	[Export]
	private Timer fireTimer;

	public override void _PhysicsProcess(double delta)
	{
		takenDamageThisTick = false;
		UpdateHealth();
		UpdateLives();
		CheckLostLives();
		Vector2 velocity = Velocity;
		// Get the input direction and handle the movement/deceleration.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down"); 
		Vector2 JoystickDirection = Input.GetVector("joystick_left", "joystick_right", "joystick_up", "joystick_down"); 
		var resultVec = direction + JoystickDirection;
		if (resultVec != Vector2.Zero)
		{
			velocity = resultVec.Normalized() * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();

		if (Input.IsActionPressed("shoot") && fireTimer.TimeLeft == 0)
		{
			world.SpawnProjectile(Position, Position.AngleToPoint(GetGlobalMousePosition()), 1000f, DamageableKind.Enemy);
			fireTimer.Start();
		}
	}

	//takes health from player based on int damage
	public void TakeDamage(int damage, Vector2 _)
	{
		if (!takenDamageThisTick)
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
