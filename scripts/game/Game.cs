using Godot;
using System;
using System.Collections.Generic;

public partial class Game : Node
{
	// ref to playsound
	[Export] private AudioStreamPlayer _startGameSoundPlayer;
	[Export] private AudioStreamPlayer _enterTitleSoundPlayer;
	
	// refs to scenes to be loaded in and out
	[Export] private PackedScene _gameScenePacked;
	[Export] private PackedScene _mainMenuScenePacked;
	[Export] private PackedScene _gameOverScenePacked;
	
	private Node _mainMenuScene;
	private Node _gameScene;
	private Node _gameOverScene;
	
	public override void _Ready()
	{
		_enterTitleSoundPlayer.Play();
		GD.Print("Game _ready");
		_mainMenuScene = _mainMenuScenePacked.Instantiate();
		AddChild(_mainMenuScene);
		
		// Bind static events
		StaticEventManager.StartGame += StartGameMethod;
		StaticEventManager.ReturnToMainMenu += ReturnToMainMenu;
		StaticEventManager.GameOver += GameOver;
		StaticEventManager.StartNewGame += StartNewGame;
		
	}

	private void StartNewGame()
	{
		GD.Print("STARTING NEW GAME!");
		_gameScene.QueueFree();
		_gameScene = _gameScenePacked.Instantiate();
		AddChild(_gameScene);
		//_gameScene = _gameScenePacked.Instantiate();
		GD.Print($"IS _gameScene VALID? {IsInstanceIdValid(_gameScene.GetInstanceId())}");
	}

	private void GameOver()
	{
		GD.Print("show game over screen");
		_gameOverScene = _gameOverScenePacked.Instantiate();
		AddChild(_gameOverScene);
	}

	public override void _ExitTree()
	{
		StaticEventManager.StartGame -= StartGameMethod;
		StaticEventManager.ReturnToMainMenu -= ReturnToMainMenu;
		StaticEventManager.GameOver -= GameOver;
		StaticEventManager.StartNewGame -= StartNewGame;
	}

	
	// Implement event functions
	private void ReturnToMainMenu()
	{
		_enterTitleSoundPlayer.Play();
		GD.Print("return to main menu");
		_gameScene.QueueFree();
		_mainMenuScene = _mainMenuScenePacked.Instantiate();
		AddChild(_mainMenuScene);
	}
	

	private void StartGameMethod()
	{
		_startGameSoundPlayer.Play();
		GD.Print("start game");
		_mainMenuScene.QueueFree();
		_gameScene = _gameScenePacked.Instantiate();
		AddChild(_gameScene);
	}
}
