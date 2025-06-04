using Godot;
using System;

public partial class Spawner : Node2D
{
	[Export] public Bounds Bounds;
	public PackedScene FoodScene = GD.Load<PackedScene>("res://scenes/Food.tscn");
	public PackedScene TailScene = GD.Load<PackedScene>("res://scenes/Tail.tscn");

	public override void _Ready()
	{
		GD.Print("spawner Ready");
	}

	public void SpawnFood()
	{
		// create spawnpoint for food
		Vector2 SpawnPoint = new Vector2();
		SpawnPoint.X = (float)GD.RandRange(Bounds.XMin + Global.GridSize, Bounds.XMax - Global.GridSize);
		SpawnPoint.Y = (float)GD.RandRange(Bounds.YMin + Global.GridSize, Bounds.YMax - Global.GridSize);
		SpawnPoint.X = float.Floor(SpawnPoint.X / Global.GridSize) * Global.GridSize;
		SpawnPoint.Y = float.Floor(SpawnPoint.Y / Global.GridSize) * Global.GridSize;
		
		GD.Print("spawnpoint = ", SpawnPoint);
		
		// instantiate food
		Node2D food = (Node2D) FoodScene.Instantiate();
		food.Position = SpawnPoint;
		
		// add food to scene tree
		GetParent().AddChild(food);
		
	}

	public void SpawnTail(Vector2 SpawnPoint)
	{
		Node2D tail = (Node2D) TailScene.Instantiate();
		tail.Position = SpawnPoint;
		GetParent().AddChild(tail);
		StaticEventManager.BroadcastTailAddedEvent((Tail)tail);
	}
	
}
