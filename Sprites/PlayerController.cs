using Godot;

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
    private string CurrentAnimation = "Idle"; // Tracks the active animation
    private int CurrentAnimationPriority = 0;

    private Vector2 velocity = new Vector2();

    public override void _Ready()
    {
        GetNode<PlayerData>("/root/PlayerData").HealthChanged += OnHealthChanged;

        // Connect animation finished signal
        GetNode<AnimatedSprite2D>("AnimatedSprite2D").AnimationFinished += OnAnimationFinished;
    }

    public override void _PhysicsProcess(double delta)
    {
        velocity = Velocity;

        if (!IsOnFloor())
            velocity += GetGravity() * (float)delta;

        HandleJump();
        HandleMovement();
        HandleDash();

        Velocity = velocity;
        MoveAndSlide();
    }

    private void HandleJump()
    {
        if (Input.IsActionJustPressed("ui_accept"))
        {
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
                velocity.Y = JumpVelocity;
                IsWallJumping = false;
                IsInAir = true;
            }

            SetAnimation("Jump");
        }
        else if (IsOnFloor())
        {
            IsInAir = false;
        }
    }

    private void HandleMovement()
    {
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        if (direction != Vector2.Zero)
        {
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = direction.X < 0;

            if (!IsInAir)
                SetAnimation("Run");

            velocity.X = Mathf.Lerp(velocity.X, direction.X * Speed, Acceleration);
        }
        else
        {
            if (!IsInAir)
                SetAnimation("Idle");

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

            //SetAnimation("Dash", 3);
        }
    }

    private void OnHealthChanged(int newHealth)
    {
        GD.Print($"PlayerController: Health updated to: {newHealth}");

        // Play the hurt animation
        if (newHealth > 0)
        {
            SetAnimation("Hurt", 4);
        }
        else 
        {
            SetAnimation("Death", 5);
        }
    }

    private void SetAnimation(string animation, int priority = 0)
    {
        // Only switch animations if the new animation has a higher priority
        if (priority >= CurrentAnimationPriority)
        {
            CurrentAnimation = animation;
            CurrentAnimationPriority = priority;
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play(animation);
        }
    }

    private void OnAnimationFinished()
    {
        CurrentAnimationPriority = 0;
        /* // Reset to Idle when Hurt animation ends
        if (CurrentAnimation == "Hurt")
        {
            CurrentAnimation = "Idle";
            GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("Idle");
        } */
    }
}
