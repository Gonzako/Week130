﻿/*
 *
 * Copyright (c) Gonzako
 * Gonzako123@gmail.com
 *
 */

using System;
using UnityEngine;

public class possesableMovement : MonoBehaviour, IPossesable
{
    #region Public Fields
    public static event Action onAnyPosses;
    public event Action onThisPosses;
    #endregion

    #region Private Fields
    bool possessed = false;
    Rigidbody2D rb;
    [SerializeField]
    float jumpForce, horizontalSpeed;
    int horizontalInput { get { return (int)Input.GetAxisRaw("Horizontal"); } };
    #endregion

    #region Public Methods
    public void onPosess()
    {
        possessed = true;
        onAnyPosses?.Invoke();
        onThisPosses?.Invoke();
    }
    #endregion

    #region Private Methods
    #endregion


#if true
    #region Unity API

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
 
    void Update()
    {

    }

    void FixedUpdate()
    {

    }

    #endregion
#endif

}