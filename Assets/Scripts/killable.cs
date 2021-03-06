﻿/*
 *
 * Copyright (c) Gonzako
 * Gonzako123@gmail.com
 *
 */

using System;
using System.Collections;
using UnityEngine;
 
public class killable : MonoBehaviour, Ikillable
{
    #region Public Fields
    public static event Action<GameObject> onAnyKill;
    public event Action<GameObject> onThisKill;
    [SerializeField]
    MonoBehaviour[] componentsToDisable;
    #endregion
 
    #region Private Fields
    #endregion

    #region Public Methods

    public void onKill()
    {

        onThisKill?.Invoke(this.gameObject);
        onAnyKill?.Invoke(this.gameObject);
        foreach(MonoBehaviour n in componentsToDisable)
        {
            n.enabled = false;
        }
        this.gameObject.layer = LayerMask.NameToLayer("Interactibles");
        this.enabled = false;
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