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
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
		if (user != null) {
			nicknameLabel.text = "User: " + user.Email;
		}
		else{
			nicknameLabel.text = "User: not signed in";
		}
	}
}
