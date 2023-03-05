using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float size;
    [SerializeField] private List<Vector3> wallPositions;
    public Pathfinding mapPathfinding;
    private void Start()
    {
        mapPathfinding = new Pathfinding(width, height, size, transform.position);
        foreach (Vector3 wall in wallPositions)
        {
            mapPathfinding.grid.GetObjectVal(wall).isWalkable = false;
        }
    }
}
