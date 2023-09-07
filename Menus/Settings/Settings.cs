using Godot;

public partial class Settings : Control
{
	// Change back to Main Menu scene.
	public void _on_back_pressed()
	{
		this.GetRoot().SetScene(Scene.MainMenu);
	}
}
