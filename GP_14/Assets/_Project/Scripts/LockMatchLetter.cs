using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LockMatchLetter : MonoBehaviour
{

    public Button button;
    public GameObject lockbtn;
    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.GetInt("LockMatchLetter") == 1)
        {
            Debug.Log("Coming in if");
            button.interactable = true;
            lockbtn.SetActive(false);
        }

    }
}
