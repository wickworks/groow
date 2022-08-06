using Godot;
using System;

public class SimpleBezierCurve : Node2D
{
	[Export] private Color curveColor = new Color(0.5f, 0.5f, 0.5f);
	[Export] private float curveWidth = 2;
	
	[Export] private Color pointAColor = new Color(1, 0, 0);
	[Export] private Color pointBColor = new Color(0, 1, 0);
	
	[Export] private float pointRadius = 10;
	[Export] private float handleRadius = 5;
	[Export] private float handleLineWidth = 0.5f;
	
	private Position2D _pointA;
	private Position2D _handleA;
	private Position2D _pointB;
	private Position2D _handleB;

	private Curve2D _curve = new Curve2D();

	public override void _Ready()
	{
		// get references for control points and handles

		var nodePointA = _pointA = GetNode<Position2D>("PointA");
		var nodePointB = _pointB = GetNode<Position2D>("PointB");
		var nodeHandleA = _handleA = GetNode<Position2D>("PointA/HandleA");
		var nodeHandleB = _handleB = GetNode<Position2D>("PointB/HandleB");
			
		// get bezier control positions

		var pointA = nodePointA.Position;
		var handleA = nodeHandleA.Position;
		var pointB = nodePointB.Position;
		var handleB = nodeHandleB.Position;
		
		// add these points to the curve
		
		_curve.AddPoint(pointA, -handleA, handleA);
		_curve.AddPoint(pointB, handleB, -handleB);
	}

	public override void _Draw()
	{
		// draw curve
		var points = _curve.Tessellate();
		for (int i = 0; i < points.Length - 1; i++) 
			DrawLine(points[i], points[i + 1], curveColor, curveWidth, true);

		// draw control points and handles and stuff
		DrawLine(_pointA.Position, _pointA.Position + _handleA.Position, pointAColor, handleLineWidth, true);
		DrawLine(_pointB.Position, _pointB.Position + _handleB.Position, pointBColor, handleLineWidth, true);
		DrawCircle(_pointA.Position, pointRadius, pointAColor);
		DrawCircle(_pointB.Position, pointRadius, pointBColor);
		DrawCircle(_pointA.Position + _handleA.Position, handleRadius, pointAColor);
		DrawCircle(_pointB.Position + _handleB.Position, handleRadius, pointBColor);
	}
}
