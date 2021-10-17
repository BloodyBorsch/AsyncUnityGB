using UnityEngine;
using UnityEngine.EventSystems;


namespace AsyncUnityGB
{
    public sealed class InputManager
    {
        private Camera _camera;
        private UnitManager _parent;

        public InputManager(UnitManager manager)
        {
            _camera = Camera.main;
            _parent = manager;
            manager.OnUpdate += MouseClick;
        }

        public void MouseClick()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                if (CheckTarget(ray)) _parent.DoDamage();
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (CheckTarget(ray)) _parent.RecieveHealing();
            }
        }

        private bool CheckTarget(Ray ray)
        {
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                var selected = hitInfo.collider.GetComponent<Unit>();                

                if (selected != null)
                {
                    _parent.GetUnit(selected);
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}