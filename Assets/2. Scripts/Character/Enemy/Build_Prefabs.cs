using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Prefabs : MonoBehaviour
{
    [SerializeField] private Material Red;
    [SerializeField] private Material Green;
    private Material[] materials;
    private MeshRenderer meshRenderer;
    private LayerMask layerMask;
    [SerializeField] private CreftTable creftTable;
    public Ray ray;
    public bool IsNotBuild = false;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        materials = new Material[meshRenderer.materials.Length];
        for (int i = 0; i < materials.Length; i++)
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
            creftTable.CanDrop = false;
            Debug.Log("배치 불가능");

        }
        else if (!IsNotBuild)
        {

            ColorChange(2);
            creftTable.CanDrop = true;
            Debug.Log("배치 가능");


        }
    }

    public void ColorChange(int index)
    {
        Material[] NewMaterialArray = new Material[materials.Length];
        for (int i = 0; i < NewMaterialArray.Length; i++)
        {
            NewMaterialArray[i] = materials[index];
        }
        meshRenderer.materials = NewMaterialArray;
    }


    // 일정거리 이상 멀어질 경우 빨간색 변경 필요
    // 공중에서 크로스헤어와 일치하지 않는 현상 수정 필요
}
