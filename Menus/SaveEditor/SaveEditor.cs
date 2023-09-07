using Godot;

public partial class SaveEditor : VBoxContainer
{
	[Export]
	private LineEdit NameBox;

	[Export]
	private SpinBox DeathsBox;

	[Export]
	private SpinBox KillsBox;

	public override void _Ready()
	{
		var p = this.GetSaveGame().Profile;

		NameBox.Text = p.Name;
		DeathsBox.Value = p.Deaths;
		KillsBox.Value = p.Kills;
	}

	private void _on_save_btn_pressed()
	{
		var saveGame = this.GetSaveGame();
		var p = saveGame.Profile;

		p.Name = NameBox.Text;
		p.Deaths = (uint)DeathsBox.Value;
		p.Kills = (uint)KillsBox.Value;

		saveGame.Save();
	}

	private void _on_back_btn_pressed()
	{
		this.GetRoot().SetScene(Scene.MainMenu);
	}
}
