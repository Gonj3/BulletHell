using System;
using Godot;

public partial class Game : Node
{
	[Export]
	private Overlay Overlay;

	private double Timer;
	private int Kills;

	public override void _Process(double delta)
	{
		Timer += delta;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("esc"))
		{
			var pauseScreen = ResourceLoader.Load<PackedScene>("res://Game/PauseScreen.tscn");
			var inst = (PauseScreen)pauseScreen.Instantiate();
			inst.Overlay = Overlay;
			Overlay.AddItem(inst);
		}
	}

	public void _on_player_death()
	{
		var saveGame = this.GetSaveGame();
		saveGame.Profile.Deaths += 1;
		saveGame.Profile.TimeAlive += Timer;
		saveGame.Save();

		var deathScreen = ResourceLoader.Load<PackedScene>("res://Game/DeathScreen.tscn");
		var inst = (DeathScreen)deathScreen.Instantiate();
		inst.TimeAlive = Timer;
		inst.Kills = Kills;
		Overlay.AddItem(inst);
	}

	private void _on_overlay_overlay_shown()
	{
		GetTree().Paused = true;
	}

	private void _on_overlay_overlay_hidden()
	{
		GetTree().Paused = false;
	}
}
