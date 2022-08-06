namespace Task1.Test;

public class SecondImplementationTest
{
    const int ReaderCount = 10;
    const int WriterCount = 10;
    
    private readonly int[] _readCount = new int[ReaderCount];
    private readonly int[] _writeCount = new int[WriterCount];

    [Fact]
    public async Task Server2_Must_Allow_To_Read_and_Write_Without_Race_Condition()
    {
        var cts = new CancellationTokenSource();
        
        var tasks = Enumerable.Range(0, ReaderCount)
            .Select(x=> Task.Run(() => Reader(x, cts.Token)));
        
        var tasksArray = tasks.Concat(Enumerable.Range(0, WriterCount)
            .Select(x => Task.Run(() => Writer(x, cts.Token)))).ToArray();

        await Task.Delay(TimeSpan.FromSeconds(5));
        cts.Cancel();
        
        await Task.WhenAll(tasksArray);

        Assert.Equal(Server2.GetCount(), _writeCount.Sum());
    }
    

    private void Reader(int ind, CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            var count = Server2.GetCount();
            _readCount[ind]++;
            Thread.Sleep(10);
        }
    }

    private void Writer(int ind, CancellationToken ct)
    {
        var rnd = new Random();
        while (!ct.IsCancellationRequested)
        {
            Server2.AddToCount(1);
            _writeCount[ind]++;
            Thread.Sleep(10);
        }
    }
}