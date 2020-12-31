using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController controller;
    Animator animator;

    [SerializeField]
    float speed = 10f;

    [SerializeField]
    float dashSpeed = 30f;

    float x;
    float z;
    Vector3 dir;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        //나중에 조이스틱 사용할때 주석해제
        UIEventToGame.Instance.PlayerMove += PlayerJoyMove;
        UIEventToGame.Instance.PlayerDash += PlayerBtnDash;
    }

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        //방향키 wasd이동
        //Move();
    }

    private void Update()
    {
        //Dash();
        //Guard();
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
        if (GameData.Instance.player.m_state != State.PlayerState.P_Run && 
            GameData.Instance.player.m_state != State.PlayerState.P_Idle) return;
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
        if (x != 0 || z != 0)
        {
            animator.SetBool("isInput", true);
        }
        else
            animator.SetBool("isInput", false);
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameData.Instance.player.m_state != State.PlayerState.P_Dash)
        {
            animator.SetTrigger("Dash");
        }
    }

    void PlayerJoyMove(Vector2 direction, float amount)
    {
        if (GameData.Instance.player.m_state != State.PlayerState.P_Run &&
            GameData.Instance.player.m_state != State.PlayerState.P_Idle) return;
        Vector3 dir = new Vector3(direction.x, 0f, direction.y);
        Transform cmT = Camera.main.transform;
        dir = cmT.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();
        controller.Move(dir * amount * speed * Time.deltaTime);
        transform.LookAt(transform.position + dir);

        if (direction.sqrMagnitude > 0)
        {
            animator.SetBool("isInput", true);
        }
        else
            animator.SetBool("isInput", false);
    }

    void PlayerBtnDash(bool _isDash)
    {
        if (GameData.Instance.player.m_state == State.PlayerState.P_Dash) return;
        animator.SetTrigger("Dash");
    }

    void Guard()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            animator.SetBool("isGuard", true);
        else if (Input.GetKeyUp(KeyCode.Z))
            animator.SetBool("isGuard", false);
    }
}