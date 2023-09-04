using Godot;
using System;

public partial class Menu : Control
{
	//TODO ADD GAME SCENE SOURCE.
<<<<<<< Updated upstream
	private String gameScene = "res://Game.tscn";
	//TEMP Settings Scene source.
	private String settings = "res://Settings.tscn";
	//Load Scene source.
	private String load = "res://Load.tscn";
=======
	private const String GameScene = "res://Game.tscn";
	//TEMP Settings Scene source.
	private const String Settings = "res://Settings.tscn";
	//Load Scene source.
	private const String Load = "res://Load.tscn";
>>>>>>> Stashed changes

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Button>("VBoxContainer/StartButton").GrabFocus();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	//Enter Game scene.
	public void _on_start_button_pressed() 
	{
<<<<<<< Updated upstream
		GetTree().ChangeSceneToFile(gameScene);
=======
		GetTree().ChangeSceneToFile(GameScene);
>>>>>>> Stashed changes
	}

	//Enter Load scene.
	public void _on_load_button_pressed()
	{
<<<<<<< Updated upstream
		GetTree().ChangeSceneToFile(load);
=======
		GetTree().ChangeSceneToFile(Load);
>>>>>>> Stashed changes
	}

	//Enter Settings scene.
	public void _on_settings_button_pressed()
	{
<<<<<<< Updated upstream
		GetTree().ChangeSceneToFile(settings);
=======
		GetTree().ChangeSceneToFile(Settings);
>>>>>>> Stashed changes
	}

	//Quit game.
	public void _on_quit_button_pressed()
	{
		GetTree().Quit();
	}
}
