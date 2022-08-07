extends Position2D

class_name BezierPoint

export var startingHandle = Vector2.ZERO
export var indexInCurve = 0

signal updatedPointOrHandle

var draggingPoint = false
var draggingHandle = false

# Called when the node enters the scene tree for the first time.
func _ready():
	if $Handle.position == Vector2.ZERO:
		$Handle.position = startingHandle
	updateLine()	

func setPositionAndHandle(selfPos, handlePos):
	set_position(selfPos)
	$Handle.set_position(handlePos)
	updateLine()	

func updateLine():
	$HandleLine.points = Array()
	$HandleLine.add_point(Vector2.ZERO)
	$HandleLine.add_point($Handle.position)
	
	emit_signal("updatedPointOrHandle", indexInCurve)
	
	update()

func setIndex(idx):
	indexInCurve = idx

func _process(delta):
	var mousePos = get_global_mouse_position()
	
	if draggingHandle:
		$Handle.position = mousePos - position
		updateLine()	
	elif draggingPoint:
		position = mousePos
		updateLine()	
		
func _input(event):
	if (event is InputEventMouseButton && !event.pressed):
		draggingPoint = false
		draggingHandle = false

func _on_PointArea_input_event(viewport, event, shape_idx):
	if (event is InputEventMouseButton && event.pressed):
		draggingPoint = true

func _on_HandleArea_input_event(viewport, event, shape_idx):
	if (event is InputEventMouseButton && event.pressed):
		draggingHandle = true
