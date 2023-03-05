using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPlayer : MonoBehaviour
{
    public MapGrid mapGrid;
    private Transform playerTarget;
    private void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public float speed;
    private List<Vector3> pathToPlayer = null;
    private int pathIdx = 0;
    private void Update()
    {
        mapGrid.mapPathfinding.grid.GetXY(transform.position, out int mx, out int my);
        mapGrid.mapPathfinding.grid.GetXY(playerTarget.position, out int px, out int py);

        /// if we are in the same block as player => move towards him
        if (mx == px && my == py)
        {
            pathIdx = 0;
            pathToPlayer = null;
            transform.position += speed * FrameTime() * (playerTarget.position - transform.position).normalized;
            return;
        }

        /// otherwise => find and follow a path
        if (pathToPlayer == null)
        {
            pathIdx = 0;
            pathToPlayer = mapGrid.mapPathfinding.FindPath(transform.position, playerTarget.position);
        }

        /// if we are close enough to pathnode => increase the index
        if (Vector3.SqrMagnitude(pathToPlayer[pathIdx] - transform.position) < .1f)
            pathIdx++;

        /// we reached previous player position, and he was not there =>
        /// find a new path
        if (pathIdx >= pathToPlayer.Count)
        {
            pathToPlayer = null;
            return;
        }

        /// go towards target
        transform.position += speed * FrameTime() * (pathToPlayer[pathIdx] - transform.position).normalized;
    }

    private float FrameTime()
    {
        return Mathf.Min(Time.deltaTime, 1f / Application.targetFrameRate);
    }

}
