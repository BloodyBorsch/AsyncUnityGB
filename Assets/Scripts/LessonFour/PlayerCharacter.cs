using UnityEngine;
using UnityEngine.Networking;


namespace LessonFour
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(MouseLook))]
    public class PlayerCharacter : Character
    {
        [Range(0, 100)] [SerializeField] private int health = 100;

        [Range(0.5f, 10.0f)] [SerializeField] private float movingSpeed = 8.0f;
        [SerializeField] private float acceleration = 3.0f;
        private const float gravity = -9.8f;
        private CharacterController characterController;
        private MouseLook mouseLook;

        private Vector3 currentVelocity;

        protected override FireAction fireAction { get; set; }

        protected override void Start()
        {
            base.Start();
            Init();
        }

        public override void Movement()
        {
            if (mouseLook != null && mouseLook.PlayerCamera != null)
            {
                mouseLook.PlayerCamera.enabled = hasAuthority;
            }

            if (hasAuthority)            
            {
                var moveX = Input.GetAxis("Horizontal") * movingSpeed;
                var moveZ = Input.GetAxis("Vertical") * movingSpeed;
                var movement = new Vector3(moveX, 0, moveZ);
                movement = Vector3.ClampMagnitude(movement, movingSpeed);
                movement *= Time.deltaTime;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    movement *= acceleration;
                }

                movement.y = gravity;
                movement = transform.TransformDirection(movement);

                characterController.Move(movement);
                mouseLook.Rotation();

                CmdUpdateTransform(transform.position, transform.rotation);             
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, _serverPosition, ref currentVelocity, movingSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, _serverRotation, movingSpeed * Time.deltaTime);
            }
        }

        private void Init()
        {            
            fireAction = gameObject.AddComponent<RayShooter>();
            fireAction.Reloading();
            characterController = GetComponent<CharacterController>();
            mouseLook = GetComponent<MouseLook>();
        }               

        private void OnGUI()
        {
            if (Camera.main == null)
            {
                return;
            }

            var info = $"Health: {health}\nClip: {RayShooter.BulletCount}";
            var size = 12;
            var bulletCountSize = 50;
            var posX = Camera.main.pixelWidth / 2 - size / 4;
            var posY = Camera.main.pixelHeight / 2 - size / 2;
            var posXBul = Camera.main.pixelWidth - bulletCountSize * 2;
            var posYBul = Camera.main.pixelHeight - bulletCountSize;
            GUI.Label(new Rect(posX, posY, size, size), "+");
            GUI.Label(new Rect(posXBul, posYBul, bulletCountSize * 2, bulletCountSize * 2), info);
        }
    }
}
