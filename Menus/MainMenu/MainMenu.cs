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
		this.GetRoot().SetScene(Scene.TestWorld);
		soundInstance.MenuToGameMusic();
		soundInstance.PlayButton();
	}

	// Enter Settings scene.
	public void _on_settings_button_pressed()
	{
		this.GetRoot().SetScene(Scene.Settings);
		soundInstance.PlayButton();
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
