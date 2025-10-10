using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserDataHandler : MonoBehaviour
{
    public TMP_InputField firstNameInput, lastNameInput, ageInput, dniInput, courseYearInput;
    public TMP_Text feedbackText;

    public void OnRegisterButtonClicked()
    {
        if (string.IsNullOrEmpty(firstNameInput.text) ||
            string.IsNullOrEmpty(lastNameInput.text) ||
            string.IsNullOrEmpty(ageInput.text) ||
            string.IsNullOrEmpty(dniInput.text) ||
            string.IsNullOrEmpty(courseYearInput.text))
        {
            ShowFeedback("Por favor, rellene todos los campos.");
            return;
        }

        if (!int.TryParse(ageInput.text, out int age) ||
            !int.TryParse(courseYearInput.text, out int courseYear) ||
            !int.TryParse(dniInput.text, out int dni))
        {
            ShowFeedback("La edad, el DNI y el año de cursada deben ser números.");
            return;
        }

        var user = UserDataManager.Instance.RegisterUser(
            firstNameInput.text,
            lastNameInput.text,
            age,
            dni,
            courseYear
        );

        if (user != null)
            ShowFeedback("Se registró el usuario correctamente");
        else
            ShowFeedback("El usuario con ese DNI ya existe.");
    }

    private void ShowFeedback(string message)
    {
        if (feedbackText != null)
            feedbackText.text = message;
    }
}
