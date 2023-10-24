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

	[Export]
	private AnimationPlayer playerAnimator;

	public float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public int Health = 100;
	public int Lives = 3;
	public bool Dashing = false;
	public bool NoHealthMode = false;
	public int[] Items = { 0, 0, 0, 0 };

	public DamageableKind DamageableKind { get; } = DamageableKind.Friendly;

	private bool takenDamageThisTick = false;
	private bool animBusy = false;
	private float animAngle = 2;
	private Vector2 prevDirection;
	private string[] animActions = {"Die",
	"Attack0", "Attack1", "Attack2", "Attack3",
	"Attack4", "Attack5", "Attack6", "Attack7"};

	public override void _Ready()
	{
		playerAnimator.Play("Idle2");
	}

	public override void _PhysicsProcess(double delta)
	{
		takenDamageThisTick = false;
		UpdateHealth();
		UpdateLives();
		CheckLostLives();
		CheckIfDashing();

		if (Input.IsActionPressed("dash") && dashTimer.TimeLeft == 0)
		{
			Velocity *= 4;
			Dashing = true;
			dashTimer.Start();
		}

		if (Dashing)
		{
			MoveAndSlide();
			return;
		}

		//GD.Print(dashTimer.TimeLeft.ToString() + "?");
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

		// Chek that the player is not performing a one-off action
		string animation = playerAnimator.CurrentAnimation;
		foreach (string anim in animActions)
		{
			if (anim == animation)
			{
				animBusy = true;
				break;
			}
			else
			{
				animBusy = false;
			}
		}

		// Start running or idle animations
		if (!animBusy)
		{
			if (direction != Vector2.Zero)
			{
				playerAnimator.Play("Run" + GetAnimSide(direction.Angle()));
				prevDirection = direction;
			}
			else
			{
				playerAnimator.Play("Idle" + GetAnimSide(prevDirection.Angle()));
			}
		}

		Velocity = velocity;
		MoveAndSlide();

		if (Input.IsActionPressed("shoot") && fireTimer.TimeLeft == 0)
		{
			this.GetAudioManager().PlaySound("ShootSFX");
			playerAnimator.Play("Attack" + GetAnimSide(Position.AngleToPoint(GetGlobalMousePosition())));
			world.SpawnProjectile(Position, Position.AngleToPoint(GetGlobalMousePosition()), DamageableKind.Enemy, Projectile.Type.Player);
			fireTimer.Start();
		}

		if (Input.IsActionPressed("bomb") && bombTimer.TimeLeft == 0)
		{
			var mouseAngle = Position.AngleToPoint(GetGlobalMousePosition());
			var offsetPosition = Position + Vector2.Right.Rotated(mouseAngle) * 60;
			world.ThrowBomb(offsetPosition, mouseAngle, 40);
			bombTimer.Start();
		}
	}

	private void CheckIfDashing()
	{
		if (dashTimer.TimeLeft < 4.8)
		{
			Dashing = false;
		}
	}

	//takes health from player based on int damage
	public void TakeDamage(int damage, float _, int __)
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
	private void UpdateHealth()
	{
		healthBarAnim.Play("Health" + Mathf.RoundToInt(Health / 10 * 10));
	}

	private void UpdateLives()
	{
		GetNode<Label>("LivesLabel").Text = Lives + "Live(s)";
	}

	//Checks if the Player is 0 or less than 0 health, if so removes life and resets health
	private void CheckLostLives()
	{
		if (Health <= 0)
		{
			if (Lives <= 0)
			{
				KillPlayer();
			}
			else
			{
				playerAnimator.Play("Die");
				Lives--;
				Health = 100;
			}
		}
		if (NoHealthMode == true)
		{
			if (Lives <= 0)
			{
				KillPlayer();
			}
		}
	}

	private void KillPlayer()
	{
		this.GetAudioManager().PlaySound("DeathSFX");
		EmitSignal(SignalName.Death);
	}

	private int GetAnimSide(float animDirection)
	{
		int animAngle = Mathf.RoundToInt(Mathf.Snapped(animDirection, Mathf.Pi / 4) / (Mathf.Pi / 4));
		animAngle = Mathf.Wrap(animAngle, 0, 8);
		return animAngle;
	}
}
