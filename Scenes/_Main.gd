extends Node2D

var BezierPointScene = preload("res://Scenes/BezierPoint/BezierPoint.tscn")

# Called when the node enters the scene tree for the first time.
func _ready():
	AddPoint(Vector2(0,160), Vector2(0,100))
	AddPoint(Vector2(100,80), Vector2(-30,80))
	AddPoint(Vector2(-100,0), Vector2(30,80))
	AddPoint(Vector2(100,-80), Vector2(30,80))
	AddPoint(Vector2(0,-160), Vector2(0,100))

func _unhandled_input(event):
	if (event is InputEventMouseButton && event.pressed && event.button_index == BUTTON_LEFT):
		var mousePos = get_global_mouse_position()
		var point = AddPoint(mousePos, Vector2.ZERO)
		point.draggingHandleMirror = true

func AddPoint(pointPos, handlePos):
	var point = BezierPointScene.instance()
	point.setPositionAndHandle(pointPos, handlePos)
	$LongBezierCurve.AddPoint(point)
	return point
