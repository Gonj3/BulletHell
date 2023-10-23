using Godot;

public partial class ProfileMenu : Control
{
	[Export]
	private Label NameLabel;

	[Export]
	private Label KillsLabel;

	[Export]
	private Label DeathsLabel;

	[Export]
	private Label TimeAliveLabel;

	[Export]
	private Label ItemsLabel;

	public override void _Ready()
	{
		var p = this.GetSaveGame().Profile;

		NameLabel.Text = p.Name;
		KillsLabel.Text = p.Kills.ToString();
		DeathsLabel.Text = p.Deaths.ToString();
		TimeAliveLabel.Text = Util.FormatSeconds(p.TimeAlive);
		ItemsLabel.Text = p.ItemsCollected.ToString();
	}
	
	private void _on_button_pressed()
	{
		this.GetRoot().SetScene(Scene.MainMenu);
		this.GetAudioManager().PlayButton();
	}
}
