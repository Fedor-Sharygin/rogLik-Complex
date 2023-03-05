using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    /// grid of the pathnode
    private Grid<PathNode> grid;
    /// pathnode coordinates
    public int x;
    public int y;

    public int fCost;
    public int gCost;
    public int hCost;

    public bool isWalkable;

    public PathNode prevNode;

    public PathNode(Grid<PathNode> _grid, int _x, int _y)
    {
        grid = _grid;
        x = _x;
        y = _y;
        isWalkable = true;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public override string ToString()
    {
        return x + ", " + y;
    }

}
