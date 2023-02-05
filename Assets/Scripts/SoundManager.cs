using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource kazoo;
    public AudioSource bgmusic;
    public AudioSource clickNode;
    public AudioSource exitNode;
    public AudioSource clickBox;
    public AudioSource exitBox;
    
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
    public void ChangeBGMSound(float val)
    {
        bgmusic.volume = val;
    }
    public void ChangeSFXSound(float val)
    {
        kazoo.volume = val;
        clickNode.volume = val;
        exitNode.volume = val;
        clickBox.volume = val;
        exitBox.volume = val;
    }

    public void PlayKazoo()
    {
        kazoo.Play();
    }
    public void PlayClickNode()
    {
        clickNode.Play();
    }
    public void PlayExitNode()
    {
        exitNode.Play();
    }
    public void PlayClickBox()
    {
        clickBox.Play();
    }
    public void PlayExitBox()
    {
        exitBox.Play();
    }
}
