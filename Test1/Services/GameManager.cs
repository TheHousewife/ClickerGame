using Test1.Services;

public class GameManager
{
    private readonly ClickerService _clickerService;
    private bool _isRunning = true;
    private Task? _autoClickerTask;

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
}