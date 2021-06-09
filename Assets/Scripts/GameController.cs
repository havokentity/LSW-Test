using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public List<ItemInventoryImageMapping> itemsMappingList;
    // Start is called before the first frame update
    void Start()
    {
        GameController.instance = this;
    }

    public Sprite getSpriteFromItem(Item item)
    {
        Sprite returnSprite = null;
        foreach(var itemMapping in itemsMappingList)
        {
            if(itemMapping.item == item)
            {
                returnSprite = itemMapping.image;
                break;
            }
        }

        return returnSprite;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
