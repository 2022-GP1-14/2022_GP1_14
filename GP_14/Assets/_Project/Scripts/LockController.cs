using System;
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
        try
        {
            bool isInteractable = UserData.instance?.scoreData?.Letters == 1;
            SetButtonState(isInteractable);
        }
        catch (Exception ex)
        {
            Debug.LogError($"An error occurred while setting the button state: {ex}");
        }
    }

    private void SetButtonState(bool isInteractable)
    {
        try
        {
            button.interactable = isInteractable;
            lockbtn.SetActive(!isInteractable);
        }
        catch (Exception ex)
        {
            Debug.LogError($"An error occurred while setting the button state: {ex}");
        }
    }


}
