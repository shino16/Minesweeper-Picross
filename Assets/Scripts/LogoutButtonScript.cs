using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Firebase.Auth;
using Firebase.Extensions;

public class LogoutButtonScript : MonoBehaviour, IPointerClickHandler
{
	public GameObject yourInfoObjects, verifyEmailObjects;
	
	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("logging out");
		FirebaseAuth.DefaultInstance.SignOut();
		verifyEmailObjects.SetActive(false);
		yourInfoObjects.SetActive(true);
		
	}
    
}
