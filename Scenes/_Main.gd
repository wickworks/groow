extends Node2D

var BezierPointScene = preload("res://Scenes/BezierPoint/BezierPoint.tscn")

# Called when the node enters the scene tree for the first time.
func _ready():
	AddPoint(Vector2(-80,160), Vector2(0,100))
	AddPoint(Vector2(80,0), Vector2(-100,-40))
	AddPoint(Vector2(0,-160), Vector2(20,-20))

func _unhandled_input(event):
	if (event is InputEventMouseButton && event.pressed && event.button_index == BUTTON_LEFT):
		var mousePos = get_global_mouse_position()
		var point = AddPoint(mousePos, Vector2.ZERO)
		point.draggingHandle = true

func AddPoint(pointPos, handlePos):
	var point = BezierPointScene.instance()
	point.setPositionAndHandle(pointPos, handlePos)
	$LongBezierCurve.AddPoint(point)
	return point
