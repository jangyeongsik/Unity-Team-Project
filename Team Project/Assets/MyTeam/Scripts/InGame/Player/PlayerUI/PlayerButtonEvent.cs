using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerButtonEvent : MonoBehaviour
{
    private bool shopOnOff = false;
    private bool templeOnOff = false;
    private bool talkOnOff = false;
    private bool miniMapOnOff = false;
    private bool dodbogiOnOff = false;
    [SerializeField]
    GameObject vCam;
    GameObject _instance;
    public GameObject dodboji;
    public bool minmapOnOff;

    [SerializeField] GameObject minmapCam;
    [SerializeField] RectTransform playerimg;
    private void Awake()
    {
        _instance = this.gameObject;
        if (GameObject.Find("player") && GameObject.Find("player") == _instance)
        {
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(vCam.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            Destroy(vCam.gameObject);
        }
        UIEventToGame.Instance.minMap += MinMapOnOff;
        GameEventToUI.Instance.dodbogiImgOnOff += DodbogiOnOff;
    }

    private void OnDestroy()
    {
        UIEventToGame.Instance.minMap -= MinMapOnOff;
        GameEventToUI.Instance.dodbogiImgOnOff -= DodbogiOnOff;
    }
    private void Update()
    {
        Vector3 rot = playerimg.rotation.eulerAngles;
        rot = Vector3.zero;
        rot.z = -transform.rotation.eulerAngles.y;
        playerimg.rotation = Quaternion.Euler(rot);
        if (miniMapOnOff)
        {
            //빈칸...
        }

       if (Input.anyKey)
       {
           if (!talkOnOff)
           {
               GotoShopScene();
           }
       }
    }

    void GotoShopScene()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            shopOnOff = !shopOnOff;
            GameEventToUI.Instance.OnEventShopOnOff(shopOnOff);
        }
    }
    public void MinMapOnOff()
    {
        minmapOnOff = !minmapOnOff;
        minmapCam.SetActive(minmapOnOff);
    }

   /* private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            talkOnOff = !talkOnOff;
            GameEventToUI.Instance.OnEventTalkOnOff(talkOnOff, 1000, other.gameObject.name);
        }
    }*/

    public void DodbogiOnOff(bool isOn)
    {
        dodboji.SetActive(isOn);
    }

}
