using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("Scene_05");
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo");
        Application.Quit();
    }
}
