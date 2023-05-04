using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using Firebase.Extensions;

public class MainMenuScript : MonoBehaviour
{
    public Button goToLogInButton, goToSignUpButton;
	public GameObject mainMenuObjects, logInObjects, signUpObjects;

    void Start()
    {
        goToLogInButton.onClick.AddListener(() => {
                mainMenuObjects.SetActive(false);
                logInObjects.SetActive(true);
        });
		goToSignUpButton.onClick.AddListener(() => {
                mainMenuObjects.SetActive(false);
                signUpObjects.SetActive(true);
        });
    }


}
