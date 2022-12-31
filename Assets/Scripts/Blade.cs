using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{

    private Camera mainCamera;
    private Collider bladeCollider;
    public TrailRenderer bladeTrail;

    public bool slicing {get; private set;}
    public bool sequence;

    public Vector3 direction {get; private set;}
    public float minSliceVelocity = 0.01f;
    public float sliceForce = 5f;

    Vector3 lastPos;

    private void Awake()
    {
        bladeCollider = GetComponent<Collider>();
        mainCamera=Camera.main;
        //bladeTrail = GetComponentInChildren<TrailRenderer>();
        StartSlicing(); 
        lastPos = transform.position;

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
        /*if( Input.GetMouseButtonDown(0) ) StartSlicing(); 
        else if( Input.GetMouseButtonUp(0) ) StopSlicing(); 
        else if( slicing )*/ ContinueSlicing();
    }

    private Vector3 GetNewPosition()
    {
        //Vector3 imgtgt_position = gameObject.transform.parent.position; //transform.position;
        //newPosition.z = 0f;
        return //mainCamera.WorldToScreenPoint(imgtgt_position);
            transform.position;
    }

    private void StartSlicing()
    {
        
        bladeCollider.enabled = true;
        transform.position = GetNewPosition();
        slicing = true;

        //bladeTrail.enabled=true;
        //bladeTrail.Clear();
        
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        //bladeTrail.enabled=false;
        sequence=false;

    }

    private void ContinueSlicing()
    {
        transform.position = GetNewPosition();
        direction= transform.position - lastPos;
        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;
        lastPos = transform.position;
    }
}
