using Godot;

public partial class Root : Node
{
	private static readonly Scene InitialScene = Scene.MainMenu;

	[Export]
	private Node SceneContainer;

	public override void _Ready()
	{
		SetScene(InitialScene);
	}

	public void SetScene(Scene scene)
	{
		foreach (var child in SceneContainer.GetChildren())
		{
			child.QueueFree();
		}

		var newScene = ResourceLoader.Load<PackedScene>(scene.Path).Instantiate();

		SceneContainer.AddChild(newScene);
	}
}
