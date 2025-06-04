using Godot;
using System;

public partial class Head : SnakePart
{
	
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
	}

	private void OnAreaEntered(Area2D area)
	{
		if (area.IsInGroup("Food"))
		{
			// collided with food
			GD.Print("food collide!");
			StaticEventManager.BroadcastFoodCollideEvent();
			area.QueueFree();
		}
		else
		{
			// did not collide with food
			GD.Print("food not collide!");
			StaticEventManager.BroadcastGameOverEvent();
		}
	}
}
