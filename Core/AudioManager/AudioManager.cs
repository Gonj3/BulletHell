using Godot;
using System;

public partial class AudioManager : Node
{
	[Export]
	public AudioStreamPlayer ButtonSFX;

	[Export]
	public AudioStreamPlayer MainMenu;

	[Export]
	public AudioStreamPlayer Game;

	public override void _Ready()
	{
		MainMenu.Play();
	}

	//stops/plays sound track method
	public void MenuToGameMusic()
	{
		MainMenu.Stop();
		Game.Play();
	}

	public void GameToMenuMusic()
	{
		MainMenu.Play();
		Game.Stop();
	}

	public void PlayButton()
	{
		ButtonSFX.Play();
	}

	public void PlaySound(string name)
	{
		GetNode<AudioStreamPlayer>(name).Play();
	}
}
