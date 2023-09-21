using Godot;

public partial class Overlay : Control
{
	public void SetActive(Node node)
	{
		GetTree().Paused = true;

		FreeChildren();
		AddChild(node);
		Visible = true;
	}

	public void Clear()
	{
		GetTree().Paused = false;

		FreeChildren();
		Visible = false;
	}

	private void FreeChildren()
	{
		foreach (var child in GetChildren())
		{
			child.QueueFree();
		}
	}
}
