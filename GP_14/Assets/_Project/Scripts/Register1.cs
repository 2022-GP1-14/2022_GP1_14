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

    public void goToLoginScene()
    {
        StartCoroutine(Register());
    }


    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("LoginScene");

    }


    private IEnumerator Register()
    {
        if (!ValidateInputFields())
            yield break;

        // Initialize Firebase authentication and child data
        UserData.instance.auth = FirebaseAuth.DefaultInstance;
        UserData.instance.childData = new ChildData();
        UserData.instance.childData.ChildName = childNameInput.text;

        // Set child age
        int age = int.TryParse(AgeInput.text, out age) ? age : 0;
        UserData.instance.childData.ChildAge = age;

        // Set child email, password, and sex
        UserData.instance.childData.Email = EmailInput.text;
        UserData.instance.childData.Password = passwordInput.text;
        UserData.instance.childData.ChildSex = Male.On ? "ذكر" : Female.On ? "أنثى" : "";

        // Register the user with Firebase authentication
        var registerTask = UserData.instance.auth.CreateUserWithEmailAndPasswordAsync(UserData.instance.childData.Email, UserData.instance.childData.Password);
        yield return new WaitUntil(() => registerTask.IsCompleted);

        if (registerTask.Exception != null)
        {
            // Handle errors during registration
            Debug.LogWarning($"Failed to register task with {registerTask.Exception}");
            FirebaseException firebaseEx = registerTask.Exception.GetBaseException() as FirebaseException;
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
        }
        else
        {
            // User has been successfully registered
            UserData.instance.User = registerTask.Result;

            // Set the user profile display name
            UserProfile profile = new UserProfile { DisplayName = UserData.instance.childData.ChildName };
            var profileTask = UserData.instance.User.UpdateUserProfileAsync(profile);
            yield return new WaitUntil(() => profileTask.IsCompleted);

            if (profileTask.Exception != null)
            {
                // Handle errors while setting the user profile
                Debug.LogWarning($"Failed to register task with {profileTask.Exception}");
                FirebaseException firebaseEx = profileTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                WarningText.text = "فشل فى تسجيل اسم الطفل";
            }
            else
            {
                // User profile has been successfully set
                // Create a new child data object and save it in Firestore
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

    private bool ValidateInputFields()
    {
        if (string.IsNullOrEmpty(EmailInput.text))
        {
            WarningText.text = "لا تترك حقل البريد الالكترونى فارغ";
            return false;
        }
        else if (string.IsNullOrEmpty(passwordInput.text))
        {
            WarningText.text = "لا تترك حقل الرقم السرى فارغ";
            return false;
        }
        else if (string.IsNullOrEmpty(childNameInput.text))
        {
            WarningText.text = "لا تترك حقل اسم الطفل";
            return false;
        }
        else if (string.IsNullOrEmpty(AgeInput.text))
        {
            WarningText.text = "لا تترك حقل العمر فارغ";
            return false;
        }
        else if (passwordInput.text != confirmPasswordInput.text)
        {
            WarningText.text = "كلمة المرور غير متطابقة";
            return false;
        }
        else if (!int.TryParse(AgeInput.text, out int age) || age < 6 || age > 12)
        {
            WarningText.text = "العمر يجب أن يكون بين 6 سنوات إلى 12 سنه";
            return false;
        }
        return true;
    }


    public void ShowHidePassword(TMP_InputField inputField)
    {
        inputField.contentType = inputField.contentType == TMP_InputField.ContentType.Password
            ? TMP_InputField.ContentType.Standard
            : TMP_InputField.ContentType.Password;

        inputField.ForceLabelUpdate();
    }

}

