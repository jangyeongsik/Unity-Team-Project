using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFont : MonoBehaviour
{
    //좌표설정해주는것만 gametoui에서 처리하면댐
    private Text damageTxt;
    public int damage;

    [SerializeField]
    float upVelocity;
    [SerializeField, Range(0,1)]
    float gravity;

    float vel;

    private void Awake()
    {
        damageTxt = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        vel -= gravity;
        transform.Translate(Vector3.up * vel * Time.deltaTime);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            transform.position = Input.mousePosition;
            damageTxt.text = Random.Range(100, 500).ToString();
            vel = upVelocity;
        }
    }
}
