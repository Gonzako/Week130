/*
 *
 * Copyright (c) Gonzako
 * Gonzako123@gmail.com
 *
 */
 
using UnityEngine;
 
public class onDeathVFX : MonoBehaviour
{
    #region Public Fields

    public GameObject[] VFXToSpawnOnDeath;
    #endregion

    #region Private Fields
    killable killableScript;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void handleDeath(GameObject caller)
    {
        foreach(GameObject n in VFXToSpawnOnDeath)
        {
            Instantiate<GameObject>(n, caller.transform.position, Quaternion.identity);
        }
    }
    #endregion


    #if true
    #region Unity API

    void Start()
    {
        killableScript = GetComponent<killable>();
        killableScript.onThisKill += handleDeath;
    }

    private void OnDisable()
    {
        killableScript.onThisKill -= handleDeath;
    }

    void FixedUpdate()
    {
    }

    #endregion
    #endif
 
}