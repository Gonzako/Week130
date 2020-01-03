/*
 *
 * Copyright (c) Gonzako
 * Gonzako123@gmail.com
 *
 */

using System;
using System.Collections;
using UnityEngine;


public class possesableMovement : MonoBehaviour, IPossesable
{
    #region Public Fields
    public static event Action<GameObject> onAnyPosses;
    public event Action onThisPosses;
    #endregion

    #region Private Fields
    [SerializeField, Range(0,1)]
    float contactLeeway;
    [SerializeField]
    bool possessed = false;
    [SerializeField, Range(0, 1)]
    float coyoteTime = 0.3f;
    Rigidbody2D rb;
    Collider2D col;
    [SerializeField]
    float jumpForce, horizontalSpeed;
    [SerializeField]
    float raycastDownLeeway = 0.2f;
    int horizontalInput = 0;
    bool jump = false, grounded = false;
    Coroutine groundedCoroutine;
    #endregion

    #region Public Methods
    public void onPosess()
    {
        possessed = true;
        onAnyPosses?.Invoke(this.gameObject);
        onThisPosses?.Invoke();
    }
    #endregion

    #region Private Methods
    private IEnumerator stopGrounded()
    {
        yield return new WaitForSeconds(coyoteTime);
        grounded = false;
        groundedCoroutine = null;
    }

    private void moveCharacter()
    {
        rb.AddForce(Vector2.right * horizontalInput * horizontalSpeed * Time.fixedDeltaTime);
        horizontalInput = 0;
    }

    private void doJump()
    {
        rb.AddForce(Vector2.up * jumpForce);
        grounded = false;
        jump = false;
    }
    #endregion


#if true
    #region Unity API

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }
 
    void Update()
    {
        if (possessed)
        {
            horizontalInput = (int)Input.GetAxisRaw("Horizontal");
            jump = Input.GetButton("Jump") && grounded;

        }
    }

    void FixedUpdate()
    {
        if (possessed)
        {
            moveCharacter();
            if (jump)
            {
                doJump();
            }
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (possessed)
        {

            {
                for (int i = 0; i < collision.contactCount; i++)
                {
                    if (Vector2.Dot(collision.GetContact(i).normal.normalized, Vector2.up) >= contactLeeway)
                    {
                        grounded = true;
                    }
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.contactCount == 0 && possessed)
        {
            if (groundedCoroutine == null && grounded)
            {
                groundedCoroutine = StartCoroutine("stopGrounded");

            }
        }
    }
    #endregion
#endif

}