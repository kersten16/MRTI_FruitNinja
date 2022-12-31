using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] fruitPrefabs;

    public GameObject bombPrefab;

    public bool isVisible = false;
    
    [Range(0f,1f)]
    public float bombChance = 0.05f;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -20;
    public float maxAngle = 20;

    public float minForce = 18;
    public float maxForce = 25;

    public float maxLifetime = 5f;

    public Vector3 spawnBoundsSize;

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    /*
    private void Start()
    {
         StartCoroutine(Spawn());
    }
    */
    
     private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        //two second delay on new level before spawn
        yield return new WaitForSeconds(2f);

        while (enabled)
        {
            Bounds spawnBounds = new Bounds(transform.position, spawnBoundsSize);
            GameObject prefab= fruitPrefabs[Random.Range(0,fruitPrefabs.Length)];

            if(Random.value < bombChance)
            {
                prefab=bombPrefab;
            }

            Vector3 position= new Vector3();
            Debug.Log(spawnBounds); 
            Vector3 min = spawnBounds.min;
            Vector3 max = spawnBounds.max;
            position.x = Random.Range(min.x, max.x);
            position.y = Random.Range(min.y, max.y);
            position.z = Random.Range(min.z, max.z);

            // random rotation but always towards 'up' of the parent
            Quaternion rotation = transform.parent.rotation * Quaternion.Euler(Random.Range(minAngle, maxAngle), 0f, Random.Range(minAngle,maxAngle));
            

            GameObject fruit = Instantiate(prefab, position, rotation);
            fruit.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);

            Destroy(fruit, maxLifetime);

            float force= Random.Range(minForce,maxForce) / 10;
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay,maxSpawnDelay));
        }
    }
}
