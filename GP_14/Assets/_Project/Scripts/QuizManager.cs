using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public UnityEvent levelCompleteUI;
    public GameObject applee;
    public GameObject Panel;
    public GameObject GoPanel;

    public GameObject appleee;
    public GameObject appleeee;
    public GameObject appleeeee;
    public Text QuestionTxt;
    public Text ScoreTxt;



    int tolalQuestions = 0;
    public int score;
    public List<Star> stars = new List<Star>();
    private void Start()
    {

        Panel.SetActive(true);
        GoPanel.SetActive(false);
        tolalQuestions = QnA.Count;
        applee.SetActive(false);
        appleee.SetActive(false);
        appleeee.SetActive(false);
        appleeeee.SetActive(false);
        generateQuestion();
        for (int i = tolalQuestions; i < stars.Count; i++)
        {
            stars[i].star.gameObject.SetActive(false);
        }
    }
    public IEnumerator Fillstars()
    {
        for (int i = 0; i < score; i++)
        {
            yield return StartCoroutine(stars[i].Fill(1));
        }
    }
    public void retry()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public string level;
    public LessonName lessonName;
    void GameOver()
    {
        levelCompleteUI.Invoke();
        PlayerPrefs.SetInt(level+ DateTime.Now.ToString("dddd"), score);
        UserData.instance.SaveLesson(new Lesson() { degree = score }, lessonName);

        print(level + DateTime.Now.ToString("dddd")+" :"+score);
        ScoreTxt.text = score + "/" + tolalQuestions;
        StartCoroutine(Fillstars());
        Panel.SetActive(false);
        GoPanel.SetActive(true);
    }

    public void correct()
    {
        //when you answer correct
        score += 1;
        QnA.RemoveAt(currentQuestion);
        StartCoroutine(WaitForNext());
    }
    public void wrong()
    {
        //when you answer wrong
        QnA.RemoveAt(currentQuestion);
        StartCoroutine(WaitForNext());
    }

    IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(1);
        generateQuestion();
    }


    void SetAnswers()
    {  
           
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().startColor;
            //Debug.Log(QnA[currentQuestion]);
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];
         
          
   //Debug.Log(options[i].GetComponent<AnswerScript>().isCorrect);
            
            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
             
            
            }
           
        }
    }
    public void generateQuestion()
    {
        if (QnA.Count > 0)
        {
            
              // Debug.Log(QnA.Count);
            currentQuestion = UnityEngine.Random.Range(0, QnA.Count);
           // Debug.Log("currentQuestion"+currentQuestion);
           
            QuestionTxt.text = QnA[currentQuestion].Question;
            //Audio listenier is generated here
             Debug.Log("opt");
              Debug.Log(QnA[currentQuestion].Answers[0]);
               Debug.Log(QnA[currentQuestion].Answers[1]);
   
        if((QnA[currentQuestion].Answers[0] == "1" &&QnA[currentQuestion].Answers[1] == "2") ||( QnA[currentQuestion].Answers[1] == "1" &&QnA[currentQuestion].Answers[0] == "2" )){
            applee.SetActive(false);

             appleee.SetActive(false);
        }
         if((QnA[currentQuestion].Answers[0] == "3" &&QnA[currentQuestion].Answers[1] == "2" )|| (QnA[currentQuestion].Answers[1] == "3" &&QnA[currentQuestion].Answers[0] == "2") ){
            applee.SetActive(true);

             appleee.SetActive(false);
        }

          if((QnA[currentQuestion].Answers[0] == "1" &&QnA[currentQuestion].Answers[1] == "3") ||( QnA[currentQuestion].Answers[1] == "1" &&QnA[currentQuestion].Answers[0] == "3") ){
            applee.SetActive(true);
            appleee.SetActive(true);
        }
        ///////////////////////////////////////
         if((QnA[currentQuestion].Answers[0] == "4" &&QnA[currentQuestion].Answers[1] == "1") ||( QnA[currentQuestion].Answers[1] == "4" &&QnA[currentQuestion].Answers[0] == "1" )){
            applee.SetActive(true);
            appleee.SetActive(true);
            appleeee.SetActive(true);
            appleeeee.SetActive(false);
        }
         if((QnA[currentQuestion].Answers[0] == "5" &&QnA[currentQuestion].Answers[1] == "2") ||( QnA[currentQuestion].Answers[1] == "5" &&QnA[currentQuestion].Answers[0] == "2" )){
            applee.SetActive(true);
            appleee.SetActive(true);
            appleeee.SetActive(true);
            appleeeee.SetActive(true);
        }
         
            SetAnswers();
        }
        else
        {
            Debug.Log("Out of Questions");
            GameOver();
        }
    }
}
