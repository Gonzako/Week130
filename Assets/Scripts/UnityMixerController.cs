/*
 *
 * Copyright (c) Gonzako
 * Gonzako123@gmail.com
 *
 */
 
using UnityEngine;
using UnityEngine.Audio;
 
public class UnityMixerController : MonoBehaviour
{
    #region Public Fields
    public AudioMixer aM;
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


    #if false
    #region Unity API

    void Start()
    {
    }
 
    void FixedUpdate()
    {
    }

    #endregion
    #endif
 
}