using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<Cell>
{

    public int width { get; }
    public int height { get; }
    public float cellSize { get; }
    private Cell[,] grid;
    public Vector3 startPos { get; }

    public Grid(int _w, int _h, float _cs, Vector3 _sp)
    {
        width = _w;
        height = _h;
        cellSize = _cs;
        startPos = _sp;

        grid = new Cell[_w, _h];

        for (int x = 0; x < _w; ++x)
        {
            for (int y = 0; y < _h; ++y)
            {
                Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x + 1, y), Color.black, 100f);
                Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x, y + 1), Color.black, 100f);
            }
        }

        Debug.DrawLine(GetWorldPos(0, _h), GetWorldPos(_w, _h), Color.black, 100f);
        Debug.DrawLine(GetWorldPos(_w, 0), GetWorldPos(_w, _h), Color.black, 100f);
    }

    private Vector3 GetWorldPos(int x, int y)
    {
        Vector3 worldPos = startPos;
        worldPos.x += x * cellSize;
        worldPos.y += y * cellSize;
        return worldPos;
    }

    public void GetXY(Vector3 worldPos, out int x, out int y)
    {
        Vector3 offset = worldPos - startPos;
        x = Mathf.FloorToInt(offset.x / cellSize);
        y = Mathf.FloorToInt(offset.y / cellSize);
    }

    public void SetObjectVal(int x, int y, Cell val)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            grid[x, y] = val;
        }
    }

    public void SetObjectVal(Vector3 worldPos, Cell val)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        SetObjectVal(x, y, val);
    }

    public Cell GetObjectVal(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return grid[x, y];
        }
        return default(Cell);
    }
    
    public Cell GetObjectVal(Vector3 worldPos)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        return GetObjectVal(x, y);
    }

}


