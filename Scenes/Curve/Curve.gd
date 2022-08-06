extends Node2D

onready var curve = Curve2D.new()

func _ready():
	var p0_in = Vector2.ZERO
	var p0_vertex = $P0.position
	var p0_out = $P1.position - $P0.position
	var p1_in = $P2.position - $P3.position
	var p1_vertex = $P3.position
	var p1_out = Vector2.ZERO
	
	curve.add_point(p0_vertex, p0_in, p0_out)
	curve.add_point(p1_vertex, p1_in, p1_out)

func _process(delta):
	pass
	#$LineP0P1.points = Array()
	#$LineP2P3.points = Array()
	#$LineP0P1.add_point($P0.position)
	#$LineP0P1.add_point($P1.position)
	#$LineP2P3.add_point($P2.position)
	#$LineP2P3.add_point($P3.position)
	
	#update()  # Update the node's visual representation.

func _draw():
	# Draw the handles
	draw_circle($P0.position,5.0,Color(1.0,0,0,1.0))
	draw_circle($P1.position,5.0,Color(0,1.0,0,1.0))
	draw_circle($P2.position,5.0,Color(0,1.0,0,1.0))
	draw_circle($P3.position,5.0,Color(1.0,0,0,1.0))
	#print(curve_pts)

	var curve_pts = curve.tessellate()
	for idx in len(curve_pts)-1:
		draw_line(curve_pts[idx],curve_pts[idx+1], Color(.4,.4,.4,1.0), 5.0, true)
