using Godot;
using System;

public partial class SaveGame : Node
{
	private const string SAVE_PATH = "user://SAVEGAME";

	public Profile Profile { get; private set; }

	public override void _Ready()
	{
		Console.WriteLine("doignready");

		using var file = FileAccess.Open(SAVE_PATH, FileAccess.ModeFlags.Read);

		this.Profile = file != null ? Profile.FromFile(file) : new Profile();

		this.Save();
	}

	public void Save()
	{
		using var file = FileAccess.Open(SAVE_PATH, FileAccess.ModeFlags.Write);

		if (file == null)
			return;

		this.Profile.WriteToFile(file);
	}
}
