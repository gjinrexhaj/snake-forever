using Godot;
using System;

public partial class QuitConfirmScreen : MarginContainer
{
	
	// Create events for each button
	public Action Return;
	public Action Quit;
	
	// create vars to refernce each button
	[Export] public Button ReturnButton;
	[Export] public Button QuitButton;
	
	// Attach onclikc listener to broadcast events
	public override void _Ready()
	{
		ReturnButton.Pressed += () => Return?.Invoke();
		QuitButton.Pressed += () => Quit?.Invoke();
	}
}
