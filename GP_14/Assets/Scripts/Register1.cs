using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEditor;
using RTLTMPro;
using TMPro;
using UnityEngine.SceneManagement;

public class Register1 : MonoBehaviour
{

    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField childNameInput;
    private int usersCount;
    private int usersPasswordscount;
    private int UserChildname;
    public GameObject WarningText;

    public string[] userNames;
    public string[] childNames;
    public string[] userPasswords;


    private int users, passwords, childName;

    /*
    we will add more variables here for more fields.
    public InputField childAgeInput;
    public InputField childSexInput;
    */
    public Button registerButton;
    // public Button goToLoginButton;

    ArrayList credentials;

   
    public void Start()
    {
        WarningText.gameObject.SetActive(false);
        // registerButton.onClick.AddListener(writeStuffToFile);
        // goToLoginButton.onClick.AddListener(goToLoginScene);

        if (File.Exists(Application.dataPath + "/credentials.txt"))
        {
            credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/credentials.txt", "");
        }


        // Retrieve the number of registered users
        usersCount = PlayerPrefs.GetInt("UsersCount", 0);
        usersPasswordscount = PlayerPrefs.GetInt("UsersPasswords", 0);
        UserChildname = PlayerPrefs.GetInt("UsersChildName", 0);



        Debug.Log(usersCount);
        Debug.Log(usersPasswordscount);
        Debug.Log(UserChildname);


        if (usersCount > 0)
        {
            // Create the usernames array
            userNames = new string[usersCount];
            for (int index = 0; index < usersCount; index++)
            {
                
                userNames[index] = PlayerPrefs.GetString("User" + index);
                Debug.Log(userNames[index].ToString());
                users = index;
            }
        }
        if (usersPasswordscount > 0)
        {
            // Create the passwords array...
            userPasswords = new string[usersPasswordscount];
            for (int index = 0; index < usersPasswordscount; index++)
            {
                
                userPasswords[index] = PlayerPrefs.GetString("Password" + index);
                Debug.Log(userPasswords[index].ToString());
                passwords = index;
            }
        }

        if (UserChildname > 0)
        {
            // Create the childnames array...
            childNames = new string[UserChildname];
            for (int index = 0; index < UserChildname; index++)
            {
                
                childNames[index] = PlayerPrefs.GetString("ChildName" + index);
                Debug.Log(childNames[index].ToString());
                childName = index;
            }
        }


    }
    public void SaveUserName()
    {
        PlayerPrefs.SetString("User" + usersCount, usernameInput.text);
        usersCount++;
        PlayerPrefs.SetInt("UsersCount", usersCount);
        PlayerPrefs.Save();
    }
    public void SaveChildName()
    {
        PlayerPrefs.SetString("ChildName" + UserChildname, childNameInput.text);
        UserChildname++;
        PlayerPrefs.SetInt("UsersChildName", UserChildname);
        PlayerPrefs.Save();
    }
    public void SaveUserPassword()
    {
        PlayerPrefs.SetString("Password" + usersPasswordscount, passwordInput.text);
        usersPasswordscount++;
        PlayerPrefs.SetInt("UsersPasswords", usersPasswordscount);
        PlayerPrefs.Save();

        //layerPrefs.SetString("Password", passwordInput.text);
    }

    public void goToLoginScene()
    {
        if (string.IsNullOrEmpty(passwordInput.text) || string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(childNameInput.text))
        {
            //Warningtext.text = "ÇáÑÌÇÁ ÊÚÈÆÉ ÌãíÚ ÇáÍÞæá";
            WarningText.gameObject.SetActive(true);
            StartCoroutine(Delay1());
        }

        else
        {
            WarningText.gameObject.SetActive(false);
            SaveUserName();
            SaveUserPassword();
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay1()
    {
        yield return new WaitForSeconds(3.0f);
        WarningText.gameObject.SetActive(false);
        //SceneManager.LoadScene("SignUpScene");


    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("LoginScene");

    }
    //public void writeStuffToFile()
    //{
    //    bool isExists = false;

    //    credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
    //    foreach (var i in credentials)
    //    {
    //        if (i.ToString().Contains(usernameInput.text))
    //        {
    //            isExists = true;
    //            break;
    //        }
    //    }

    //    if (isExists)
    //    {
    //        Debug.Log($"Username '{usernameInput.text}' already exists");
    //    }
    //    else
    //    {
    //        credentials.Add(usernameInput.text + ":" + passwordInput.text); //+ ":" + childAgeInput.text + ":" + childSexInput.text); add this part here to save more info to your credentials file. Also then dont forget to change conditions in the login script as well.
    //        File.WriteAllLines(Application.dataPath + "/credentials.txt", (String[])credentials.ToArray(typeof(string)));
    //        Debug.Log("Account Registered");
    //    }
    //}


}