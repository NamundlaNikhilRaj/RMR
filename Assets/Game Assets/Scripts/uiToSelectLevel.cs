using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class uiToSelectLevel : MonoBehaviour
{
    public void UIScene()
    {
        SceneManager.LoadScene("RMR_UI");
    }

    public void LevelSelection()
    {
        SceneManager.LoadScene("Level Selection");
    }

    public void RMRL1()
    {
        SceneManager.LoadScene("RMR");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Appliaction is Quitting");
    }
}
