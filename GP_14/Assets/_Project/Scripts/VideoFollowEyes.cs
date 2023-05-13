using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using UnityEngine.XR.Management;

public class VideoFollowEyes : MonoBehaviour
{
    public VideoPlayer video;
    public UnityEvent OnClick;
    public string LessonName;
    public LessonType type;
    void Awake()
    {
  
        video.loopPointReached += StopXR;
    }
  
    public GameObject Hint;
    public void Exit()
    {
        PlayerPrefs.SetInt(LessonName, 0);
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            Camera.main.ResetAspect();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        }
    }
    void StopXR(VideoPlayer vp)
    {

        PlayerPrefs.SetInt(LessonName, 1);
        UserData.instance.WatchLesson(type);
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            Camera.main.ResetAspect();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        }
        OnClick.Invoke();
    }
  
   
}