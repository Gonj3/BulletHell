using Godot;
using System;

public partial class SaveGameTesting : VBoxContainer
{
	private const String MainMenu = "res://MainMenu.tscn";
	
	private SaveGame SaveGame;
	private LineEdit NameBox;
	private SpinBox DeathsBox;
	private SpinBox KillsBox;

	public override void _Ready()
	{
		SaveGame = GetNode<SaveGame>("/root/SaveGame");
		NameBox = GetNode<LineEdit>("NameBox");
		DeathsBox = GetNode<SpinBox>("DeathsBox");
		KillsBox = GetNode<SpinBox>("KillsBox");

		var p = SaveGame.Profile;

		NameBox.Text = p.Name;
		DeathsBox.Value = p.Deaths;
		KillsBox.Value = p.Kills;
	}

	private void _on_save_btn_pressed()
	{
		var p = SaveGame.Profile;

		p.Name = NameBox.Text;
		p.Deaths = (uint)DeathsBox.Value;
		p.Kills = (uint)KillsBox.Value;

		SaveGame.Save();
	}
	
	private void _on_back_btn_pressed()
	{
		GetTree().ChangeSceneToFile(MainMenu);
	}
}
