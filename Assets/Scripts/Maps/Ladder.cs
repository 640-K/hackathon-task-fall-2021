using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public int enterLayerIndex;
    public int leaveLayerIndex;


    public void OnTriggerExit2D(Collider2D collision)
    {
        if(Vector3.Dot(Quaternion.AngleAxis(transform.rotation.z, Vector3.up) * transform.up, collision.transform.position) < 0)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = enterLayerIndex;
        }
        else
        {
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = leaveLayerIndex;
        }
    }
}
