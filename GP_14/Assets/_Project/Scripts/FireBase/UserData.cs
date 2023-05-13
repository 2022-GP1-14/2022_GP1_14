using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Management;

public class UserData : MonoBehaviour
{
    public static UserData instance;
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public FirebaseFirestore firestore;

    public ChildData childData;
    public ScoreData scoreData;
    // Start is called before the first frame update
    void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
        StopXR();
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }

        
    }
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        firestore = FirebaseFirestore.DefaultInstance;
    }

    void StopXR()
    {
        if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
        {
            XRGeneralSettings.Instance.Manager.StopSubsystems();
            Camera.main.ResetAspect();
            XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        }
    }
    public void FireStore_CreateObject(string Collectionpath, string Documentpath, object Data, Action<System.Threading.Tasks.Task> Task=null)
    {
        DocumentReference docRef = firestore.Collection(Collectionpath).Document(Documentpath);



        docRef.SetAsync(Data).ContinueWithOnMainThread(task =>
        {
            Debug.Log("Added data to " + Collectionpath + "/" + Documentpath);

        });



    }
    public void FireStore_ReadData(string Collectionpath, string Documentpath,Action<DocumentSnapshot> _documentSnapshot)
    {
        firestore = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = firestore.Collection(Collectionpath).Document(Documentpath);

        docRef.GetSnapshotAsync().ContinueWith((task) =>
        {
            _documentSnapshot(task.Result);
            

        });

       
    }
    public void FireStore_Edit(string Collectionpath, string Documentpath, object Data,Action<System.Threading.Tasks.Task> Task=null)
    {
        DocumentReference docRef = firestore.Collection(Collectionpath).Document(Documentpath);

        docRef.SetAsync(Data).ContinueWithOnMainThread(task =>
        {
            Task(task);

        });



    }
    public void SaveLesson(Lesson lesson, LessonName _lessonName)
    {
        if (scoreData == null)
        {
            FireStore_ReadData("Score", childData.Email, (_task) =>
        {
            if (!_task.Exists)
            {
                FireStore_CreateObject("Score", childData.Email, new ScoreData() { Email=childData.Email}, (_task) =>
                {
                    if (_task.IsCompleted)
                    {
                        EditeLesson(lesson, _lessonName);
                    }
                });
            }
            else
            {
                EditeLesson(lesson, _lessonName);
            }
        });
          

        }
        else
        {
            EditeLesson(lesson, _lessonName);
        }
 
    }
    public void getScore(Action<ScoreData> data)
    {
        FireStore_ReadData("Score", childData.Email, (_Data) =>
        {
            if (!_Data.Exists)
            {
                FireStore_CreateObject("Score", childData.Email, new ScoreData() { Email = childData.Email }, (_task2) =>
                {

                    if (_task2.IsCompleted)
                    {
                        scoreData = new ScoreData() { Email = childData.Email };
                        data(scoreData);
                    }
                });
            }
            else
            {
                scoreData = _Data.ConvertTo<ScoreData>();

                data(scoreData);
            }
        });
        }
    public void WatchLesson( LessonType _lessonType)
    {
        if (scoreData == null)
        {
            FireStore_ReadData("Score", childData.Email, (_task) =>
            {
                if (!_task.Exists)
                {
                    FireStore_CreateObject("Score", childData.Email, new ScoreData(), (_task) =>
                    {
                        if (_task.IsCompleted)
                        {
                            SaveLessonStatus(_lessonType);
                        }
                    });
                }
                else
                {
                    SaveLessonStatus(_lessonType);
                }
            });


        }
        else
        {
            SaveLessonStatus(_lessonType);
        }

    }
    public void EditeLesson(Lesson _lesson, LessonName lessonName)
    {
        print("scoreData.Days:" + scoreData.Days.Where((a) => a.dayName == DateTime.Today.Date.DayOfWeek.ToString()).ToList()[0].dayName);
        switch (lessonName)
        {
 
            case LessonName.ConnectLevel1:
                scoreData.Days.Where((a) => a.dayName == DateTime.Today.Date.DayOfWeek.ToString()).ToList()[0].ConnectLevel1 = _lesson;
                break;
            case LessonName.ConnectLevel2:
                scoreData.Days.Where((a) => a.dayName == DateTime.Today.Date.DayOfWeek.ToString()).ToList()[0].ConnectLevel2 = _lesson;

                break;
            case LessonName.ChooseLevel1:
                scoreData.Days.Where((a) => a.dayName == DateTime.Today.Date.DayOfWeek.ToString()).ToList()[0].ChooseLevel1 = _lesson;

                break;
            case LessonName.ChooseLevel2:
                scoreData.Days.Where((a) => a.dayName == DateTime.Today.Date.DayOfWeek.ToString()).ToList()[0].ChooseLevel2 = _lesson;

                break;
        }
        FireStore_Edit("Score", childData.Email, scoreData);
    }
    public void SaveLessonStatus(LessonType lessonType)
    {
        switch (lessonType)
        {
            case LessonType.Letters:
                scoreData.Letters=1;
                break;
            case LessonType.Numbers:
                scoreData.Numbers = 1;

                break;
        }
        FireStore_Edit("Score", childData.Email, scoreData);
    }
}
[System.Serializable]
[FirestoreData]
public class ChildData
{
    [FirestoreProperty]
    public string Email { set; get; }
    [FirestoreProperty]
    public string ChildName { set; get; }
    [FirestoreProperty]
    public int ChildAge { set; get; }
    [FirestoreProperty]
    public string Password { set; get; }
    [FirestoreProperty]
    public string ChildSex { set; get; }
    [FirestoreProperty]
    public Catgory catgory { set; get; }


}

[System.Serializable]
[FirestoreData]
public class Catgory
{
    [FirestoreProperty]
    public string CatgoryName { set; get; }
    [FirestoreProperty]
    public float ChildProgress { set; get; }
    [FirestoreProperty]
    public Letters letters { set; get; }
    [FirestoreProperty]
    public Numbers numbers { set; get; }

}

[System.Serializable]
[FirestoreData]
public class Letters
{
    [FirestoreProperty]
    public string LettersVideo { set; get; }
    [FirestoreProperty]
    public string MatchingGame { set; get; }

}
[System.Serializable]
[FirestoreData]
public class Numbers
{
    [FirestoreProperty]
    public string NumberVideo { set; get; }
    [FirestoreProperty]
    public string CountingGame { set; get; }

}
[System.Serializable]
[FirestoreData]
public class ScoreData
{
    [FirestoreProperty]
    public string Email { set; get; }
    [FirestoreProperty]
    public int Numbers { set; get; }
    [FirestoreProperty]
    public int Letters { set; get; }
    [FirestoreProperty]
    public Days[] Days { set; get; } = new Days[] {
                                                     new Days() { dayName = "Saturday"
                                                                       
                                                                }
                                                                 ,
                                                     new Days() { dayName = "Sunday"
                                                                  }
                                                                 ,
                                                      new Days() { dayName = "Monday"
                                                                 }
                                                                 ,
                                                       new Days() { dayName = "Tuesday"
                                                                  }
                                                                 ,
                                                        new Days() { dayName = "Wednesday"
                                                                        
                                                                   }
                                                                 ,
                                                         new Days() { dayName = "Thursday"
                                                                  
                                                                }
                                                                 ,
                                                          new Days() { dayName = "Friday"
                                                                  
                                                                }
                                                                 
                                                     };


}
[System.Serializable]
[FirestoreData]
public class Days
{
    [FirestoreProperty]
    public Lesson ConnectLevel1 { set; get; } = new Lesson();
    [FirestoreProperty]
    public Lesson ConnectLevel2 { set; get; } = new Lesson();
    [FirestoreProperty]
    public Lesson ChooseLevel1 { set; get; } = new Lesson();
    [FirestoreProperty]
    public Lesson ChooseLevel2 { set; get; } = new Lesson();

    [FirestoreProperty]
    public string dayName { set; get; }

}
[System.Serializable]
[FirestoreData]
public class Lesson
{
    
    [FirestoreProperty]
    public float degree { set; get; }

}
public enum LessonName
{
    ConnectLevel1,
    ConnectLevel2,
    ChooseLevel1,
    ChooseLevel2
}
public enum LessonType
{
    Numbers,
    Letters
}