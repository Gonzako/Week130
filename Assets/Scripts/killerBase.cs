/*
 *
 * Copyright (c) Gonzako
 * Gonzako123@gmail.com
 *
 */
 
using UnityEngine;
 

[RequireComponent(typeof(Collider2D))]
public abstract class killerBase : MonoBehaviour
{
    #region Public Fields
    #endregion

    #region Private Fields
    Collider2D col;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion


#if true
    #region Unity API
    protected virtual void Start()
    {
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //other.attachedRigidbody.velocity = Vector2.zero;
        other.attachedRigidbody.constraints = 0;
        other.GetComponent<killable>()?.onKill();

    }

    #endregion
#endif

}