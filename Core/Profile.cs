using Godot;

public class Profile
{
	public string Name { get; set; }

	public uint Deaths { get; set; }

	public uint Kills { get; set; }

	public double TimeAlive { get; set; }

	public static Profile FromFile(FileAccess file)
	{
		return new Profile { Name = file.GetPascalString(), Deaths = file.Get32(), Kills = file.Get32(), TimeAlive = file.GetDouble() };
	}

	public void WriteToFile(FileAccess file)
	{
		file.StorePascalString(Name);
		file.Store32(Deaths);
		file.Store32(Kills);
		file.StoreDouble(TimeAlive);
	}
}
