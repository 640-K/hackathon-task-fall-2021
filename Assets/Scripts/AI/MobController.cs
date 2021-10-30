using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class MobController : EntityController
{

    private GameObject player;
    public AIGround ground;

    public bool showPath;

    public bool alwaysFollow;

    public bool showGizmos;

    public float followRadius;

    public bool alwaysFollowAfterSee;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected void ApplyMotion()
    {
        if (controlledEntity == null) return;

        Vector2 motion = Vector2.zero;
        PathNode startNode = ground.pathfinding.grid.getNodeFromWorldPosition(transform.position);
        PathNode playerNode = ground.pathfinding.grid.getNodeFromWorldPosition(player.transform.position);
        if (playerNode == null || startNode == null)
        {
            controlledEntity.Move(motion);
            return;
        }

        if (!alwaysFollow)
            if (Vector2.Distance(player.transform.position, transform.position) > followRadius)
            {
                controlledEntity.Move(motion);
                return;
            }
            else
                if (alwaysFollowAfterSee)
                alwaysFollow = true;
        List<PathNode> path = null;
        if (playerNode.isWalkable)
            path = ground.pathfinding.FindPath(startNode.x, startNode.y, playerNode.x, playerNode.y);
        else if (startNode.isWalkable)
        {
            path = ground.pathfinding.FindPath(playerNode.x, playerNode.y, startNode.x, startNode.y);
            if(path!=null)
                path.Reverse();
        }
        if (path != null)
        {
            if (showPath)
            {
                Debug.DrawLine(ground.pathfinding.grid.GetWorldPositionGizmos(startNode.x, startNode.y), ground.pathfinding.grid.GetWorldPositionGizmos(path[0].x, path[0].y), Color.green);
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(ground.pathfinding.grid.GetWorldPositionGizmos(path[i].x, path[i].y), ground.pathfinding.grid.GetWorldPositionGizmos(path[i + 1].x, path[i + 1].y), Color.green);
                }
            }
            if (startNode.y <= path[0].y)
                motion += Vector2.up;
            if (startNode.x >= path[0].x)
                motion -= Vector2.right;
            if (startNode.y >= path[0].y)
                motion -= Vector2.up;
            if (startNode.x <= path[0].x)
                motion += Vector2.right;
        }
        controlledEntity.Move(motion);

    }

    protected override void Update() => ApplyMotion();
}
