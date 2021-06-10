﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthOrder : MonoBehaviour
{
    public float tolerance;
    public int depthCorrection;

    Vector2 oldPosition;

    private List<SpriteRenderer> spriteRenderers;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderers = new List<SpriteRenderer>();
        oldPosition = new Vector2(99999, 99999);

        foreach (SpriteRenderer objectRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderers.Add(objectRenderer);
        }print(Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2Int currentPosInt = Vector2Int.CeilToInt(currentPosition);

        if(currentPosInt.x < 0 || currentPosInt.x > Screen.width)
        {
            return;
        }

        if (currentPosInt.y < 0 || currentPosInt.y > Screen.height)
        {
            return;
        }

        float mag = (currentPosition - oldPosition).sqrMagnitude;
        if(mag > tolerance)
        {
            oldPosition = currentPosition;

            foreach(SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.sortingOrder = Screen.height - currentPosInt.y + depthCorrection;
            }
        }
    }
}