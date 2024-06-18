using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]  //����� �����༭ Ÿ��Ʋ�� ������� (����)
    public float moveSpeed;  //���ǵ�
    private Vector2 curMovementInput;  //��ǲ�׼ǿ��� �޾ƿ� ���� �־��� ��
    public float jumpPower;
    public LayerMask groundLayerMask;


    [HideInInspector]
    public bool canLook = true;

    private Rigidbody _rigidbody;  //������ٵ� �޾ƿ�

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();  //������ٵ� �޾ƿ�
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  //���콺 ��� ����� (Ŀ�� �����)
    }

    private void FixedUpdate()
    {
        Move();
    }

  

    public void OnMoveInput(InputAction.CallbackContext context)  // context ������� �޾ƿ���
    {
        if (context.phase == InputActionPhase.Performed) // Phase �б��� - Started (Ű�� ��������)�� �ѹ��� �۵��ؼ� Performed�� �ٲ���
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

   

    private void Move()  //ĳ���� ���� �̵�
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;  //w a s d ������ �� �� �� ��
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;  // y�� �ʱ�ȭ

        _rigidbody.velocity = dir;  //���Ⱚ
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
