using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Silhumyoung : MonoBehaviour
{

    Vector3 moveDir;
    Rigidbody Rigidbody;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Move();
    }

    public void OnMove(InputValue inputValue)
    {
        moveDir = inputValue.Get<Vector2>();
  
    }

    private void Move()
    {
        Vector3 n = moveDir.x * transform.right + transform.forward * moveDir.y;
        n *= 3f;
        n.y = Rigidbody.velocity.y;
        Rigidbody.velocity = n;

    }

}
