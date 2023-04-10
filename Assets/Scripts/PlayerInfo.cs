using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using Firebase.Extensions;

public class PlayerInfo : MonoBehaviour
{
    public Button logInButton, startButton;
    public TMP_InputField passwordInput, emailInput;
	public TMP_Text errorLabel;
    public GameObject yourInfoObjects, howToPlayObjects, mainGame, verifyEmailObjects;

    private void Start()
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
        logInButton.onClick.AddListener(() => {
			Debug.Log("Clicked login");
			FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInput.text, passwordInput.text).ContinueWithOnMainThread(task => {
				if (task.IsCanceled) {
					Debug.LogError("SignIn was canceled.");
					return;
				}
				if (task.IsFaulted) {
					Debug.LogError("SignIn encountered an error: " + task.Exception);
					errorLabel.SetText(task.Exception.Message);
					return;
				}
				Debug.Log("Signin successful");
				Firebase.Auth.FirebaseUser newUser = task.Result;
				Debug.LogFormat("User signed in successfully: {0} ({1})",
				newUser.DisplayName, newUser.UserId);	
				if(newUser.IsEmailVerified){
					yourInfoObjects.SetActive(false);
					howToPlayObjects.SetActive(true);
					startButton.gameObject.SetActive(true);
				}else{
					yourInfoObjects.SetActive(false);
					verifyEmailObjects.SetActive(true);
					Debug.Log("Email not verified");
				}
			});
			
            
            startButton.onClick.AddListener(() => {
                howToPlayObjects.SetActive(false);
                startButton.gameObject.SetActive(false);
                mainGame.SetActive(true);
            });
        });
    }

}
