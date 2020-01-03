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
    #region Public Fields
    [SerializeField]
    AudioMixer aM;
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
        aM.SetFloat("Master Volume", value);
    }
    public void setSFXVolume(float value)
    {
        aM.SetFloat("SFX Volume", value);
    }
    public void setMusicVolume(float value)
    {
        aM.SetFloat("Music Volume", value);
    }
    #endregion

    #region Private Methods
    #endregion


#if true
    #region Unity API

    private void Start()
    {
        
    }

    private void OnDisable()
    {
        float masterVolume = 0, musicVolume = 0, sfxVolume = 0;

        aM.GetFloat("Master Volume", out masterVolume);
        aM.GetFloat("SFX Volume", out sfxVolume);
        aM.GetFloat("Music Volume", out musicVolume);



    }

    #endregion
#endif

}