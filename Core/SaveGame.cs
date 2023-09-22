using Godot;

public partial class SaveGame : Node
{
	private const string SAVE_PATH = "user://SAVEGAME";

	public Profile Profile { get; private set; }

	public override void _Ready()
	{
		using var file = FileAccess.Open(SAVE_PATH, FileAccess.ModeFlags.Read);
		Profile = file != null ? Profile.FromFile(file) : new Profile();
	}

	public void Save()
	{
		using var file = FileAccess.Open(SAVE_PATH, FileAccess.ModeFlags.Write);

		if (file == null)
			return;

		Profile.WriteToFile(file);
	}
}
