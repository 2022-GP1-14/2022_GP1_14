using System;
using System.Collections;
using UnityEngine;
using RTLTMPro;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Firestore;
using Firebase.Auth;
using Firebase;
using Firebase.Extensions;

public class Register1 : MonoBehaviour
{

 

    public TMP_InputField EmailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;
    public TMP_InputField childNameInput;
    public TMP_InputField AgeInput;
    public Lean.Gui.LeanToggle Male, Female;
    public RTLTextMeshPro WarningText;

    private void Awake()
    {
        
    }


    public void goToLoginScene()
    {

        StartCoroutine(Register());


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

  

    private IEnumerator Register()
    {
        UserData.instance.auth = FirebaseAuth.DefaultInstance;
        UserData.instance.childData = new ChildData();
        UserData.instance.childData.ChildName = childNameInput.text;

        if (string.IsNullOrEmpty(AgeInput.text))
        {
            UserData.instance.childData.ChildAge = 0;
        }
        else
        {
            UserData.instance.childData.ChildAge = int.Parse(AgeInput.text);
        }
        UserData.instance.childData.Email = EmailInput.text;
        UserData.instance.childData.Password = passwordInput.text;
        if (Male.On)
        {
            UserData.instance.childData.ChildSex = "ذكر";
        }
        if (Female.On)
        {
            UserData.instance.childData.ChildSex = "أنثى";
        }
        if (UserData.instance.childData.Email == "")
        {
            //If the username field is blank show a warning
            WarningText.text = "لا تترك حقل البريد الالكترونى فارغ";
        }
        else if (UserData.instance.childData.Password == "")
        {
            //If the password does not match show a warning
            WarningText.text = "لا تترك حقل الرقم السرى فارغ";
        }
        else if (childNameInput.text == "")
        {
            WarningText.text = "لا تترك حقل اسم الطفل";
        }
        else if (AgeInput.text == "")
        {
            WarningText.text = "لا تترك حقل العمر فارغ";
        }
        else if (passwordInput.text != confirmPasswordInput.text)
        {
            WarningText.text = "كلمة المرور غير متطابقة";
        }
        else if (6 > int.Parse(AgeInput.text) || int.Parse(AgeInput.text) > 12)
        {
            WarningText.text = "العمر يجب أن يكون بين 6 سنوات إلى 12 سنه";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = UserData.instance.auth.CreateUserWithEmailAndPasswordAsync(UserData.instance.childData.Email, UserData.instance.childData.Password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        WarningText.text = "خطأ فى البريد الالكترونى";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        WarningText.text = "خطأ فى الرقم السرى";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        WarningText.text = " الرقم السرى ضعيف";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        WarningText.text = "البريد الالكترونى مسجل مسبقا";
                        break;
                }
                //WarningText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                UserData.instance.User = RegisterTask.Result;

                if (UserData.instance.User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = UserData.instance.childData.ChildName };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = UserData.instance.User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        WarningText.text = "فشل فى تسجيل اسم الطفل";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        UserData.instance.childData.catgory = new Catgory()
                        {
                            CatgoryName = "",
                            letters = new Letters()
                            {
                                LettersVideo = "",
                                MatchingGame = ""
                            },
                            numbers = new Numbers()
                            {
                                CountingGame = "",
                                NumberVideo = ""
                            },
                            ChildProgress = 0
                        };
                        UserData.instance.FireStore_CreateObject("Users", UserData.instance.childData.Email, UserData.instance.childData);
                        WarningText.text = "";
                        StartCoroutine(Delay());


                    }
                }
            }
        }
    }
  
    public void ShowHidePassword(bool Show)
    {
        if (Show)
        {
            passwordInput.contentType = TMP_InputField.ContentType.Standard;
            confirmPasswordInput.contentType = TMP_InputField.ContentType.Standard;

        }
        else
        {
            passwordInput.contentType = TMP_InputField.ContentType.Password;
            confirmPasswordInput.contentType = TMP_InputField.ContentType.Password;
        }
        passwordInput.ForceLabelUpdate();
        confirmPasswordInput.ForceLabelUpdate();

    }
    public void ShowHidePassword(TMP_InputField _InputField)
    {
        if (_InputField.contentType == TMP_InputField.ContentType.Password)
        {
            _InputField.contentType = TMP_InputField.ContentType.Standard;
          

        }
        else
        {
            _InputField.contentType = TMP_InputField.ContentType.Password;
        }
        passwordInput.ForceLabelUpdate();
        confirmPasswordInput.ForceLabelUpdate();

    }

}

