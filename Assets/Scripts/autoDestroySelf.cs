/*
 *
 * Copyright (c) Gonzako
 * Gonzako123@gmail.com
 *
 */
 
using UnityEngine;
 
public class autoDestroySelf : MonoBehaviour
{
    #region Public Fields
    [SerializeField]
    private float timeUpUntilDeath = 2f;
    #endregion
 
    #region Private Fields
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion


    #if true
    #region Unity API

    void Awake()
    {
        Destroy(this.gameObject, timeUpUntilDeath);
    }
 
    void FixedUpdate()
    {
    }

    #endregion
    #endif
 
}