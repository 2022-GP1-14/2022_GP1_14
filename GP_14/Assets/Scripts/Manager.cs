using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject A, B, C, D, ADark, BDark, CDark, DDark;

    Vector2 AInitialPos, BInitialPos, CInitialPos, DInitialPos;

    public AudioSource source;
    public AudioClip[] correct;
    public AudioClip incorrect;

    bool ABool, BBool, CBool, DBool = false;

    void Start()
    {
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
        if (distance < 50)
        {
            A.transform.position = ADark.transform.position;
            //Score.scoreNumber += 1;
            //carrotBool = true;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
            ABool = true;
        }
        else
        {
            A.transform.position = AInitialPos;
            source.clip = incorrect;
            source.Play();
        }
    }
    public void DropB()
    {

        float distance = Vector3.Distance(B.transform.position, BDark.transform.position);
        if (distance < 50)
        {
            B.transform.position = BDark.transform.position;
            //Score.scoreNumber += 1;
            BBool = true;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
        }
        else
        {
            B.transform.position = BInitialPos;
            source.clip = incorrect;
            source.Play();
        }
    }
    public void DropC()
    {

        float distance = Vector3.Distance(C.transform.position, CDark.transform.position);
        if (distance < 50)
        {
            C.transform.position = CDark.transform.position;
            //Score.scoreNumber += 1;
            CBool = true;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
        }
        else
        {
            C.transform.position = CInitialPos;
            source.clip = incorrect;
            source.Play();
        }
    }
    public void DropD()
    {

        float distance = Vector3.Distance(D.transform.position, DDark.transform.position);
        if (distance < 50)
        {
            D.transform.position = DDark.transform.position;
            //Score.scoreNumber += 1;
            DBool = true;
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
        }
        else
        {
            D.transform.position = DInitialPos;
            source.clip = incorrect;
            source.Play();
        }
    }
    void Update()
    {
        //if (ABool && BBool && CBool && DBool || Timer.time <= 0)
        if (ABool && BBool && CBool && DBool)
        {

            Debug.Log("You Win!");
        }
    }
}
