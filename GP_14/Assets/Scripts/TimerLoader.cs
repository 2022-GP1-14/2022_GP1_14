using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerLoader : MonoBehaviour
{
    public string LevelToLoad;
    private float timer = 5f;
    //private Text timerSeconds;



    // Use this for initialization
   /* void Start()
    {
        timerSeconds = GetComponent<Text>();
    }*/

 
    void Update()
    {
        timer -= Time.deltaTime;
        //timerSeconds.text = timer.ToString("f0");
        if (timer <= 0)
        {
            //int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene("LoginScene");
           

        }

    }
}
