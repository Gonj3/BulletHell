using System;
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
	private AnimationPlayer animPlayer;
	private bool running;
	private Vector2 prevDirection;
	private string[] animActions = {"Die", 
	"Attack Up", "Attack Up-Right",
	 "Attack Right", "Attack Down-Right",
	  "Attack Down", "Attack Down-Left",
	   "Attack Left", "Attack Up-Left"};


    public override void _Ready()
    {
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		prevDirection = Vector2.Zero;
		running = true;
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
				running = false;
				break;
			} 
			else 
			{
				running = true;
			}
		}
		
		// Start running or idle animations
		if(running) {
			if (direction != Vector2.Zero)
			{
				if (direction.X > 0) 
				{
					if (direction.Y > 0)
					{
						animPlayer.Play("Down-Right");
						prevDirection = Vector2.Right + Vector2.Down;
					} 
					else if (direction.Y < 0)
					{
						animPlayer.Play("Up-Right");
						prevDirection = Vector2.Right + Vector2.Up;
					}
					else
					{
						animPlayer.Play("Right");
						prevDirection = Vector2.Right;
					}
				} 
				else if (direction.X < 0)
				{
					
					if (direction.Y > 0)
					{
						animPlayer.Play("Down-Left");
						prevDirection = Vector2.Left + Vector2.Down;
					} 
					else if (direction.Y < 0)
					{
						animPlayer.Play("Up-Left");
						prevDirection = Vector2.Left + Vector2.Up;
					}
					else
					{
						animPlayer.Play("Left");
						prevDirection = Vector2.Left;
					}
				}
				else 
				{
					if (direction.Y > 0)
					{
						animPlayer.Play("Down");
						prevDirection = Vector2.Down;
					} 
					else if (direction.Y < 0)
					{
						animPlayer.Play("Up");
						prevDirection = Vector2.Up;
					}
				}
			} 
			else if (prevDirection != Vector2.Zero)
			{
				if (prevDirection.X > 0) 
				{
					if (prevDirection.Y > 0)
					{
						animPlayer.Play("Idle Down-Right");
					} 
					else if (prevDirection.Y < 0)
					{
						animPlayer.Play("Idle Up-Right");
					}
					else
					{
						animPlayer.Play("Idle Right");
					}
				} 
				else if (prevDirection.X < 0)
				{
					
					if (prevDirection.Y > 0)
					{
						animPlayer.Play("Idle Down-Left");
					} 
					else if (prevDirection.Y < 0)
					{
						animPlayer.Play("Idle Up-Left");
					}
					else
					{
						animPlayer.Play("Idle Left");
					}
				}
				else 
				{
					if (prevDirection.Y > 0)
					{
						animPlayer.Play("Idle Down");
					} 
					else if (prevDirection.Y < 0)
					{
						animPlayer.Play("Idle Up");
					}	
				}

			}
			else 
			{
				if (animPlayer.CurrentAnimation != "Die" && animPlayer.IsPlaying() == false) 
				{
					animPlayer.Play("Idle Down");
				}
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
			if (prevDirection != Vector2.Zero)
			{
				if (prevDirection.X > 0) 
				{
					if (prevDirection.Y > 0)
					{
						animPlayer.Play("Attack Down-Right");
					} 
					else if (prevDirection.Y < 0)
					{
						animPlayer.Play("Attack Up-Right");
					}
					else
					{
						animPlayer.Play("Attack Right");
					}
				} 
				else if (prevDirection.X < 0)
				{
					
					if (prevDirection.Y > 0)
					{
						animPlayer.Play("Attack Down-Left");
					} 
					else if (prevDirection.Y < 0)
					{
						animPlayer.Play("Attack Up-Left");
					}
					else
					{
						animPlayer.Play("Attack Left");
					}
				}
				else 
				{
					if (prevDirection.Y > 0)
					{
						animPlayer.Play("Attack Down");
					} 
					else if (prevDirection.Y < 0)
					{
						animPlayer.Play("Attack Up");
					}	
				}

			}
			else 
			{
				animPlayer.Play("Attack Up");
			}
		}

		area.QueueFree();
	}
}
