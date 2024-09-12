using System;

public class Item
{
    private static int _idCounter = 0;
    public int Id { get; private set; }
    public int Value { get; set; }

    public Item(int value)
    {
        Id = System.Threading.Interlocked.Increment(ref _idCounter);
        Value = value;
    }
}