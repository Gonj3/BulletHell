public class Scene
{
	private Scene(string path) { Path = path; }
	public string Path { get; private set; }

	public static readonly Scene MainMenu = new("res://Menus/MainMenu/MainMenu.tscn");
	public static readonly Scene TestWorld = new("res://Game/TestWorld.tscn");
	public static readonly Scene Settings = new("res://Menus/Settings/Settings.tscn");
	public static readonly Scene SaveEditor = new("res://Menus/SaveEditor/SaveEditor.tscn");
}
