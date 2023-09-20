using Godot;

public partial class MainMenu : Control
{
	[Export]
	private Button DefaultButton;

	private AudioStreamPlayer IntroPlayer;
	public override void _Ready()
	{
		IntroPlayer =  this.GetRoot().GetNode<AudioStreamPlayer>("IntroPlayer");
		if(!IntroPlayer.Playing) {
			IntroPlayer.Play();
		}
		DefaultButton.GrabFocus();
	}

	// Enter Game scene.
	public void _on_start_button_pressed()
	{
		IntroPlayer.Playing = false;
		this.GetRoot().SetScene(Scene.TestWorld);
	}

	// Enter Settings scene.
	public void _on_settings_button_pressed()
	{
		this.GetRoot().SetScene(Scene.Settings);
	}

	// Quit game.
	public void _on_quit_button_pressed()
	{
		GetTree().Quit();
	}

	// Enter Save Editor.
	public void _on_saveeditor_button_pressed()
	{
		this.GetRoot().SetScene(Scene.SaveEditor);
	}
}
