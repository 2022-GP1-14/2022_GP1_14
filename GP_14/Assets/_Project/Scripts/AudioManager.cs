using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager : MonoBehaviour
{
    [SerializeField] Image SoundOn;
    [SerializeField] Image SoundOff;
    private bool muted = false;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }

        else
        {
            Load();
        }
        UpdateButtonImage();
        AudioListener.pause = muted;


    }

    public void OnButtonPress()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;

        }

        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateButtonImage();
    }

    private void UpdateButtonImage()
    {
        if (muted == false)
        {
            SoundOn.enabled = true;
            SoundOff.enabled = false;
        }
        else
        {
            SoundOn.enabled = false;
            SoundOff.enabled= true;
        }
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;


    }

    private void Save()//due to that PlayerPrefs doesn't accept bool var
    {
        PlayerPrefs.SetInt("muted",muted? 1 : 0);

    }

  
}
