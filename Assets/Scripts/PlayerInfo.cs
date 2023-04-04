using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    public Button submitButton, startButton;
    public TMP_InputField firstNameInput, lastNameInput, emailInput;
    public GameObject yourInfoObjects, howToPlayObjects, mainGame;

    private void Start()
    {
        submitButton.onClick.AddListener(() => {
            string firstName = firstNameInput.text;
            string lastName = lastNameInput.text;
            string email = emailInput.text;
            SavePlayerInfo(firstName, lastName, email);

            yourInfoObjects.SetActive(false);
            howToPlayObjects.SetActive(true);
            startButton.gameObject.SetActive(true);

            startButton.onClick.AddListener(() => {
                howToPlayObjects.SetActive(false);
                startButton.gameObject.SetActive(false);
                mainGame.SetActive(true);
            });
        });
    }

    private void SavePlayerInfo(string firstName, string lastName, string email) {
        Debug.Log($"Received first name: {firstName}");
        Debug.Log($"Received last name: {lastName}");
        Debug.Log($"Received email: {email}");
    }
}
