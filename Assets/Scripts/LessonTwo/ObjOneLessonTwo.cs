using Unity.Collections;
using Unity.Jobs;
using UnityEngine;


namespace LessonTwo
{
    public class ObjOneLessonTwo : MonoBehaviour
    {
        private int _numbersCount = 100;
        private int _minRange = 0;
        private int _maxRange = 21;

        private NativeArray<int> _numbersArray;        

        private JobHandle jobHandler;        
        private RecalculateJob _recalculatedJob;

        private void Start()
        {
            _numbersArray = new NativeArray<int>(_numbersCount, Allocator.Persistent);         
            _recalculatedJob = new RecalculateJob();

            if (Calculation())
            {
                _recalculatedJob.RecalculateNumbers = _numbersArray;
                jobHandler = _recalculatedJob.Schedule();
                jobHandler.Complete();
                if (jobHandler.IsCompleted) ShowResult(_recalculatedJob.RecalculateNumbers);
            }
        }        

        private bool Calculation()
        {
            int aboveTenCount = 0;
            int zeroCount = 0;

            for (int i = 0; i < _numbersArray.Length; i++)
            {
                _numbersArray[i] = Random.Range(_minRange, _maxRange);
                Debug.Log($"Число {i + 1} - {_numbersArray[i]}");
            }

            foreach (int number in _numbersArray)
            {
                if (number > 10)
                {
                    aboveTenCount++;
                }

                if (number <= 0)
                {
                    zeroCount++;
                }
            }

            Debug.Log($"Всего значений больше 10 = {aboveTenCount}");
            Debug.Log($"Всего значений равных 0 = {zeroCount}");

            return true;
        }        

        private void ShowResult(NativeArray<int> array)
        {
            int counter = 0;

            for (int i = 0; i < array.Length; i++)
            {
                Debug.Log($"Число {i + 1} - {array[i]}");

                if (array[i] == 0)
                {
                    counter++;
                }
            }

            Debug.Log($"После пересчета нулевых значений стало - {counter}");
        }

        private void OnDestroy()
        {
            _numbersArray.Dispose();
        }
    }
}