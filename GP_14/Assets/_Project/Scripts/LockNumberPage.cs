using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LockNumberPage : MonoBehaviour
{

    public Button button;
    public GameObject lockbtn;
    private void Start()
    {
        bool isInteractable = UserData.instance.scoreData != null && UserData.instance.scoreData.Numbers == 1;
        SetButtonState(isInteractable);
    }

    private void SetButtonState(bool isInteractable)
    {
        button.interactable = isInteractable;
        lockbtn.SetActive(!isInteractable);
    }

}
