using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleSystem : MonoBehaviour
{
    ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (particleSystem.isPlaying == false)
            gameObject.SetActive(false);
    }
}
