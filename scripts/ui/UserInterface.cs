using Godot;
using System;
using System.Diagnostics.Tracing;

public partial class UserInterface : Control
{
	
	// Get exported references to windows so we can add event listeners from them
	[Export] private QuickMenuScreen _quickMenuScreen;
	[Export] private QuickSettingsScreen _quickSettingsScreen;
	[Export] private QuitConfirmScreen _quitConfirmScreen;
	
	// get audiplayer instance
	[Export] private AudioStreamPlayer _audioPlayer;
	
	// bool to track if game is paused
	public bool GamePaused = false;
	
	// Set up listeners to all events
	// TODO: fully implement settigns and quit
	public override void _Ready()
	{
		GD.Print("UI SCRIPT READY");
		// quick menu screen bindings
		_quickMenuScreen.Resume += ResumeGame;
		_quickMenuScreen.Settings += ShowSettingsWindow;
		_quickMenuScreen.Quit += ShowQuitConfirmWindow;
		// quick settings screen bindings
		_quickSettingsScreen.Return += ExitSettingsWindow;
		
		// quit confirm screen bindings
		_quitConfirmScreen.Return += ExitConfirmWindow;

	}
	


	// Implement event methods
	private void ResumeGame()
	{
		_audioPlayer.Play();
		GD.Print("RESUME THE GAME");
		GamePaused = false;
		_quickMenuScreen.Visible = false;
	}
	
	private void ShowSettingsWindow()
	{
		_audioPlayer.Play();
		GD.Print("SHOW SETTINGS WINDOW");
		_quickSettingsScreen.Show();
		_quickSettingsScreen.ResolutionButton.GrabFocus();
		_quickMenuScreen.Hide();
	}
	
	private void ShowQuitConfirmWindow()
	{
		_audioPlayer.Play();
		GD.Print("SHOW QUIT CONFIRM");
		_quitConfirmScreen.Show();
		_quickMenuScreen.Hide();
		_quitConfirmScreen.ReturnButton.GrabFocus();
	}

	private void ExitSettingsWindow()
	{
		_audioPlayer.Play();
		GD.Print("EXIT SETTINGS WINDOW");
		_quickMenuScreen.Show();
		_quickSettingsScreen.Hide();
		_quickMenuScreen.SettingsButton.GrabFocus();
	}
	
	private void ExitConfirmWindow()
	{
		_audioPlayer.Play();
		GD.Print("EXIT QUIT CONFIRM WINDOW");
		_quickMenuScreen.Show();
		_quickMenuScreen.QuitButton.GrabFocus();
		_quitConfirmScreen.Hide();
	}


	// Unsubscribe to events when UI is no longer in scene tree
	public override void _ExitTree()
	{
		_quickMenuScreen.Resume -= ResumeGame;
		_quickMenuScreen.Settings -= ShowSettingsWindow;
		_quickMenuScreen.Quit -= ShowQuitConfirmWindow;
		_quickSettingsScreen.Return -= ExitSettingsWindow;
		_quitConfirmScreen.Return -= ExitConfirmWindow;
	}
	
	

	
	// Handle opening QuickMenuScreen when user presses escape
	public override void _UnhandledInput(InputEvent @event)
	{
		// show pause menu when pause button is pressed
		if (@event is not InputEventKey) return;
		if (GamePaused)
		{
			return;
		}
		if (Input.IsActionJustPressed("pause"))
		{
			GD.Print("pause the game!");
			GamePaused = true;
			_quickMenuScreen.Visible = true;
			_quickMenuScreen.ResumeButton.GrabFocus();
		}
	}
}
