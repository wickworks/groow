extends Position2D

class_name BezierPoint

export var startingHandle = Vector2.ZERO

signal updatedPointOrHandle

var draggingPoint = false
var draggingHandle = false

# Called when the node enters the scene tree for the first time.
func _ready():
	$Handle.position = startingHandle
	updateLine()	

func updateLine():
	$HandleLine.points = Array()
	$HandleLine.add_point(Vector2.ZERO)
	$HandleLine.add_point($Handle.position)
	
	emit_signal("updatedPointOrHandle")
	
	update()
	
func _process(delta):
	var mousePos = get_global_mouse_position()
	
	if draggingPoint:
		position = mousePos
		updateLine()	
		
	if draggingHandle:
		$Handle.position = mousePos - position
		updateLine()	


func _input(event):
	if (event is InputEventMouseButton && !event.pressed):
		draggingPoint = false
		draggingHandle = false

func _on_PointArea_input_event(viewport, event, shape_idx):
	if (event is InputEventMouseButton && event.pressed):
		draggingPoint = true
		print("click point")

func _on_HandleArea_input_event(viewport, event, shape_idx):
	if (event is InputEventMouseButton && event.pressed):
		draggingHandle = true
		print("click handle")
