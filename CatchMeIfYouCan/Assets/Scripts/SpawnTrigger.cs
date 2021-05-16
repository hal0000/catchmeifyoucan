using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    bool _entry;
    public delegate void EntryAction();
    public static event EntryAction OnPlayerEntry;

    void OnTriggerEnter(Collider other)
    {
        if(!_entry && other.GetComponent<PlayerTag>() != null)
        {
            _entry = true;
            OnPlayerEntry();
        }
    }
}