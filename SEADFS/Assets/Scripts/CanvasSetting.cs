using System.Collections.Generic;
using UnityEngine;

public class CanvasSetting : MonoBehaviour
{
    [SerializeField] private List<GameObject> panelList = new List<GameObject>();

    public void ChangePanel(int index)
    {
        foreach (GameObject panel in panelList)
        {
            panel.SetActive(false);
        }

        panelList[index].SetActive(true);
    }
}
