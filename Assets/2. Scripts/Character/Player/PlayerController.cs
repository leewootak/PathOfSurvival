using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerCondition Stamina;

    [Header("Movement")]
    public float moveSpeed; //이동 속도
    public float triggerspeed;
    public float jumpPower;
    private Vector2 curMovementInput; // 현재 이동하는 값
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook; // 최소 시야각
    public float maxXLook; // 최대 시야각
    private float camCurXRot; // 마우스 델타값 저장 공간
    public float lookSensitivity; // 카메라 민감도 dpi
    private Vector2 mouseDelta; // 마우스 변화값

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 제어
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        // 2D 입력값을 3D 에 맞게 변환한것, y 는 플레이어 기준 앞뒤 움직임을 나태냄

        float triggerspeed = 1.0f;

        //// 스테미나가 부족한 경우 속도 감소
        //if (Stamina != null && Stamina.curValue <= 0f)
        //{
        //    triggerspeed *= 0.1f; // 스테미나가 0 이하일 때 90% 느리게
        //}

        // y 좌표가 15.5 이하일 경우 속도 감소
        if (transform.position.y <= 15.5f)
        {
            triggerspeed *= 0.3f; // y 좌표가 15.5 이하일 때 50% 느리게
        }

        // 최종 속도 계산
        float finalSpeed = moveSpeed * triggerspeed;

        // 방향에 최종 속도 적용
        dir *= finalSpeed;
        dir.y = _rigidbody.velocity.y; // y 방향 속도는 그대로 유지

        // Rigidbody의 속도 업데이트
        _rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        // 마우스 움직임의 변화량(mouseDelta)중 y(위 아래)값에 민감도를 곱한다.
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // 최소,최댓값을 넘어가면 최소,최댓값을 받아온다
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // 마우스 움직임의 변화량(mouseDelta)중 x(좌우)값에 민감도를 곱한다.
        // 좌우 회전은 플레이어(transform)를 회전시켜준다.
        // 회전시킨 방향을 기준으로 앞뒤좌우 움직여야하니까.
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed) // 키입력이 유지 되는 동안
        {
            curMovementInput = context.ReadValue<Vector2>(); // 값을 받아와서 저장
        }
        else if (context.phase == InputActionPhase.Canceled) // 키입력이 없어졌다면
        {
            curMovementInput = Vector2.zero; // 멈추도록
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>(); //마우스는 항상 입력이 되므로 값만 읽어 오기
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.3f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.3f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.3f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.3f) + (transform.up * 0.1f), Vector3.down)
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.5f, groundLayerMask))
            { // 현재 Ray가 groundLayerMask에 속하는 오브젝트와 충돌하는지 검사 (거리: 0.5)
                return true;
            }
        }
        return false;
    }
}
