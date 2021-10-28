using Unity.Collections;
using Unity.Jobs;
using UnityEngine;


namespace LessonTwo
{
    public struct PositionsAndVelocities : IJobParallelFor
    {
        public NativeArray<Vector3> Positions;
        public NativeArray<Vector3> Velocities;

        public void Execute(int index)
        {
            Positions[index] = Positions[index].normalized;
            Velocities[index] = Velocities[index].normalized;
        }
    }
}