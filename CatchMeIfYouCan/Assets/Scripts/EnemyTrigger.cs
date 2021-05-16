using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    bool _entry;
    public delegate void GameOverAction();

    public static event GameOverAction OnHitPlayer;

    void OnTriggerEnter(Collider other)
    {
        if (!_entry && other.GetComponent<PlayerTag>() != null)
        {
            _entry = true;
            OnHitPlayer();
        }
    }
}