using Godot;
using System;
using System.Collections.Generic;


// TODO: fully implement settings screen

public partial class QuickSettingsScreen : MarginContainer
{
	// Create events for each button
	public Action Resolution;
	public Action Vsync;
	public Action Apply;
	public Action Return;
	
	
	// create refs to buttons
	[Export] public Button ResolutionButton;
	[Export] public Button VsyncButton;
	[Export] public Button ApplyButton;
	[Export] public Button ReturnButton;
	
	// create vars to track current res and vsync toggle. Ideally, we'd do this in a separate top-level "game" class
	public List<string> Resolutions = ["1920x1080", "1280x720"];
	public int ResolutionIndex = 0;
	
	// Bind click listeners to invoke each button's respecitve event
	public override void _Ready()
	{
		ResolutionButton.Pressed += () => Resolution?.Invoke();
		VsyncButton.Pressed += () => Vsync?.Invoke();
		ApplyButton.Pressed += () => Apply?.Invoke();
		ReturnButton.Pressed += () => Return?.Invoke();
	}
}
