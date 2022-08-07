using System.Collections.Generic;
using Godot;
using System;

public class LongBezierCurve : Node2D
{
	[Export] private Color curveColor = new Color(0.5f, 0.5f, 0.5f);
	[Export] private float curveWidth = 2;
	
	private readonly List<Position2D> _points = new List<Position2D>();

	private Curve2D _curve = new Curve2D();

	public override void _Ready()
	{
		
	}
	
	public void AddPoint(Position2D point)
	{
		var handle = point.GetNode<Position2D>("Handle");
		_curve.AddPoint(point.Position, handle.Position, -handle.Position);

		point.Call("setIndex", _points.Count);
		_points.Add(point);
		AddChild(point);
		
		point.Connect("updatedPointOrHandle", this, "UpdatePoint");
		
		Update();
	}
	
	
	private void UpdatePoint(int idx)
	{
		var point = _points[idx];
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

}



