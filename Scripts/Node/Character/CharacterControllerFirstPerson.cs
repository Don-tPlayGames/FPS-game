using System;
using FPSgame.Scripts.Base.Character;
using FPSgame.Scripts.Base.Input;
using FPSgame.Scripts.Node.Extension;
using FPSgame.Scripts.Node.Input;
using Godot;

namespace FPSgame.Scripts.Node.Character;

public partial class CharacterControllerFirstPerson : CharacterBody3D
{
	[Export] private float MovementResponsiveness = 10.0f;
	[Export] private float InAirMovementResponsiveness = 0.5f;
	
	private ICharacterPhysicalStats _characterStats;
	
	private IMovementInputMap _input;

	//TODO: make a layer of abstraction for camera.
	private FirstPersonCamera _camera;

	private Vector2 _horizontalVelocity;
	private double _jumpCooldownTimer;

	public override void _Ready()
	{
		//TODO: find a way to get rid of node naming dependencies.
		
		_characterStats = GetNode<PlayerCharacterStats>("Stats");
		
		_input = PlayerInputManager.Default;
		_input.Jump += DoJump;

		_camera = GetNode<FirstPersonCamera>("CameraPivotYaw");
	}

	public override void _PhysicsProcess(double deltaTime)
	{
		TickGravity(deltaTime);
		TickJump(deltaTime);
		HandleVelocity(deltaTime);
		MoveAndSlide();
	}

	private void HandleVelocity(double deltaTime)
	{
		//TODO: code organization.
		
		Vector3 inputDirectionAbsolute = new Vector3(_input.Horizontal, 0, _input.Vertical);
		
		float cameraGlobalRotationY = Vector3.Forward.SignedAngleTo(_camera.LookVector.ToVector3Horizontal(), Vector3.Up);
		Vector3 inputDirectionRelative = (Transform.Basis * inputDirectionAbsolute)
			.Normalized()
			.Rotated(Vector3.Up, cameraGlobalRotationY);

		Vector2 targetHorizontalVelocity = inputDirectionRelative.ToVector2Horizontal() * _characterStats.BaseSpeed;
		if (_input.Sprint > 0.0f)
		{
			targetHorizontalVelocity *= _characterStats.SprintSpeedMul;
			
			if (_input is PlayerInputManager pim)
				if (pim.IsCrouchToggle())
					pim.ResetCrouch();
		}
		else if (_input.Crouch > 0.0f)
		{
			targetHorizontalVelocity *= _characterStats.CrouchSpeedMul;
		}
		
		_horizontalVelocity = _horizontalVelocity.Lerp(
			to: targetHorizontalVelocity,
			weight: (float)deltaTime * (IsOnFloor() ? MovementResponsiveness : InAirMovementResponsiveness));

		Velocity = new Vector3(_horizontalVelocity.X, Velocity.Y, _horizontalVelocity.Y);
	}

	private void TickGravity(double deltaTime)
	{
		if (!IsOnFloor())
			Velocity = new Vector3(Velocity.X, Velocity.Y - WorldSettings.Physics.GlobalGravity * (float)deltaTime, Velocity.Z);
	}
	
	private void TickJump(double deltaTime)
	{
		if (_jumpCooldownTimer > 0.0f)
		{
			_jumpCooldownTimer -= Math.Min(deltaTime, _jumpCooldownTimer);
		}
	}

	private void DoJump()
	{
		if (_jumpCooldownTimer <= 0.0f && IsOnFloor())
		{
			Velocity = new Vector3(Velocity.X, _characterStats.JumpVelocity, Velocity.Z);
			_jumpCooldownTimer = _characterStats.JumpCooldown;
		}
	}
}