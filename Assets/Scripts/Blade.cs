using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{

    private Camera mainCamera;
    private Collider bladeCollider;
    private TrailRenderer bladeTrail;

    public bool slicing {get; private set;}
    public bool sequence;

    public Vector3 direction {get; private set;}
    public float minSliceVelocity = 0.01f;
    public float sliceForce = 5f;

    private void Awake()
    {
        bladeCollider = GetComponent<Collider>();
        mainCamera=Camera.main;
        bladeTrail = GetComponentInChildren<TrailRenderer>();
        

    }

    private void OnEnable()
    {
        StopSlicing();
    }

     private void OnDisable()
    {
        StopSlicing();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)){

            StartSlicing();

        } else if( Input.GetMouseButtonUp(0)){
            StopSlicing();

        } else if (slicing){
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        
        bladeCollider.enabled=true;
        
        Vector3 newPosition= mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.z * -1));
        newPosition.z = 0f;

        transform.position = newPosition;
        slicing =true;

        bladeTrail.enabled=true;
        bladeTrail.Clear();
        
    }

    private void StopSlicing()
    {
        slicing=false;
        bladeCollider.enabled=false;
        bladeTrail.enabled=false;
        sequence=false;

    }

    private void ContinueSlicing()
    {
        Vector3 newPosition= mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.transform.position.z * -1));
        newPosition.z = 0f;

        direction= newPosition-transform.position;

        float velocity = direction.magnitude / Time.deltaTime;

        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;

    }
}
