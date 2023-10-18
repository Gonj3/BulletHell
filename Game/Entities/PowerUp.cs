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
		Random rand = new Random();
		type = (int)rand.Next(0, 3);
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
}
