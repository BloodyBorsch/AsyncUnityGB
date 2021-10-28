using Unity.Collections;
using Unity.Jobs;
using UnityEngine;


namespace LessonTwo
{
    public class ObjTwoLessonTwo : MonoBehaviour
    {
        private int _numberOfentities = 50;
        private int _batchCount = 5;
        private int _startDistance = 5;
        private int _startVelocity = 2;

        private NativeArray<Vector3> _positions;
        private NativeArray<Vector3> _velocities;
        private NativeArray<Vector3> _finalPositions;
        private PositionsAndVelocities _startPositions;
        private JobHandle _handler;

        private void Start()
        {
            _positions = new NativeArray<Vector3>(_numberOfentities, Allocator.Persistent);
            _velocities = new NativeArray<Vector3>(_numberOfentities, Allocator.Persistent);
            _finalPositions = new NativeArray<Vector3>(_numberOfentities, Allocator.Persistent);
            _startPositions = new PositionsAndVelocities();

            if (Calculation())
            {
                _handler = _startPositions.Schedule(_numberOfentities, _batchCount);
                _handler.Complete();
                if (_handler.IsCompleted) ShowResult();
            }            
        }

        private bool Calculation()
        {
            _startPositions.Positions = _positions;
            _startPositions.Velocities = _velocities;

            for (int i = 0; i < _numberOfentities; i++)
            {
                _startPositions.Positions[i] = Random.insideUnitSphere * Random.Range(0, _startDistance);
                _startPositions.Velocities[i] = Random.insideUnitSphere * Random.Range(0, _startVelocity);
            }

            return true;
        }

        private void ShowResult()
        {
            for (int i = 0; i < _numberOfentities; i++)
            {
                _finalPositions[i] = _startPositions.Positions[i] + _startPositions.Velocities[i];
                Debug.Log($"Final Position {i + 1} is {_finalPositions[i]}");
            }
        }

        private void OnDestroy()
        {
            _finalPositions.Dispose();
            _positions.Dispose();
            _velocities.Dispose();
        }
    }
}