using Godot;
using System;

public partial class Load : Control
{
	//Main Menu scene source.
	private String mainScene = "res://MainMenu.tscn";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	//Change back to Main Menu scene.
	public void _on_back_button_pressed() 
	{
		GetTree().ChangeSceneToFile(mainScene);
	}
}
