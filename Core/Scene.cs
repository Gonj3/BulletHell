public class Scene
{
	private Scene(string path) { Path = path; }
	public string Path { get; private set; }

	public static readonly Scene WelcomeMenu = new("res://Menus/WelcomeMenu/WelcomeMenu.tscn");
	public static readonly Scene MainMenu = new("res://Menus/MainMenu/MainMenu.tscn");
	public static readonly Scene Game = new("res://Game/Game.tscn");
	public static readonly Scene Settings = new("res://Menus/Settings/Settings.tscn");
	public static readonly Scene ProfileMenu = new("res://Menus/ProfileMenu/ProfileMenu.tscn");
}
