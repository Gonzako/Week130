/*
 *
 * Copyright (c) Gonzako
 * Gonzako123@gmail.com
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class spikeScript : killerBase
{
    #region Public Fields
    /// <summary>
    /// Game object is caller, collider is reciever
    /// </summary>
    public static event Action<GameObject, Collider2D> onAnySpike;
    public event Action onThisSpike;
    #endregion

    #region Private Fields
    [SerializeField]
    float freezeTime = 3f;
    [SerializeField]
    bool freezeForever = false;

    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private IEnumerator unFreezeRb(Collider2D other)
    {
        yield return new WaitForSeconds(freezeTime);
        other.attachedRigidbody.constraints = 0;
        other.attachedRigidbody.AddForce(Vector2.down);
        other.attachedRigidbody.AddTorque(UnityEngine.Random.Range(-1, 1));
    }
    #endregion


    #if true
    #region Unity API

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);        
        onAnySpike?.Invoke(this.gameObject, other);
        onThisSpike?.Invoke();
        other.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        if (!freezeForever) { StartCoroutine(unFreezeRb(other)); }
    }
    #endregion
#endif

}