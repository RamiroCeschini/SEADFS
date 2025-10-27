using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProfileViewer : MonoBehaviour
{
    [SerializeField] private GameObject profilePrefab;
    [SerializeField] private RectTransform parentTransform;
    [SerializeField] private TMP_Text studentText;

    private void Awake()
    {
        UserData user = UserDataManager.Instance.userList.users.Find(u => u.dni == UserDataManager.Instance.currentUser);
        if (user == null)
        {
            Debug.LogError("No user found with DNI: " + UserDataManager.Instance.currentUser);
            return;
        }
        studentText.text = "Alumno: " + user.firstName + " " + user.lastName;
        foreach (AttemptData attempt in user.attempts)
        {
            AddObject(attempt);
        }
        
        parentTransform.sizeDelta = new Vector2(parentTransform.sizeDelta.x, UserDataManager.Instance.userList.users.Count * 70);
    }

    private void AddObject(AttemptData userData) 
    {
        GameObject newCard = Instantiate(profilePrefab, parentTransform);
        newCard.transform.localScale = Vector3.one;
        newCard.transform.localPosition = Vector3.zero;
        ProfileAttempt data = newCard.GetComponent<ProfileAttempt>();

        if(userData.faultDetected == true)
        {
            data.faultDetected.text = "Detección: Sí";
        }
        else
        {
            data.faultDetected.text = "Detección: No";
        }

        data.reactionTime.text = "Reacción: " + userData.reactionTime.ToString() + "ms" ;

        if (userData.originRecognized == true)
        {
            data.originRecognized.text = "Identificación: Sí";
        }
        else
        {
            data.originRecognized.text = "Identificación: No";
        }

        data.timestamp.text = userData.timestamp;
    }
}
