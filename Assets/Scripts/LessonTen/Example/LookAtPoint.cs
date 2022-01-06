using UnityEngine;

[ExecuteInEditMode]
public class LookAtPoint : MonoBehaviour
{
    public Vector3 LookAt = Vector3.zero;

    public void Update()
    {
        transform.LookAt(LookAt);
    }
}