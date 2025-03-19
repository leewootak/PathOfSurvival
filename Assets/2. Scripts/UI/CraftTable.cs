using UnityEngine;
using UnityEngine.UI;

public class CraftTable : MonoBehaviour
{
    public GameObject prefabs;               // 배치할 프리팹
    public Button button;                    // 프리팹 제작 버튼
    private Collider col;
    private Player player;                  // 플레이어 참조
    private Camera cam;                     // 메인 카메라 참조
    private LayerMask layerMask;            // 배치 가능한 오브젝트 레이어
    private Ray ray;                      // 화면 중앙에서 발사되는 레이
    private GameObject currentPrefab;       // 현재 배치 중인 프리팹
    [SerializeField] private InventoryUI inventoryUI; // 인벤토리 UI 참조

    private float Angle;                    // 프리팹 회전 각도

    private bool IsBuildMode = false;       // 배치 모드 활성 여부
    public bool CanPlace = false;           // 배치 가능 상태 여부

    [SerializeField] private BuildColorSetting buildColorSetting; // 빌드 색상 설정 스크립트

    // 초기화: 카메라, 플레이어, 배치 가능한 레이어, 인벤토리 UI 설정
    private void Awake()
    {
        cam = FindAnyObjectByType<Camera>();
        player = FindAnyObjectByType<Player>();
        layerMask = LayerMask.GetMask("Buildable");
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    // 버튼에 제작(Craft) 메서드 연결
    private void Start()
    {
        button.onClick.AddListener(Craft);
    }

    // 매 프레임 배치(Place) 체크
    private void Update()
    {
        Place();
    }

    // 고정 시간 간격마다 이동(Moving) 처리 및 화면 중앙에서 레이 생성
    private void FixedUpdate()
    {
        Moving();
        ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
    }

    // 배치 모드 시작 및 프리팹 생성
    private void Craft()
    {
        if (!IsBuildMode)
        {
            IsBuildMode = true;
            currentPrefab = Instantiate(prefabs, prefabs.transform.position, Quaternion.identity);
            Debug.Log("부품 선택");

            // 새로 생성된 프리팹의 색상을 기본(배치 가능) 상태로 설정
            currentPrefab.transform.GetChild(0).GetComponent<BuildColorSetting>().ColorChange(2);
            currentPrefab.transform.GetChild(0).GetComponent<BuildColorSetting>().IsPlaced = false;
        }
        // 인벤토리 UI 종료 처리
        inventoryUI.OnClickExitButton();
        buildColorSetting.IsPlaced = false;
    }

    // 프리팹 이동 및 배치 가능 여부 업데이트
    private void Moving()
    {
        if (IsBuildMode)
        {
            RaycastHit hit;
            // 레이캐스트로 배치 가능한 영역 탐지 및 플레이어와의 거리 확인
            if (Physics.Raycast(ray, out hit, 15f, layerMask) && Vector3.Distance(player.transform.position, hit.point) < 13f)
            {
                if (hit.collider != null)
                {
                    CanPlace = true;
                    // 배치 가능 상태 색상 적용
                    currentPrefab.transform.GetChild(0).GetComponent<BuildColorSetting>().ColorChange(2);
                    Debug.Log("배치 가능 상태: " + CanPlace);

                    // 표면의 법선 방향에 맞춰 회전하고 추가 회전 적용
                    Quaternion baseRotation = Quaternion.LookRotation(-hit.normal);
                    Quaternion additionalRotation = Quaternion.Euler(0, 0, Angle);
                    currentPrefab.transform.rotation = baseRotation * additionalRotation;

                    // 프리팹 위치를 레이캐스트 충돌 지점으로 이동
                    currentPrefab.transform.position = hit.point;
                }
            }
            else
            {
                CanPlace = false;
                // 배치 불가 상태 색상 적용
                currentPrefab.transform.GetChild(0).GetComponent<BuildColorSetting>().ColorChange(1);
                Debug.Log("배치 불가 상태: " + CanPlace);
            }
        }
    }

    // 배치(Place) 처리: 좌클릭 시 배치 완료, 우클릭 시 회전
    private void Place()
    {
        // 좌클릭으로 배치 가능 시 배치 완료 처리
        if (CanPlace && Input.GetMouseButtonDown(0))
        {
            IsBuildMode = false;
            // 물리 시뮬레이션 정지 후 고정
            currentPrefab.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            col = currentPrefab.GetComponentInChildren<Collider>();
            col.isTrigger = false;

            // 배치 완료 상태 색상 적용
            currentPrefab.transform.GetChild(0).GetComponent<BuildColorSetting>().ColorChange(0);
            // 레이어 변경으로 배치 완료 처리
            currentPrefab.layer = 10;
            currentPrefab.transform.GetChild(0).gameObject.layer = 10;

            buildColorSetting.IsPlaced = true;
            Debug.Log("배치");
        }
        // 우클릭 시 프리팹을 90도씩 회전
        else if (Input.GetMouseButtonDown(1))
        {
            Angle += 90f;
        }
    }
}
