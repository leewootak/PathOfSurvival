using UnityEngine;

public class Build_Prefabs : MonoBehaviour
{
    public Ray ray;

    [SerializeField] private Material Red;
    [SerializeField] private Material Green;
    [SerializeField] private CraftTable craftTable;
    private LayerMask layerMask;
    private Material[] materials;
    private MeshRenderer meshRenderer;
    public bool IsBatchComplete = false;


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        // 현재 메시 렌더러의 머티리얼들을 복사하여 배열에 저장
        materials = new Material[meshRenderer.materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = meshRenderer.materials[i];
        }

        layerMask = LayerMask.GetMask("ground", "Wall");
        craftTable = FindAnyObjectByType<CraftTable>();
    }

    private void OnTriggerStay(Collider other)
    {
        // 충돌한 오브젝트가 그라운드 레이어가 아니라면
        if (other.gameObject.layer != LayerMask.NameToLayer("ground") && other.gameObject.layer != LayerMask.NameToLayer("Wall"))
        {
            // 배치 불가능 상태 머티리얼
            if (!IsBatchComplete)
                ColorChange(1);


            // CreftTable의 배치 가능 여부를 false로 설정
            craftTable.CanPlace = false;
            Debug.Log("배치 불가능");
        }
    }

    public void ColorChange(int index)
    {
        // 원본 머티리얼 배열의 길이와 같은 새 배열 생성
        Material[] NewMaterialArray = new Material[materials.Length];

        // 모든 머티리얼을 인덱스에 해당하는 머티리얼로 변경
        for (int i = 0; i < NewMaterialArray.Length; i++)
        {
            NewMaterialArray[i] = materials[index];
        }
        // 메시 렌더러에 새로운 머티리얼 배열 적용
        meshRenderer.materials = NewMaterialArray;
    }
}
