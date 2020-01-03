/*
 *
 * Copyright (c) Gonzako
 * Gonzako123@gmail.com
 *
 */
 
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
 
public class UnityMixerController : MonoBehaviour
{
    private const string masterVolume = "Master Volume";
    private const string sfxVolume = "SFX Volume";
    private const string musicVolume = "Music Volume";
    #region Public Fields
    [SerializeField]
    AudioMixer audioMixer;
    [Space(10)]
    [SerializeField]
    Slider masterSlider;
    [SerializeField]
    Slider sfxSlider, musicSlider;

    #endregion
 
    #region Private Fields
    #endregion

    #region Public Methods
    public void setMasterVolume(float value)
    {
        audioMixer.SetFloat(masterVolume, value);
    }
    public void setSFXVolume(float value)
    {
        audioMixer.SetFloat(sfxVolume, value);
    }
    public void setMusicVolume(float value)
    {
        audioMixer.SetFloat(musicVolume, value);
    }
    #endregion

    #region Private Methods
    #endregion


#if true
    #region Unity API

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat(masterVolume, 0);
        musicSlider.value = PlayerPrefs.GetFloat(musicVolume, 0);
        sfxSlider.value = PlayerPrefs.GetFloat(sfxVolume, 0);

    }

    private void OnDisable()
    {
        float masterVolumeValue = 0, musicVolumeValue = 0, sfxVolumeValue = 0;

        audioMixer.GetFloat(masterVolume, out masterVolumeValue);
        audioMixer.GetFloat(sfxVolume, out sfxVolumeValue);
        audioMixer.GetFloat(musicVolume, out musicVolumeValue);

        PlayerPrefs.SetFloat(masterVolume, masterVolumeValue);
        PlayerPrefs.SetFloat(sfxVolume, sfxVolumeValue);
        PlayerPrefs.SetFloat(musicVolume, musicVolumeValue);

        PlayerPrefs.Save();
    }

    #endregion
#endif

}