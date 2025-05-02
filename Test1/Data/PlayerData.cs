using System.ComponentModel.DataAnnotations;

namespace Test1.Data;

public class PlayerData
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public double CurrentCount { get; set; }
    public int AutoClickerAmount { get; set; }
    public int AutoClickerLevel { get; set; }
    public int AutoClickerStrength { get; set; }
    public int ClickStrength { get; set; }
    public int ClickMultiplier { get; set; }
    public double AutoClickerPrice { get; set; } = 10;
    public double AutoClickerUpgradeCost { get; set; } = 100;
    public double ClickUpgradeCost { get; set; } = 50;
}