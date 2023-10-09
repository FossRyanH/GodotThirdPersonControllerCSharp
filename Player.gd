extends CharacterBody2D


@export var move_speed : float = 250.0

func _physics_process(delta: float) -> void:
	var input_dir = Vector2.ZERO
	
