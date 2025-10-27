using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class VPObjContainer : MonoBehaviour
{
    public TMP_Text userName, userDNI, userYear;
    public Button selectButton;
    public Image panelImage;
    public int dni;

    public static event Action OnSelectedUser;

    void SelectUser()
    {
        OnSelectedUser?.Invoke();
    }
    private void Start()
    {
        selectButton.onClick.AddListener(UpdateCurrentUser);
    }
    private void UpdateCurrentUser()
    {
        UserDataManager.Instance.currentUser = dni;
        SelectUser();
        selectButton.interactable = false;
    }

    private void OnEnable() => VPObjContainer.OnSelectedUser += ShowSelectedButton;
    private void OnDisable() => VPObjContainer.OnSelectedUser -= ShowSelectedButton;

    void ShowSelectedButton()
    {
        selectButton.interactable = true;
    }

}
