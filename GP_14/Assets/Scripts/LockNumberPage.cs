using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LockNumberPage : MonoBehaviour
{

    public Button button;
    public GameObject lockbtn;
    // Start is called before the first frame update
    void Update()
    {

        if (PlayerPrefs.GetInt("LockNumberPage") == 1)
        {
            Debug.Log("Coming in if");
            button.interactable = true;
            lockbtn.SetActive(false);
        }

    }
}
