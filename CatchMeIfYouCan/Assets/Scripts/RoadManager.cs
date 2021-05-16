using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    static RoadManager _instance;
    public static RoadManager Instance { get { return _instance; } }

    public List<GameObject> Roads;
    float _offset = -20f;

    public void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    void Start()
    {
        if (Roads != null && Roads.Count > 0)
            Roads = Roads.OrderByDescending(a => a.transform.position.x).ToList();
    }

    /// <summary>
    /// takes first gameobject of road list sets new Pos
    /// </summary>
    public void MoveRoad()
    {
        GameObject firstRoad = Roads[0];
        Roads.Remove(firstRoad);
        var newX = Roads[Roads.Count - 1].transform.position.x + _offset;
        firstRoad.transform.position = new Vector3(newX, 0, 0);
        Roads.Add(firstRoad);
    }
}
