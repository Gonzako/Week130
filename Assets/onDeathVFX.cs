/*
 *
 * Copyright (c) Gonzako
 * Gonzako123@gmail.com
 *
 */

using System;
using System.Collections.Generic;
using UnityEngine;
 
public class onDeathVFX : MonoBehaviour
{
    #region Public Fields

    public List<GameObject> VFXToSpawnOnDeath;

    #endregion

    #region Private Fields
    private List<GameObject> gameObjects;
    killable killableScript;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void handleDeath(GameObject caller)
    {
        foreach(GameObject n in gameObjects)
        {
            n.transform.position = caller.transform.position;
            n.SetActive(true);
        }
    }
    #endregion


    #if true
    #region Unity API

    void Start()
    {
        killableScript = GetComponent<killable>();
        killableScript.onThisKill += handleDeath;
        poolVFX();
    }

    private void poolVFX()
    {
        gameObjects = new List<GameObject>(VFXToSpawnOnDeath.Count);
        for (int i = 0; i < VFXToSpawnOnDeath.Count; i++)
        {
            gameObjects[i] = Instantiate<GameObject>(VFXToSpawnOnDeath[i]);
            
        }
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