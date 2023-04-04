using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    public Button infoButton;
    public GameObject howToPlayObjects;

    private void Start()
    {
        infoButton.onClick.AddListener(() =>
            howToPlayObjects.SetActive(!howToPlayObjects.activeSelf));
    }
}
