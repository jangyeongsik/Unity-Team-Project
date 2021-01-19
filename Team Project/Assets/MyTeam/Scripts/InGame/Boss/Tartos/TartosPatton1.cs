using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TartosPatton1 : MonoBehaviour
{
    BossData tartos;
    public Animator animator;

    Vector3 boxSize = new Vector3(2, 3, 10);

    private void Start()
    {
        tartos = animator.transform.parent.GetComponent<BossData>();
    }

    public void Start_Skill1_1()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, boxSize * 0.5f, tartos.position.rotation, LayerMask.GetMask("Player"));
        if(colliders.Length >= 1)
        {
            GameEventToUI.Instance.OnPlayerBossHit(tartos.position, 2, tartos.bossState);
        }
        transform.parent.gameObject.SetActive(false);
    }
   
}
