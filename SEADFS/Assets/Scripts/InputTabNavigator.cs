using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InputTabNavigator : MonoBehaviour
{
    public List<TMP_InputField> inputFields;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SelectNextField();
        }
    }

    private void SelectNextField()
    {
        GameObject current = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        int index = inputFields.FindIndex(field => field.gameObject == current);

        if (index >= 0)
        {
            int nextIndex = (index + 1) % inputFields.Count;
            inputFields[nextIndex].Select();
        }
    }
}
