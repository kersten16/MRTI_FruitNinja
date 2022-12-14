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
   private Color comboColor = new Color(63,12,94);
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

            if(ready){
                blade.bladeTrail.material.color=comboColor;
                ready=false;
                active=true;
                StartCoroutine("StartPulse");
            }
        }
   }

    public void updateScore(int combo){
        counter.text=combo.ToString();
        //change colour with number
        float fade = Mathf.Clamp01(combo/10);
        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(new Color(70,70,70), comboColor, fade);
        if(combo>=10){
            ready=true;;
        }
        ready= false;
    }

    public void Reset(){
        active=false;
        StopAllCoroutines();
        blade.bladeTrail.material.color=Color.white;
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
    }
}