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
    public Ray ray;
    public bool IsnotBuild = false;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        materials = new Material[meshRenderer.materials.Length];
        for(int i = 0; i < materials.Length; i++) 
        {
            materials[i] = meshRenderer.materials[i];
      

        }
        layerMask = LayerMask.GetMask("ground");
        
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("ground"))
        {
            
            ColorChange(1);
        }
        else
        {
            if (!IsnotBuild)
                ColorChange(2);
        }
    }

    public void ColorChange(int index)
    {
        Material[] NewMaterialArray = new Material[materials.Length];
        for(int i = 0;i < NewMaterialArray.Length;i++)
        {
            NewMaterialArray[i] = materials[index];
        }
        meshRenderer.materials = NewMaterialArray;
    }

}
