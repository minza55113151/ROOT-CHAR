using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public int stage;
    public List<string> ansWord;
    public List<int> lengthAnsWord;


    bool isChange = false;
    public float checkRate;
    float checkTime = 0f;

    bool isChanging = false;
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
    private void Start()
    {
        stage = PlayerPrefs.GetInt("stage", 0);
        /*if (stage == 0)
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex - 2;
            if(sceneIndex >= 0)
            {
                stage = sceneIndex;
                PlayerPrefs.SetInt("stage", stage);
            }
        }*/
        ResetAns();
    }

    private void Update()
    {
        Check();
    }
    private void Check()
    {
        if (isChanging) return;
        checkTime += Time.deltaTime;
        if (checkTime < checkRate) return;
        checkTime -= checkRate;

        Debug.Log("STAGE" + stage.ToString());
        
        if (isChange)
        {   
            int lengthAns = lengthAnsWord[stage];
            for (int i = 0; i < lengthAns; i++)
            {
                if (PlayerPrefs.GetInt("ans" + i.ToString(), 0) == 0)
                {
                    isChange = false;
                    return;
                }
            }
            if (isChanging == false)
            {
                StartCoroutine(ChangeStage());
            }
        }
        else if (isChange == false)
        {
            ResetAns();
        }
        isChange = false;
    }

    public void Change()
    {
        isChange = true;
    }
    public IEnumerator ChangeStage()
    {
        if (isChanging) yield return null;
        isChanging = true;
        stage += 1;
        PlayerPrefs.SetInt("stage", stage);
        SceneLoadManager.instance.LoadScene("Stage" + stage.ToString());
        yield return new WaitForSeconds(checkRate*1.1f);
        ResetAns();
        isChanging = false;
    }
    public void ChangeStage(int i)
    {
        if(i == -1 && stage > 0)
        {
            if (isChanging) return;
            isChanging = true;
            stage -= 1;
            PlayerPrefs.SetInt("stage", stage);
            SceneLoadManager.instance.LoadScene("Stage" + stage.ToString());
            ResetAns();
            isChanging = false;
        }
        else
        {
            ChangeStage();
        }
    }
    private void ResetAns()
    {
        for (int i = 0; i < 50; i++)
        {
            PlayerPrefs.SetInt("ans" + i.ToString(), 0);
        }
    }
}
