using System;
using Godot;

public partial class PauseScreen : Node
{
	[Signal]
	public delegate void ResumeEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("esc"))
		{
			CallDeferred("_on_resume_button_pressed");
		}
	}

	private void _on_resume_button_pressed()
	{
		EmitSignal(SignalName.Resume);
	}

	private void _on_quit_button_pressed()
	{
		this.GetRoot().SetScene(Scene.MainMenu);
	}
}
