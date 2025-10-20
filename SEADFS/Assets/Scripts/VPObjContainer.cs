using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VPObjContainer : MonoBehaviour
{
    public TMP_Text userName, userDNI, userYear;
    public Button selectButton;
    public Image panelImage;
    public int dni;
    private void Start()
    {
        selectButton.onClick.AddListener(UpdateCurrentUser);
    }
    private void UpdateCurrentUser()
    {
        UserDataManager.Instance.currentUser = dni;

    }

}
