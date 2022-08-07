using System.Collections.Generic;
using Godot;
using System;

public class LongBezierCurve : Node2D
{
	[Export] private Color curveColor = new Color(0.5f, 0.5f, 0.5f);
	[Export] private float curveWidth = 2;
	[Export] private float swayAmplitude = 10f;
	
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
		point.Connect("deletePoint", this, "DeletePoint");
		
		//Update();
	}
	
	
	private void UpdatePoint(int idx)
	{
		var point = _points[idx];
		var handle = point.GetNode<Position2D>("Handle");
	
		_curve.SetPointPosition(idx, point.Position);
		_curve.SetPointIn(idx, handle.Position);
		_curve.SetPointOut(idx, -handle.Position);
		
		//Update();
	}
	
	private void DeletePoint(int idx)
	{
		_points.RemoveAt(idx);
		_curve.RemovePoint(idx);
		
		// Need to tell all the points what their new index is.
		for (int i = 0; i < _points.Count; i++) 
		{
			_points[i].Call("setIndex", i);
		}
		
		//Update();
	}
	
	public override void _Process(float delta)
	{
		// Apply an animation to all the points
		var animTimer = GetNode<Timer>("AnimationTimer");
		
		for (int i = 1; i < _points.Count-1; i++) 
		{
			var point = _points[i];
			var position = point.Position;
			var tween = (float) ((animTimer.TimeLeft + i*4f) / animTimer.WaitTime);
			var offset = Math.Sin(360f * tween) * swayAmplitude;
			_curve.SetPointPosition(i, new Vector2((float) position.x + (float) offset, position.y));
			//_curve.SetPointIn(i, handle.Position);
			//_curve.SetPointOut(i, -handle.Position);
		}
		
		Update();
	}

	public override void _Draw()
	{
		// draw curve
		var points = _curve.Tessellate();
		for (int i = 0; i < points.Length - 1; i++) 
			DrawLine(points[i], points[i + 1], curveColor, curveWidth, true);
	}

}



