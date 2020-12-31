using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestMove : MonoBehaviour
{
    public GameObject bullet;
    public GameObject player;
    float speed = 5;

    bool isUp = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fire());
        StartCoroutine(move());
    }

    // Update is called once per frame
    void Update()
    {
        if(isUp)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * -speed * Time.deltaTime);
        }
    }

    IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            GameObject obj = Instantiate(bullet);
            obj.transform.position = transform.position;
            obj.transform.LookAt(player.transform);
            obj.GetComponent<EnemyTestBullet>().enemyTR = transform;
            obj.GetComponent<EnemyTestBullet>().start = transform.position;
        }
    }

    IEnumerator move()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            isUp = isUp ? false : true;
        }
    }
}
