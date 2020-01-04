using UnityEngine;

public class GhostController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _playerSprite;
    private Transform _transform;
    private bool _canMove;
    

    [Header("Movement Properties ")]
    [SerializeField] private float _floatSpeed;

    public float _horizontalInput;
    public float _verticalInput;
    public delegate void GhostEvents();
    public GhostEvents onDeath;
    public GhostEvents onEnable;
    public GhostEvents onDisable;
  
    public bool isPhasingThroughObject()
    {
        if (!_playerSprite.isVisible)
        {
            Debug.Log("Inside a wall");
            return true;
        }
        return false;
    }

    private void Update()
    {
        FlipCharacter();
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            HorizontalMovement();
            VerticalMovement();
        }
    }

    private void OnEnable()
    {
        CacheRefences();
        possesableMovement.onAnyPosses += GhostDisable;
        possesableMovement.onAnyDeposses += GhostEnable;
        killable.onAnyKill += GhostEnable;
    }

    private void OnDisable()
    {
        possesableMovement.onAnyPosses -= GhostDisable;
        possesableMovement.onAnyDeposses -= GhostEnable;
    }

    private void CacheRefences()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
        _canMove = true;
    }

    private void HorizontalMovement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");

        _rigidbody.AddForce(new Vector2(_horizontalInput  * _floatSpeed, 0F));
    }

    private void VerticalMovement()
    {
        _verticalInput = Input.GetAxis("Vertical");

        _rigidbody.AddForce(new Vector2(0, _verticalInput  * _floatSpeed));
    }

    private void FlipCharacter()
    {
        if (_horizontalInput < 0)
        {
            _playerSprite.flipX = true;
        }
        else if (_horizontalInput > 0)
        {
            _playerSprite.flipX = false;
        }
    }

    private void GhostDisable(GameObject disabler)
    {
        _canMove = false;
        _playerSprite.enabled = false;
        onDisable?.Invoke();
    }

    private void GhostEnable(GameObject disabler)
    {
        _canMove = true;
        this.enabled = true;
        _playerSprite.enabled = true;
        onEnable?.Invoke();
       
    }
}