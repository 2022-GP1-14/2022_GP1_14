using RTLTMPro;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public List<string> DayNames;
    public RTLTextMeshPro Date;
    public RTLTextMeshPro numberLessons;
    public Image FillLessons;
    float lessons;
    public List<Image> connectBars1, connectBars2, chooseBars1, chooseBars2;
    void Start()
    {
        ScoreData data=new ScoreData();
        Date.text = DateTime.Now.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE"));
        UserData.instance.getScore((a) =>
        {
            data = a;
        });
        lessons += UserData.instance.scoreData.Numbers;
        lessons += UserData.instance.scoreData.Letters;
        FillLessons.fillAmount =(float) lessons / 2;
        for (int i = 0; i < UserData.instance.scoreData.Days.Length; i++)
        {
            print("connectBars1" + UserData.instance.scoreData.Days[i].dayName + ":" + UserData.instance.scoreData.Days[i].ConnectLevel1.degree
                + "\n connectBars2" + UserData.instance.scoreData.Days[i].dayName + ":" + UserData.instance.scoreData.Days[i].ConnectLevel2.degree
                + "\n chooseBars1" + UserData.instance.scoreData.Days[i].dayName + ":" + UserData.instance.scoreData.Days[i].ChooseLevel1.degree
                + "\n chooseBars2" + UserData.instance.scoreData.Days[i].dayName + ":" + UserData.instance.scoreData.Days[i].ChooseLevel2.degree);
            connectBars1[i].fillAmount = (float)UserData.instance.scoreData.Days[i].ConnectLevel1.degree / 5;
            connectBars2[i].fillAmount = (float)UserData.instance.scoreData.Days[i].ConnectLevel2.degree / 5;
            chooseBars1[i].fillAmount = (float)UserData.instance.scoreData.Days[i].ChooseLevel1.degree / 5;
            chooseBars2[i].fillAmount = (float)UserData.instance.scoreData.Days[i].ChooseLevel2.degree / 5;
        }
        numberLessons.text = lessons.ToString() + "/2";
        //lessons += PlayerPrefs.GetInt("Numbers", 0);
        //lessons += PlayerPrefs.GetInt("Letters", 0);


        //for (int i = 0; i < 7; i++)
        //{
        //    print("connectBars1"+ DayNames[i] +":" + PlayerPrefs.GetInt("ConnectLevel1" + DayNames[i], 0) 
        //        + "\n connectBars2" + DayNames[i] + ":" + PlayerPrefs.GetInt("ConnectLevel2" + DayNames[i], 0) 
        //        + "\n chooseBars1" + DayNames[i] + ":" + PlayerPrefs.GetInt("ChooseLevel1" + DayNames[i], 0) 
        //        + "\n chooseBars2" + DayNames[i] + ":" + PlayerPrefs.GetInt("ChooseLevel2" + DayNames[i], 0) );
        //    connectBars1[i].fillAmount= (float)PlayerPrefs.GetInt("ConnectLevel1" + DayNames[i], 0)/5;
        //    connectBars2[i].fillAmount = (float)PlayerPrefs.GetInt("ConnectLevel2" + DayNames[i], 0)/5;
        //    chooseBars1[i].fillAmount = (float)PlayerPrefs.GetInt("ChooseLevel1" + DayNames[i], 0)/5;
        //    chooseBars2[i].fillAmount = (float)PlayerPrefs.GetInt("ChooseLevel2" + DayNames[i], 0)/5;
        //}

    }
    
}
