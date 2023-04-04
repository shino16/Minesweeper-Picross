using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    public Button infoButton;
    public GameObject howToPlayText, howToPlayBackground;

    void Start()
    {
        infoButton.onClick.AddListener(() =>
            howToPlayText.SetActive(!howToPlayText.activeSelf));
        infoButton.onClick.AddListener(() =>
            howToPlayBackground.SetActive(!howToPlayBackground.activeSelf));
    }
}
