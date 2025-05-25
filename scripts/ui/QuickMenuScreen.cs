using Godot;
using System;

public partial class QuickMenuScreen : MarginContainer
{
	// Create events for every button
	public Action Resume;
	public Action Settings;
	public Action Quit;
	
	
	// Define exported references to each button
	[Export] public Button ResumeButton;
	[Export] public Button SettingsButton;
	[Export] public Button QuitButton;

	public override void _Ready()
	{
		// subscribe each button to their respective events
		ResumeButton.Pressed += () => Resume?.Invoke();
		SettingsButton.Pressed += () => Settings?.Invoke();
		QuitButton.Pressed += () => Quit?.Invoke();
	}
}
