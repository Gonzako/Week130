using UnityEngine;

public class GhostController : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    private SpriteRenderer _playerSprite;
    private Transform _transform;
    private bool _ghostEnabled;

    [SerializeField] private possesableMovement _possessableCharacter = null;
    private possesableMovement[] _possesableCharacters;
    private possesableMovement _currentlyPossessed;

    public float _horizontalInput;
    public float _verticalInput;

    [Header("Movement Properties ")]
    [SerializeField] private float _floatSpeed;

    public delegate void GhostEvents();
    public GhostEvents onDeath;
   
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
        if (_ghostEnabled)
        {
            _possessableCharacter = GetNearestPosessableCharacter();
            FlipCharacter();
            PromptCharacterPosession();
        }
        else if(!_ghostEnabled && Input.GetKeyDown(KeyCode.E))
        {
            _currentlyPossessed.onDeposess();
        }
    }

    private void FixedUpdate()
    {
        if (_ghostEnabled)
        {
            HorizontalMovement();
            VerticalMovement();

            Debug.Log(isPhasingThroughObject());
        }
    }

    private void OnEnable()
    {
        CacheRefences();
        possesableMovement.onAnyPosses += GhostDisable;
        possesableMovement.onAnyDeposses += GhostEnable;
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
        _possesableCharacters = FindObjectsOfType<possesableMovement>();
        _ghostEnabled = true;
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
        _ghostEnabled = false;
        _playerSprite.enabled = false;
    }

    private void GhostEnable(GameObject disabler)
    {
        _ghostEnabled = true;
        _playerSprite.enabled = true;
    }

    private void PromptCharacterPosession()
    {
        if (_possessableCharacter != null)
        {
            Debug.Log(_possessableCharacter.name + "can be controlled!");
            if (Input.GetKeyDown(KeyCode.E))
            {
                _possessableCharacter.onPosess();
                _currentlyPossessed = _possessableCharacter;
            }
        }
    }

    private possesableMovement GetNearestPosessableCharacter()
    {
        possesableMovement nearest = null;
        //Currently we have no character scripts to

        foreach (possesableMovement character in _possesableCharacters)
        {
            if (nearest == null)
            {
                nearest = character;
            }
            else if (Vector2.Distance(nearest.transform.position, _transform.position) >
               Vector2.Distance(character.transform.position, _transform.position))
            {
                nearest = character;
            }
        }

        return nearest;
    }
}