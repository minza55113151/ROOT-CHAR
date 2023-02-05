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

    public GameObject winningPrefab;
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
                StartCoroutine(ChangeStage(1));
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
    public IEnumerator ChangeStage(int i)
    {
        if (isChanging) yield return null;
        isChanging = true;
        
        Transform canvasTransform = GameObject.Find("Canvas").transform;
        Instantiate(winningPrefab, canvasTransform);
        SoundManager.instance.PlayKazoo();  
        yield return new WaitForSeconds(2f);
        stage += i;
        PlayerPrefs.SetInt("stage", stage);
        SceneLoadManager.instance.LoadScene("Stage" + stage.ToString());
        yield return new WaitForSeconds(checkRate*1.1f);
        ResetAns();
        isChanging = false;
    }
    public void ChangeStageAdmin(int i)
    {
        StartCoroutine(ChangeStage(i));
    }
    private void ResetAns()
    {
        for (int i = 0; i < 50; i++)
        {
            PlayerPrefs.SetInt("ans" + i.ToString(), 0);
        }
    }
}
