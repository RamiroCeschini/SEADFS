
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttemptData
{
    public bool faultDetected;         
    public float reactionTime;         
    public bool originRecognized;     
    public string timestamp;

    public AttemptData(bool faultDetected, float reactionTime, bool originRecognized)
    {
        this.faultDetected = faultDetected;
        this.reactionTime = reactionTime;
        this.originRecognized = originRecognized;
        this.timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}

[System.Serializable]
public class UserData
{
    public string firstName;
    public string lastName;
    public int age;
    public int dni;
    public int courseYear;

    public List<AttemptData> attempts = new List<AttemptData>();
}

[System.Serializable]
public class UserDataList
{
    public List<UserData> users = new List<UserData>();
}
