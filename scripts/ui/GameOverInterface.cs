using Godot;
using System;

public partial class GameOverInterface : Control
{
	
	// TODO: main things i've learned!
	//  We must decouple UI code, and manage game operations using static event manager
	
	[Export] private Button _newGameButton;
	[Export] private Button _quitButton;


	public override void _Ready()
	{
		_newGameButton.GrabFocus();
		// broadcast events, as defined in static method manager
		_newGameButton.Pressed += NewGameButtonOnPressed;
		_quitButton.Pressed += QuitButtonOnPressed;

	}

	private void NewGameButtonOnPressed()
	{
		StaticEventManager.BroadcastStartNewGame();
		QueueFree();
	}

	private void QuitButtonOnPressed()
	{
		StaticEventManager.BroadcastReturnToMainMenuEvent();
		QueueFree();
	}
}
