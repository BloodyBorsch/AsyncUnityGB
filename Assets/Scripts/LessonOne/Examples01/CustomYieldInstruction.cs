using System.Collections;

public abstract class CustomYieldInstruction : IEnumerator
{
    public abstract bool keepWaiting
    {
        get;
    }

    public object Current => null;

    public abstract bool MoveNext();

    public abstract void Reset();
}