using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
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

    [SerializeField] private Build_Prefabs build_Prefabs;


    private bool IsSelect = false;
    public bool IsBatch;

    private float Angle;

    private void Awake()
    {
        cam = FindAnyObjectByType<Camera>();
        layerMask = LayerMask.GetMask("ground");
    }
    private void Start()
    {
        button.onClick.AddListener(CraftBatch);
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

    private void CraftBatch()
    {
        UI.gameObject.SetActive(false);
        Instantiate(box, box.transform.position, Quaternion.identity);
        box.transform.GetChild(0).GetComponent<Build_Prefabs>().ColorChange(2);

        IsSelect = true;
        IsBatch = true;
        Debug.Log("부품 선택");
    }

    private void Moving()
    {
        if (IsSelect)
        {
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 20f, layerMask) && Vector3.Distance(Player.transform.position, hit.point) < 10f)
            {
                if (hit.collider != null)
                {
                    IsBatch = true;
                    box.transform.GetChild(0).GetComponent<Build_Prefabs>().ColorChange(2);
                    Vector3 forward = Vector3.Cross(hit.normal, Vector3.right);
                    //범선 벡터와 오른쪽으로 이루어진 평면의 수직 백터(앞방향)

                    box.transform.position = hit.point;
                }
            }
            else if (Vector3.Distance(Player.transform.position, hit.point) >= 10f)
            {
                IsBatch = false;
                build_Prefabs.IsNotBuild = true;
                box.transform.GetChild(0).GetComponent<Build_Prefabs>().ColorChange(1);
                Debug.Log("너무 멀음");
            }
        }
    }

    private void Drop()
    {
        if (IsBatch && Input.GetMouseButtonDown(0))
        {
            IsSelect = false;
            box.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            box.transform.GetChild(0).GetComponent<Build_Prefabs>().IsNotBuild = true;
            box.transform.GetChild(0).GetComponent<Build_Prefabs>().ColorChange(0);
            UI.gameObject.SetActive(true);
            Debug.Log("설치");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Angle += 90f;
        }
    }

    private void ResetState()
    {
        IsSelect = true;
        IsBatch = true;
    }
}