using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PattonThreeDummy : MonoBehaviour
{
    private BossKelgon b_kelgon;

    public bool chargeOn = false;
    float coolTime;

    public GameObject pattonThreeCharge;
    public GameObject pattonThree;
    private void start()
    {
        b_kelgon = GetComponent<BossKelgon>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
