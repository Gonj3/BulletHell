using Godot;

public partial class Settings : Control, IOverlayItem
{
	[Export]
	private Label Up;
	[Export]
	private Label Down;
	[Export]
	private Label Right;
	[Export]
	private Label Left;
	[Export]
	private Label bomb;
	[Export]
	private Label dash;

	public override void _Ready()
	{
		string UpKey = InputMap.ActionGetEvents("ui_up")[0].AsText();
		Up.Text = "Move Up: " + UpKey;

		string DownKey = InputMap.ActionGetEvents("ui_down")[0].AsText();
		Down.Text = "Move Down: " + DownKey;

		string LeftKey = InputMap.ActionGetEvents("ui_left")[0].AsText();
		Left.Text = "Move Left: " + LeftKey;

		string RightKey = InputMap.ActionGetEvents("ui_right")[0].AsText();
		Right.Text = "Move Right: " + RightKey;

		string BombKey = InputMap.ActionGetEvents("bomb")[0].AsText();
		bomb.Text = "Bomb: " + BombKey;

		string DashKey = InputMap.ActionGetEvents("dash")[0].AsText();
		dash.Text = "dash: " + DashKey;
	}
	
	public Overlay Overlay { get; set; }

	// hide/show settings method
	private void ToggleSettingsPanel(Control panelToShow)
	{
		foreach (Control child in GetNode<Control>("settings").GetChildren())
		{
			child.Visible = child == panelToShow;
			this.GetAudioManager().PlayButton();
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
		Back();
		this.GetAudioManager().PlayButton();
	}

	private void Back()
	{
		if (Overlay != null)
		{
			Overlay.Back();
		}
		else
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
		AudioServer.SetBusVolumeDb(master_bus, value);

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
		AudioServer.SetBusVolumeDb(music_bus, value);

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
		AudioServer.SetBusVolumeDb(sfx_slider, value);

		if (value == -30)
		{
			AudioServer.SetBusVolumeDb(sfx_slider, -80);
		}
		else
		{
			AudioServer.SetBusVolumeDb(sfx_slider, value);
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("esc"))
		{
			AcceptEvent();
			Back();
			return;
		}

		if (remappingKey && @event is InputEventKey keyEvent && keyEvent.Pressed)
		{
			string currentUpKey = InputMap.ActionGetEvents("ui_up")[0].AsText();
			string currentDownKey = InputMap.ActionGetEvents("ui_down")[0].AsText();
			string currentLeftKey = InputMap.ActionGetEvents("ui_left")[0].AsText();
			string currentRightKey = InputMap.ActionGetEvents("ui_right")[0].AsText();
			string currentBombKey = InputMap.ActionGetEvents("bomb")[0].AsText();
			string currentDashKey = InputMap.ActionGetEvents("dash")[0].AsText();


			if (remappingLeftKey)
			{
				while (currentUpKey == keyEvent.AsText() || currentDownKey == keyEvent.AsText() || currentRightKey == keyEvent.AsText()
						|| currentBombKey == keyEvent.AsText() || currentDashKey == keyEvent.AsText())
				{
					Left.Text = "Move Left: can't assign same key";
					return;
				}
				InputMap.ActionEraseEvents("ui_left");
				InputMap.ActionAddEvent("ui_left", keyEvent);
				Left.Text = "Move Left: " + keyEvent.AsText();
			}
			else if (remappingRightKey)
			{
				while (currentUpKey == keyEvent.AsText() || currentDownKey == keyEvent.AsText() || currentLeftKey == keyEvent.AsText()
						|| currentBombKey == keyEvent.AsText() || currentDashKey == keyEvent.AsText())
				{
					Right.Text = "Move Right: can't assign same key";
					return;
				}
				InputMap.ActionEraseEvents("ui_right");
				InputMap.ActionAddEvent("ui_right", keyEvent);
				Right.Text = "Move Right: " + keyEvent.AsText();
			}
			else if (remappingUpKey)
			{
				while (currentDownKey == keyEvent.AsText() || currentLeftKey == keyEvent.AsText() || currentRightKey == keyEvent.AsText()
						|| currentBombKey == keyEvent.AsText() || currentDashKey == keyEvent.AsText())
				{
					Up.Text = "Move Up: can't assign same key";
					return;
				}
				InputMap.ActionEraseEvents("ui_up");
				InputMap.ActionAddEvent("ui_up", keyEvent);
				Up.Text = "Move Up: " + keyEvent.AsText();
			}
			else if (remappingDownKey)
			{
				while (currentUpKey == keyEvent.AsText() || currentLeftKey == keyEvent.AsText() || currentRightKey == keyEvent.AsText()
							|| currentBombKey == keyEvent.AsText() || currentDashKey == keyEvent.AsText())
				{
					Down.Text = "Move Down: can't assign same key";
					return;
				}
				InputMap.ActionEraseEvents("ui_down");
				InputMap.ActionAddEvent("ui_down", keyEvent);
				Down.Text = "Move Down: " + keyEvent.AsText();
			}
			else if (remappingBombKey)
			{
				while (currentUpKey == keyEvent.AsText() || currentLeftKey == keyEvent.AsText() || currentRightKey == keyEvent.AsText()
						   || currentDownKey == keyEvent.AsText())
				{
					bomb.Text = "Bomb: can't assign same key";
					return;
				}
				InputMap.ActionEraseEvents("bomb");
				InputMap.ActionAddEvent("bomb", keyEvent);
				bomb.Text = "Bomb: " + keyEvent.AsText();
			}
			else if (remappingDashKey)
			{
				while (currentUpKey == keyEvent.AsText() || currentLeftKey == keyEvent.AsText() || currentRightKey == keyEvent.AsText()
						   || currentDownKey == keyEvent.AsText())
				{
					dash.Text = "Dash: can't assign same key";
					return;
				}
				InputMap.ActionEraseEvents("dash");
				InputMap.ActionAddEvent("dash", keyEvent);
				dash.Text = "Dash: " + keyEvent.AsText();
			}
			remappingKey = false;
			remappingLeftKey = false;
			remappingRightKey = false;
			remappingUpKey = false;
			remappingDownKey = false;
			remappingBombKey = false;
			remappingDashKey = false;
		}
	}

	private bool remappingKey = false;
	private bool remappingLeftKey = false;
	private bool remappingRightKey = false;
	private bool remappingUpKey = false;
	private bool remappingDownKey = false;
	private bool remappingBombKey = false;
	private bool remappingDashKey = false;

	private void _on_left_button_pressed()
	{
		if (remappingKey)
		{
			return;
		}
		remappingKey = true;
		remappingLeftKey = true;
		Left.Text = "Move Left: Press New Key";
	}

	private void _on_right_button_pressed()
	{
		if (remappingKey)
		{
			return;
		}
		remappingKey = true;
		remappingRightKey = true;
		Right.Text = "Move Right: Press New Key";
	}

	private void _on_up_button_pressed()
	{
		if (remappingKey)
		{
			return;
		}
		remappingKey = true;
		remappingUpKey = true;
		Up.Text = "Move Up: Press New Key";
	}

	private void _on_down_button_pressed()
	{
		if (remappingKey)
		{
			return;
		}
		remappingKey = true;
		remappingDownKey = true;
		Down.Text = "Move Down: Press New Key";
	}

	private void _on_bomb_button_pressed()
	{
		if (remappingKey)
		{
			return;
		}
		remappingKey = true;
		remappingBombKey = true;
		bomb.Text = "Bomb: Press New Key";
	}
	private void _on_dash_button_pressed()
	{
		if (remappingKey)
		{
			return;
		}
		remappingKey = true;
		remappingDashKey = true;
		dash.Text = "Dash: Press New Key";
	}
}
