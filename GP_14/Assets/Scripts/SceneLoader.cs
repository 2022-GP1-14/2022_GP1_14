using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{


    public void GotoNumbersScene()
    {
        SceneManager.LoadScene("Numbers page");
    }
    public void GotoLettersScene()
    {
        SceneManager.LoadScene("Letters page");
    }


    public void Gotomatchingscene()
    {
        SceneManager.LoadScene("Match letters game");
    }

    public void GotoMatchingLvl1()
    {
        SceneManager.LoadScene("MatchingLevel1");
    }

    public void Gotocountingscene()
    {
        SceneManager.LoadScene("Counting numbers game");
    }

    public void GotoNumbersGameLevel1()
    {
        SceneManager.LoadScene("NumbersGameLevel1");
    }


    //
    public void LoadHomeScene()
    {
        DisableLockNumberPage();
        SceneManager.LoadScene("Homepage");
    }

    public void BackHomeScene()
    {
        DisableLock();
        SceneManager.LoadScene("Homepage");

    }

    public void GoHomeScene()
    {
        SceneManager.LoadScene("Homepage");
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene("LoginScene");
    }
    //public void LoadNextScene()
    //{
    //    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //    SceneManager.LoadScene(currentSceneIndex +1);
    //}
    //public void LoadPreScene()
    //{
    //    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    //    SceneManager.LoadScene(currentSceneIndex - 1);
    //}
    public void LoadProgressScene()
    {
        SceneManager.LoadScene("progress page");
    }
    public void LoadNumbersScene()
    {
        //DisableLock();
        SceneManager.LoadScene("Numbers page");
    }
    public void LoadLettersScene()
    {
        SceneManager.LoadScene("Letters page");
    }
    public void loadcountingscene()
    {
        SceneManager.LoadScene("Counting numbers game");
        //LockCounting Number
        DisableLockCountingNumber();
    }
    public void loadmatchingscene()
    {
        SceneManager.LoadScene("Match letters game");
        //Player
        DisableLockMatchLetter();
    }
    public void loadFirstLessonLettersscene()
    {
        SceneManager.LoadScene("First lesson letters");
    }
    public void loadFirstLessonNumbersscene()
    {
        SceneManager.LoadScene("First lesson numbers");
    }

    public void loadChildProfilescene()
    {
        SceneManager.LoadScene("ChildProfileScene");
    }

    public void loadNumbersClassroom()
    {
        SceneManager.LoadScene("NumbersClassroom");
    }

    public void LogOut()
    {
        SceneManager.LoadScene("LoginScene");
        PlayerPrefs.SetInt("Logged", 0);
    }

    public void loadPassPage()
    {
        SceneManager.LoadScene("You pass");
    }

    public void loadPassPage1()
    {
        SceneManager.LoadScene("You pass2");
    }
    public void NumbersGameLevel1()
    {
        SceneManager.LoadScene("NumbersGameLevel1");
        //LockCounting Number
        DisableLockCountingNumber();
    }

    public void NumbersGameLevel2()
    {
        SceneManager.LoadScene("NumbersGameLevel2");
    }
    public void loadNumbersLesson()
    {
        SceneManager.LoadScene("NumbersLesson");
    }
    public void loadLettersLesson()
    {
        SceneManager.LoadScene("LettersLesson");
    }

    public void LoadMatchingLvl1()
    {
        SceneManager.LoadScene("MatchingLevel1");
        // Playeer
        DisableLockMatchLetter();
    }

    public void LoadMatchingLvl2()
    {
        SceneManager.LoadScene("matchLetterleve2");

    }
    public void DisableLock()
    {
        PlayerPrefs.SetInt("Lock", 1);

    }

    public void DisableLockMatchLetter()
    {
        PlayerPrefs.SetInt("LockMatchLetter", 1);

    }

    public void DisableLockNumberPage()
    {
        PlayerPrefs.SetInt("LockNumberPage", 1);

    }

    public void DisableLockCountingNumber()
    {
        PlayerPrefs.SetInt("DisableLockCountingNumber", 1);

    }
}
