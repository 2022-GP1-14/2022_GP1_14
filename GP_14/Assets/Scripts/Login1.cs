using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using RTLTMPro;
using TMPro;
using UnityEngine.SceneManagement;

public class Login1 : MonoBehaviour
{

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    private int usersCount,usersPasswordscount;
    private int users,passwords;
    public GameObject alertpopUp;
    public string[] userNames;
    public string[] userPasswords;

    ArrayList credentials;

    
    public void Start()
    {
        alertpopUp.SetActive(false);

        //  loginButton.onClick.AddListener(adminDetails);
        // goToRegisterButton.onClick.AddListener(moveToRegister);

        if (File.Exists(Application.dataPath + "/credentials.txt"))
        {
            credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        }
        else
        {
            Debug.Log("Credential file doesn't exist");
        }

        // Retrieve the number of registered users
        usersCount = PlayerPrefs.GetInt("UsersCount", 0);
        usersPasswordscount = PlayerPrefs.GetInt("UsersPasswords", 0);

        if (usersCount > 0)
        {
            
            userNames = new string[usersCount];
            for (int index = 0; index < usersCount; index++)
            {
                
                userNames[index] = PlayerPrefs.GetString("User" + index);
               // Debug.Log(userNames[index].ToString());
                users = index;
            }
        }
        if (usersPasswordscount > 0)
        {
            
            userPasswords = new string[usersPasswordscount];
            for (int index = 0; index < usersPasswordscount; index++)
            {
                
                userPasswords[index] = PlayerPrefs.GetString("Password" + index);
                //Debug.Log(userPasswords[index].ToString());
                passwords = index;
            }
        }
    }
    public void LoginButton()
    {
        Debug.Log("" + usernameInput.text);
        Debug.Log("" + passwordInput.text);

        for (int index = 0; index < usersCount; index++)
        {
            for (int j = 0; j < usersPasswordscount; j++)
            {
                if (userPasswords[j] == passwordInput.text && userNames[j] == usernameInput.text)
                {
                    Debug.Log("User authenticated");
                    loadWelcomeScreen();
                }
                else
                {
                    Debug.Log("Invalid authenticated");
                    StartCoroutine(Delay());
                }
            }
        }

        PlayerPrefs.SetInt("Logged", 1);
        //if (userNames[users] == usernameInput.text && userPasswords[passwords] == passwordInput.text)
        //{
        //    //Debug.Log("User authenticated");
        //    //loadWelcomeScreen();
        //}
        //else
        //{
        //    alertpopUp.SetActive(true);
        //    Debug.Log("Invalid password");
        //}
    }



    // Update is called once per frame
    //public void login()
    //{
    //    bool isExists = false;

    //    credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));

    //    foreach (var i in credentials)
    //    {
    //        string line = i.ToString();
    //        //Debug.Log(line);
    //        //Debug.Log(line.Substring(11));
    //        //substring 0-indexof(:) - indexof(:)+1 - i.length-1
    //        if (i.ToString().Substring(0, i.ToString().IndexOf(":")).Equals(usernameInput.text) &&
    //            i.ToString().Substring(i.ToString().IndexOf(":") + 1).Equals(passwordInput.text))
    //        {
    //            isExists = true;
    //            break;
    //        }
    //    }

    //    if (isExists)
    //    {
    //        Debug.Log($"Logging in '{usernameInput.text}'");
    //        loadWelcomeScreen();
    //    }
    //    else
    //    {
    //        Debug.Log("Incorrect Usernsme/Password");
    //    }
    //}

    public void moveToRegister()
    {
        SceneManager.LoadScene("SignUpScene");
    }

  


    public void loadWelcomeScreen()
    {
        SceneManager.LoadScene("Homepage");
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.5f);
        alertpopUp.SetActive(true);

    }
}