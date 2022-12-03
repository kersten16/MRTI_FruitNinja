using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour
{

    private ParticleSystem explodeParticleEffect;
    public GameObject bomb;


    private void Awake()
    {
        explodeParticleEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void CreateExplosion()
    {
        bomb.SetActive(false);
        explodeParticleEffect.Play();
        FindObjectOfType<GameManager>().Explode();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CreateExplosion();
        }
    }
}
