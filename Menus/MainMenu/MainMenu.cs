using Godot;

public partial class MainMenu : Control
{
	[Export]
	private Button DefaultButton;

	public override void _Ready()
	{
		DefaultButton.GrabFocus();
	}

	// Enter Game scene.
	public void _on_start_button_pressed()
	{
		this.GetRoot().SetScene(Scene.Game);
		this.GetAudioManager().MenuToGameMusic();
		this.GetAudioManager().PlayButton();
	}

	// Enter Settings scene.
	public void _on_settings_button_pressed()
	{
		this.GetRoot().SetScene(Scene.Settings);
		this.GetAudioManager().PlayButton();
	}

	public void _on_profile_button_pressed()
	{
		this.GetRoot().SetScene(Scene.ProfileMenu);
		this.GetAudioManager().PlayButton();
	}

	// Quit game.
	public void _on_quit_button_pressed()
	{
		GetTree().Quit();
	}
}
