using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isAdmin = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        AdminInput();
        Admin();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneLoadManager.instance.LoadScene("MainMenu");
        }
    }
    bool a = false;
    bool d = false;
    bool m = false;
    bool i = false;
    bool n = false;
    private void AdminInput()
    {
        if (a && d && m && i && n)
        {
            //unlock admin
            isAdmin = true;
            a = false;
            d = false;
            m = false;
            i = false;
            n = false;
        }
        else if (a && d && m && i && Input.GetKeyDown(KeyCode.N))
        {
            n = true;
            Debug.Log("N");
        }
        else if (a && d && m && Input.GetKeyDown(KeyCode.I))
        {
            i = true;
            Debug.Log("I");
        }
        else if (a && d && Input.GetKeyDown(KeyCode.M))
        {
            m = true;
            Debug.Log("M");
        }
        else if (a && Input.GetKeyDown(KeyCode.D))
        {
            d = true;
            Debug.Log("D");
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            a = true;
            Debug.Log("A");
        }
        else if (Input.anyKeyDown) 
        {
            a = false;
            d = false;
            m = false;
            i = false;
            n = false;
        }
    }
    private void Admin()
    {
        if (!isAdmin) return;
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            StageManager.instance.ChangeStage(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            StageManager.instance.ChangeStage(1);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
            StageManager.instance.stage = 0;
        }
    }
}
