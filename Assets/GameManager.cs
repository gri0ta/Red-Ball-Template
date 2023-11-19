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

     void Start()
    {
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

    public void Win() //su static galima kviest klases
    {
        currentLevel++;
        Invoke("LoadScene", 1f);
        
    }

    void LoadScene()
    {
        SceneManager.LoadScene(levels[currentLevel]);
    }

    public void Lose()
    {
        hp--;
        if (hp>0)
        {
            Invoke("LoadScene",1f);
        }
        else
        {
            currentLevel = 0;
            Invoke("LoadScene", 1f);
        }
    }
}
