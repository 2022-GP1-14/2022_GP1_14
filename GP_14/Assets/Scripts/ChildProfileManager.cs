using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChildProfileManager : MonoBehaviour
{
    private int UserChildname;
    private int usersCount;

    public string[] childNames;
    public string[] userNames;

    private int users, passwords, childName;
    public InputField ChildNameInput;
    public InputField UserNameInput;

    // Start is called before the first frame update
    void Start()
    {
        UserChildname = PlayerPrefs.GetInt("UsersChildName", 0);
        usersCount = PlayerPrefs.GetInt("UsersCount", 0);

        if (UserChildname > 0)
        {
            // Create the user name array...
            childNames = new string[UserChildname];
            for (int index = 0; index < UserChildname; index++)
            {
                // ... and load them.
                childNames[index] = PlayerPrefs.GetString("ChildName" + index);
                Debug.Log(childNames[index].ToString());
                childName = index;
                ChildNameInput.text = childNames[childName];

            }
        }

        UserChildname = PlayerPrefs.GetInt("UsersChildName", 0);

        if (usersCount > 0)
        {
            // Create the user name array...
            userNames = new string[usersCount];
            for (int index = 0; index < usersCount; index++)
            {
                // ... and load them.
                userNames[index] = PlayerPrefs.GetString("User" + index);
                Debug.Log(userNames[index].ToString());
                users = index;
                UserNameInput.text = userNames[users];

            }
        }
    }
}
