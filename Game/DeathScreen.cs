using Godot;

public partial class DeathScreen : Control, IOverlayItem
{
	public double TimeAlive { get; set; }
	public uint Kills { get; set; }
	public Overlay Overlay { get; set; }

	[Export]
	private Label TimeAliveLabel;

	[Export]
	private Label KillsLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TimeAliveLabel.Text = Util.FormatSeconds(TimeAlive);
		KillsLabel.Text = Kills.ToString();
	}

	public void _on_quit_button_pressed()
	{
		GetTree().Quit();
	}

	public void _on_continue_button_pressed()
	{
		this.GetRoot().SetScene(Scene.MainMenu);
		this.GetAudioManager().GameToMenuMusic();
	}
}
