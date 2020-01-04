using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalHandler : MonoBehaviour
{
    public delegate void GoalEvents();
    public GoalEvents onPlayerTouched;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            onPlayerTouched?.Invoke();
        }
    }
}
