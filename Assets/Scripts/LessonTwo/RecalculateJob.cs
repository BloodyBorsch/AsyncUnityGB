using Unity.Collections;
using Unity.Jobs;


namespace LessonTwo
{
    public struct RecalculateJob : IJobParallelFor
    {        
        public NativeArray<int> RecalculateNumbers;

        public void Execute(int index)
        {
            if (RecalculateNumbers[index] > 10)
            {
                RecalculateNumbers[index] = 0;                
            }
        }
    }
}