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
        if (b_kelgon.isPattonThreeCharge)
        {
            pattonThree.SetActive(true);
            pattonThreeCharge.SetActive(true);
            coolTime += Time.deltaTime;
            if (pattonThreeCharge.transform.localScale.x < 1.0f)
            {
                pattonThreeCharge.transform.localScale = new Vector3(0.3f * coolTime, 0.3f * coolTime, 0.1f);
            }
            else if (pattonThreeCharge.transform.localScale.x > 1)
            {
                chargeOn = true;
            }
        }
    }
}
