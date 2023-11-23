using System;
using FPSgame.Scripts.Base.Input;
using Godot;

namespace FPSgame.Scripts.Node.Input;

public partial class PlayerInputManager : Godot.Node, IMovementInputMap, ICameraInputMap
{
    private static PlayerInputManager _instance;

    public static PlayerInputManager Default
    {
        get
        {
            if (_instance is null)
                throw new Exception($"{typeof(PlayerInputManager)} default instance cannot be accessed before it been initialized on the node 'Ready' call.");
            return _instance;
        }
        private set
        {
            if (_instance is not null)
                throw new Exception($"Multiple 'Init' calls on {typeof(PlayerInputManager)} is not allowed.");
            _instance = value;
        }
    }
    
    public float Vertical { get; private set; }
    public float Horizontal { get; private set; }
    
    public float Sprint { get; private set; }
    public float Crouch { get; private set; }
    
    public event Action Jump;
    
    public float MouseX { get; private set; }
    public float MouseY { get; private set; }

    private bool _isCrouchToggle = true;

    public override void _Ready()
    {
        Init();
    }

    public override void _PhysicsProcess(double delta)
    {
        UpdateMovementInput();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotionEvent)
        {
            Vector2 input = mouseMotionEvent.Relative;
            MouseX = input.X;
            MouseY = input.Y;
        }
    }

    private void Init()
    {
        Default = this;
    }

    private void UpdateMovementInput()
    {
        UpdateBasicMovement();

        Sprint = Godot.Input.GetActionStrength("Sprint");

        UpdateCrouch();
        
        if (Godot.Input.IsActionJustPressed("Jump")) { Jump?.Invoke(); }
    }

    private void UpdateBasicMovement()
    {
        Vector2 movementInput = Godot.Input.GetVector("Left", "Right", "Forward", "Back");
        Vertical = movementInput.Y;
        Horizontal = movementInput.X;
    }

    private void UpdateCrouch()
    {
        if (_isCrouchToggle)
        {
            if (Godot.Input.IsActionJustPressed("Crouch"))
            {
                if (Crouch <= 0.0f) Crouch = 1.0f;
                else ResetCrouch();
            } 
        }
        else
        {
            Crouch = Godot.Input.GetActionStrength("Crouch");
        }
    }

    public void SetCrouchToggle(bool value) { _isCrouchToggle = value; }

    public bool IsCrouchToggle() => _isCrouchToggle;

    public void ResetCrouch() => Crouch = 0.0f;
}