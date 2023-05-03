using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;

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
		FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
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

					//Update DisplayName of the user.
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

						//Add user's document in the score database.
						DocumentReference docRef = db.Collection("users").Document(newUser.UserId);
						Dictionary<string, object> userData = new Dictionary<string, object>
						{
        					{ "email", newUser.Email },
        					{ "highScore", 0 },
        					{ "username", newUser.DisplayName }
						};
						docRef.SetAsync(userData).ContinueWithOnMainThread(task => {
        					Debug.Log("Added new user's data to the users collection.");
						});
					});

					//Send verification email
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