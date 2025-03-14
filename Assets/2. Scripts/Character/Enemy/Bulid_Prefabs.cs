using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulid_Prefabs : MonoBehaviour
{
    [SerializeField] private Material Red;
    [SerializeField] private Material Green;
    private Material[] materials;
    private MeshRenderer meshRenderer;
    private LayerMask layerMask;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        materials = new Material[meshRenderer.materials.Length];
        for(int i = 0; i < materials.Length; i++) 
        {
            materials[i] = meshRenderer.materials[i];

        }
        layerMask = LayerMask.GetMask("Ground");
    }

    private void OnTriggerStay(Collider other)
    {
       
   
    }

    private void ColorChange(Material Color)
    {
        Material[] NewMaterialArray = new Material[materials.Length];
        for(int i = 0;i < NewMaterialArray.Length;i++)
        {
            NewMaterialArray[i] = Color;
        }
        meshRenderer.materials = NewMaterialArray;


    }

}
