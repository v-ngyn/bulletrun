using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene("bulletrun");
    }

    public void menu()
    {
        SceneManager.LoadScene("menu");
    }

    public void exit()
    {
        Application.Quit();
    }
}
