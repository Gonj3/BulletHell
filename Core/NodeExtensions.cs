using Godot;

public static class NodeExtensions
{
	public static Root GetRoot(this Node node)
	{
		return node.GetNode<Root>("/root/Root");
	}

	public static SaveGame GetSaveGame(this Node node)
	{
		return node.GetNode<SaveGame>("/root/SaveGame");
	}

	public static AudioManager GetAudioManager(this Node node)
	{
		return node.GetNode<AudioManager>("/root/AudioManager");
	}
}
