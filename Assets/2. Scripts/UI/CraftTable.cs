using UnityEngine;
using UnityEngine.UI;

public class CraftTable : MonoBehaviour
{
    // 배치할 프리팹과 UI 버튼
    public GameObject prefabs; // 배치할 아이템 프리팹
    public Button buildButton; // 배치모드 버튼

    private Collider col;              
    private Player player;             
    private Camera cam;                
    private LayerMask layerMask;       
    private Ray ray;                   
    private GameObject currentPrefab; // 현재 배치 중인 프리팹

    [SerializeField] private InventoryUI inventoryUI; // 인벤토리 UI 참조

    private float Angle; // 회전 각도

    private bool IsBuildMode = false; // 배치 모드 활성 여부
    public bool CanPlace = false; // 배치 가능한 상태 여부

    [SerializeField] private BuildColorSetting buildColorSetting; // 배치 색상 설정 스크립트 참조

    private void Awake()
    {
        cam = FindAnyObjectByType<Camera>();           
        player = FindAnyObjectByType<Player>();        
        layerMask = LayerMask.GetMask("Buildable");    
        inventoryUI = FindObjectOfType<InventoryUI>(); 
    }

    private void Start()
    {
        buildButton.onClick.AddListener(Craft);  // 버튼 클릭 시 Craft() 함수 실행
    }

    private void Update()
    {
        Place();
    }

    private void FixedUpdate()
    {
        Moving();

        // 화면 중앙에서 레이 생성 (플레이어 시점 기준)
        ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
    }

    // Craft: 배치 모드 시작 및 프리팹 생성
    private void Craft()
    {
        if (!IsBuildMode)
        {
            IsBuildMode = true;
            // 프리팹을 생성하고 현재 배치할 아이템으로 설정
            currentPrefab = Instantiate(prefabs, prefabs.transform.position, Quaternion.identity);
            Debug.Log("부품 선택");

            // 생성된 프리팹의 색상을 배치 가능 상태로 초기화
            currentPrefab.transform.GetChild(0).GetComponent<BuildColorSetting>().ColorChange(2);
            currentPrefab.transform.GetChild(0).GetComponent<BuildColorSetting>().IsPlaced = false; // 아직 배치되지 않음
        }
        // 인벤토리 UI를 종료하고 배치 색상 상태 초기화
        inventoryUI.OnClickExitButton();
        buildColorSetting.IsPlaced = false;
    }

    // 배치 모드에서 프리팹의 위치와 회전 업데이트
    private void Moving()
    {
        if (IsBuildMode)
        {
            RaycastHit hit;
            // 배치 가능 범위 탐색
            if (Physics.Raycast(ray, out hit, 15f, layerMask) && Vector3.Distance(player.transform.position, hit.point) < 13f)
            {
                if (hit.collider != null)
                {
                    CanPlace = true;

                    // 배치 가능한 상태 -> 초록색
                    currentPrefab.transform.GetChild(0).GetComponent<BuildColorSetting>().ColorChange(2);
                    Debug.Log("배치 가능 상태: " + CanPlace);

                    // 감지된 표면의 법선 방향에 맞춰 회전 적용
                    Quaternion baseRotation = Quaternion.LookRotation(-hit.normal);
                    Quaternion additionalRotation = Quaternion.Euler(0, 0, Angle);
                    currentPrefab.transform.rotation = baseRotation * additionalRotation;

                    // 프리팹 위치를 레이캐스트로 감지된 지점으로 이동
                    currentPrefab.transform.position = hit.point;
                }
            }
            else
            {
                // 배치 불가능한 상태 -> 빨간색
                CanPlace = false;
                currentPrefab.transform.GetChild(0).GetComponent<BuildColorSetting>().ColorChange(1);
                Debug.Log("배치 불가 상태: " + CanPlace);
            }
        }
    }

    // 아이템 배치 (좌클릭) 및 회전 (우클릭) 처리
    private void Place()
    {
        // 좌클릭하여 배치 가능한 상태일 때
        if (CanPlace && Input.GetMouseButtonDown(0))
        {
            IsBuildMode = false;
            // 배치가 완료된 아이템은 물리적 영향을 받지 않도록 설정
            currentPrefab.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            col = currentPrefab.GetComponentInChildren<Collider>();
            col.isTrigger = false;

            // 배치 완료 상태로 색상 및 레이어 변경
            currentPrefab.transform.GetChild(0).GetComponent<BuildColorSetting>().ColorChange(0);
            currentPrefab.layer = 10;
            currentPrefab.transform.GetChild(0).gameObject.layer = 10;

            buildColorSetting.IsPlaced = true;
            Debug.Log("배치");
        }
        // 우클릭 시 아이템을 90도씩 회전
        else if (Input.GetMouseButtonDown(1))
        {
            Angle += 90f;
        }
    }
}
