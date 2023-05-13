using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase;
using Firebase.Extensions;

public class ChildProfileManager : MonoBehaviour
{
    public TMP_InputField Email, Name, Age;
    public TMP_Dropdown Gender;
    public Image Icon;
    public Sprite Male, Female;

    public void FireStore_Edit  ()
    {
        UserData.instance.childData.ChildName = Name.text;
        UserData.instance.childData.ChildAge = int.Parse(Age.text);
        UserData.instance.childData.ChildSex = Gender.options[Gender.value].text;
        UserData.instance.FireStore_Edit("Users", Email.text, UserData.instance.childData, (_Task) =>
        {
            Debug.Log("Edit data to " + "Users" + "/" + Email.text);
        });
    }
    private IEnumerator TakeScreenshotAndShare(bool share)
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();
        string dataPath = "";
        dataPath = Application.persistentDataPath;
        if (Application.platform == RuntimePlatform.Android)
        {
            dataPath = dataPath.Substring(0, dataPath.IndexOf("/Android"))+ "/Download";

            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }
        }
        string filePath = Path.Combine(dataPath, UserData.instance.childData.ChildName+System.DateTime.Now.ToString("yyyy'_'MM'_'dd'T'HH'_'mm'_'ss") + ".png");
        print(filePath);
        File.WriteAllBytes(filePath, ss.EncodeToPNG());
        if (!share)
        {
            //new NativeShare().AddFile(filePath)
            //    .SetSubject(UserData.instance.childData.Email)
            //    .SetCallback((result, shareTarget) => {
            //        Debug.Log("Share result: " + result + ", selected app: " + shareTarget); }).
            //    ;
        }
        else
        {
            new NativeShare().AddFile(filePath)
                .SetSubject(UserData.instance.childData.Email)
                .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget)).Share();
                ;
        }
        // To avoid memory leaks
        Destroy(ss);
    }
    public void Download(bool share)
    {
        StartCoroutine(TakeScreenshotAndShare(share));

    }
    // Start is called before the first frame update
    void Start()
    {
        if (Email != null)
        {
            Email.text = UserData.instance.childData.Email;
        }
        if (Name != null)
        {
            Name.text = UserData.instance.childData.ChildName;
        }
        if (Age != null)
        {
            Age.text = UserData.instance.childData.ChildAge.ToString();
        }
        if (Gender != null)
        {
            Gender.value = Gender.options.FindIndex(option => option.text == UserData.instance.childData.ChildSex);

            Gender.onValueChanged.AddListener(ChangeSprite);
            Gender.onValueChanged.Invoke(Gender.value);
        }
        if (UserData.instance.childData.ChildSex == "???")
        {
            ChangeSprite(0);
        }
        else
        {
            ChangeSprite(1);

        }
    }
    public void ChangeSprite(Int32 Index)
    {
        if (Index == 0)
        {
            Icon.sprite = Male;
        }
        else if(Index == 1)
        {
            Icon.sprite = Female;
        }
    }

}
