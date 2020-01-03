using UnityEngine;

public class GhostController : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    private SpriteRenderer _playerSprite;
    private Transform _transform;

    private possesableMovement _possessableCharacter = null;
    private possesableMovement[] _possesableCharacters;

    public float _horizontalInput;
    public float _verticalInput;

    [Header("Movement Properties ")]
    [SerializeField] private float _walkSpeed;

    [SerializeField] private float _runSpeed;

    // Start is called before the first frame update

    // Update is called once per frame
    private void Update()
    {
        FlipCharacter();
    }

    private void FixedUpdate()
    {
        HorizontalMovement();
        VerticalMovement();
    }

    private void OnEnable()
    {
        CacheRefences();
        possesableMovement.onAnyPosses += GhostDisable;
    }

    private void OnDisable()
    {
        possesableMovement.onAnyPosses -= GhostDisable;
    }

    private void CacheRefences()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
        _possesableCharacters = FindObjectsOfType<possesableMovement>();
    }

    private void HorizontalMovement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");

        _rigidbody.velocity += new Vector2(_horizontalInput * _walkSpeed, 0F);
    }

    private void VerticalMovement()
    {
        _verticalInput = Input.GetAxis("Vertical");

        _rigidbody.velocity += new Vector2(0, _verticalInput * _walkSpeed);
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
        this.gameObject.SetActive(false);
    }

    private void PromptCharacterPosession()
    {
        if (_possessableCharacter != null)
        {
            Debug.Log(_possessableCharacter.name + "can be controlled!");
            if (Input.GetKeyDown(KeyCode.E))
            {
                _possessableCharacter.onPosess();
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
            else if (Vector2.Distance(nearest.transform.position, _transform.position) <
               Vector2.Distance(character.transform.position, _transform.position))
            {
                nearest = character;
            }
        }

        return nearest;
    }
}