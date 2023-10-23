using System;
using System.Collections;
using System.Diagnostics;
using System.Xml;
using Godot;

public partial class Player : CharacterBody2D
{
	[Signal]
	public delegate void DeathEventHandler();

	[Signal]
	public delegate void KillEventHandler();

	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public int Health = 100;
	public int Lives = 3;

	// Private animation variables
	[Export]
	private AnimationPlayer animPlayer;
	private bool animBusy = false;
	private Vector2 prevDirection;
	private string[] animActions = {"Die", 
	"Attack0", "Attack1", "Attack2", "Attack3",
	"Attack4", "Attack5", "Attack6", "Attack7"};

    public override void _Ready()
    {
		animPlayer.Play("Idle Down");
    }

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

		// Chek that the player is not performing a one-off action
		string animation = animPlayer.CurrentAnimation;
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
			if(direction != Vector2.Zero) 
			{
				animPlayer.Play("" + _getAnimSide(direction));
				prevDirection = direction;
			}
			else
			{
				animPlayer.Play("Idle" + _getAnimSide(prevDirection));
			}
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

				// Start death one-off animation
				animPlayer.Stop();
				animPlayer.Play("Die");
				
				Health = 100;
			}
		}
	}

	public void _on_area_2d_area_entered(Area2D area)
	{
		if (!(area is Projectile) && !(area is Enemy))
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

			// Start directional attack one-off animation
			animPlayer.Play("Attack" + _getAnimSide(prevDirection));
		}

		area.QueueFree();
	}

	// Private function to get an integer referring to the animation direction from a vector of movement
	
	private int _getAnimSide(Vector2 animDirection) 
	{
		int animAngle = Mathf.RoundToInt(Mathf.Snapped(animDirection.Angle(), (Math.PI / 4)) / (Math.PI / 4));
		animAngle = Mathf.Wrap(animAngle, 0, 8);
		return animAngle;
	} 
}
