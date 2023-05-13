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
using Firebase.Auth;
using Firebase;
using Firebase.Firestore;

public class Login1 : MonoBehaviour
{ 
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    public GameObject alertpopUp;
    public TextMeshProUGUI WrningMessage;
   
   

    public void LoginButton()
    {  
        

        StartCoroutine(Login());
  
      

      


    }
    public void RestPassword()
    {

        if (UserData.instance.auth != null)
        {
            UserData.instance.auth.SendPasswordResetEmailAsync(usernameInput.text).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("Password reset email sent successfully.");
                WrningMessage.text = "تم ارسال كود التحقق الى بريدك الالكترونى";
            });
        }
    }
    public void ShowHidePassword(bool Show)
    {
        if (Show)
        {
            passwordInput.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            passwordInput.contentType = TMP_InputField.ContentType.Password;
        }
            passwordInput.ForceLabelUpdate();
    }
    public void FireStore_ReadData(string Collectionpath, string Documentpath)
    {
        UserData.instance.FireStore_ReadData(Collectionpath, Documentpath, (task) =>
        {

            if (task.Exists)
            {


                UserData.instance.childData = task.ConvertTo<ChildData>();


            }
        });
      
      
        loadWelcomeScreen();
    }
    private IEnumerator Login()
    {
        UserData.instance.auth = FirebaseAuth.DefaultInstance;
             //Call the Firebase auth signin function passing the email and password
             var LoginTask = UserData.instance.auth.SignInWithEmailAndPasswordAsync(usernameInput.text, passwordInput.text);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            WrningMessage.text = message;
            StartCoroutine(Delay());

        }
        else
        {
            //User is now logged in
            //Now get the result
            UserData.instance.User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",UserData.instance. User.DisplayName, UserData.instance.User.Email);
            WrningMessage.text = "";
            WrningMessage.text = "Logged In";
        FireStore_ReadData("Users", usernameInput.text);
           
           
           
        }
    }


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