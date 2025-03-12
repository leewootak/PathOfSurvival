using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed; //이동 속도
    private Vector2 curMovementInput; // 현재 이동하는 값

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
    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) // 키입력이 시작 되었을때
        {
            curMovementInput = context.ReadValue<Vector2>(); // 값을 받아와서 저장
        }
        else if (context.phase == InputActionPhase.Canceled) // 키입력이 없어졌다면
        {
            curMovementInput = Vector2.zero; // 멈추도록
        }
    }
}
