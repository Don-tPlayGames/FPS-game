using System;
using FPSgame.Scripts.Base.Input;
using Godot;

namespace FPSgame.Scripts.Node.Input;

public partial class PlayerInputManager : Godot.Node, IMovementInputMap, ICameraInputMap
{
    //TODO: optimize mouse input values update, if possible.
    
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
    
    public bool Sprint { get; private set; }

    public event Action OnCrouchKeyPressed;
    public event Action OnCrawlKeyPressed;
    
    public event Action OnJumpKeyPressed;
    
    public float MouseX { get; private set; }
    public float MouseY { get; private set; }

    private Vector2 _mousePositionPrev;
    private Vector2 _mousePositionCur;

    public override void _Ready()
    {
        Init();
        
        Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Captured;
    }

    public override void _PhysicsProcess(double delta)
    {
        UpdateMouseInput();
        UpdateMovementInput();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotionEvent)
        {
            _mousePositionCur = mouseMotionEvent.Relative;
        }
        
        if (@event is InputEventKey)
        {
            if(Godot.Input.IsActionJustPressed("ui_cancel"))
                if(Godot.Input.MouseMode == Godot.Input.MouseModeEnum.Captured)
                    Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Visible;
        }

        if (@event is InputEventMouseButton && @event.IsPressed())
        {
            if(Godot.Input.MouseMode == Godot.Input.MouseModeEnum.Visible)
                Godot.Input.MouseMode = Godot.Input.MouseModeEnum.Captured;
        }
    }

    private void Init()
    {
        Default = this;
    }

    private void UpdateMouseInput()
    {
        if (_mousePositionCur != _mousePositionPrev)
        {
            MouseX = _mousePositionCur.X;
            MouseY = _mousePositionCur.Y;
            _mousePositionPrev = _mousePositionCur;
        }
        else
        {
            MouseX = 0;
            MouseY = 0;
        }
    }

    private void UpdateMovementInput()
    {
        UpdateBasicMovement();

        Sprint = Godot.Input.IsActionPressed("Sprint");

        if (Godot.Input.IsActionJustPressed("Crouch")) { OnCrouchKeyPressed?.Invoke(); }
        
        if (Godot.Input.IsActionJustPressed("Crawl")) { OnCrawlKeyPressed?.Invoke(); }
        
        if (Godot.Input.IsActionJustPressed("Jump")) { OnJumpKeyPressed?.Invoke(); }
    }

    private void UpdateBasicMovement()
    {
        Vector2 movementInput = Godot.Input.GetVector("Left", "Right", "Forward", "Back");
        Vertical = movementInput.Y;
        Horizontal = movementInput.X;
    }
}