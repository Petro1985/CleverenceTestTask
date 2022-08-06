namespace Task1;

public static class Server
{
    private static int _count = 0;

    public static int GetCount()
    {
        return _count;
    }

    public static void AddToCount(int add)
    {
        Interlocked.Add(ref _count, add);
    }
}

public static class Server2
{
    private static readonly ReaderWriterLockSlim _countLock = new ReaderWriterLockSlim();
    private static int _count = 0;
    
    public static int GetCount()
    {
        try
        {
            _countLock.EnterReadLock();
            return _count;
        }
        finally
        {
            _countLock.ExitReadLock();
        }
    }
    public static void AddToCount(int add)
    {
        try
        {
            _countLock.EnterWriteLock();
            _count = _count + add;
        }
        finally
        {
            _countLock.ExitWriteLock();
        }
    }
}