using UnityEngine;

public class WaitForMouseDown : CustomYieldInstruction
{
    public override bool keepWaiting => !Input.GetMouseButtonDown(1);

    public override bool MoveNext()
    {
        return keepWaiting;
    }

    public override void Reset()
    {
    }

    public WaitForMouseDown()
    {
        Debug.Log("Waiting for Mouse right button down");
    }
}