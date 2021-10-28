using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace AsyncUnityGB
{
    public class ObjThreeLessonOne : MonoBehaviour
    {
        private CancellationTokenSource _tokenSource;
        private CancellationToken _token;

        private int _taskOneWaiting = 60;
        private int _taskTwoCountOfFrames = 60;

        private void Start()
        {           
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;

            var result = WhatTaskFasterAsync();

            Debug.Log($"Все задачи отработаны, результат {result}");
        }

        private void OnDestroy()
        {
            Cancel();
            _tokenSource.Dispose();
        }

        private async Task<bool> WhatTaskFasterAsync()
        {            
            var task1 = TaskOneAsync(_token, _taskOneWaiting);
            var task2 = TaskTwoAsync(_token, _taskTwoCountOfFrames);

            var task = await Task.WhenAny(task1, task2);
            var result = task.Result;
            Cancel();

            Debug.Log("Все задачи отработаны");

            return result;
        }


        private async Task<bool> TaskOneAsync(CancellationToken cancelToken, int time)
        {
            Debug.Log("Начато выполнение первой задачи");

            if (cancelToken.IsCancellationRequested)
            {
                Debug.Log("Операция прервана токеном.");
                return false;
            }
            else
            {
                await Task.Delay(time);
                Debug.Log("Первая задача отработана");
                return true;
            }
        }

        private async Task<bool> TaskTwoAsync(CancellationToken cancelToken, int frames)
        {
            Debug.Log("Начато выполнение второй задачи");

            if (cancelToken.IsCancellationRequested)
            {
                Debug.Log("Операция прервана токеном.");
                return false;
            }
            else
            {
                for (int i = 0; i < frames; i++)
                {
                    await Task.Yield();
                }

                Debug.Log("Вторая задача отработана");
                return false;
            }
        }

        private void Cancel()
        {
            _tokenSource.Cancel();
        }
    }
}