using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Manager : MonoBehaviour
{
    //do not forget to write the distance between the object and its shape in the document
    public GameObject A, B, C, D, ADark, BDark, CDark, DDark, GoPanel, panel;

    Vector2 AInitialPos, BInitialPos, CInitialPos, DInitialPos;

    public int score = 0;
    public Text ScoreTxt;

    public AudioSource source;
    public AudioClip[] correct;
    public AudioClip incorrect;

    bool ABool, BBool, CBool, DBool = false;
    bool AABool, BBBool, CCBool, DDBool = false;

    void Start()
    {
        panel.SetActive(true);
        GoPanel.SetActive(false);
        AInitialPos = A.transform.position;
        BInitialPos = B.transform.position;
        CInitialPos = C.transform.position;
        DInitialPos = D.transform.position;
    }

    public void DragA()
    {
        A.transform.position = Input.mousePosition;
    }
    public void DragB()
    {
        B.transform.position = Input.mousePosition;
    }
    public void DragC()
    {
        C.transform.position = Input.mousePosition;
    }
    public void DragD()
    {
        D.transform.position = Input.mousePosition;
    }


    public void DropA()
    {

        float distance = Vector3.Distance(A.transform.position, ADark.transform.position);
        if (distance < 300)
        {
            A.transform.position = ADark.transform.position;
            //Score.scoreNumber += 1;
            //carrotBool = true;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
            score += 1;
            ABool = true;
        }
        else
        {
            A.transform.position = AInitialPos;
            source.clip = incorrect;
            source.Play();
            AABool = true;
            A.SetActive(false);

        }
    }
    public void DropB()
    {

        float distance = Vector3.Distance(B.transform.position, BDark.transform.position);
        if (distance < 300)
        {
            B.transform.position = BDark.transform.position;
            //Score.scoreNumber += 1;
            
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
            score += 1;
            BBool = true;
        }
        else
        {
            B.transform.position = BInitialPos;
            source.clip = incorrect;
            source.Play();
            B.SetActive(false);
            BBBool = true;
        }
    }
    public void DropC()
    {

        float distance = Vector3.Distance(C.transform.position, CDark.transform.position);
        if (distance < 300)
        {
            C.transform.position = CDark.transform.position;
            //Score.scoreNumber += 1;
            CBool = true;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
            score += 1;
            
        }
        else
        {
            C.transform.position = CInitialPos;
            source.clip = incorrect;
            source.Play();
            C.SetActive(false);
            CCBool = true;
        }
    }
    public void DropD()
    {

        float distance = Vector3.Distance(D.transform.position, DDark.transform.position);
        if (distance < 300)
        {
            D.transform.position = DDark.transform.position;
            //Score.scoreNumber += 1;
            DBool = true;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
            score += 1;
        }
        else
        {
            D.transform.position = DInitialPos;
            source.clip = incorrect;
            source.Play();
            D.SetActive(false);
            DDBool = true;
        }
    }
    void Update()
    {
        //if (ABool  || Timer.time <= 0)
        if ((ABool || AABool) && (BBool || BBBool) && (CBool || CCBool) && (DBool || DDBool))
        {
            // if (ABool && BBool && CBool && DBool)

            GoPanel.SetActive(true);
            panel.SetActive(false);
            Debug.Log(score);
            ScoreTxt.text = score + "/" + 4;
          
        }
    }
}
