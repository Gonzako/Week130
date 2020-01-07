/*
 *
 * Copyright (c) Gonzako
 * Gonzako123@gmail.com
 *
 */
 
using UnityEngine;
 
public class gravityScaler : MonoBehaviour
{
    #region Public Fields
    [SerializeField, Range(1, 5)]
    private float fallScale = 2;
    [SerializeField]
    private float donwardVelCheck ;
    #endregion

    #region Private Fields
    private Rigidbody2D rb;
    private float defaultGravScale;
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void CacheReferences()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGravScale = rb.gravityScale;
    }
    #endregion


    #if true
    #region Unity API

    void Start()
    {
        CacheReferences();
    }
 
    void FixedUpdate()
    {

        donwardVelCheck = rb.velocity.y;
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = fallScale;
        }
        else
        {
            rb.gravityScale = defaultGravScale;
        }
    }

    #endregion
    #endif
 
}