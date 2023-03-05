using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private const int STRAIGHT_MOVE_COST = 10;
    private const int DIAGONAL_MOVE_COST = 14;

    public Grid<PathNode> grid;
    public Pathfinding(int width, int height, float size, Vector3 startPos)
    {
        grid = new Grid<PathNode>(width, height, size, startPos);
    }

    public List<Vector3> FindPath(Vector3 startPos, Vector3 endPos)
    {
        grid.GetXY(startPos, out int _sx, out int _sy);
        grid.GetXY(endPos, out int _ex, out int _ey);

        List<PathNode> path = FindPath(_sx, _sy, _ex, _ey);
        if (path == null)
        {
            return null;
        }

        List<Vector3> v3Path = new List<Vector3>(path.Count);
        int idx = 0;
        foreach (PathNode node in path)
        {
            v3Path[idx++] = grid.startPos + new Vector3(node.x, node.y) * grid.cellSize + (Vector3)Vector2.one * grid.cellSize / 2f;
        }
        return v3Path;
    }

    private List<PathNode> openList;
    private List<PathNode> clsdList;
    public List<PathNode> FindPath(int _sx, int _sy, int _ex, int _ey)
    {
        PathNode startNode = grid.GetObjectVal(_sx, _sy);
        PathNode endNode = grid.GetObjectVal(_ex, _ey);

        openList = new List<PathNode> { startNode };
        clsdList = new List<PathNode> { };

        for (int x = 0; x < grid.width; ++x)
        {
            for (int y = 0; y < grid.height; ++y)
            {
                PathNode curNode = grid.GetObjectVal(x, y);
                curNode.gCost = int.MaxValue;
                curNode.CalculateFCost();
                curNode.prevNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode curNode = GetLowestCostNode(openList);

            if (curNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(curNode);
            clsdList.Add(curNode);

            foreach (PathNode nghb in GetNeighbors(curNode))
            {
                if (!nghb.isWalkable)
                    clsdList.Add(nghb);
                if (clsdList.Contains(nghb))
                    continue;

                int tempGCost = curNode.gCost + CalculateDistanceCost(curNode, nghb);
                if (tempGCost < nghb.gCost)
                {
                    nghb.prevNode = curNode;
                    nghb.gCost = tempGCost;
                    nghb.hCost = CalculateDistanceCost(nghb, endNode);
                    nghb.CalculateFCost();

                    if (!openList.Contains(nghb))
                    {
                        openList.Add(nghb);
                    }
                }
            }
        }

        /// did not find a path
        return null;
    }

    private List<PathNode> CalculatePath(PathNode node)
    {
        List<PathNode> path = new List<PathNode> { node };
        PathNode curNode = node;
        while (curNode.prevNode != null)
        {
            path.Add(curNode.prevNode);
            curNode = curNode.prevNode;
        }
        path.Reverse();
        return path;
    }

    private List<PathNode> GetNeighbors(PathNode node)
    {
        List<PathNode> nghb = new List<PathNode>(8);
        int idx = 0;
        for (int i = -1; i <= 1; ++i)
        {
            if (node.x == 0 && i == -1)
                continue;
            if (node.x == grid.width-1 && i == 1)
                continue;

            for (int j = -1; j <= 1; ++j)
            {
                if (node.y == 0 && j == -1)
                    continue;
                if (node.y == grid.height - 1 && j == 1)
                    continue;

                if (i == 0 && j == 0)
                    continue;

                nghb[idx++] = grid.GetObjectVal(node.x + i, node.y + j);
            }
        }
        return nghb;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDist = Mathf.Abs(a.x - b.x);
        int yDist = Mathf.Abs(a.y - b.y);
        int remain = Mathf.Abs(xDist - yDist);
        return DIAGONAL_MOVE_COST * Mathf.Max(xDist, yDist) + STRAIGHT_MOVE_COST * remain;
    }

    private PathNode GetLowestCostNode(List<PathNode> list)
    {
        PathNode lowNode = list[0];
        foreach (PathNode node in list)
        {
            if (node.fCost < lowNode.fCost)
                lowNode = node;
        }
        return lowNode;
    }

}
