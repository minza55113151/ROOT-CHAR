using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadManager: MonoBehaviour
{
    public static SceneLoadManager instance;

    public float fadeTime;
    public float loadingTime;
    public Image FadeImage;
    public GameObject LoadingText;
    public bool isLoading = false;

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
    public void LoadScene(string sceneName)
    {
        if (isLoading) return;
        isLoading = true;
        StartCoroutine(LoadSceneIE(sceneName));
    }
    public IEnumerator LoadSceneIE(string sceneName)
    {
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(fadeTime * 1.3f);

            
        LoadingText.SetActive(true);
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(loadingTime);
        LoadingText.SetActive(false);

        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(fadeTime);

        isLoading = false;
    }
    public IEnumerator FadeOut()
    {
        for (float i = 0; i < fadeTime; i += Time.deltaTime)
        {
            FadeImage.color = Color.Lerp(Color.clear, Color.black, i / fadeTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    public IEnumerator FadeIn()
    {
        for (float i = 0; i < fadeTime; i += Time.deltaTime)
        {
            FadeImage.color = Color.Lerp(Color.black, Color.clear, i / fadeTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
