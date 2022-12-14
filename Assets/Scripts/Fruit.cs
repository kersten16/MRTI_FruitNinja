using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
   //switch between Whole and sliced

    public GameObject Whole;
    public GameObject Sliced;
    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceParticleEffect;

    public int points =1;


   private void Awake()
   {
    fruitCollider=GetComponent<Collider>();
    fruitRigidbody=GetComponent<Rigidbody>();
    juiceParticleEffect = GetComponentInChildren<ParticleSystem>();

   }

   private void Slice(Vector3 direction,Vector3 position, float force)
   {
        FindObjectOfType<GameManager>().IncreaseScore(points);

        Whole.SetActive(false);
        Sliced.SetActive(true);
        fruitCollider.enabled=false;

        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        Sliced.transform.rotation= Quaternion.Euler(0f,0f,angle);

        Rigidbody[] slices = Sliced.GetComponentsInChildren<Rigidbody>();

        foreach ( Rigidbody slice in slices)
        {
            slice.velocity=fruitRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
        juiceParticleEffect.Play();
   }

   private void OnTriggerEnter(Collider other)
   {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Sliced");
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
            blade.sequence=true;
        }
   }
}
