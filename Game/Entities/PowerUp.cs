using Godot;
using System;

public partial class PowerUp : Area2D
{
	public int healthIncrease = 40;
	public int lifeIncrease = 1;
	public float speedIncrease = 100f;
	public int type { get; set; } = 0;
	
	public void InitialiseType()
	{
		/*
		0 - Health Increase
		1 - Life++
		2 - Speed Increase
		3 - Bomb
		*/
		Random rand = new Random();
		type = (int)rand.Next(0, 4);
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InitialiseType();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void _on_body_entered(Node2D body)
	{
		if(body is Player)
		{
			GD.Print("powerUpEnteredPlayerBody");
			switch(type)
			{
				case 0:
					((Player)body).Heal(healthIncrease);
					((Player)body).Items[0]++;
					break;
				case 1:
					((Player)body).Lives += lifeIncrease;
					((Player)body).Items[1]++;
					break;
				case 2:
					((Player)body).Speed += speedIncrease;
					((Player)body).Items[2]++;
					break;
				case 3:
					((Player)body).Items[3]++;
					break;
			}	
		}
	}
}

