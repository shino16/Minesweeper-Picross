using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using Firebase.Extensions;

public class SignUp : MonoBehaviour
{
    public Button signUpButton;
    public TMP_InputField passwordInput, rePasswordInput, emailInput, usernameInput;
	public TMP_Text errorLabel;
    public GameObject signUpObjects, verifyEmailObjects;
	private void OnEnable(){
		emailInput.text = "";
		passwordInput.text = "";
		rePasswordInput.text = "";
		errorLabel.SetText("");
		usernameInput.text = "";
	}
    private void Start()
    {
        signUpButton.onClick.AddListener(() => {
			Debug.Log("Clicked signup");
			errorLabel.SetText("");
			if(passwordInput.text == rePasswordInput.text){
				FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(emailInput.text, passwordInput.text).ContinueWithOnMainThread(task => {
					string username = usernameInput.text;
					emailInput.text = "";
					passwordInput.text = "";
					rePasswordInput.text = "";
					usernameInput.text = "";
					if (task.IsCanceled) {
						Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
						return;
					}
					if (task.IsFaulted) {
						Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
						errorLabel.SetText(task.Exception.Message);
						return;
					}

					// Firebase user has been created.
					Debug.Log("Signup successful");
					Firebase.Auth.FirebaseUser newUser = task.Result;
					Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile {
						DisplayName = username,
					};
					newUser.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task => {
						if (task.IsCanceled) {
							Debug.LogError("UpdateUserProfileAsync was canceled.");
							return;
						}
						if (task.IsFaulted) {
							Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
							return;
						}
						Debug.Log("User profile updated successfully.");
					});
					newUser.SendEmailVerificationAsync().ContinueWithOnMainThread(t => {
						Debug.Log("SendEmailVerificationAsync Success");
					});
					Debug.Log("Please verify your email");
					Debug.LogFormat("Firebase user created successfully: {0} ({1})",
					newUser.DisplayName, newUser.UserId);
					signUpObjects.SetActive(false);
					verifyEmailObjects.SetActive(true);
					Debug.Log("Email not verified");
				});
			}else{
				errorLabel.SetText("Passwords do not match");
			}
			
        });
    }

}
