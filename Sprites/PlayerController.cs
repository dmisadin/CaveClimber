using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
	public const float Speed = 200.0f;
	public const float JumpVelocity = -420.0f;

	private float Friction = 0.1f;
	private float Acceleration = .25f;
	private int DashSpeed = 700;
	private bool IsDashing = false;
	private bool IsWallJumping = false;
	private bool IsInAir = false;
	private Vector2 velocity = new Vector2();

	public override void _PhysicsProcess(double delta)
	{
		velocity = Velocity;
		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		HandleJump();
		HandleMovement();
		HandleDash();

		Velocity = velocity;
		MoveAndSlide();
	}

	private void HandleJump()
	{
		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept"))
		{
			// Handle Wall Jump
			if (GetNode<RayCast2D>("RaycastLeft").IsColliding() && !IsWallJumping)
			{
				velocity.X = -JumpVelocity;
				velocity.Y = JumpVelocity;
				IsWallJumping = true;
				IsInAir = true;
			}
			else if (GetNode<RayCast2D>("RaycastRight").IsColliding() && !IsWallJumping)
			{
				velocity.X = JumpVelocity;
				velocity.Y = JumpVelocity;
				IsWallJumping = true;
				IsInAir = true;
			}
			else if (IsOnFloor())
			{
				velocity.Y = JumpVelocity; // Normal jump
				IsWallJumping = false;
				IsInAir = true;
			}

			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Jump");
		}
		else if (IsOnFloor())
		{
			IsInAir = false;
		}
	}

	private void HandleMovement()
	{
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			//velocity.X = direction.X * Speed;
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = direction.X < 0;
			if (!IsInAir)
				GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Run");
			velocity.X = Mathf.Lerp(velocity.X, direction.X * Speed, Acceleration);
		}
		else
		{
			//velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			if (!IsInAir)
				GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Idle");
			velocity.X = Mathf.Lerp(velocity.X, 0, Friction);
		}
	}

	private void HandleDash()
	{
		if (Input.IsActionJustPressed("dash"))
		{
			if (Input.IsActionPressed("ui_left"))
				velocity.X = -DashSpeed;
			if (Input.IsActionPressed("ui_right"))
				velocity.X = DashSpeed;
		}
	}
}
