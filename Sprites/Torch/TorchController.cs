using Godot;
using System;

public partial class TorchController : PointLight2D
{
	private AnimatedSprite2D _sprite;
	private bool _isOn = true;

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("TorchSprite");
	}

	public void TurnOff()
	{
		if (_isOn)
		{
			_isOn = false;
			_sprite.Play("Off");  // Play the "Off" animation
		}
	}

	private void OnProximityBodyEntered(Node body)
	{
		GD.Print("BODY ENTERED!");
		if (body is PlayerController)  // Check if the player touches the torch
		{
			TurnOff();  // Turn off the torch when the player interacts with it
			this.Energy = 0;
		}
	}
}
