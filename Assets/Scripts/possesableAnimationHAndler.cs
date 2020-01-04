/*
 *
 * Copyright (c) Gonzako
 * Gonzako123@gmail.com
 *
 */

using System;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class possesableAnimationHandler : MonoBehaviour
{
    #region Public Fields
    [SerializeField]
    Animator animator;
    #endregion

    #region Private Fields
    Vector3 nextLocalSpace;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void flipCheck()
    {
        if(Input.GetAxisRaw("Horizontal") == 1)
        {
            nextLocalSpace = transform.localScale;
            nextLocalSpace.x = Mathf.Abs(nextLocalSpace.x);
            transform.localScale = nextLocalSpace;
        }
        else if(Input.GetAxisRaw("Horizontal") == -1)
        {
            nextLocalSpace = transform.localScale;
            nextLocalSpace.x = -Mathf.Abs(nextLocalSpace.x);
            transform.localScale = nextLocalSpace;
        }
    }
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

    private void Update()
    {
        flipCheck();
    }



    void FixedUpdate()
    {
    }

    #endregion
    #endif
 
}