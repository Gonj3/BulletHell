using Godot;
using System;

public partial class Menu : Control
{
	//TODO ADD GAME SCENE SOURCE.
	private String gameScene = "res://Game.tscn";
	//TEMP Settings Scene source.
	private String settings = "res://Settings.tscn";
	//Load Scene source.
	private String load = "res://Load.tscn";

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
		GetTree().ChangeSceneToFile(gameScene);
	}

	//Enter Load scene.
	public void _on_load_button_pressed()
	{
		GetTree().ChangeSceneToFile(load);
	}

	//Enter Settings scene.
	public void _on_settings_button_pressed()
	{
		GetTree().ChangeSceneToFile(settings);
	}

	//Quit game.
	public void _on_quit_button_pressed()
	{
		GetTree().Quit();
	}
}
