using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{

    public InputField usernameInput;
    public InputField passwordInput;
    /*
    You can similarly add more variables here if you need more fields.
    I have added child age and child sex variables here.

    public InputField childAgeInput;
    public InputField childSexInput;
    */
    public Button registerButton;
    public Button goToLoginButton;

    ArrayList credentials;

    // Start is called before the first frame update
    public void Start()
    {
        registerButton.onClick.AddListener(writeStuffToFile);
        goToLoginButton.onClick.AddListener(goToLoginScene);

        if (File.Exists(Application.dataPath + "/credentials.txt"))
        {
            credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/credentials.txt", "");
        }

    }

    public void goToLoginScene()
    {
        SceneManager.LoadScene("Login");
    }


    public void writeStuffToFile()
    {
        bool isExists = false;

        credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        foreach (var i in credentials)
        {
            if (i.ToString().Contains(usernameInput.text))
            {
                isExists = true;
                break;
            }
        }

        if (isExists)
        {
            Debug.Log($"Username '{usernameInput.text}' already exists");
        }
        else
        {
            credentials.Add(usernameInput.text + ":" + passwordInput.text); //+ ":" + childAgeInput.text + ":" + childSexInput.text); add this part here to save more info to your credentials file. Also then dont forget to change conditions in the login script as well.
            File.WriteAllLines(Application.dataPath + "/credentials.txt", (String[])credentials.ToArray(typeof(string)));
            Debug.Log("Account Registered");
        }
    }


}