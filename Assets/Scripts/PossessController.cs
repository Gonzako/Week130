using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessController : MonoBehaviour
{

    [SerializeField] private possesableMovement _possessableCharacter = null;
    private possesableMovement[] _possesableCharacters;
    private possesableMovement _currentlyPossessed;
    private GhostController _ghost;
    private Transform _transform;
    private bool _canPossess;

    private void OnEnable()
    {
        CacheRefrences();
        _ghost.onEnable += StopPosession;
        _ghost.onDisable += StartPosession;
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
        _possesableCharacters = FindObjectsOfType<possesableMovement>();
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
            _currentlyPossessed.onDeposess();
            _currentlyPossessed = null;
        }
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

    private void StopPosession()
    {
        _transform.position = _currentlyPossessed.transform.position + new Vector3(0F, 1F);
        _canPossess = true;
    }

    private void StartPosession()
    {
        _canPossess = false;
    }
}
