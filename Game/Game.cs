using System;
using System.Linq;
using Godot;

public partial class Game : Node
{
	[Export]
	private Overlay overlay;

	[Export]
	private Label waveLabel;

	[Export]
	private Timer waveTimer;

	[Export]
	private World world;

	private double timer;
	private uint kills;
	private uint waveCount;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		timer += delta;
		if (!world.GetChildren().Any(c => c is Enemy))
			StartWave();
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("esc"))
		{
			var pauseScreen = ResourceLoader.Load<PackedScene>("res://Game/PauseScreen.tscn");
			var inst = (PauseScreen)pauseScreen.Instantiate();
			inst.Overlay = overlay;
			overlay.AddItem(inst);
		}
	}

	private void StartWave()
	{
		waveCount++;
		waveLabel.Text = $"Wave {waveCount}";

		for (var i = 0; i < waveCount * 2; i++)
			world.SpawnEnemy(new Callable(this, "_on_player_kill"));

		waveTimer.Start();
	}

	public void _on_player_death()
	{
		UpdateSaveGame();
		ShowDeathScreen();
	}

	private void UpdateSaveGame()
	{
		var saveGame = this.GetSaveGame();

		saveGame.Profile.Kills += kills;
		saveGame.Profile.Deaths += 1;
		saveGame.Profile.TimeAlive += timer;

		saveGame.Profile.LongestRun = Math.Max(timer, saveGame.Profile.LongestRun);
		saveGame.Profile.HighestWave = Math.Max(waveCount, saveGame.Profile.HighestWave);
		saveGame.Save();
	}

	private void ShowDeathScreen()
	{
		var deathScreen = ResourceLoader.Load<PackedScene>("res://Game/DeathScreen.tscn");
		var inst = (DeathScreen)deathScreen.Instantiate();
		inst.TimeAlive = timer;
		inst.Kills = kills;
		overlay.AddItem(inst);
	}

	public void _on_player_kill()
	{
		kills++;
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
