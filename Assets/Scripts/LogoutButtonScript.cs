using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Firebase.Extensions;

public class LogoutButtonScript : MonoBehaviour, IPointerClickHandler
{
	public GameObject mainMenuObjects, currentObjects, gameOverPanel;
	public MainGame mainGame;

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("logging out");
		FirebaseAuth.DefaultInstance.SignOut();
		currentObjects.SetActive(false);
		mainMenuObjects.SetActive(true);
		gameOverPanel.SetActive(false);
		// mainGame.Start();
		SceneManager.LoadScene("MindsweeperPicross");
	}

}
