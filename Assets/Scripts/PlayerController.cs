using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]  //헤더를 적어줘서 타이틀을 만들어줌 (구분)
    public float moveSpeed;  //스피드
    private Vector2 curMovementInput;  //인풋액션에서 받아온 값을 넣어줄 곳
    public float jumpPower;
    public LayerMask groundLayerMask;


    [HideInInspector]
    public bool canLook = true;

    private Rigidbody _rigidbody;  //리지드바디 받아옴

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();  //리지드바디 받아옴
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  //마우스 모양 숨기기 (커서 숨기기)
    }

    private void FixedUpdate()
    {
        Move();
    }

  

    public void OnMoveInput(InputAction.CallbackContext context)  // context 현재상태 받아오기
    {
        if (context.phase == InputActionPhase.Performed) // Phase 분기점 - Started (키가 눌렸을때)는 한번만 작동해서 Performed로 바꿔줌
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

   

    private void Move()  //캐릭터 실제 이동
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;  //w a s d 움직임 상 하 좌 우
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;  // y값 초기화

        _rigidbody.velocity = dir;  //방향값
    }



    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }


}
