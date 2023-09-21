using System;
using Godot;

public partial class Game : Node
{
	[Export]
	private Overlay Overlay;

	private double Timer;
	private int Kills;
	private double DamageTaken;

	public override void _Process(double delta)
	{
		Timer += delta;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("esc"))
		{
			var pauseScreen = ResourceLoader.Load<PackedScene>("res://Game/PauseScreen.tscn");
			var inst = (PauseScreen)pauseScreen.Instantiate();
			inst.Resume += Overlay.Clear;
			Overlay.SetActive(inst);
		}
	}

	public void _on_player_death()
	{
		var deathScreen = ResourceLoader.Load<PackedScene>("res://Game/DeathScreen.tscn");
		var inst = (DeathScreen)deathScreen.Instantiate();
		inst.TimeAlive = Timer;
		inst.Kills = Kills;
		Overlay.SetActive(inst);
	}
}
