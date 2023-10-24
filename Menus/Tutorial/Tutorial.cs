using Godot;
using System;

public partial class Tutorial : Control, IOverlayItem
{
	public Overlay Overlay { get; set; }

	private void ToggleTutorial(RichTextLabel labellToShow)
	{
		foreach (RichTextLabel child in GetNode<VBoxContainer>("Tutorials").GetChildren())
		{
			child.Visible = child == labellToShow;
			this.GetAudioManager().PlayButton();
		}
	}

	private void _on_gameplay_pressed() 
	{
		ToggleTutorial(GetNode<RichTextLabel>("Gameplay"));
	}

	private void _on_player_pressed()
	{
		ToggleTutorial(GetNode<RichTextLabel>("Player"));
	}

	private void _on_power_ups_pressed() 
	{
		ToggleTutorial(GetNode<RichTextLabel>("PowerUps"));
	}

	private void _on_enemies_pressed()
	{
		ToggleTutorial(GetNode<RichTextLabel>("Enemies"));
	}

	private void _on_projectiles_pressed()
	{
		ToggleTutorial(GetNode<RichTextLabel>("Projectiles"));
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
