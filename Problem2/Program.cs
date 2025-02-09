

public interface IGenericStack<T>
{
    T GetByIndex(int index);
}

public class GenericStack<T> : IGenericStack<T>
{
    private List<T> items = new List<T>();

    public void Push(T item)
    {
        items.Add(item);
    }

    public T Pop()
    {
        if (items.Count > 0)
        {
            T item = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            return item;
        }
        return default(T); 
    }

    public T Peek()
    {
        if (items.Count > 0)
            return items[items.Count - 1];
        return default(T);
    }

    public T GetByIndex(int index)
    {
        if (index >= 0 && index < items.Count)
            return items[index];
        return default(T);
    }
}

class Program
{
    static void Main()
    {
        GenericStack<int> stack = new GenericStack<int>();
        stack.Push(10);
        stack.Push(20);
        stack.Push(30);

        Console.WriteLine(stack.GetByIndex(1));
        Console.WriteLine(stack.GetByIndex(0));
        Console.WriteLine(stack.GetByIndex(5)); 
    }
}
