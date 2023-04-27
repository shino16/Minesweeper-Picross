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
		Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
			var dependencyStatus = task.Result;
			if (dependencyStatus == Firebase.DependencyStatus.Available) {
				// Create and hold a reference to your FirebaseApp,
				// where app is a Firebase.FirebaseApp property of your application class.
			
				// Set a flag here to indicate whether Firebase is ready to use by your app.
			} else {
				UnityEngine.Debug.LogError(System.String.Format(
				"Could not resolve all Firebase dependencies: {0}", dependencyStatus));
				// Firebase Unity SDK is not safe to use here.
			}
		});
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
