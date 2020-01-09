using UnityEngine;

public class GhostController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _playerSprite;
    private Transform _transform;
    private bool _canMove;
    private PossessController _possessCont;
    private killable _currentPossessed;
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
        _possessCont.onPossessing += GhostDisable;
        _possessCont.onDepossessing += GhostEnable;
        //killable.onAnyKill += GhostEnable;
    }

    private void OnDisable()
    {
        _possessCont.onPossessing -= GhostDisable;
        _possessCont.onDepossessing -= GhostEnable;
    }

    private void CacheRefences()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
        _possessCont = GetComponent<PossessController>();
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
            transform.localScale = new Vector3(1, 1);
        }
        else if (_horizontalInput > 0)
        {
            transform.localScale = new Vector3(-1, 1);
            
        }
    }

    private void GhostDisable(GameObject target)
    {
        _canMove = false;
        _playerSprite.enabled = false;
        _currentPossessed = target.GetComponent<killable>();
        _currentPossessed.onThisKill += GhostEnable;
        onDisable?.Invoke();
    }

    private void GhostEnable(GameObject disabler)
    {
        _currentPossessed.onThisKill -= GhostEnable;
        _currentPossessed = null;
        _canMove = true;
        this.enabled = true;
        _playerSprite.enabled = true;
        onEnable?.Invoke();
       
    }
}