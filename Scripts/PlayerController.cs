using Godot;
using System;

public partial class PlayerController : CharacterBody3D
{
	public float walkSpeed = 3.41f;
	public float sprintSpeed = 12f;
	public float jumpForce = 7f;

	[Export] float sensitivity = 0.085f;

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseMotion)
		{
			RotateY(Mathf.DegToRad(-mouseMotion.Relative.X * sensitivity));
		}
	}


	// Get the gravity from the project settings to be synced with RigidBody nodes.
	[Export] float gravity = 9.81f;

	public override void _PhysicsProcess(double delta)
	{
		// 
	}
}
