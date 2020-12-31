using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    bool isFire = false;
    public Transform target;
    public Transform body;
    Vector3 TargetPos;
    Vector3 direction;

    private void Update()
    {

        transform.Translate(Vector3.forward * 15f * Time.deltaTime);
        /*if (isFire)
        {
            isFire = true;

            // 빗나가지 않게 하기
            //Vector3 dir = (target.transform.position - transform.position).normalized;
            //transform.Translate(dir * 15f * Time.deltaTime);

            // 빗나가게 하기
            transform.Translate(direction * 15f * Time.deltaTime);

            float distanceToTarget = (transform.position - target.transform.position).magnitude;
            
            // 피격
            if (distanceToTarget < 0.2)
            {
                Destroy(gameObject);
                isFire = false;
            }
        }*/

    }

    public void Fire(Transform _target)
    {
        isFire = true;
        target = _target;

        Vector3 v = target.position;

        v.y = 0;

        body.LookAt(v);

        direction = (target.position - body.position).normalized;
    }
}
