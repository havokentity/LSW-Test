using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthOrder : MonoBehaviour
{
    public float tolerance;
    public int depthCorrection;

    Vector2 oldPosition;

    private List<SpriteRenderer> spriteRenderers;
    private List<int> metaControlValues;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderers = new List<SpriteRenderer>();
        metaControlValues = new List<int>();
        oldPosition = new Vector2(99999, 99999);

        foreach (SpriteRenderer objectRenderer in GetComponentsInChildren<SpriteRenderer>(true))
        {
            spriteRenderers.Add(objectRenderer);
            var metaControl = objectRenderer.GetComponent<DepthOrderMetaControl>();
            if (metaControl != null)
            {
                metaControlValues.Add(metaControl.metaOffset);
            }
            else
            {
                metaControlValues.Add(0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2Int currentPosInt = Vector2Int.CeilToInt(currentPosition);

        if(currentPosInt.x < -200 || currentPosInt.x > (Screen.width + 200))
        {
            return;
        }

        if (currentPosInt.y < -200 || currentPosInt.y > (Screen.height + 200))
        {
            return;
        }

        float mag = (currentPosition - oldPosition).sqrMagnitude;
        if(mag > tolerance)
        {
            oldPosition = currentPosition;

            for(int i = 0; i < spriteRenderers.Count; i++)
            {
                spriteRenderers[i].sortingOrder = Mathf.Max(Screen.height - currentPosInt.y + Mathf.CeilToInt((float)depthCorrection * (float)Screen.width/1920.0f) + metaControlValues[i], 1);
            }
        }
    }
}
