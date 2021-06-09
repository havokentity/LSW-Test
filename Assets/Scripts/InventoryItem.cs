using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Item
{
    NONE,
    HAT_TYPE_1
}

[System.Serializable]
public class ItemInventoryImageMapping
{
    public Item item;
    public Sprite image;
}

public class InventoryItem : MonoBehaviour
{
    public Inventory parentInventory;

    
    public Item item;
    public float shopPrice, sellingPrice;

    public Image cellItemImage;

    public void UpdateItem()
    {
        switch(item)
        {
            case Item.NONE:
                cellItemImage.color = new Color(1, 1, 1, 0);
                break;
            default:
                cellItemImage.sprite = GameController.instance.getSpriteFromItem(item);
                cellItemImage.color = new Color(1, 1, 1, 1);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateItem();
    }
}
