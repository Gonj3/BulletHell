using System;
using Godot;

public partial class PauseScreen : Control, IOverlayItem
{
	public Overlay Overlay { get; set; }

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("esc"))
		{
			AcceptEvent();
			Overlay.Back();
		}
	}

	private void _on_resume_button_pressed()
	{
		Overlay.Back();
		this.GetAudioManager().PlayButton();
	}

	private void _on_settings_button_pressed()
	{
		var settingsMenu = ResourceLoader.Load<PackedScene>("res://Menus/Settings/Settings.tscn");
		var inst = (Settings)settingsMenu.Instantiate();
		Overlay.AddItem(inst);
		this.GetAudioManager().PlayButton();
	}

	private void _on_tutorial_button_pressed()
	{
		var tutorialMenu = ResourceLoader.Load<PackedScene>("res://Menus/Tutorial/Tutorial.tscn");
		var inst = (Tutorial)tutorialMenu.Instantiate();
		Overlay.AddItem(inst);
		this.GetAudioManager().PlayButton();
	}

	private void _on_quit_button_pressed()
	{
		this.GetRoot().SetScene(Scene.MainMenu);
		this.GetAudioManager().GameToMenuMusic();
		this.GetAudioManager().PlayButton();
	}
}
