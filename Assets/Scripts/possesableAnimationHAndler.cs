/*
 *
 * Copyright (c) Gonzako
 * Gonzako123@gmail.com
 *
 */
 
using UnityEngine;
 

[RequireComponent(typeof(Animator))]
public class possesableAnimationHandler : MonoBehaviour
{
    #region Public Fields
    [SerializeField]
    Animator animator;
    #endregion
 
    #region Private Fields
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion


    #if true
    #region Unity API

    void Start()
    {
        if(animator != null)
        {
            animator = GetComponent<Animator>();
        }
    }
 
    void FixedUpdate()
    {
    }

    #endregion
    #endif
 
}