using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public delegate void GameEvents();
    public static GameEvents onLevelComplete;
    public static GameEvents onLevelFail;
    public static GameEvents onLevelStart;

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
        spikeScript.onAnySpike += LevelFailure;
        GoalHandler.onPlayerTouchedAnyGoal += LevelComplete;
    }

    private void OnDisable()
    {
        spikeScript.onAnySpike -= LevelFailure;
        GoalHandler.onPlayerTouchedAnyGoal -= LevelComplete;
    }

    public void LevelFailure(GameObject ob, Collider2D col)
    {
        if(ob.transform.tag == "Player")
            onLevelFail?.Invoke();
    }

    public void LevelComplete()
    {
        onLevelComplete?.Invoke();
    }

}
