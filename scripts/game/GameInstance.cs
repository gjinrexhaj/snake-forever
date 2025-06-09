using Godot;
using System;
using System.Collections.Generic;


public partial class GameInstance : Node
{
	[Export] private Head _snakeHead;
	[Export] private Bounds _boundaries;
	[Export] private Spawner _spawner;
	[Export] private Label _scoreLabel;

	private double _timeBetweenMoves = 1000.0;
	private double _timeSinceLastMove = 0.0;
	private double _speed = 5000.0;
	
	private bool _gamePaused;
	
	private int _score = 0;
	
	public Vector2 MoveDirection = Vector2.Right;

	private Godot.Collections.Array<SnakePart> _snakeParts = new();
	
	// handle snake movement
	public override void _Ready()
	{
		StaticEventManager.FoodCollide += FoodCollide;
		StaticEventManager.TailAdded += TailAdded;
		StaticEventManager.PauseGame += () => _gamePaused = true;
		StaticEventManager.ResumeGame += () => _gamePaused = false;
		StaticEventManager.GameOver += () => _gamePaused = true;
		_spawner.SpawnFood();
		_snakeParts.Add(_snakeHead);
		_gamePaused = false;
		GD.Print("game instance initialized");
		GD.Print($"IS _spawner VALID? {IsInstanceIdValid(_spawner.GetInstanceId())}");
		

	}

	private void TailAdded(Tail tail)
	{
		GD.Print("TAIL ADDED");
		_snakeParts.Add(tail);
	}

	private void FoodCollide()
	{
		GD.Print("FOOD EATEN");
		// spawn new food
		_spawner.CallDeferred("SpawnFood");
		// add tail
		_spawner.CallDeferred("SpawnTail", _snakeParts[_snakeParts.Count - 1].LastPosition);
		GD.Print($"spawntail, attempting to add to _snakeParts[{_snakeParts.Count - 1}]");
		
		// increase speed
		_speed += 100;
		// update score
		_score++;
		_scoreLabel.Text = $"SCORE: {_score}";
		

	}

	public void UpdateSnake()
	{
		// move snake by an amount
		//_snakeHead.Position = _snakeHead.Position + MoveDirection * Global.GridSize;
		Vector2 newPosition = _snakeHead.Position + MoveDirection * Global.GridSize;
		newPosition = _boundaries.WrapVector(newPosition);
		_snakeHead.MoveTo(newPosition);

		for (int i = 1; i < _snakeParts.Count; i++)
		{
			_snakeParts[i].MoveTo(_snakeParts[i - 1].LastPosition);
		}
	}

	public override void _ExitTree()
	{
		StaticEventManager.FoodCollide -= FoodCollide;
		StaticEventManager.TailAdded -= TailAdded;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_gamePaused)
		{
			return;
		}
		_timeSinceLastMove += delta * _speed;
		if (_timeSinceLastMove >= _timeBetweenMoves)
		{
			UpdateSnake();
			_timeSinceLastMove = 0.0;
		}
	}

	public override void _Process(double delta)
	{
		if (_gamePaused)
		{
			return;
		}
		if (Input.IsActionPressed("ui_right"))
		{
			MoveDirection = Vector2.Right;	
		}

		if (Input.IsActionPressed("ui_left"))
		{
			MoveDirection = Vector2.Left;	
		}

		if (Input.IsActionPressed("ui_up"))
		{
			MoveDirection = Vector2.Up;
		}

		if (Input.IsActionPressed("ui_down"))
		{
			MoveDirection = Vector2.Down;
		}
	}
}
