using UnityEngine;


public sealed class ShaderScript : MonoBehaviour
{
    private Material _material;

    private void Awake()
    {
        _material = GetComponentInChildren<Material>(true);
    }

    private void Start()
    {
        _material.SetColor("_Color", Color.white);
        //float mixValue = _material.GetFloat("_MixValue");
    }
}
