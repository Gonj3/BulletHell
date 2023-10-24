using Godot;

public partial class Player : CharacterBody2D, IDamageable
{
	[Signal]
	public delegate void DeathEventHandler();

	[Export]
	private World world;

	[Export]
	private Timer fireTimer;

	[Export]
	private AnimationPlayer healthBarAnim;

	[Export]
	private Timer dashTimer;

	[Export]
	private Timer bombTimer;

	public float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public int Health = 100;
	public int Lives = 3;
	public bool Dashing = false;
	public bool NoHealthMode = false;
	public int[] Items = { 0, 0, 0, 0 };

	public DamageableKind DamageableKind { get; } = DamageableKind.Friendly;

	private bool takenDamageThisTick = false;

	public override void _PhysicsProcess(double delta)
	{
		takenDamageThisTick = false;
		UpdateHealth();
		UpdateLives();
		CheckLostLives();
		CheckIfDashing();

		if (Input.IsActionPressed("ui_select") && dashTimer.TimeLeft == 0)
		{
			Speed = 800.0f;
			Dashing = true;
			dashTimer.Start();
		}
		else if (Input.IsActionPressed("controller_dash") && dashTimer.TimeLeft == 0)
		{
			Speed = 800.0f;
			Dashing = true;
			dashTimer.Start();
		}

		//GD.Print(dashTimer.TimeLeft.ToString() + "?");
		Vector2 velocity = Velocity;
		// Get the input direction and handle the movement/deceleration.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector2 JoystickDirection = Input.GetVector("joystick_left", "joystick_right", "joystick_up", "joystick_down");
		Vector2 JoystickAim = Input.GetVector("joystick_aim_right", "joystick_aim_left", "joystick_aim_up", "joystick_aim_down");

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

		float joystickAngle = Mathf.Atan2(JoystickAim.Y, JoystickAim.X);

		if (Input.IsActionPressed("shoot") && fireTimer.TimeLeft == 0)
		{
			world.SpawnProjectile(Position, Position.AngleToPoint(GetGlobalMousePosition()), DamageableKind.Enemy, Projectile.Type.Player);
			fireTimer.Start();
		}
		else if (Input.IsActionPressed("controller_shoot") && fireTimer.TimeLeft == 0)
		{
			world.SpawnProjectile(Position, joystickAngle, DamageableKind.Enemy, Projectile.Type.Player);
			fireTimer.Start();
		}

		if (Input.IsActionPressed("bomb") && bombTimer.TimeLeft == 0)
		{
			var mouseAngle = Position.AngleToPoint(GetGlobalMousePosition());
			var offsetPosition = Position + Vector2.Right.Rotated(mouseAngle) * 60;
			world.ThrowBomb(offsetPosition, mouseAngle);
			bombTimer.Start();
		}
		else if (Input.IsActionPressed("controller_bomb") && bombTimer.TimeLeft == 0)
		{
			var offsetPosition = Position + Vector2.Right.Rotated(joystickAngle) * 60;
			world.ThrowBomb(offsetPosition, joystickAngle);
			bombTimer.Start();
		}
	}

	public void CheckIfDashing()
	{
		if (dashTimer.TimeLeft < 4)
		{
			Dashing = false;
			Speed = 300.0f;
		}
	}

	//takes health from player based on int damage
	public void TakeDamage(int damage, Vector2 _)
	{
		if (!takenDamageThisTick && Dashing == false)
		{
			if (NoHealthMode == true)
			{
				Lives--;
				takenDamageThisTick = true;
			}
			else
			{
				Health -= damage;
				takenDamageThisTick = true;
			}
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
		healthBarAnim.Play("Health" + Mathf.RoundToInt(Health / 10 * 10));
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
		if (NoHealthMode == true)
		{
			if (Lives <= 0)
			{
				EmitSignal(SignalName.Death);
			}
		}
	}
}
