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
		var newScene = ResourceLoader.Load<PackedScene>(scene.Path);

		CallDeferred(nameof(SetSceneDeferred), newScene);
	}

	private void SetSceneDeferred(PackedScene packedScene)
	{
		foreach (var child in SceneContainer.GetChildren())
		{
			child.QueueFree();
		}

		SceneContainer.AddChild(packedScene.Instantiate());

		GetTree().Paused = false;
	}
}
