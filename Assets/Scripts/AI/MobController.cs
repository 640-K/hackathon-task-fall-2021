using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using Entities;

public class MobController : EntityController
{

    private GameObject player;
    public AIGround ground;

    public bool showPath;

    public bool alwaysFollow;

    public bool showGizmos;

    public float followRadius;

    private float postFollowRadius;

    public bool alwaysFollowAfterSee;

    float ScaredSpeed;

    [Range(0, 10)]
    public float stopBetween;

    [Range(0, 10)]
    public float stopAttack;

    public Transform target;

    public Transform localTarget;
    protected PathNode playerNode;

    bool isAllowNextStep = true;

    public bool isScared = false;


    bool isAutoWalk = true;
    private void Start()
    {
        ScaredSpeed = 1.5f * controlledEntity.maxSpeed;
        postFollowRadius = 3 * followRadius;
        if (localTarget == null)
        {
            GameObject temp = new GameObject();
            temp.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            localTarget = temp.transform;
            playerNode = ground.pathfinding.grid.getNodeFromWorldPosition(localTarget.position);
        }
        else
            isAutoWalk = false;
    }
    protected bool ApplyMotion()
    {
        if (controlledEntity == null) return false;

        PathNode startNode = ground.pathfinding.grid.getNodeFromWorldPosition(transform.position);

        Vector2 motion = Vector2.zero;
        if (!alwaysFollow)
        {

            if (Vector2.Distance(target.position, transform.position) > followRadius)
            {

                if (controlledEntity.maxSpeed == ScaredSpeed)
                    controlledEntity.maxSpeed = ScaredSpeed/1.5f;


                if (postFollowRadius == followRadius)
                    followRadius = postFollowRadius / 3;

                playerNode = ground.pathfinding.grid.getNodeFromWorldPosition(localTarget.position);
                if (isAllowNextStep && isAutoWalk && controlledEntity.maxSpeed != ScaredSpeed)
                        StartCoroutine(MoveCoroutine());
            }
            else
            {


                if (followRadius != postFollowRadius)
                    followRadius = postFollowRadius;

                if (alwaysFollowAfterSee)
                    alwaysFollow = true;
                if (isScared)
                {
                    if (isAllowNextStep)
                    {
                        isAllowNextStep = false;

                        playerNode = generetePosition();
                        if (controlledEntity.maxSpeed != ScaredSpeed)
                            controlledEntity.maxSpeed = ScaredSpeed;
                    }

                    if (startNode == playerNode)
                        isAllowNextStep = true;
                }
                else
                    playerNode = ground.pathfinding.grid.getNodeFromWorldPosition(target.position);


            }
            if (!isAutoWalk && Vector2.Distance(localTarget.position, transform.position) < stopBetween && followRadius != postFollowRadius)
                playerNode = null;
        }else
            playerNode = ground.pathfinding.grid.getNodeFromWorldPosition(target.position);

        if (!isScared && Vector2.Distance(target.position, transform.position) < stopAttack)
        {
            playerNode = null;
            Attack();
        }
        
        if (playerNode == null || startNode == null)
        {
            controlledEntity.Move(motion);
            return false;
        }
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
        return startNode == playerNode;
    }
    IEnumerator MoveCoroutine()
    {
        isAllowNextStep = false;
        PathNode endNode = generetePosition();
        localTarget.position = ground.pathfinding.grid.GetWorldPosition(endNode.x, endNode.y);
        yield return new WaitForSeconds(Random.Range(2, 8));
        isAllowNextStep = true;
    }

    PathNode generetePosition()
    {
        PathNode startNode = ground.pathfinding.grid.getNodeFromWorldPosition(transform.position);


        List<PathNode> nodes = new List<PathNode>();
        for (int i = -3; i < 4; i++)
            for (int j = -3; j < 4; j++)
                if (i != 0 && j != 0)
                {
                    PathNode _node = ground.pathfinding.grid.GetPathNode(startNode.x + i, startNode.y + j);
                    if (_node!=null&&_node.isWalkable)
                        nodes.Add(_node);
                }    
       return nodes[Random.Range(0, nodes.Count)];
    }

    private void OnDrawGizmos()
    {
        if (alwaysFollow || !showGizmos)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, followRadius);
    }
    protected override void Update() => ApplyMotion();


    protected virtual void Attack()
    {
        Priest priest = controlledEntity as Priest;

        if(priest != null)
        {
            if(Random.Range(0f, 100f) < 33f)
                priest.UseEffectArea();
            else
                priest.UseStaff();

            return;
        }


        BattleMonk monk = controlledEntity as BattleMonk;

        if(monk != null)
        {
            if (Random.Range(0f, 100f) < 90f)
                monk.Attack();
            else
                monk.Speak();

            return;
        }


        Monk healerMonk = controlledEntity as Monk;
        
        if(healerMonk != null)
           healerMonk.Worship();
    }
}
