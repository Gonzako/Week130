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
    Rigidbody2D rb;
    Vector3 nextLocalSpace;
    possesableMovement PossesableMovement;
    private const string jumpTrigger = "Jump";
    private const string walkingBool = "Walking";
    private const string verticalVelVariable = "Vertical Velocity";
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

    private void refreshAnimatorVariables()
    {
        if (Input.GetButton("Jump"))
        {
            animator.SetTrigger(jumpTrigger);
        }
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            animator.SetBool(walkingBool, true);
        }
        else
        {
            animator.SetBool(walkingBool, false);
        }
        animator.SetFloat(verticalVelVariable, rb.velocity.y);
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
        rb = GetComponent<Rigidbody2D>();
        PossesableMovement = GetComponent<possesableMovement>();
    }

    private void Update()
    {
        if (PossesableMovement.Possessed)
        {
            flipCheck();
            refreshAnimatorVariables(); 
        }
    }


    void FixedUpdate()
    {
    }

    #endregion
    #endif
 
}