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
    float dashSpeed = 20f;

    float x;
    float z;
    Vector3 dir;

    Outline outline;

    public AnimationClip[] ciol;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        controller = GetComponent<CharacterController>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        //나중에 조이스틱 사용할때 주석해제
        //UIEventToGame.Instance.PlayerMove += PlayerJoyMove;
        //UIEventToGame.Instance.PlayerDash += PlayerBtnDash;
    }

    private void Start()
    {
        if(GameData.Instance.player.position == null)
            GameData.Instance.player.position = transform;
        if (GameData.Instance.player.controller == null)
            GameData.Instance.player.controller = controller;
        if (GameData.Instance.player.animator == null)
            GameData.Instance.player.animator = animator;
        if (GameData.Instance.player.overrideController == null)
            GameData.Instance.player.overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

        AnimationClip[] clips = GameData.Instance.player.overrideController.animationClips;
        for(int i = clips.Length-1; i >= 0; --i)
        {
            if(clips[i].name.Contains("Base"))
            {
                GameData.Instance.player.orgList.Add(clips[i]);
                GameData.Instance.player.aniList.Add(clips[i]);
            }
        }
    }

    private void FixedUpdate()
    {
        //방향키 wasd이동
        Move();
        PlayerDash();
    }

    private void Update()
    {
        controller.Move(Vector3.down * GameData.Instance.player.gravity * Time.deltaTime);
        Dash();
        Guard();
        //WallCheck();
    }

    private void OnDestroy()
    {
        //조이스틱사용할때 주석해제
        //UIEventToGame.Instance.PlayerMove -= PlayerJoyMove;
        //UIEventToGame.Instance.PlayerDash -= PlayerBtnDash;
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
        //Transform cmT = Camera.main.transform;
        //dir = cmT.TransformDirection(dir);
        dir.y -= GameData.Instance.player.gravity; 
        dir.Normalize();

        controller.Move(dir * speed * Time.deltaTime);
        dir.y = 0;
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
        if (Input.GetKeyDown(KeyCode.Space) && GameData.Instance.player.m_state != State.PlayerState.P_Dash && GameData.Instance.player.isDashPossible)
        {
            animator.SetTrigger("Dash");
            dashSpeed = 20f;
        }
    }

    void PlayerDash()
    {
        if (GameData.Instance.player.m_state != State.PlayerState.P_Dash) return;
        controller.Move(transform.forward * dashSpeed * Time.deltaTime);
        if (dashSpeed > 0)
            dashSpeed -= 0.5f;
    }

    void PlayerJoyMove(Vector2 direction, float amount)
    {
        if (GameData.Instance.player.m_state != State.PlayerState.P_Run &&
            GameData.Instance.player.m_state != State.PlayerState.P_Idle) return;
        Vector3 dir = new Vector3(direction.x, 0f, direction.y);
        //Transform cmT = Camera.main.transform;
        //dir = cmT.TransformDirection(dir);
        //dir.y = 0;
        dir.Normalize();
        controller.Move(dir * amount * speed * Time.deltaTime);
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
        private void OnTriggerEnter(Collider other)
    {
        if(LayerMask.NameToLayer("Temple") == other.gameObject.layer)
        {
            GameEventToUI.Instance.OnEventInterActionOnOff(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (LayerMask.NameToLayer("Temple") == other.gameObject.layer)
        {
            GameEventToUI.Instance.OnEventInterActionOnOff(false);
        }
    }


    void WallCheck()
    {
        RaycastHit hit;
        Vector3 dir = (transform.position+ Vector3.up) - Camera.main.transform.position;
        Debug.DrawRay(Camera.main.transform.position, dir);
        if(Physics.Raycast(Camera.main.transform.position,dir,out hit,dir.magnitude))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Object"))
                outline.enabled = true;
            else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                outline.enabled = false;   
        }
    }

}
