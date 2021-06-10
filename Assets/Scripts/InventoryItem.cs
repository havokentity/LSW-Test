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
    public int shopPrice, sellingPrice;

    public Image cellItemImage;

    public void RemoveItem()
    {
        item = Item.NONE;
        shopPrice = 0;
        sellingPrice = 0;
        UpdateItem();
    }

    public void CloneItem(InventoryItem inventoryItem)
    {
        item = inventoryItem.item;
        shopPrice = inventoryItem.shopPrice;
        sellingPrice = inventoryItem.sellingPrice;
        UpdateItem();
    }

    public void SwapItem(InventoryItem inventoryItem)
    {
        var oldItemEnum = item;
        var oldShopPrice = shopPrice;
        var oldSellingPrice = sellingPrice;

        item = inventoryItem.item;
        shopPrice = inventoryItem.shopPrice;
        sellingPrice = inventoryItem.sellingPrice;
        inventoryItem.item = oldItemEnum;
        inventoryItem.shopPrice = oldShopPrice;
        inventoryItem.sellingPrice = oldSellingPrice;
        inventoryItem.UpdateItem();

        UpdateItem();
    }

    public void ItemToggleSelected()
    {
        parentInventory.SetCurrentSelectedItem(this);
    }

    public void ItemClicked()
    {
        parentInventory.HandleCellClick(this);
    }

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
