using Godot;
using System;

public partial class Menu : Control
{
	//TODO ADD GAME SCENE SOURCE.
	private const String GameScene = "res://Game.tscn";
	//TEMP Settings Scene source.
	private const String Settings = "res://Settings.tscn";
	//Load Scene source.
	private const String Load = "res://Load.tscn";
	//SaveGame Scene source.
	private const String SaveEditor = "res://SaveGameTesting.tscn";

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
		GetTree().ChangeSceneToFile(GameScene);
	}

	//Enter Load scene.
	public void _on_load_button_pressed()
	{
		GetTree().ChangeSceneToFile(Load);
	}

	//Enter Settings scene.
	public void _on_settings_button_pressed()
	{
		GetTree().ChangeSceneToFile(Settings);
	}

	//Quit game.
	public void _on_quit_button_pressed()
	{
		GetTree().Quit();
	}
	
	public void _on_saveeditor_button_pressed()
	{
		GetTree().ChangeSceneToFile(SaveEditor);
	}
}
