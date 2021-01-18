using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invenCheck : MonoBehaviour
{
    BoxCollider collider;


    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        //조건 완료 후 박스 콜리이더 트리거 체크

        collider.isTrigger = true;
    }

}
