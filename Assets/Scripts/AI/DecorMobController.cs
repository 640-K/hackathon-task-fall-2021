using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;
using UnityEngine.Rendering;


namespace Entities
{
    public class DecorMobController : MobController
    {


        bool isAllowNextStep = true;

        private void Start()
        {
            GameObject temp = new GameObject();
            temp.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            target = temp.transform;
            playerNode = ground.pathfinding.grid.getNodeFromWorldPosition(target.position);
        }

        protected override void Update() {
            if (ApplyMotion())
            {
                if (isAllowNextStep)
                {
                    
                    StartCoroutine(MoveCoroutine());

                }
            }
            

        }

        IEnumerator MoveCoroutine()
        {
            isAllowNextStep = false;
            PathNode endNode = generetePosition();
            target.position = ground.pathfinding.grid.GetWorldPosition(endNode.x, endNode.y);
            Debug.Log(endNode.x + " ; " + endNode.y);
            yield return new WaitForSeconds(Random.Range(5,10));
            isAllowNextStep = true;
        }

        PathNode generetePosition()
        {
            PathNode node = ground.pathfinding.grid.GetPathNode(Random.Range(playerNode.x-5, playerNode.x+5), Random.Range(playerNode.y - 5, playerNode.y + 5));
            if (node!=null&&!node.isWalkable)
                return generetePosition();
            return node;
        }


    }
}
