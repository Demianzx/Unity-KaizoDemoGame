using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitGame : MonoBehaviour {
    public GameObject loadingImage;

    public void doExitGame()
    {
        loadingImage.SetActive(true);
        Application.Quit();
    }
}
