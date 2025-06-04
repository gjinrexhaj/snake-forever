using Godot;
using System;
using Godot.Collections;

public partial class MainMenuInterface : Control
{
	// Track settings vars
	public Array<Vector2I> ResolutionsArray = [new (640, 360), new (854,480), new (1280, 720), new (1920, 1080), new (2560, 1440), new (3840, 2160),];
	public Vector2I Resolution = new (1920, 1080);
	public int ResolutionsIndex = 3;
	public bool VsyncEnabled = true;
	
	// Get references to all buttons
	[Export] public Button StartButton;
	[Export] public Button SettingsButton;
	[Export] public Button AboutButton;
	[Export] public Button QuitButton;
	
	[Export] public Button ResolutionButton;
	[Export] public Button VsyncButton;
	[Export] public Button ApplyChangesButton;
	[Export] public Button SettingsReturnButton;
	[Export] public Button AboutReturnButton;
	[Export] public Label ResolutionLabel;
	
	// Get references to UI screens in main menu
	[Export] public VBoxContainer MenuWindow;
	[Export] public VBoxContainer SettingsWindow;
	[Export] public VBoxContainer AboutWindow;
	
	// Get ref to audio players
	[Export] private AudioStreamPlayer _clickPlayer;
	[Export] private AudioStreamPlayer _navPlayer;
	[Export] private AudioStreamPlayer _applyPlayer;
	
	public override void _Ready()
	{
		// get focus
		StartButton.GrabFocus();
		
		// implement buttons
		StartButton.Pressed += StartButtonOnPressed;
		SettingsButton.Pressed += SettingsButtonOnPressed;
		AboutButton.Pressed += AboutButtonOnPressed;
		
		ResolutionButton.Pressed += ResolutionButtonOnPressed;
		VsyncButton.Pressed += VsyncButtonOnPressed;
		ApplyChangesButton.Pressed += ApplyChangesButtonOnPressed;
		SettingsReturnButton.Pressed += SettingsReturnButtonOnPressed;
		AboutReturnButton.Pressed += AboutReturnButtonOnPressed;
		
		
		QuitButton.Pressed += () => {GetTree().Quit();};
	}

	private void AboutReturnButtonOnPressed()
	{
		_clickPlayer.Play();
		AboutWindow.Hide();
		MenuWindow.Show();
		AboutButton.GrabFocus();
	}

	private void ApplyChangesButtonOnPressed()
	{
		_applyPlayer.Play();
		GD.Print("applying changes with, res: ", Resolution, " and vsync: ", VsyncEnabled);
		DisplayServer.WindowSetSize(Resolution);
		DisplayServer.WindowSetVsyncMode(VsyncEnabled ? DisplayServer.VSyncMode.Enabled : DisplayServer.VSyncMode.Disabled);
		
	}

	private void SettingsReturnButtonOnPressed()
	{
		_clickPlayer.Play();
		SettingsWindow.Hide();
		MenuWindow.Show();
		SettingsButton.GrabFocus();
	}

	private void VsyncButtonOnPressed()
	{
		_clickPlayer.Play();
		VsyncEnabled = !VsyncEnabled;
		GD.Print("vsync: ", VsyncEnabled);
		VsyncButton.Text = VsyncEnabled ? "Vsync ON" : "Vsync OFF";
	}

	private void ResolutionButtonOnPressed()
	{
		_clickPlayer.Play();
		ResolutionsIndex = ResolutionsIndex == ResolutionsArray.Count - 1 ? 0 : ResolutionsIndex + 1;
		
		Vector2 newRes = ResolutionsArray[ResolutionsIndex];
		ResolutionLabel.Text = newRes.X + "x" + newRes.Y;
		
		Resolution = ResolutionsArray[ResolutionsIndex];
		
	}

	private void StartButtonOnPressed()
	{
		StaticEventManager.BroadcastStartGameEvent();
	}
	
	private void SettingsButtonOnPressed()
	{
		_clickPlayer.Play();
		MenuWindow.Hide();
		SettingsWindow.Show();
		ResolutionButton.GrabFocus();
		Vector2 currentRes = DisplayServer.WindowGetSize();
		ResolutionLabel.Text = currentRes.X + "x" + currentRes.Y;
	}
	
	private void AboutButtonOnPressed()
	{
		MenuWindow.Hide();
		AboutWindow.Show();
		_clickPlayer.Play();
		AboutReturnButton.GrabFocus();
	}
}
