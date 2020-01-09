/*
 *
 * Copyright (c) Gonzako
 * Gonzako123@gmail.com
 *
 */
 
using UnityEngine;
 
public class spikeDeathSFX : MonoBehaviour
{
    #region Public Fields
    public AudioClip[] sounds;
    #endregion

    #region Private Fields
    private AudioSource spikeSFXAS;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion


    #if true

    #region Unity API

    void Start()
    {
        spikeSFXAS = gameObject.AddComponent<AudioSource>();
        spikeScript.onAnySpike += handleSpiking;
    }

    private void OnDisable()
    {
        spikeScript.onAnySpike -= handleSpiking;
    }

    private void handleSpiking(GameObject spike, Collider2D reciever)
    {
        spikeSFXAS.clip = sounds[Random.Range(0, sounds.Length)];
        spikeSFXAS.pitch = Random.Range(0.9f, 1.2f);
        spikeSFXAS.Play();
    }

    void FixedUpdate()
    {
    }

    #endregion
    #endif
 
}