using Godot;
using System;

public partial class WelcomeMenu : Control
{
	[Export]
	private LineEdit NameEdit;

	private void _on_button_pressed()
	{
		var name = NameEdit.Text.Trim();

		if (name.Length == 0)
		{
			return;
		}

		this.GetSaveGame().Profile.Name = name;
		this.GetSaveGame().Save();
		this.GetRoot().SetScene(Scene.MainMenu);
	}
}
