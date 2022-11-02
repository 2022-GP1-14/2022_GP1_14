using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEditor;

public class Register : MonoBehaviour
{

    public InputField usernameInput;
    public InputField passwordInput;
    public InputField childNameInput;
    private int usersCount;
    private int usersPasswordscount;
    private int UserChildname;


    public string[] userNames;
    public string[] childNames;
    public string[] userPasswords;


    private int users, passwords, childName;

    /*
    You can similarly add more variables here if you need more fields.
    I have added child age and child sex variables here.

    public InputField childAgeInput;
    public InputField childSexInput;
    */
    public Button registerButton;
   // public Button goToLoginButton;

    ArrayList credentials;

    // Start is called before the first frame update
    public void Start()
    {
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
            // Create the user name array...
            userNames = new string[usersCount];
            for (int index = 0; index < usersCount; index++)
            {
                // ... and load them.
                userNames[index] = PlayerPrefs.GetString("User" + index);
                Debug.Log(userNames[index].ToString());
                users = index;
            }
        }
        if (usersPasswordscount > 0)
        {
            // Create the user name array...
            userPasswords = new string[usersPasswordscount];
            for (int index = 0; index < usersPasswordscount; index++)
            {
                // ... and load them.
                userPasswords[index] = PlayerPrefs.GetString("Password" + index);
                Debug.Log(userPasswords[index].ToString());
                passwords = index;
            }
        }

        if (UserChildname > 0)
        {
            // Create the user name array...
            childNames = new string[UserChildname];
            for (int index = 0; index < UserChildname; index++)
            {
                // ... and load them.
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
        SaveUserName();
        SaveUserPassword();
        SaveChildName();
        StartCoroutine(Delay());
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