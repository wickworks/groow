extends Position2D

class_name BezierPoint

export var startingHandle = Vector2.ZERO
export var indexInCurve = 0

signal updatedPointOrHandle

var draggingPoint = false
var draggingHandle = false
var draggingHandleMirror = false

# Called when the node enters the scene tree for the first time.
func _ready():
	if $Handle.position == Vector2.ZERO:
		setHandlePosition(startingHandle)
		
	updateLine()	

func setPositionAndHandle(selfPos, handlePos):
	set_position(selfPos)
	setHandlePosition(handlePos)

func setHandlePosition(handlePos):
	$Handle.set_position(handlePos)
	$HandleMirror.set_position(-handlePos)
	updateLine()

func updateLine():
	$HandleLine.points = Array()
	$HandleLine.add_point($HandleMirror.position)
	$HandleLine.add_point($Handle.position)
	
	emit_signal("updatedPointOrHandle", indexInCurve)
	
	update()

func setIndex(idx):
	indexInCurve = idx

func _process(delta):
	var mousePos = get_global_mouse_position()
	
	if draggingHandle:
		setHandlePosition(mousePos - position)
	elif draggingHandleMirror:
		setHandlePosition(position - mousePos)
	elif draggingPoint:
		position = mousePos
		updateLine()
		
func _input(event):
	if (event is InputEventMouseButton && !event.pressed):
		draggingPoint = false
		draggingHandle = false
		draggingHandleMirror = false

func _on_HandleButton_button_down():
	draggingHandle = true

func _on_MirrorButton_button_down():
	draggingHandleMirror = true

func _on_PointButton_button_down():
	draggingPoint = true



