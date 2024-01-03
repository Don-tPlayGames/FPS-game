using System;
using FPSgame.Scripts.Node.Character.Camera;
using Godot;

namespace FPSgame.Scripts.Node.Character;

public partial class CharacterBasic : CharacterBody3D
{
	//TODO: move this somewhere else
	[Export] private float _movementResponsiveness = 10.0f;
	[Export] private float _inAirMovementResponsiveness = 0.5f;

	[Export] private NodePath _camera;
	protected ICamera Camera;

	public bool IsGravityApplied { get; internal set; } = true;

	//TODO: replace with abstraction
	public PlayerCharacterStats Stats { get; private set; }

	protected double JumpCooldownTimer = 0.0d;

	protected Vector2 TargetVelocity;
	
	public event Action OnReady;

	public override void _Ready()
	{
		Camera = GetNode<ICamera>(_camera);
		
		Stats = GetNode<PlayerCharacterStats>("Stats");
		
		OnReady?.Invoke();
	}

	public override void _PhysicsProcess(double deltaTime)
	{
		UpdateVelocity(deltaTime);
		HandleGravity(deltaTime);
		MoveAndSlide();
		
		UpdateJumpTimer(deltaTime);
	}

	public void SetEyeHeight(float value)
	{
		Camera.SetEyeHeight(value);
	}

	public void ResetEyeHeight() => SetEyeHeight(Stats.EyeHeightNormal);

	public Vector3 GetLookVector()
	{
		return Camera.LookVector;
	}
	
	public void SetTargetVelocity(Vector2 value)
	{
		TargetVelocity = value;
	}

	private void UpdateVelocity(double deltaTime)
	{
		Velocity = Velocity.Lerp(
			to: new Vector3(TargetVelocity.X, Velocity.Y, TargetVelocity.Y),
			weight: (float)deltaTime * (IsOnFloor() ? _movementResponsiveness : _inAirMovementResponsiveness));
	}

	private void HandleGravity(double deltaTime)
	{
		if (IsGravityApplied)
			TickGravity(deltaTime);
	}

	private void TickGravity(double deltaTime)
	{
		if (!IsOnFloor())
			Velocity = new Vector3(Velocity.X, Velocity.Y - WorldSettings.Physics.GlobalGravity * (float)deltaTime, Velocity.Z);
	}

	private void UpdateJumpTimer(double deltaTime)
	{
		if (JumpCooldownTimer > 0.0d)
			JumpCooldownTimer -= deltaTime;
	}

	public void SetJumpCooldown(double timeInSeconds)
	{
		JumpCooldownTimer = timeInSeconds;
	}

	public void PerformJump()
	{
		Velocity = new Vector3(Velocity.X, Stats.JumpVelocity, Velocity.Z);
	}

	public bool TryPerformJump()
	{
		if (!CanJump()) return false;
        
		PerformJump();
		return true;
	}
	
	public bool CanJump()
	{
		return JumpCooldownTimer <= 0.0d && IsOnFloor();
	}
}