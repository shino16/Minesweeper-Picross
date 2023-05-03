using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using Firebase.Extensions;

public class LogIn : MonoBehaviour
{
    public Button logInButton, startButton;
    public TMP_InputField passwordInput, emailInput;
	public TMP_Text errorLabel;
    public GameObject yourInfoObjects, howToPlayObjects, mainGame, verifyEmailObjects;
	private void OnEnable(){
		emailInput.text = "";
		passwordInput.text = "";
		errorLabel.SetText("");
	}
    private void Start()
    {
        logInButton.onClick.AddListener(() => {
			Debug.Log("Clicked login");
			FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInput.text, passwordInput.text).ContinueWithOnMainThread(task => {
				emailInput.text = "";
				passwordInput.text = "";
				errorLabel.SetText("");
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
