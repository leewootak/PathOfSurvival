using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Awake()
    {
        cam = FindAnyObjectByType<Camera>();
        layerMask = LayerMask.GetMask("Ground");
    }
    private void Start()
    {
        button.onClick.AddListener(Craft);
    }

    private void FixedUpdate()
    {
        ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        Moving();
        Drop();
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

        if (IsSelect)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                if (hit.collider != null)
                {
                    Vector3 vector3 = new Vector3(hit.point.x, hit.collider.bounds.size.y, hit.point.z);
                    if (Vector3.Distance(Player.transform.position, vector3) >= 3f)
                    {
                        box.transform.position = Vector3.Lerp(box.transform.position, vector3, Time.deltaTime * 5f);
                        IsBatch = true;
                    }
                    else
                    {
                        box.transform.position = vector3;
                        IsBatch = true;
                    }
                }
            }
            else
            {
                box.transform.position = hit.point;
            }
        }
    }

    private void Drop()
    {
        if(IsBatch)
        {
            if(Input.GetMouseButtonDown(0))
            {
                IsSelect = false;
                box.AddComponent<Rigidbody>();
                box.GetComponent<Rigidbody>().isKinematic = true;
                
            }
        }
    }

}
