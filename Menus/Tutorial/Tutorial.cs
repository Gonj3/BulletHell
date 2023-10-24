using Godot;
using System;

public partial class Tutorial : Control, IOverlayItem
{
	[Export]
	private VBoxContainer tutorials;

	[Export]
	private RichTextLabel gameplay;

	[Export]
	private RichTextLabel player;

	[Export]
	private RichTextLabel enemies;

	[Export]
	private RichTextLabel powerups;

	[Export]
	private RichTextLabel projectiles;

	public Overlay Overlay { get; set; }

	private void ToggleTutorial(RichTextLabel labellToShow)
	{
		foreach (RichTextLabel child in tutorials.GetChildren())
		{
			child.Visible = child == labellToShow;
			this.GetAudioManager().PlayButton();
		}
	}

	private void _on_gameplay_pressed()
	{
		ToggleTutorial(gameplay);
	}

	private void _on_player_pressed()
	{
		ToggleTutorial(player);
	}

	private void _on_power_ups_pressed()
	{
		ToggleTutorial(powerups);
	}

	private void _on_enemies_pressed()
	{
		ToggleTutorial(enemies);
	}

	private void _on_projectiles_pressed()
	{
		ToggleTutorial(projectiles);
	}

	private void _on_exit_pressed()
	{
		Back();
		this.GetAudioManager().PlayButton();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("esc"))
		{
			AcceptEvent();
			Back();
		}
	}

	private void Back()
	{
		if (Overlay != null)
		{
			Overlay.Back();
		}
		else
		{
			this.GetRoot().SetScene(Scene.MainMenu);
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
