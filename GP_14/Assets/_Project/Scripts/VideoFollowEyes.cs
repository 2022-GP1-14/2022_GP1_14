using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using UnityEngine.XR.Management;

public class VideoFollowEyes : MonoBehaviour
{
    public VideoPlayer video;
    public UnityEvent OnClick;
    public LessonType type;
    void Awake()
    {
  
        video.loopPointReached += StopXR;
    }
  
    public void Exit()
    {
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            Camera.main.ResetAspect();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        }
    }
    void StopXR(VideoPlayer vp)
    {
        try
        {
            UserData.instance.WatchLesson(type);

            if (UserData.instance.scoreData == null)
            {
                UserData.instance.scoreData = new ScoreData();
            }

            switch (type)
            {
                case LessonType.Numbers:
                    UserData.instance.scoreData.Numbers = 1;
                    break;
                case LessonType.Letters:
                    UserData.instance.scoreData.Letters = 1;
                    break;
            }

            if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
            {
                XRGeneralSettings.Instance.Manager.StopSubsystems();
                Camera.main.ResetAspect();
                XRGeneralSettings.Instance.Manager.DeinitializeLoader();
            }

            OnClick.Invoke();
        }
        catch (Exception ex)
        {
            Debug.LogError($"An error occurred while stopping XR: {ex}");
        }
    }


}