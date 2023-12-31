using Godot;

public partial class Root : Node
{
	[Export]
	private Node SceneContainer;

	public override void _Ready()
	{
		var name = this.GetSaveGame().Profile.Name;
		if (name == null || name == "")
		{
			SetScene(Scene.WelcomeMenu);
		}
		else
		{
			SetScene(Scene.MainMenu);
		}
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
