using Test1.Services;

public class GameManager
{
    private readonly ClickerService _clickerService;
    private bool _isRunning = true;
    private Task? _autoClickerTask;
    private Task? _superClickerTask;

    public event Action? OnGameUpdated;

    public GameManager(ClickerService clickerService)
    {
        _clickerService = clickerService;
    }

    public void StartAutoClickers()
    {
        if (_autoClickerTask != null) return;

        _autoClickerTask = Task.Run(async () =>
        {
            while (_isRunning)
            {
                if (_clickerService.AutoClickerAmount > 0)
                {
                    int powerBoost = _clickerService.AutoClickerStrength;
                    _clickerService.CurrentCount += (_clickerService.AutoClickerAmount * powerBoost);
                
                    OnGameUpdated?.Invoke();
                }
                await Task.Delay(1000 / _clickerService.AutoClickerLevel);
            }
        });
    }
    
    public void StartSuperClickers()
    {
        if (_superClickerTask != null) return;

        _superClickerTask = Task.Run(async () =>
        {
            while (_isRunning)
            {
                if (_clickerService.SuperClickerAmount > 0)
                {
                    int powerBoost = _clickerService.SuperClickerStrength;
                    _clickerService.CurrentCount += (_clickerService.SuperClickerAmount * powerBoost);
                
                    OnGameUpdated?.Invoke();
                }
                await Task.Delay(150 / _clickerService.SuperClickerLevel);
            }
        });
    }
    
}