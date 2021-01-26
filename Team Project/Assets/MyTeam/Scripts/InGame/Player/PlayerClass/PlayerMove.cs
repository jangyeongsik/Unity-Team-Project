using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController controller;
    Animator animator;

    float speed = 7f;

    float minus = 0.3f;
    float startDashSpeed = 15f;
    float dashSpeed;

    float x;
    float z;
    Vector3 dir;


    public AnimationClip[] ciol;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = transform.GetChild(0).GetComponent<Animator>();
       
        animator.enabled = false;
    }

    private void Start()
    {
        if(GameData.Instance.player.position == null)
            GameData.Instance.player.position = transform;
        if (GameData.Instance.player.controller == null)
            GameData.Instance.player.controller = controller;
        if (GameData.Instance.player.animator == null)
            GameData.Instance.player.animator = animator;
        

        //나중에 조이스틱 사용할때 주석해제
        UIEventToGame.Instance.PlayerMove += PlayerJoyMove;
        UIEventToGame.Instance.PlayerDash += PlayerBtnDash;
    }

    private void FixedUpdate()
    {
        //방향키 wasd이동
        //Move();
        PlayerDash();
        controller.Move(Vector3.down * GameData.Instance.player.gravity * Time.deltaTime);
        if(GameData.Instance.player.currentHp <= 0 && GameData.Instance.player.m_state != State.PlayerState.P_Dead && GameData.Instance.player.curSceneName != "")
        {
            animator.SetTrigger("Dead");
            GameData.Instance.player.m_state = State.PlayerState.P_Dead;
        }
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
        //dir.y -= GameData.Instance.player.gravity; 
        dir.Normalize();

        controller.Move(dir * 40 * Time.deltaTime);
        dir.y = 0;
        transform.LookAt(transform.position + dir);
        if (x != 0 || z != 0)
        {
            animator.SetBool("isInput", true);
        }
        else
            animator.SetBool("isInput", false);
    }

    void PlayerDash()
    {
        if (GameData.Instance.player.m_state != State.PlayerState.P_Dash) return;
        Vector3 dir = transform.forward;
        //dir.y -= GameData.Instance.player.gravity;
        controller.Move(dir * dashSpeed * Time.deltaTime);
        if (dashSpeed > 0)
            dashSpeed -= minus;
    }

    void PlayerJoyMove(Vector2 direction, float amount)
    {
        if (GameData.Instance.player.m_state != State.PlayerState.P_Run &&
            GameData.Instance.player.m_state != State.PlayerState.P_Idle) return;
        float mSpeed = speed + GameData.Instance.player.movespeed * 0.5f;
        Vector3 dir = new Vector3(direction.x, 0f, direction.y);
        dir.Normalize();
        controller.Move(dir * amount * mSpeed * Time.deltaTime);
        transform.LookAt(transform.position + dir);

        if (amount > 0)
        {
            animator.SetBool("isInput", true);
        }
        else
            animator.SetBool("isInput", false);
    }

    void PlayerBtnDash(bool _isDash)
    {
        if (GameData.Instance.player.m_state == State.PlayerState.P_Dash ||
            !GameData.Instance.player.isDashPossible) return;
        animator.SetTrigger("Dash");
        dashSpeed = startDashSpeed + GameData.Instance.player.movespeed;
        minus = 0.3f + GameData.Instance.player.movespeed * 0.03f;
    }
}
