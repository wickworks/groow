extends Node2D

var BezierPointScene = preload("res://Scenes/BezierPoint/BezierPoint.tscn")

# Called when the node enters the scene tree for the first time.
func _ready():
	AddPoint(Vector2(0,0), Vector2(20,-20))
	AddPoint(Vector2(80,80), Vector2(-100,-40))
	AddPoint(Vector2(-80,160), Vector2(-40,40))

func _unhandled_input(event):
	if (event is InputEventMouseButton && event.pressed):
		var mousePos = get_global_mouse_position()
		AddPoint(mousePos, Vector2.ZERO)

func AddPoint(pointPos, handlePos):
	var point = BezierPointScene.instance()
	point.setPositionAndHandle(pointPos, handlePos)
	$LongBezierCurve.AddPoint(point)