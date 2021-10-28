using Unity.Collections;
using Unity.Jobs;


namespace LessonTwo
{
    public struct RecalculateJob : IJob
    {        
        public NativeArray<int> RecalculateNumbers;

        public void Execute()
        {
            for (int i = 0; i < RecalculateNumbers.Length; i++)
            {
                if (RecalculateNumbers[i] > 10)
                {
                    RecalculateNumbers[i] = 0;
                }
            }
        }
    }
}