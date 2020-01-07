using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public delegate void GameEvents();
    public static GameEvents onLevelComplete;
    public static GameEvents onLevelFail;
    public static GameEvents onLevelStart;

    private const string _playerBodyTag = "PlayerControllable";

    private GhostController _ghost;
    private killable _playerKillable;

    // Start is called before the first frame update
    private void Start()
    {

        onLevelStart?.Invoke();
    }

    private void CacheRefences()
    {
        _playerKillable = GameObject.FindGameObjectWithTag(_playerBodyTag).GetComponent<killable>();
        _ghost = FindObjectOfType<GhostController>();
    }

    private void OnEnable()
    {
        CacheRefences();
        _playerKillable.onThisKill += LevelFailure;
        GoalHandler.onPlayerTouchedAnyGoal += LevelComplete;
    }

    private void OnDisable()
    {
        _playerKillable.onThisKill += LevelFailure;
        GoalHandler.onPlayerTouchedAnyGoal -= LevelComplete;
    }

    public void LevelFailure(GameObject caller)
    {
        Debug.Log("Tag : "+ caller.transform.tag);
        if(caller.transform.tag == "PlayerControllable")
            onLevelFail?.Invoke();
    }

    public void LevelComplete()
    {
        onLevelComplete?.Invoke();
    }

}
