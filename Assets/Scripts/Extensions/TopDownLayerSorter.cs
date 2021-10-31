using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownLayerSorter : MonoBehaviour
{
    public Transform heightPoint;
    public SpriteRenderer spriteRenderer;

    public int minLayerOrder;
    public int maxLayerOrder;

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(heightPoint.position);

        if (screenPoint.x > 0f && screenPoint.x < Screen.width && screenPoint.y > 0f && screenPoint.y < Screen.height)
            spriteRenderer.sortingOrder = minLayerOrder + (int)((maxLayerOrder - minLayerOrder) * (1f - screenPoint.y / Screen.height));
    }
}
