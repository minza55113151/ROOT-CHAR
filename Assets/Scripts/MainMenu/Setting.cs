using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        bgmSlider.value = PlayerPrefs.GetFloat("BGM", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 1f);
        SoundManager.instance.ChangeBGMSound(bgmSlider.value);
        SoundManager.instance.ChangeSFXSound(sfxSlider.value);
    }
    private void Update()
    {
        SoundManager.instance.ChangeBGMSound(bgmSlider.value);
        SoundManager.instance.ChangeSFXSound(sfxSlider.value);
        PlayerPrefs.SetFloat("BGM", bgmSlider.value);
        PlayerPrefs.SetFloat("SFX", sfxSlider.value);
    }
}
