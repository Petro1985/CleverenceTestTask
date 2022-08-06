namespace Task2.Test;

public class AsyncCallerInvokeTest
{
    [Fact]
    public void AsyncCallerInvoke_Returns_False_When_Handler_Takes_Too_Long()
    {
        const int timeOut = 200;
        var testedAsyncCaller = new AsyncCaller( (sender, args) =>
        {
            Thread.Sleep(timeOut + 100);
        });
        
        var result = testedAsyncCaller.Invoke(timeOut, null, EventArgs.Empty);
        
        Assert.False(result);
    }
    
    [Fact]
    public void AsyncCallerInvoke_Returns_True_When_Handler_Is_Quick_Enough()
    {
        const int timeOut = 200;
        var testedAsyncCaller = new AsyncCaller( (sender, args) =>
        {
            Thread.Sleep(timeOut - 100);
        });
        
        var result = testedAsyncCaller.Invoke(timeOut, null, EventArgs.Empty);
        
        Assert.True(result);
    }
    
    [Fact]
    public void AsyncCallerInvoke_Must_Do_Something_If_Handler_Threw()
    {
        const int timeOut = 200;
        var testedAsyncCaller = new AsyncCaller( (sender, args) => throw new Exception(""));
        
        var result = testedAsyncCaller.Invoke(timeOut, null, EventArgs.Empty);
        
        // Assert.****(result);  // don't know what to assert to
    }
}