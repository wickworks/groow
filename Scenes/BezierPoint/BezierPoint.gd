extends Position2D

export var startingHandle = Vector2.ZERO

# Called when the node enters the scene tree for the first time.
func _ready():
	$Handle.position = startingHandle
	updateLine()	

func updateLine():
	$HandleLine.points = Array()
	$HandleLine.add_point(Vector2.ZERO)
	$HandleLine.add_point($Handle.position)
	update()
	

