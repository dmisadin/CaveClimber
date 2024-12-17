using Godot;
using System;

public partial class Chest : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void _on_chest_area_body_entered(Node body) 
    {
        GD.Print("chest area has a body entered");
        if (body is PlayerController)  // Check if the player touches the torch
		{
            GD.Print("entered chest and is player");
            if (GetNode<PlayerData>("/root/PlayerData").HasItem("key")) {
                GD.Print("entered chest and has key");
			    GetNode<AnimatedSprite2D>("ChestSprite").Play("open");
                // End game, 
            }
		}
    }
}
