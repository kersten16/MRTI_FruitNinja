using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public Text scoreText;
    private int score;
    public int combo = 1;
    private Blade blade;
    private Spawner spawn;
    public Image fadeImage;

    private void Awake()
    {
        blade =  FindObjectOfType<Blade>();
        spawn = FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        Time.timeScale = 1f;
        blade.enabled=true;
        spawn.enabled=true;
        score=0;
        scoreText.text = score.ToString();

        ClearScene();
    }

    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach( Fruit fruit in fruits){
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach( Bomb bomb in bombs){
            Destroy(bomb.gameObject);
        }
    }

    public void IncreaseScore(int points)
    {
        //Following code enables combo scoring for multiple fruit hit in the same strike. Disable for now
        // if(blade.sequence){
        //     combo= Math.Min(combo+1,5);
        // }else{
        //     combo=1;
        // }


        score+=(points*combo);
        scoreText.text = score.ToString();

    }

    public void Explode()
    {
        blade.enabled=false;
        spawn.enabled=false;
        StartCoroutine(ExplodeSequence());
    }

    private IEnumerator ExplodeSequence()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        float elapsed = 0f;
        float duration = 0.5f;
        while( elapsed<duration)
        {
            float fade = Mathf.Clamp01(elapsed/duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, fade);

            Time.timeScale = 1f - fade;
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        NewGame();
        elapsed =0f;
        while( elapsed<duration)
        {
            float fade = Mathf.Clamp01(elapsed/duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear,  fade);

            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
