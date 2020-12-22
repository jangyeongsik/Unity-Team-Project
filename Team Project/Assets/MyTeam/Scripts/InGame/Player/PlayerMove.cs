using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController controller;
    public Animator animator;

    [SerializeField]
    float speed = 10f;

    [SerializeField]
    float dashSpeed = 30f;
    Vector3 startPos;
    bool isDash;

    float x;
    float z;
    Vector3 dir;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        //나중에 조이스틱 사용할때 주석해제
        UIEventToGame.Instance.PlayerMove += PlayerJoyMove;
        UIEventToGame.Instance.PlayerDash += PlayerBtnDash;
    }

    private void Start()
    {
        isDash = false;
    }

    private void FixedUpdate()
    {
        //방향키 wasd이동
        Move();
        GameEventToUI.Instance.OnFollowPlayerUI(Camera.main.WorldToScreenPoint(transform.position));
    }

    private void Update()
    {
        Dash();
    }

    private void OnDestroy()
    {
        //조이스틱사용할때 주석해제
        UIEventToGame.Instance.PlayerMove -= PlayerJoyMove;
        UIEventToGame.Instance.PlayerDash -= PlayerBtnDash;
    }

    void Move()
    {
        //대쉬중이면 못움직임
        if (isDash) return;
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        dir = new Vector3(x, 0, z).normalized;
        //카메라 방향으로 변환
        Transform cmT = Camera.main.transform;
        dir = cmT.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();

        controller.Move(dir * speed * Time.deltaTime);

        transform.LookAt(transform.position + dir);
        if (x > 0 || z > 0)
        {
            animator.SetBool("isInput", true);
            Debug.Log('a');
        }
        else
            animator.SetBool("isInput", false);
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDash)
        {
            isDash = true;
            startPos = transform.position;
        }
        if (isDash)
        {
            controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            float d = Vector3.Distance(startPos, transform.position);
            if (d > 3)
                isDash = false;
        }
    }

    void PlayerJoyMove(Vector2 direction, float amount)
    {
        Vector3 dir = new Vector3(direction.x, 0f, direction.y);
        Transform cmT = Camera.main.transform;
        dir = cmT.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();
        controller.Move(dir * amount * speed * Time.deltaTime);
        transform.LookAt(transform.position + dir);
    }

    void PlayerBtnDash(bool _isDash)
    {
        if (isDash) return;
        isDash = _isDash;
        startPos = transform.position;

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

    }

}