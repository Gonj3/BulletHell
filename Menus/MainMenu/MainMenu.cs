using Godot;

public partial class MainMenu : Control
{
	[Export]
	private Button DefaultButton;
	private Audio soundInstance;

	public override void _Ready()
	{
		DefaultButton.GrabFocus();
		soundInstance = GetNode<Audio>("/root/Audio");
	}

	// Enter Game scene.
	public void _on_start_button_pressed()
	{
		this.GetRoot().SetScene(Scene.Game);
		soundInstance.MenuToGameMusic();
		soundInstance.PlayButton();
	}

	// Enter Settings scene.
	public void _on_settings_button_pressed()
	{
		this.GetRoot().SetScene(Scene.Settings);
		soundInstance.PlayButton();
	}

	public void _on_profile_button_pressed()
	{
		this.GetRoot().SetScene(Scene.ProfileMenu);
	}

	// Quit game.
	public void _on_quit_button_pressed()
	{
		GetTree().Quit();
	}
}
