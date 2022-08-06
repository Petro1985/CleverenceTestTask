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