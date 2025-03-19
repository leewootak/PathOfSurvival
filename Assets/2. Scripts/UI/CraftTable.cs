using UnityEngine;
using UnityEngine.UI;

public class CraftTable : MonoBehaviour
{
    public GameObject prefabs;
    public Button button;
    private Collider col;
    private Player player;
    private Camera cam;
    private LayerMask layerMask;
    private Ray ray;
    private GameObject currentPrefab; // 현재 배치 중인 프리팹
    [SerializeField] private InventoryUI inventoryUI;

    private float Angle;

    // 배치 모드 활성 여부
    private bool IsBuildMode = false;
    // 배치 가능 상태 여부
    public bool CanPlace = false;

    [SerializeField] private BuildColorSetting buildColorSetting;


    private void Awake()
    {
        //ui = transform.parent.gameObject;
        cam = FindAnyObjectByType<Camera>();
        player = FindAnyObjectByType<Player>();
        layerMask = LayerMask.GetMask("Buildable");
        inventoryUI = FindObjectOfType<InventoryUI>();

    }

    private void Start()
    {
        //Craft();
        button.onClick.AddListener(Craft);
    }

    private void Update()
    {
        Place();
    }

    private void FixedUpdate()
    {
        Moving();

        // 화면 중앙에서 레이 생성
        ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
    }

    // 배치 모드 시작 및 아이템 프리팹 생성
    private void Craft()
    {
        if (!IsBuildMode)
        {
            IsBuildMode = true;
            // 아이템 프리팹 생성
            currentPrefab = Instantiate(prefabs, prefabs.transform.position, Quaternion.identity);
            Debug.Log("부품 선택");

            
            // 새로 생성된 프리팹의 색상을 기본값으로 초기화 (배치 가능 상태)
            currentPrefab.transform.GetChild(0).GetComponent<BuildColorSetting>().ColorChange(2);
            currentPrefab.transform.GetChild(0).GetComponent<BuildColorSetting>().IsPlaced = false; // 배치되지 않은 상태로 초기화
        }
        inventoryUI.OnClickExitButton();
        buildColorSetting.IsPlaced = false;
    }

    private void Moving()
    {
        if (IsBuildMode)
        {
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 15f, layerMask) && Vector3.Distance(player.transform.position, hit.point) < 13f)
            {
                if (hit.collider != null)
                {
                    CanPlace = true;
                    currentPrefab.transform.GetChild(0).GetComponent<BuildColorSetting>().ColorChange(2);
                    Debug.Log("배치 가능 상태: " + CanPlace);

                    // 표면의 법선에 맞춰 회전
                    Quaternion baseRotation = Quaternion.LookRotation(-hit.normal);
                    Quaternion additionalRotation = Quaternion.Euler(0, 0, Angle);
                    currentPrefab.transform.rotation = baseRotation * additionalRotation;

                    // 아이템 위치를 감지된 지점으로 이동
                    currentPrefab.transform.position = hit.point;
                }
            }
            else
            {
                CanPlace = false;
                currentPrefab.transform.GetChild(0).GetComponent<BuildColorSetting>().ColorChange(1);
                Debug.Log("배치 불가 상태: " + CanPlace);
            }
        }
    }

    // 아이템 배치 및 회전 (클릭 계속하면 디버그 로그 계속 찍히는 현상 수정 필요)
    private void Place()
    {
        if (CanPlace && Input.GetMouseButtonDown(0))
        {
            IsBuildMode = false;
            currentPrefab.transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            col = currentPrefab.GetComponentInChildren<Collider>();
            col.isTrigger = false;


            // 배치 완료 상태 색상으로 변경
            currentPrefab.transform.GetChild(0).GetComponent<BuildColorSetting>().ColorChange(0);
            currentPrefab.layer = 10;
            currentPrefab.transform.GetChild(0).gameObject.layer = 10;

            buildColorSetting.IsPlaced = true;
            Debug.Log("배치");
        }
        // 우클릭: 아이템 회전
        else if (Input.GetMouseButtonDown(1))
        {
            Angle += 90f;
        }
    }
}
// 버튼과 설치가 동시에 되는 현상 수정 필요 ex) 클릭 설치 => 다른 키
// 빌드 모드 취소 로직 및 잘못 설치한 프리팹 되돌리기 기능 필요
