using Godot;
using System;

public partial class BatSpawner : Node2D 
{
	[Export] public PackedScene Bat; 
	[Export] public int MaxBats = 1; 
	[Export] public float SpawnCooldown = 5.0f;

	private int _currentBatCount = 0;
	private bool _canSpawn = true;

	public override void _Ready()
	{
	
	}

	private void _on_bat_spawner_area_body_entered(Node body)
	{
		if (body is PlayerController)
		{
			HandlePlayerEntered();
		}
	}

	private async void HandlePlayerEntered()
	{
		if (_currentBatCount < MaxBats && _canSpawn)
		{
			SpawnBat();
			_canSpawn = false;

			await ToSignal(GetTree().CreateTimer(SpawnCooldown), "timeout");
			_canSpawn = true;
		}
	}

	private void SpawnBat()
	{
		if (Bat == null)
		{
			GD.PrintErr("Bat scene is not assigned in the inspector!");
			return;
		}

		CallDeferred("DeferredSpawnBat");
	}

	private void DeferredSpawnBat()
	{
		Node2D bat = (Node2D)Bat.Instantiate();

		GetParent().AddChild(bat);

		bat.GlobalPosition = GlobalPosition;

		_currentBatCount++;

		GD.Print("Bat spawned at the center of the spawner!");
	}
}
