using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerLoader : MonoBehaviour
{
    public string LevelToLoad;
    public float timer = 5f;
    public Slider slider;
    private void Start()
    {
        StartCoroutine(LoadSceneAfterTime());
    }
    IEnumerator LoadSceneAfterTime()
    {
        if (slider != null)
        {
            LeanTween.value(new GameObject(), (f) =>
            {
                slider.value = f;
            }, 0, timer, timer);
        }
        yield return new WaitForSeconds(timer);
        SceneManager.LoadSceneAsync(LevelToLoad);
    }
    
}
