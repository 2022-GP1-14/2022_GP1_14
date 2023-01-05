using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class MatchLogic : MonoBehaviour { 

    static MatchLogic Instance;
    public int maxPoints = 4;
    public Text pointsText;
    public GameObject levelCompleteUI;
    private int points = 0;
    
    void Start()
    {
       // levelCompleteUI.SetActive(false);
        Instance =this;

    }
    void UpdatePotinsText()
    {
        pointsText.text = points + "/" + maxPoints;
        Debug.Log("UpdatePotinsText");//8
                                      //if (points == maxPoints)
                                      //{
                                      //if (MatchItem.counter == 4)
        if (points == maxPoints)
        {
            levelCompleteUI.SetActive(true);
        }
        
       // }
        Debug.Log(points);
    }

   
    public static void AddPoint()
            {
                AddPoints(1);
        Debug.Log("AddPoint");//9
    }

    public static void AddPoints(int points)
    {
         Instance.points += points;
         Instance.UpdatePotinsText(); }
    }