using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;


// TODO: fully implement settings screen

public partial class QuickSettingsScreen : MarginContainer
{
	// Create events for each button
	public Action<Vector2I> Resolution;
	public Action<bool> Vsync;
	public Action Apply;
	public Action Return;
	
	// create refs to buttons
	[Export] public Button ResolutionButton;
	[Export] public Button VsyncButton;
	[Export] public Button ApplyButton;
	[Export] public Button ReturnButton;
	[Export] public Label ResolutionLabel;
	
	// Create dictionary that houses window resolutions
	public Array<Vector2I> ResolutionsArray = [new (640, 360), new (854,480), new (1280, 720), new (1920, 1080), new (2560, 1440), new (3840, 2160),];
	public int ResolutionsIndex;
	public bool VsyncEnabled = true;
	
	// Bind click listeners to invoke each button's respecitve event, first two invoke
	// functions within the top-level "game" node, if attached
	public override void _Ready()
	{
		ResolutionsIndex = 3;
		
		ResolutionButton.Pressed += ResolutionButtonOnPressed;
		VsyncButton.Pressed += VsyncButtonOnPressed;
		ApplyButton.Pressed += () => Apply?.Invoke();
		ReturnButton.Pressed += () => Return?.Invoke();
	}

	private void ResolutionButtonOnPressed()
	{
		// Cycle through all indices, and update value of label with newly selected resolution
		ResolutionsIndex = ResolutionsIndex == ResolutionsArray.Count - 1 ? 0 : ResolutionsIndex + 1;
		Vector2 newRes = ResolutionsArray[ResolutionsIndex];
		ResolutionLabel.Text = newRes.X + "x" + newRes.Y;
		
		// Broadcast event with vec2 as parameter
		Resolution?.Invoke(ResolutionsArray[ResolutionsIndex]);
	}

	private void VsyncButtonOnPressed()
	{
		// change state of vsync enabled
		VsyncEnabled = !VsyncEnabled;
		VsyncButton.Text = VsyncEnabled ? "Vsync ON" : "Vsync OFF";
		
		Vsync?.Invoke(VsyncEnabled);
	}
}
