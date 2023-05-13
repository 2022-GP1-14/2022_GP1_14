using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MatchLogic : MonoBehaviour
{

    static MatchLogic Instance;
    public int maxPoints = 4;
    public Text pointsText;
    public GameObject Panel, Gopanel;
    public UnityEvent levelCompleteUI;
    int points = 0;
    static int c = 0;
    public string Level;
    public List<Star> stars = new List<Star>();
    void Start()
    {
        Panel.SetActive(true);
        Gopanel.SetActive(false);
        Instance = this;
       for(int i = maxPoints; i < stars.Count; i++)
        {
            stars[i].star.gameObject.SetActive(false);
        }

    }
    void UpdatePotinsText()
    {

        pointsText.text = points + "/" + maxPoints;


    }

    public static void cal()
    {
        c++;


        Instance.write(c);

    }
    public LessonName lessonName;
    public void write(int c)
    {
        if (c % 4 == 0)
        {
            PlayerPrefs.SetInt(Level+ DateTime.Now.ToString("dddd"), points);
            UserData.instance.SaveLesson(new Lesson() { degree = points }, lessonName);

            print(Level + DateTime.Now.ToString("dddd") + "   : " + points);
                levelCompleteUI.Invoke();
                Panel.SetActive(false);
                Gopanel.SetActive(true);

            StartCoroutine(Fillstars());
        }

    }
    public IEnumerator Fillstars()
    {
        for (int i = 0; i < points; i++)
        {
            yield return StartCoroutine(stars[i].Fill(1));
         }
    }
    public static void AddPoint()
    {
        AddPoints(1);
        //Debug.Log("AddPoint");//9
    }

    public static void AddPoints(int points)
    {
        Instance.points += points;
        Instance.UpdatePotinsText();
    }
}