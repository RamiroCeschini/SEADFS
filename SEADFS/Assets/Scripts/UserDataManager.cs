using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public static UserDataManager Instance { get; private set; }

    private string filePath;
    public UserDataList userList = new UserDataList();
    public int currentUser;
    public string currentUserName;

    void Awake()
    {
        Singleton();

        filePath = Path.Combine(Application.persistentDataPath, "users.json");
        LoadData();
    }

    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(userList, true);
        File.WriteAllText(filePath, json);
        Debug.Log("User data saved at: " + filePath);
    }

    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            userList = JsonUtility.FromJson<UserDataList>(json);
        }
        else
        {
            userList = new UserDataList();
            SaveData();
        }
    }

    public UserData RegisterUser(string firstName, string lastName, int age, int dni, int courseYear)
    {
        UserData existingUser = userList.users.Find(u => u.dni == dni);
        if (existingUser != null)
        {
            Debug.LogWarning("User with DNI " + dni + " already exists.");
            return null;
        }

        UserData newUser = new UserData
        {
            firstName = firstName,
            lastName = lastName,
            age = age,
            dni = dni,
            courseYear = courseYear
        };

        userList.users.Add(newUser);
        SaveData();

        Debug.Log("New user registered: " + firstName + " " + lastName);
        return newUser;
    }

    public void AddAttempt( bool faultDetected, float reactionTime, bool originRecognized)
    {
        UserData user = userList.users.Find(u => u.dni == currentUser);
        if (user == null)
        {
            Debug.LogError("No user found with DNI: " + currentUser);
            return;
        }

        AttemptData newAttempt = new AttemptData(faultDetected, reactionTime, originRecognized);
        user.attempts.Add(newAttempt);
        SaveData();

        Debug.Log("Attempt added for user: " + user.firstName + " " + user.lastName);
    }
}
