using Godot;
using System;

public partial class Audio : Node
{
	public AudioStreamPlayer ButtonSFX;
	public AudioStreamPlayer MainMenu;
	public AudioStreamPlayer Game;
	
	public override void _Ready()
	{
		//sound effect test
		ButtonSFX = GetNode<AudioStreamPlayer>("ButtonSFX");
		MainMenu = GetNode<AudioStreamPlayer>("MainMenu");
		Game = GetNode<AudioStreamPlayer>("Game");
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
}
