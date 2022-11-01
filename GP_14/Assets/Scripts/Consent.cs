using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Consent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Logged", 0) == 1)
        {
            SceneManager.LoadScene("Homepage");

        }
        else
        {
            SceneManager.LoadScene("LoginScene");
        }
    }
}
