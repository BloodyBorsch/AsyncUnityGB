using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;


namespace AsyncUnityGB
{
    public class ObjTwoLessonOne : MonoBehaviour
    {
        private CancellationTokenSource _tokenSource;
        private CancellationToken _token;
        private List<Task> _tasks;

        private int _taskOneWaiting = 60;
        private int _taskTwoCountOfFrames = 60;

        private async void Start()
        {
            _tasks = new List<Task>();
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;

            _tasks.Add(Task.Run(() => TaskOneAsync(_token, _taskOneWaiting)));
            _tasks.Add(Task.Run(() => TaskTwoAsync(_token, _taskTwoCountOfFrames)));

            await Task.WhenAll(_tasks);
            Debug.Log("Все задачи отработаны");
            _tasks.Clear();
        }

        private void OnDestroy()
        {
            Cancel();
            _tokenSource.Dispose();
        }

        private async Task TaskOneAsync(CancellationToken cancelToken, int time)
        {
            Debug.Log("Начато выполнение первой задачи");

            if (cancelToken.IsCancellationRequested)
            {
                Debug.Log("Операция прервана токеном.");                
            }
            else
            {
                await Task.Delay(time);
                Debug.Log("Первая задача отработана");
            }                       
        }

        private async Task TaskTwoAsync(CancellationToken cancelToken, int frames)
        {
            Debug.Log("Начато выполнение второй задачи");

            if (cancelToken.IsCancellationRequested)
            {
                Debug.Log("Операция прервана токеном.");
            }
            else
            {
                for (int i = 0; i < frames; i++)
                {                    
                    await Task.Yield();
                }
                
                Debug.Log("Вторая задача отработана");
            }
        }

        private void Cancel()
        {
            _tokenSource.Cancel();
        }
    }
}