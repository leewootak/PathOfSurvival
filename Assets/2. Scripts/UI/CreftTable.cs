using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;



public class CreftTable : MonoBehaviour
{
    public Button button; //이건 나중에 사라져도 되나?
    public GameObject box; // 전달 받을 아이템의 프리펩
    public GameObject UI; // 이걸 닫아야 함
    private LayerMask layerMask; //그라운드만 닿게
    private Camera cam;
    private Ray ray; //레이
    public GameObject Player; // 나중에 받게 할거임


    private bool IsSelect = false;
    private bool IsBatch = false;

    private float Angle;
    private void Awake()
    {
        cam = FindAnyObjectByType<Camera>();
        layerMask = LayerMask.GetMask("ground");
    }
    private void Start()
    {
        button.onClick.AddListener(Craft);
    }

    private void Update()
    {
        Drop();
    }
    private void FixedUpdate()
    {
        ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        Moving();
    }
    private void Craft()
    {
        CraftBatch();
    }

    private void CraftBatch()
    {
        UI.gameObject.SetActive(false);
        IsSelect = true;
        Debug.Log("HI");
    }

    private void Moving()
    {
        //box.gameObject.GetComponent<MeshRenderer>().materials[1];
        if (IsSelect)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                if (hit.collider != null)
                {

                    Vector3 forward = Vector3.Cross(hit.normal, Vector3.right);
                    //범선 벡터와 오른쪽으로 이루어진 평면의 수직 백터(앞방향)

                    if (Vector3.Distance(Player.transform.position, hit.point) >= 3f)
                    {
                        IsBatch = true; 
                        box.transform.position = Vector3.Lerp(box.transform.position, hit.point, Time.deltaTime * 5f);
                        box.transform.GetChild(0).transform.localRotation = Quaternion.LookRotation(forward, hit.normal) * 
                            Quaternion.AngleAxis(Angle, Vector3.up);
                        //box.transform.GetChild(0).transform.localRotation = Quaternion.LookRotation(forward, hit.normal);
                    }
                    else
                    {
                        box.transform.position = hit.point;
                        IsBatch = true;
                    }
                }
            }
            else
            {
                IsBatch = false;
                box.transform.position = ray.origin + new Vector3(0.06f, 0, 3f);
                box.transform.GetChild(0).localRotation = Quaternion.identity;
            }
        }
    }

    private void Drop()
    {
        if (IsBatch)
        {

            if (Input.GetMouseButtonDown(0))
            {
                IsSelect = false;
                box.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
                box.transform.GetChild(0).GetComponent<Bulid_Prefabs>().IsnotBuild = true;
                box.transform.GetChild(0).GetComponent<Bulid_Prefabs>().ColorChange(0);
            }

            if (Input.GetMouseButtonDown(1))
            {
                Angle += 90f;
            }
        }
    
    }

}
