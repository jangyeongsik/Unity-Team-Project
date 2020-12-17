using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IsoMetric : MonoBehaviour
{
    [SerializeField] private float offsetY = 4.0f;
    public GameObject player;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + offsetY, player.transform.position.z);
    }
}
