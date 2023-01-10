using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class MatchLogic : MonoBehaviour
{

    static MatchLogic Instance;
    public int maxPoints = 4;
    public Text pointsText;
    public GameObject levelCompleteUI;
    int points = 0;
    static int c = 0;
    void Start()
    {
        // levelCompleteUI.SetActive(false);
        Instance = this;

    }
    void UpdatePotinsText()
    {

        pointsText.text = points + "/" + maxPoints;

        //Debug.Log("UpdatePotinsText");//8


        Debug.Log("points is :" + points);
    }

    public static void cal()
    {
        c++;
        Debug.Log(" sec counter is :" + c);

        Instance.write(c);

    }
    public void write(int c)
    {
        if (c % 4 == 0)
        {
            levelCompleteUI.SetActive(true);

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