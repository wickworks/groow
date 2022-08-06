using Godot;
using System;

public class SimpleBezierCurve : Node2D
{
	[Export] private Color curveColor = new Color(0.5f, 0.5f, 0.5f);
	[Export] private float curveWidth = 2;
	
	private Position2D _pointA;
	private Position2D _pointB;

	private Curve2D _curve = new Curve2D();

	public override void _Ready()
	{
		// get references for control points and handles

		var nodePointA = _pointA = GetNode<Position2D>("PointA");
		var nodeHandleA = nodePointA.GetNode<Position2D>("Handle");

		var nodePointB = _pointB = GetNode<Position2D>("PointB");
		var nodeHandleB = nodePointB.GetNode<Position2D>("Handle");
		
		// get bezier control positions

		var pointA = nodePointA.Position;
		var handleA = nodeHandleA.Position;
		var pointB = nodePointB.Position;
		var handleB = nodeHandleB.Position;
		
		// add these points to the curve
		
		_curve.AddPoint(pointA, handleA, -handleA);
		_curve.AddPoint(pointB, handleB, -handleB);
	}
	
	private void UpdatePoint(int idx, Position2D point)
	{
		var handle = point.GetNode<Position2D>("Handle");
	
		_curve.SetPointPosition(idx, point.Position);
		_curve.SetPointIn(idx, handle.Position);
		_curve.SetPointOut(idx, -handle.Position);
		
		Update();
	}
	
	public override void _Process(float delta)
	{
		//Update();
	}

	public override void _Draw()
	{
		// draw curve
		var points = _curve.Tessellate();
		for (int i = 0; i < points.Length - 1; i++) 
			DrawLine(points[i], points[i + 1], curveColor, curveWidth, true);

	}
	
	private void _on_PointA_updatedPointOrHandle()
	{
		UpdatePoint(0, GetNode<Position2D>("PointA"));
	}


	private void _on_PointB_updatedPointOrHandle()
	{
		UpdatePoint(1, GetNode<Position2D>("PointB"));
	}
}



