namespace Task2;

public class AsyncCaller
{
    private readonly EventHandler _eventHandler;
    
    public AsyncCaller(EventHandler eventHandler)
    {
        _eventHandler = eventHandler;
    }

    public bool Invoke(int time, object? sender, EventArgs args)
    {
        var mainTask = Task.Run(() =>
        {
            _eventHandler.Invoke(sender, args);
        });
        var waitTask = Task.Delay(time);

        if (Task.WaitAny(mainTask, waitTask) == 0)
        {
            return true;
        };
        return false;
    }
}