using Godot;
using System;

public partial class PlayerController : CharacterBody3D
{
	Node3D cameraMount;
	AnimationPlayer animPlayer;

	public static float walkSpeed = 3.41f;
	public static float sprintSpeed = 9f;
	public float jumpForce = 4.5f;
	int jumpCount = 0;

	static float moveSpeed = walkSpeed;

	[Export] float sensitivity = 0.085f;

	[Export] float gravity = 9.81f;

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;

		cameraMount = GetNode<Node3D>("CameraMount");
		animPlayer = GetNode<AnimationPlayer>("Visuals/mixamo_base/AnimationPlayer");
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseMotion)
		{
			RotateY(Mathf.DegToRad(-mouseMotion.Relative.X * sensitivity));
			cameraMount.RotateX(Mathf.DegToRad(-mouseMotion.Relative.Y * sensitivity));
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity.Y -= gravity * (float)delta;
		}

		if (Input.IsActionJustPressed("jump") && jumpCount < 1)
		{
			velocity.Y = jumpForce;
		}
		else
		{
			jumpCount = 0;
		}

		if (Input.IsActionPressed("sprint"))
		{
			moveSpeed = sprintSpeed;
		}
		else if (Input.IsActionJustReleased("sprint"))
		{
			moveSpeed = walkSpeed;
		}

		Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0 , inputDir.Y)).Normalized();

		if (direction != Vector3.Zero)
		{
			if (animPlayer.CurrentAnimation != "walking" && moveSpeed != sprintSpeed)
			{
				animPlayer.Play("walking");
			}
			else if (animPlayer.CurrentAnimation != "running" && moveSpeed == sprintSpeed)
			{
				animPlayer.Play("running");
			}
			velocity.X = direction.X * moveSpeed;
			velocity.Z = direction.Z * moveSpeed;
		}
		else
		{
			if (animPlayer.CurrentAnimation != "idle")
			{
				animPlayer.Play("idle");
			}
			velocity.X = Mathf.MoveToward(Velocity.X, 0 ,moveSpeed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0 ,moveSpeed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
