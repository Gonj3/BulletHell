using Godot;
using System;

public partial class Audio : Node
{
	public AudioStreamPlayer sound_effect;
	public AudioStreamPlayer main_menu_music;
	public AudioStreamPlayer game_music;


	public override void _Ready()
	{
		//sound effect test
		sound_effect = GetNode<AudioStreamPlayer>("sound_effect");
		sound_effect.Play();

		main_menu_music = GetNode<AudioStreamPlayer>("main_menu_music");
		main_menu_music.Play();

		game_music = GetNode<AudioStreamPlayer>("game_music");
	}

	//stops/plays music methods
	public void MenuToGameMusic()
	{
		main_menu_music.Stop();
		sound_effect.Stop();
		game_music.Play();
	}

	public void GameToMenuMusic()
	{
		main_menu_music.Play();
		sound_effect.Play();
		game_music.Stop();
	}
}
