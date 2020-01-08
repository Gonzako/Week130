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
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].transform.position = caller.transform.position;
            gameObjects[i].SetActive(true);
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
            gameObjects[i] = Instantiate(VFXToSpawnOnDeath[i]);
            gameObjects[i].SetActive(false);
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