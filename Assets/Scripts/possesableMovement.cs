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
    public static event Action<GameObject> onAnyPosses, onAnyDeposses;
    public event Action onThisPosses, onThisDeposses, onThisJump;
    public bool Possessed { get { return possessed; } }
    #endregion

    #region Private Fields
    [SerializeField]
    LayerMask groundLayers;
    [SerializeField, Range(0,1)]
    float contactLeeway;
    [SerializeField]
    bool possessed = false;
    [SerializeField, Range(0, 1)]
    float coyoteTime = 0.3f;
    Rigidbody2D rb;
    [SerializeField]
    float jumpForce, horizontalSpeed;
    [SerializeField]
    float raycastDownLeeway = 0.2f;
    int horizontalInput = 0;
    bool jump = false, grounded = false;
    Coroutine groundedCoroutine;
    [SerializeField]
    private float extraDistanceCheck;
    CapsuleCollider2D cb;
    private bool areWeGrounded;
    private bool areWeCoyoteTime => canWeCoyoteTime && Time.time <= coyoteTimer;
    private float coyoteTimer;
    private bool canWeCoyoteTime;
    #endregion


    #region GBSavers

#if true
    private Color _DebugColour;
    private float _CurrentHitDistance;
#endif
    private RaycastHit2D _castHit;
    private RaycastHit2D lastUpdateHit;
    #endregion

    #region Public Methods
    public void onPosess()
    {
        possessed = true;
        onAnyPosses?.Invoke(this.gameObject);
        onThisPosses?.Invoke();
    }

    public void onDeposess()
    {
        possessed = false;
        onThisPosses?.Invoke();
        onAnyDeposses?.Invoke(this.gameObject);
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
        rb.AddForce(Vector2.right * horizontalInput * horizontalSpeed * Time.fixedDeltaTime, ForceMode2D.Force);
        horizontalInput = 0;
    }

    private void doJump()
    {

        onThisJump?.Invoke();
        rb.AddForce(Vector2.up * jumpForce);
        canWeCoyoteTime = false;
        areWeGrounded = false;
        jump = false;
    }

    private void CheckTheGround()
    {
        _castHit = Physics2D.CapsuleCast(transform.position-(Vector3)cb.offset, cb.size, cb.direction, 0, -transform.up, extraDistanceCheck, groundLayers);

        if (_castHit.collider != null)
        {
#if UNITY_EDITOR
            _DebugColour = Color.green;
            _CurrentHitDistance = _castHit.distance + cb.bounds.extents.y;
#endif
            
            if (lastUpdateHit.collider == null)
            {
                canWeCoyoteTime = true;
            }
            areWeGrounded = true;
            Debug.Log("We are grounded");
        }
        else
        {
#if UNITY_EDITOR
            _DebugColour = Color.red;
            _CurrentHitDistance = (cb.bounds.extents.y + extraDistanceCheck);
            Debug.Log(this.gameObject + "cannot jump");
#endif

            areWeGrounded = false;
            if (lastUpdateHit.collider != null)
            {
                startCoyoteTime();
            }

        }
        lastUpdateHit = _castHit;

    }
    private void startCoyoteTime()
    {
        coyoteTimer = Time.time + coyoteTime;
    }

    #endregion


#if true
    #region Unity API

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cb = GetComponent<CapsuleCollider2D>();
    }
 
    void Update()
    {
        if (possessed)
        {
            horizontalInput = (int)Input.GetAxisRaw("Horizontal");
            jump = Input.GetButtonDown("Jump") && (areWeGrounded);

        }
    }

    void FixedUpdate()
    {
        CheckTheGround();
        if (possessed)
        {
            moveCharacter();
            if (jump)
            {
                doJump();
            }
        }
    }


    private void OnDrawGizmos()
    {

        if (true)
        {
            Gizmos.color = _DebugColour;
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - _CurrentHitDistance));
            Gizmos.DrawWireSphere(new Vector2(transform.position.x, transform.position.y + cb.bounds.extents.x - _CurrentHitDistance), cb.bounds.extents.x);
        }
    }
    #endregion
#endif

}