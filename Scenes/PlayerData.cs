using Godot;
using System.Collections.Generic;

public partial class PlayerData : Node
{
    // Player's health (default is 3 hearts)
    private int _health = 3;

    // Player's inventory
    private HashSet<string> _inventory = new HashSet<string>();

    // Getter for health
    public int Health => _health;

    // Add an item to the inventory
    public void AddItem(string item)
    {
        _inventory.Add(item);
    }

    // Check if the inventory contains an item
    public bool HasItem(string item)
    {
        return _inventory.Contains(item);
    }

    // Remove an item from the inventory
    public void RemoveItem(string item)
    {
        _inventory.Remove(item);
    }

    // Decrease player's health
    public void DecreaseHealth(int amount = 1)
    {
        _health = Mathf.Max(0, _health - amount);
    }

    // Increase player's health
    public void IncreaseHealth(int amount = 1)
    {
        _health = Mathf.Min(3, _health + amount); // Max hearts = 3
    }

    // Reset data (optional, useful for restarting)
    public void ResetData()
    {
        _health = 3;
        _inventory.Clear();
    }
}
