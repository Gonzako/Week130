using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PossessController : MonoBehaviour
{
    [SerializeField] private possesableMovement _possessableCharacter = null;
    [SerializeField] private float _possessionRadius;
    private List<possesableMovement> _possesableCharacters;
    private possesableMovement _currentlyPossessed;
    private possesableMovement _lastHighlighted;
    private GhostController _ghost;
    private Transform _transform;
    private bool _canPossess;

    public event Action<GameObject> onPossessing, onDepossessing;

    public delegate void PosessionEvents(possesableMovement character);

    public static PosessionEvents onAnyPosessionPrompt;
    public static PosessionEvents onAnyPosessionPromptEnd;

    private void OnEnable()
    {
        CacheRefrences();
        _ghost.onEnable += StopPosession;
        _ghost.onDisable += StartPosession;
        killable.onAnyKill += RemoveDeadCharacterFromPossessables;
    }

    private void OnDisable()
    {
        _ghost.onEnable -= StopPosession;
        _ghost.onDisable -= StartPosession;
    }

    private void CacheRefrences()
    {
        _ghost = GetComponent<GhostController>();
        _transform = GetComponent<Transform>();
        _possesableCharacters = FindObjectsOfType<possesableMovement>().
            OfType<possesableMovement>().ToList();
        _canPossess = true;
    }

    private void Update()
    {
        Debug.Log(_canPossess);
        if (_canPossess)
        {
            _possessableCharacter = GetNearestPosessableCharacter();
            PromptCharacterPosession();
        }
        else if (!_canPossess && Input.GetKeyDown(KeyCode.E))
        {
            onDepossessing?.Invoke(_currentlyPossessed.gameObject);
            _currentlyPossessed.onDeposess();
            _currentlyPossessed = null;
        }
    }

    private void PromptCharacterPosession()
    {
        if (_possessableCharacter != null && Vector2.Distance(_transform.position,
            _possessableCharacter.transform.position) < _possessionRadius)
        {
            onAnyPosessionPrompt?.Invoke(_possessableCharacter);
            _lastHighlighted = _possessableCharacter;
            Debug.Log(_possessableCharacter.name + "can be controlled!");
            if (Input.GetKeyDown(KeyCode.E))
            {
                _possessableCharacter.onPosess();
                _currentlyPossessed = _possessableCharacter;
                onPossessing.Invoke(_currentlyPossessed.gameObject);
                _possessableCharacter = null;
                onAnyPosessionPromptEnd?.Invoke(_currentlyPossessed);
            }
        }
        else
            onAnyPosessionPromptEnd?.Invoke(_lastHighlighted);
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

    private void StopPosession()
    {
        _transform.position = _currentlyPossessed.transform.position + new Vector3(0F, 1F);
        _canPossess = true;
    }

    private void StartPosession()
    {
        _canPossess = false;
    }

    private void RemoveDeadCharacterFromPossessables(GameObject possessable)
    {
        _possesableCharacters.
            Remove(possessable.GetComponent<possesableMovement>());
    }
}