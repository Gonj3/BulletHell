using Godot;
using System;

public partial class Settings : Control
{
	//Main Menu scene source.
<<<<<<< Updated upstream
	private String mainScene = "res://MainMenu.tscn";
=======
	private const String MainScene = "res://MainMenu.tscn";
>>>>>>> Stashed changes

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	//Change back to Main Menu scene.
	public void _on_back_pressed() 
	{
<<<<<<< Updated upstream
		GetTree().ChangeSceneToFile(mainScene);
=======
		GetTree().ChangeSceneToFile(MainScene);
>>>>>>> Stashed changes
	}
}
