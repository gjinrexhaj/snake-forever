using Godot;
using System;

public partial class Bounds : Node2D
{
	[Export] public Marker2D UpperLeftMarker;
	[Export] public Marker2D LowerRightMarker;

	public float XMin;
	public float YMin;
	public float XMax;
	public float YMax;

	public override void _Ready()
	{
		XMax = LowerRightMarker.Position.X;
		XMin = UpperLeftMarker.Position.X;
		YMax = LowerRightMarker.Position.Y;
		YMin = UpperLeftMarker.Position.Y;
		
	}

	public Vector2 WrapVector(Vector2 vector)
	{
		if (vector.X > XMax)
		{
			return new Vector2(XMin, vector.Y);
		} 
		else if (vector.X < XMin)
		{
			return new Vector2(XMax, vector.Y);
		} 
		else if (vector.Y > YMax)
		{
			return new Vector2(vector.X, YMin);
		} 
		else if (vector.Y < YMin)
		{
			return new Vector2(vector.X, YMax);
		} 
		return vector;
	}
}
