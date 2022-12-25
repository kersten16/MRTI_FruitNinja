using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
   public Text counter;
   private bool ready=false;
   public bool active=false;
   private Color comboColor = new Color32(63, 12, 94, 255);
   private Collider bonusCollider;
   private Rigidbody bonusRigidBody;
   private Blade blade;

    private void Awake()
    {
        bonusCollider = GetComponent<Collider>();
        bonusRigidBody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            blade = other.GetComponent<Blade>();

            if(ready)
            {
                blade.bladeTrail.material.color = comboColor;
                ready=false;
                active=true;
                StartCoroutine(StartPulse());
            }
        }
    }

    public void updateScore(int combo)
    {
        counter.text=combo.ToString();
        //change colour with number
        float fade = Mathf.Clamp01(combo/10.0f);
        GetComponent<Renderer>().material.color = Color32.Lerp(new Color32(70, 70, 70, 255), comboColor, fade);
        ready = (combo >= 10);
    }

    public void Reset(){
        active=false;
        StopAllCoroutines();
        if(blade) blade.bladeTrail.material.color = Color.white;
        updateScore(0);
    }

    private IEnumerator StartPulse(){
        for (float i=0f; i<=1f; i+=0.1f){
            transform.localScale=new Vector3(
                (Mathf.Lerp(transform.localScale.x,transform.localScale.x+0.025f, Mathf.SmoothStep(0f,1f,i))),
                (Mathf.Lerp(transform.localScale.y,transform.localScale.y+0.025f, Mathf.SmoothStep(0f,1f,i))),
                (Mathf.Lerp(transform.localScale.z,transform.localScale.z+0.025f, Mathf.SmoothStep(0f,1f,i)))
            );
            yield return new WaitForSeconds(0.015f);
        }
        for (float i=0f; i<=1f; i+=0.1f){
            transform.localScale=new Vector3(
                (Mathf.Lerp(transform.localScale.x,transform.localScale.x-0.025f, Mathf.SmoothStep(0f,1f,i))),
                (Mathf.Lerp(transform.localScale.y,transform.localScale.y-0.025f, Mathf.SmoothStep(0f,1f,i))),
                (Mathf.Lerp(transform.localScale.z,transform.localScale.z-0.025f, Mathf.SmoothStep(0f,1f,i)))
            );
            yield return new WaitForSeconds(0.015f);
        }
        // reset color here ?
    }
}
