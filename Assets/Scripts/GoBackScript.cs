using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GoBackScript : MonoBehaviour, IPointerClickHandler
{
	public GameObject mainMenuObjects, currentObjects;
	
	public void OnPointerClick(PointerEventData eventData)
	{
		currentObjects.SetActive(false);
		mainMenuObjects.SetActive(true);
		
	}
    
}
