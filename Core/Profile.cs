using Godot;

public class Profile
{
	public string Name { get; set; }

	public uint Kills { get; set; }

	public uint Deaths { get; set; }

	public uint ItemsCollected { get; set; }

	public double TimeAlive { get; set; }

	public double LongestRun { get; set; }

	public uint HighestWave { get; set; }


	public static Profile FromFile(FileAccess file)
	{
		return new Profile
		{
			Name = file.GetPascalString(),
			Kills = file.Get32(),
			Deaths = file.Get32(),
			ItemsCollected = file.Get32(),
			TimeAlive = file.GetDouble(),
			LongestRun = file.GetDouble(),
			HighestWave = file.Get32()
		};
	}

	public void WriteToFile(FileAccess file)
	{
		file.StorePascalString(Name);
		file.Store32(Kills);
		file.Store32(Deaths);
		file.Store32(ItemsCollected);
		file.StoreDouble(TimeAlive);
		file.StoreDouble(LongestRun);
		file.Store32(HighestWave);
	}
}
