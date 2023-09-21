using Godot;
using System;

public partial class Settings : Node2D
{
	private Audio soundInstance;

    public override void _Ready()
    {   
        soundInstance = GetNode<Audio>("/root/Audio");
    }

	// hide/show settings method
	private void ToggleSettingsPanel(Control panelToShow)
	{
		foreach (Control child in GetNode<Control>("settings").GetChildren())
		{
			child.Visible = (child == panelToShow);
			soundInstance.PlayButton();
		}
	}
	private void _on_button_pressed(Control panelToShow)
	{
		ToggleSettingsPanel(panelToShow);
	}

	// video button function
	private void _on_video_button_pressed()
	{
		_on_button_pressed(GetNode<Control>("settings/video_settings"));
	}

	// audio button function
	private void _on_audio_button_pressed()
	{
		_on_button_pressed(GetNode<Control>("settings/audio_settings"));
	}

	// gameplay button function
	private void _on_gameplay_button_pressed()
	{
		_on_button_pressed(GetNode<Control>("settings/gameplay_settings"));
	}

	// controls buttoon funciton
	private void _on_controls_button_pressed()
	{
		_on_button_pressed(GetNode<Control>("settings/controls_settings"));
	}

	//main menu/back button functtion
	private void _on_main_menu_button_pressed()
	{
		this.GetRoot().SetScene(Scene.MainMenu);
		soundInstance.PlayButton();
	}

	//esc key function
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("esc"))
		{
			this.GetRoot().SetScene(Scene.MainMenu);
		}
	}

	// display option function
 	private void _on_display_option_item_selected(int index)
	{
		string selectedOption = ((OptionButton)GetNode("settings/video_settings/display_option")).GetItemText(index);

		if (selectedOption == "Full Screen")
		{
			 DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		}
		else if (selectedOption == "Windowed")
		{
			 DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
		}
	}

	// resolution function
    private void _on_resolution_option_item_selected(int index)
	{
		string selectedOption = ((OptionButton)GetNode("settings/video_settings/resolution_option")).GetItemText(index);

		if (selectedOption == "1920x1080")
		{
			DisplayServer.WindowSetSize(new Vector2I(1920, 1080));	
		}
		else if (selectedOption == "1366x768")
		{
			DisplayServer.WindowSetSize(new Vector2I(1366, 768));	
		}
       	else if (selectedOption == "1280x720")
		{
			DisplayServer.WindowSetSize(new Vector2I(1280, 720));	
		}
        else if (selectedOption == "640x480")
		{	
			DisplayServer.WindowSetSize(new Vector2I(640, 480));
		}
	}

	// quality option function
	private void _on_quality_option_item_selected(int index)
	{
		string selectedOption = ((OptionButton)GetNode("settings/video_settings/quality_option")).GetItemText(index);

		if (selectedOption == "High")
		{
	
		}
		else if (selectedOption == "Medium")
		{
		
		}
        else if (selectedOption == "Low")
		{
	
		}
	}

	// frame rate option function
	private void _on_frame_rate_option_item_selected(int index)
	{
		string selectedOption = ((OptionButton)GetNode("settings/video_settings/frame_rate_option")).GetItemText(index);

		if (selectedOption == "Unlimited")
		{
			Engine.MaxFps = -0;	
		}
		else if (selectedOption == "144")
		{
			Engine.MaxFps = 144;	  
		}
        else if (selectedOption == "120")
		{
			Engine.MaxFps = 120;
		}
        else if (selectedOption == "60")
		{
			Engine.MaxFps = 60;
		}
		 else if (selectedOption == "30")
		{
			Engine.MaxFps = 30;
		}
	}

	// vsync check box function
	private void _on_vsync_check_pressed()
	{
		DisplayServer.VSyncMode currentMode = DisplayServer.WindowGetVsyncMode();

		if (currentMode == DisplayServer.VSyncMode.Disabled)
		{
			DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Enabled);
		}
		else
		{
			DisplayServer.WindowSetVsyncMode(DisplayServer.VSyncMode.Disabled);
		}
	}

	// diffculty option function
	private void _on_diffculty_option_item_selected(int index)
	{
		string selectedOption = ((OptionButton)GetNode("settings/gameplay_settings/diffculty_option")).GetItemText(index);

		if (selectedOption == "Hard")
		{

		}
		else if (selectedOption == "Medium")
		{

		}
        else if (selectedOption == "Easy")
		{

		}
	}

	// hide hud check box function
	private void _on_hide_hud_check_pressed()
	{	
		
	}

	// fps display check box function
	private void _on_fps_display_check_pressed()
	{
		// show/hide fps label
		var fps_display = GetNode<Control>("Control/CanvasLayer/fps");
		fps_display.Visible = !fps_display.Visible;
	}

	//master volume slider function
	private void _on_master_slider_value_changed(float value)
	{
	var master_bus = AudioServer.GetBusIndex("Master");
	AudioServer.SetBusVolumeDb(master_bus,value);

	 if (value == -30)
    	{
        AudioServer.SetBusVolumeDb(master_bus, -80);
   		}
    else
   		{
        AudioServer.SetBusVolumeDb(master_bus, value);
    	}
	}

	//music volume slider function
	private void _on_music_slider_value_changed(float value)
	{
	var music_bus = AudioServer.GetBusIndex("Music");
	AudioServer.SetBusVolumeDb(music_bus,value);

	 if (value == -30)
    	{
        AudioServer.SetBusVolumeDb(music_bus, -80);
   		}
    else
    	{
        AudioServer.SetBusVolumeDb(music_bus, value);
    	}
	}

	//sfx volume slider function
	private void _on_sfx_slider_value_changed(float value)
	{

	var sfx_slider = AudioServer.GetBusIndex("SFX");
	AudioServer.SetBusVolumeDb(sfx_slider,value);

	 if (value == -30)
    	{
        AudioServer.SetBusVolumeDb(sfx_slider, -80);
    	}
    	else
    	{
        AudioServer.SetBusVolumeDb(sfx_slider, value);
   		}
	}
}