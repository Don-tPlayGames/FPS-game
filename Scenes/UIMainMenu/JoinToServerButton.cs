using FPSgame.Scripts.Base.Networking;
using Godot;
using System;

public partial class JoinToServerButton : Button
{
    MultiplayerController multiplayerController;

    private TextEdit InputServerName;
    private TextEdit InputIPAddress;
    private TextEdit InputPort;
    public override void _Ready()
	{
        InputServerName = GetNode<TextEdit>("InputServerName");
        InputIPAddress = GetNode<TextEdit>("InputIPAddress");
        InputPort = GetNode<TextEdit>("InputPort");


        //multiplayerController.CreatingHostGame();
    }

	private void OnPressedJoinToServer()
	{
        //SetProcess(!IsProcessing());
        //string someText = InputServerName.Text;
        //multiplayerController.CreatingHostGame();
    }

}
