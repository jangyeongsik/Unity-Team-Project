using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoving : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float range = 3.0f;

    Vector3 forward, right;

    private bool shopOnOff = false;
    private bool talkOnOff = false;



    private void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0f;
        forward = Vector3.Normalize(forward);

        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        SceneManager.LoadScene("ShopScene", LoadSceneMode.Additive);
    }

    private void Update()
    {
        if(Input.anyKey)
        {
            if (!talkOnOff)
            {
                Move();
                GotoShopScene();
            }
        }
        FindTarget();
    }

    void Move()
    {
        Vector3 RightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 ForwardMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 FinalMovement = RightMovement + ForwardMovement;

        Vector3 direction = Vector3.Normalize(FinalMovement);

        if(direction != Vector3.zero)
        {
            transform.forward = direction;
            transform.position += FinalMovement;
        }
    }

    void GotoShopScene()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            shopOnOff = !shopOnOff;

            BetweenPlayerAndShop.Instance.OnEventShopOnOff(shopOnOff);
        }
    }

    public void FindTarget()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
        {
            if(hit.collider.gameObject.CompareTag("Npc"))
            {
                if(Input.GetKeyDown(KeyCode.F))
                {
                    talkOnOff = !talkOnOff;
                    BetweenPlayerAndShop.Instance.OnEventTalkOnOff(talkOnOff, 1001, hit.collider.gameObject.name);
                }
            }
        }
        
    }
}
