using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
 public void LoadHomeScene()
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
        SceneManager.LoadScene("Numbers page");
    }
    public void LoadLettersScene()
    {
        SceneManager.LoadScene("Letters page");
    }
    public void loadcountingscene()
    {
        SceneManager.LoadScene("Counting numbers game");
    }
    public void loadmatchingscene()
    {
        SceneManager.LoadScene("Match letters game");
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
}
