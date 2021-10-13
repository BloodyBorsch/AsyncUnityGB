using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class AsyncExample : MonoBehaviour
{

    /*private WaitForSeconds _water = new WaitForSeconds(1f);
    IEnumerator Start()
    {
        Debug.Log("Right now");
        yield return _water;
        Debug.Log("Second later");
    }*/

    #region First

    async void PrintAsync()
    {
        Debug.Log($"Message was printed instantly.");
        await Task.Delay(1000);
        Debug.Log($"Message was printed over 1 second.");
    }

    #endregion

    #region Second
    async void PrintAsync(string message, int times)
    {
        while (times > 0)
        {
            times--;
            Debug.Log(message);
            await Task.Yield();
        }
    }
    #endregion

    #region Third

    async void UnitTasksAsync()
    {
        var tasks = new List<Task>();
        tasks.Add(Task.Run(() => Unit1Async()));
        tasks.Add(Task.Run(() => Unit2Async()));
        tasks.Add(Task.Run(() => { Unit3Async(); }));
        await Task.WhenAll(tasks);
        Debug.Log("All units have finished their tasks.");
    }

    async Task Unit3Async()
    {
        Debug.Log("Unit1 starts rest.");
        await Task.Delay(1000);
        Debug.Log("Unit1 finishes rest.");
    }

    async Task Unit1Async()
    {
        Debug.Log("Unit1 starts chopping wood.");
        await Task.Delay(3000);
        Debug.Log("Unit1 finishes chopping wood.");
    }

    async Task Unit2Async()
    {
        Debug.Log("Unit2 starts patrolling.");
        await Task.Delay(5000);
        Debug.Log("Unit2 finishes patrolling.");
    }

    #endregion

    #region Fourth

    /*async void Start()
    {
        Debug.Log("Start");
        var task1 = WaitRandomTime();
        var task2 = WaitRandomTime();
        var taskResult = await Task.WhenAny(task1, task2);
        Debug.Log(taskResult.Result);
    }

    async Task<bool> WaitRandomTime()
    {
        int rnd = Random.Range(1000, 2000);
        await Task.Delay(rnd);
        return true;
    }*/

    #endregion

    #region Fifth

    /*CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
    
    
    public void Cancel()
    {
        cancelTokenSource.Cancel();
    }

    private void OnDestroy()
    {
        cancelTokenSource.Dispose();
    }

    async void Start()
    {
        CancellationToken cancelToken = cancelTokenSource.Token;
        Debug.Log("Start");
        var result = await FactorialAsync(cancelToken, 5);
        Debug.Log($"task result = {result}");
    }

    async Task<long> FactorialAsync(CancellationToken cancelToken, int x)
    {
        int result = 1;
        for (int i = 1; i < x; i++)
        {
            if (cancelToken.IsCancellationRequested)
            {
                Debug.Log("Операция прервана токеном.");
                return result;
            }

            result *= i;
            await Task.Delay(2000);
        }

        return result;
    }*/

    #endregion
}
