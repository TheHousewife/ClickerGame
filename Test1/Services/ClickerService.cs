using Test1.Data;
using Microsoft.EntityFrameworkCore;

namespace Test1.Services;

public class ClickerService
{
    private readonly AppDbContext _dbContext;
    private PlayerData _player;
    private bool _isAutoSaving;

    public ClickerService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        LoadPlayer("default");
    }

    public void LoadPlayer(string username)
    {
        _player = _dbContext.Players.FirstOrDefault(p => p.Username == username);

        if (_player == null)
        {
            _player = new PlayerData
            {
                Username = username,
                CurrentCount = 0,
                AutoClickerAmount = 0,
                AutoClickerLevel = 1,
                AutoClickerStrength = 1,
                ClickStrength = 1,
                ClickMultiplier = 1,
                AutoClickerPrice = 10,
                AutoClickerUpgradeCost = 100,
                ClickUpgradeCost = 50
            };

            _dbContext.Players.Add(_player);
            _dbContext.SaveChanges();
        }

        StartAutoSave();
    }

    public double CurrentCount 
    { 
        get => _player.CurrentCount;
        set { _player.CurrentCount = value; }
    }

    public int AutoClickerAmount => _player.AutoClickerAmount;
    public double AutoClickerPrice => _player.AutoClickerPrice;
    public int AutoClickerStrength => _player.AutoClickerStrength;
    public int AutoClickerLevel => _player.AutoClickerLevel;
    public double AutoClickerUpgradeCost => _player.AutoClickerUpgradeCost;

    public int ClickStrength => _player.ClickStrength;
    public int ClickMultiplier => _player.ClickMultiplier;
    public int ClickUpgradeLevel => _player.ClickStrength;
    public double ClickUpgradeCost => _player.ClickUpgradeCost;

    public bool CanAffordAutoClicker => CurrentCount >= AutoClickerPrice;
    public bool CanAffordAutoClickerUpgrade => CurrentCount >= AutoClickerUpgradeCost;
    public bool CanAffordClickUpgrade => CurrentCount >= ClickUpgradeCost;
    
    public bool IsAutoClickerNearMilestone => (_player.AutoClickerLevel + 1) % 10 == 0;
    public bool IsClickUpgradeNearMilestone => (_player.ClickStrength + 1) % 10 == 0;

    public event Action? OnClickUpdated;
    public event Action? OnMilestoneReached;

    public void Click()
    {
        CurrentCount += ClickStrength * ClickMultiplier;
        OnClickUpdated?.Invoke();
    }

    public bool TryUpgradeClick()
    {
        if (!CanAffordClickUpgrade) return false;

        _player.ClickStrength++;
        CurrentCount -= _player.ClickUpgradeCost;
        _player.ClickUpgradeCost = Math.Round(_player.ClickUpgradeCost * 1.5);

        if (_player.ClickStrength % 10 == 0)
        {
            _player.ClickMultiplier++;
            OnMilestoneReached?.Invoke();
        }

        SaveProgress();
        return true;
    }

    public bool TryPurchaseAutoClicker()
    {
        if (!CanAffordAutoClicker) return false;

        _player.AutoClickerAmount++;
        CurrentCount -= _player.AutoClickerPrice;
        _player.AutoClickerPrice = Math.Round(_player.AutoClickerPrice * 1.25);

        OnClickUpdated?.Invoke();
        SaveProgress();
        return true;
    }

    public bool TryUpgradeAutoClicker()
    {
        if (!CanAffordAutoClickerUpgrade) return false;

        _player.AutoClickerLevel++;
        CurrentCount -= _player.AutoClickerUpgradeCost;
        _player.AutoClickerUpgradeCost = Math.Round(_player.AutoClickerUpgradeCost * 1.5);

        if (_player.AutoClickerLevel % 10 == 0)
        {
            _player.AutoClickerStrength++;
            OnMilestoneReached?.Invoke();
        }

        SaveProgress();
        return true;
    }

    public void SaveProgress()
    {
        _dbContext.Players.Update(_player);
        _dbContext.SaveChanges();
        Console.WriteLine("Game progress saved!");
    }

    private async Task StartAutoSave()
    {
        if (_isAutoSaving) return;
        _isAutoSaving = true;

        while (_isAutoSaving)
        {
            await Task.Delay(60000);
            SaveProgress();
            Console.WriteLine("Game progress auto-saved!");
        }
    }
}