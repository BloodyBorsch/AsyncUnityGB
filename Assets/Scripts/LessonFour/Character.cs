using System;
using UnityEngine;
using UnityEngine.Networking;


namespace LessonFour
{
    [RequireComponent(typeof(CharacterController))]
    public abstract class Character : NetworkBehaviour
    {
        protected abstract FireAction fireAction { get; set; }

        [SyncVar] protected Vector3 _serverPosition;
        [SyncVar] protected Quaternion _serverRotation;

        protected virtual void Start()
        {
            GameManager.SubscribeOnUpdate(Movement);
        }

        public abstract void Movement();

        [Command]
        protected void CmdUpdateTransform(Vector3 position, Quaternion rotation)
        {
            _serverPosition = position;
            _serverRotation = rotation;
        }

        private void OnDestroy()
        {
            GameManager.UnSubscribeOnUpdate(Movement);
        }
    }
}