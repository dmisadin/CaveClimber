using Godot;
using System;

public partial class Key : Node2D
{
    private Node2D _player;

    private void _on_key_area_body_entered(Node body)
    {
        GD.Print("body entered");
        if (body is PlayerController)
        {
            GD.Print("body entered in player");
            GetNode<PlayerData>("/root/PlayerData").AddItem("key");
            QueueFree();  // Remove the key
        }
    }
}
