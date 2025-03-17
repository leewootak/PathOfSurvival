using UnityEngine;
using UnityEngine.UI;

public class CraftTable : MonoBehaviour
{
    public Button button;
    public GameObject box;
    public GameObject UI;
    public GameObject Player;
    private Camera cam;
    private LayerMask layerMask;
    private Ray ray;

    private float Angle;

    // 배치 모드 활성 여부
    private bool IsSelect = false;
    // 배치 가능 상태 여부
    public bool CanPlace;

    [SerializeField] private Build_Prefabs build_Prefabs;


    private void Awake()
    {
        cam = FindAnyObjectByType<Camera>();
        layerMask = LayerMask.GetMask("ground", "Wall");
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
        Place();

        // 화면 중앙에서 레이 생성
        ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
    }

    // 배치 모드 시작 및 아이템 프리팹 생성
    private void Craft()
    {
        UI.gameObject.SetActive(false);

        // 아이템 프리팹 생성
        Instantiate(box, box.transform.position, Quaternion.identity);

        IsSelect = true;
        CanPlace = true;

        Debug.Log("부품 선택");
    }

    private void Place()
    {
        if (IsSelect)
        {
            RaycastHit hit;

            // 10 이내의 그라운드를 감지하고, 플레이어와의 거리가 10 미만일 때
            if (Physics.Raycast(ray, out hit, 10f, layerMask) && Vector3.Distance(Player.transform.position, hit.point) < 10f)
            {
                if (hit.collider != null)
                {
                    CanPlace = true;

                    // 배치 가능 상태 색상으로 변경
                    box.transform.GetChild(0).GetComponent<Build_Prefabs>().ColorChange(2);

                    //// 충돌면의 법선과 오른쪽 벡터의 외적으로 전방 방향 계산
                    //Vector3 forward = Vector3.Cross(hit.normal, Vector3.right);

                    // 표면의 법선에 맞춰 회전
                    Quaternion baseRotation = Quaternion.LookRotation(-hit.normal);
                    Quaternion additionalRotation = Quaternion.Euler(0, Angle, 0);
                    box.transform.rotation = baseRotation * additionalRotation;



                    // 아이템 위치를 감지된 지점으로 이동
                    box.transform.position = hit.point;
                }
            }
            // 플레이어와의 거리가 10 이상이면 배치 불가
            else if (Vector3.Distance(Player.transform.position, hit.point) >= 10f)
            {
                CanPlace = false;

                // 배치 불가 상태 색상으로 변경
                box.transform.GetChild(0).GetComponent<Build_Prefabs>().ColorChange(1);

                Debug.Log("너무 멀음");
            }
        }
    }

    // 아이템 배치 및 회전 (클릭 계속하면 디버그 로그 계속 찍히는 현상 수정 필요)
    private void Drop()
    {
        if (CanPlace && Input.GetMouseButtonDown(0))
        {
            IsSelect = false;
            box.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;

            // 배치 완료 상태 색상으로 변경
            box.transform.GetChild(0).GetComponent<Build_Prefabs>().ColorChange(0);

            // 배치 UI 다시 활성화
            UI.gameObject.SetActive(true);

            Debug.Log("배치");
        }
        // 우클릭: 아이템 회전
        else if (Input.GetMouseButtonDown(1))
        {
            Angle += 90f;
        }
    }
}
