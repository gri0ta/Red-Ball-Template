using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int hp;
    public int currentLevel;
    public List<string> levels;
    public static GameManager instance;
    AudioSource source;

    public AudioClip winSound;
    public AudioClip loseSound;
    //public AudioClip backgroundMusic;
    public AudioClip gameOver;

    public Transform transition;
    Vector3 targetScale;

     void Awake()
    {
        source = GetComponent<AudioSource>();

        if (instance ==null)
        {
            instance = this; 
            DontDestroyOnLoad(this); //this yra GameManager
        }
        else
        {
            Destroy(this);//if another singleton instance is found it will die
        }
        
    }
   void Update()
    {
        transition.localScale = Vector3.MoveTowards(transition.localScale, targetScale, 60 * Time.deltaTime);
    }

    public void Win() //su static galima kviest klases
    {
        source.PlayOneShot(winSound);
        currentLevel++;
        Invoke("LoadScene", 1f);
        targetScale = Vector3.one * 30;
    }

    void LoadScene()
    {
        targetScale = Vector3.zero;
        SceneManager.LoadScene(levels[currentLevel]);
    }

    public void Lose()
    {
        targetScale = Vector3.one * 30f;
        hp--;
        if (hp>0)
        {
            Invoke("LoadScene",1f);
            source.PlayOneShot(loseSound);
        }
        else
        {
            currentLevel = 0;
            Invoke("LoadScene", 1f);
            source.PlayOneShot(gameOver);
            hp = 3;
        }
    }
}
