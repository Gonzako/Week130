using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public delegate void GameEvents();
    public GameEvents onLevelComplete;
    public GameEvents onLevelFail;
    public GameEvents onLevelStart;

    private GhostController _ghost;


    // Start is called before the first frame update
    private void Start()
    {
        onLevelStart?.Invoke();
    }

    private void CacheRefences()
    {
        _ghost = FindObjectOfType<GhostController>();
    }

    private void OnEnable()
    {
        CacheRefences();
        _ghost.onDeath += LevelFailure;
    }

    private void OnDisable()
    {
        _ghost.onDeath -= LevelFailure;
    }

    public void LevelFailure()
    {
        onLevelFail?.Invoke();
    }

    public void LevelComplete()
    {
        onLevelComplete?.Invoke();
    }

}
