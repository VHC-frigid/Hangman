using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
    }
}
