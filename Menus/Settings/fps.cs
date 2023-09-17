using Godot;
using System;

public partial class Fps : Label
{
	public override void _Process(double delta)
	{
		// shows fps
		Text = "FPS: " + Engine.GetFramesPerSecond();
	}
}