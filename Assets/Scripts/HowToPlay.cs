using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    public Button howToPlayButton;
    public GameObject howToPlayObjects;

    private void Start()
    {
        howToPlayButton.onClick.AddListener(() =>
            howToPlayObjects.SetActive(!howToPlayObjects.activeSelf));
    }
}
