using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    bool _entry;
    public delegate void HitAction();
    public delegate void ExitAction();

    public static event HitAction OnObstacleIn;
    public static event ExitAction OnObstacleOut;

    void OnTriggerEnter(Collider other)
    {
        if (!_entry && other.GetComponent<PlayerTag>() != null)
        {
            _entry = true;
            OnObstacleIn();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_entry && other.GetComponent<PlayerTag>() != null)
        {
            _entry = false;
            OnObstacleOut();
        }
    }
}