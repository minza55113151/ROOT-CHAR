using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;
    public GameObject[] menus;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        OpenMenu("MainMenu");
    }

    public void PlayGame()
    {
        SceneLoadManager.instance.LoadScene("Game");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void OpenMenu(GameObject gameObject)
    {
        OpenMenu(gameObject.name);
    }
    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].name == menuName)
            {
                menus[i].SetActive(true);
            }
            else
            {
                menus[i].SetActive(false);
            }
        }
    }
}
