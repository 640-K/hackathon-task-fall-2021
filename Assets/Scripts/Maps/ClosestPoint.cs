using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestPoint : MonoBehaviour
{
    public Transform player;
    public List<Transform> points;
    private TopDownLayerSorter sorter;


    public void Awake()
    {
        sorter = GetComponent<TopDownLayerSorter>();
    }

    public void Update()
    {
        Transform point = points[0];

        foreach(Transform _point in points)
        {
            if(Vector2.Distance(_point.position, player.position) < Vector2.Distance(point.position, player.position))
            {
                point = _point;
            }
        }

        sorter.heightPoint = point;
    }
}
