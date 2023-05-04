using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;

public class GetCurrentUser : MonoBehaviour
{
	Firebase.Auth.FirebaseAuth auth;
	public TMP_Text nicknameLabel;
    // Start is called before the first frame update
    void Start()
    {

		Debug.Log("Setting up Firebase Auth");
		Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
			var dependencyStatus = task.Result;
			if (dependencyStatus == Firebase.DependencyStatus.Available) {
				auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
			} else {
				UnityEngine.Debug.LogError(System.String.Format(
				"Could not resolve all Firebase dependencies: {0}", dependencyStatus));
				// Firebase Unity SDK is not safe to use here.
			}
		});
    }

    // Update is called once per frame
    void Update()
    {
		if (auth != null && auth.CurrentUser != null) {
			nicknameLabel.text = "User: " + auth.CurrentUser.DisplayName;
		}
		else{
			nicknameLabel.text = "User: not signed in";
		}
	}
}
