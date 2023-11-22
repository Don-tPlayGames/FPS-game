using FPSgame.Scripts.Node.Input;
using Godot;

namespace FPSgame.Scripts.Node;

public partial class CharacterControllerFirstPerson : CharacterBody3D
{
	[Export] private float Speed = 5.0f;
	[Export] private float JumpVelocity = 4.5f;

	private PlayerInputManager _input;

	private bool _isJumping;

	public override void _Ready()
	{
		_input = PlayerInputManager.Default;
		_input.Jump += DoJump;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		if (!IsOnFloor())
			velocity.Y -= WorldSettings.Physics.GlobalGravity * (float)delta;

		if (_isJumping)
		{
			if (IsOnFloor())
				velocity.Y = JumpVelocity;
			_isJumping = false;
		}

		Vector2 inputDir = new Vector2(_input.Horizontal, _input.Vertical);
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void DoJump()
	{
		_isJumping = true;
	}
}