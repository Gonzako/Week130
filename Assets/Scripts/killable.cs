/*
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
    public event Action onThisKill;
    [SerializeField]
    #endregion
 
    #region Private Fields
    #endregion

    #region Public Methods

    public void onKill()
    {
        onAnyKill?.Invoke(this.gameObject);
        onThisKill.Invoke();
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