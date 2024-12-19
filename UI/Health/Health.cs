using Godot;
using System;

public partial class Health : HBoxContainer
{
    private PlayerData _playerData;
    [Export]
    private Texture2D FullHeartTexture;
    [Export]
    private Texture2D EmptyHeartTexture;
    private int PlayerHealth;

    public override void _Ready()
    {
        _playerData = GetNode<PlayerData>("/root/PlayerData"); // Reference the autoload
        UpdateHearts();
    }

    public override void _Process(double delta)
    {
        UpdateHearts();
    }

    public void UpdateHearts()
    {
        if (PlayerHealth == _playerData.Health)
            return;

        GD.Print($"UPDATE HEARTS; Old: {PlayerHealth}, New: {_playerData.Health}");
        PlayerHealth = _playerData.Health;

        for (int i = 1; i <= 3; i++)
        {
            var heart = GetNode<TextureRect>($"Heart{i}");
            heart.Texture = (i <= PlayerHealth) ? FullHeartTexture : EmptyHeartTexture;
        }
    }
}
