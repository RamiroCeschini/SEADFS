using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CanvasSetting : MonoBehaviour
{
    [SerializeField] private List<GameObject> panelList = new List<GameObject>();
    [SerializeField] private TMP_Text infoDisplay;

    private void Start()
    {
        SetDisplay();
    }
    public void ChangePanel(int index)
    {
        foreach (GameObject panel in panelList)
        {
            panel.SetActive(false);
        }

        panelList[index].SetActive(true);
    }

    public void SetDisplay()
    {
        string studentName = UserDataManager.Instance.currentUserName;
        infoDisplay.text =
            "EL ALUMNO SELECCIONADO ES: " + "\n" +
            studentName.ToUpper() + "\n\n" +
            "COLÓQUESE LOS AURICULARES\n\n" +
            "VERIFIQUE EL FUNCIONAMIENTO DE LOS MISMOS";
    }
}
