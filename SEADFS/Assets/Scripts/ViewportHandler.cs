using System.Collections.Generic;
using UnityEngine;

public class ViewportHandler : MonoBehaviour
{
    [SerializeField] private GameObject VPObjPrefab;
    [SerializeField] private RectTransform parentTransform;
    private void Awake()
    {
        foreach(UserData user in UserDataManager.Instance.userList.users)
        {
            AddObject(user);
        }
        
        parentTransform.sizeDelta = new Vector2(parentTransform.sizeDelta.x, UserDataManager.Instance.userList.users.Count * 70);
    }

    private void AddObject(UserData userData) 
    {
        GameObject newCard = Instantiate(VPObjPrefab, parentTransform);
        newCard.transform.localScale = Vector3.one;
        newCard.transform.localPosition = Vector3.zero;
        VPObjContainer data = newCard.GetComponent<VPObjContainer>();
        data.userName.text = userData.firstName + " " + userData.lastName;
        data.userDNI.text = userData.dni.ToString();
        data.userYear.text = userData.courseYear.ToString();
        data.dni = userData.dni;
    }
}
