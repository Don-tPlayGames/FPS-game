﻿using Godot;
using System;

public partial class MainMenu : Control
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        //Button NewGame = GetNode<Button>("NewGameButton");
    }

    private void OnPressedMultiplayerButton()
    {
        SetProcess(!IsProcessing());
        GetTree().ChangeSceneToFile("res://Scenes/Multiplayer.tscn");
    }

    private void OnNewGameButtonPressed()
    {
        SetProcess(!IsProcessing());
        GetTree().ChangeSceneToFile("res://Scenes/SceneTest.tscn");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
    }
}
