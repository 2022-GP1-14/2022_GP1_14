using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LockController : MonoBehaviour
{

    public Button button;
    public GameObject lockbtn;
    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.GetInt("Lock") == 1)
        {
            Debug.Log("Coming in if");
            button.interactable = true;
            lockbtn.SetActive(false);
        }
        else
        {
            Debug.Log("Coming in else");

            button.interactable = false;
            lockbtn.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
