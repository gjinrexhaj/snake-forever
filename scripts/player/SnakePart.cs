using Godot;
using System;

public partial class SnakePart : Area2D
{
	
	public Vector2 LastPosition;

	public void MoveTo(Vector2 newPosition)
	{
		LastPosition = Position;
		Position = newPosition;
	}
}
