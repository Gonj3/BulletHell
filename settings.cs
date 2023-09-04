using Godot;
using System;

public partial class settings : Node2D
{

	// hide/show settings method
	private void ToggleSettingsPanel(Control panelToShow)
	{
		foreach (Control child in GetNode<Control>("settings").GetChildren())
		{
			child.Visible = (child == panelToShow);
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
	   // GetTree().ChangeSceneToFile("res://main menu.tscn");
	   GetTree().Quit();
	}

	//esc key function
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("esc"))
		{
			// GetTree().ChangeSceneToFile("res://main menu.tscn");
	   GetTree().Quit();
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
			  
		}
		else if (selectedOption == "1366x768")
		{
			
		}
        else if (selectedOption == "1280x720")
		{
			
		}
        else if (selectedOption == "640x480")
		{
   
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
			
		}
		else if (selectedOption == "144")
		{
			  
		}
        else if (selectedOption == "120")
		{
			
		}
        else if (selectedOption == "60")
		{
		
		}
		 else if (selectedOption == "30")
		{
		
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
}