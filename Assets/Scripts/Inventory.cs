using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public GameObject cellsParent, sellMode;
    public bool buySellMode, isShop;

    public InventoryItem currentSelectedItem;
    public TextMeshProUGUI priceText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    public void BuyAndSell()
    {
        if (currentSelectedItem.item != Item.NONE)
        {
            if (isShop)
            {
                if (GameController.instance.CheckCost(currentSelectedItem.shopPrice))
                {
                    if (GameController.instance.playerInventory.AddItem(currentSelectedItem))
                    {
                        GameController.instance.IncurCost(currentSelectedItem.shopPrice);
                        currentSelectedItem.RemoveItem();
                        SetCurrentSelectedItem(currentSelectedItem);
                    }
                }
            } else
            {
                if (GameController.instance.shopInventory.AddItem(currentSelectedItem))
                {
                    GameController.instance.IncurCost(-currentSelectedItem.sellingPrice);
                    currentSelectedItem.RemoveItem();
                    SetCurrentSelectedItem(currentSelectedItem);
                }                
            }
        }
    }

    public bool AddItem(InventoryItem inventoryItem)
    {
        foreach(InventoryItem tempInventoryItem in GetComponentsInChildren<InventoryItem>())
        {
            if(tempInventoryItem.item == Item.NONE)
            {
                tempInventoryItem.CloneItem(inventoryItem);
                return true;
            }
        }


        GameController.instance.systemMessage.ShowMessage("Woah this inventory is full!", 7.0f);
        return false;
    }

    public void SetCurrentSelectedItem(InventoryItem inventoryItem)
    {
        currentSelectedItem = inventoryItem;

        if (isShop)
        {
            priceText.text = currentSelectedItem.shopPrice.ToString();
        } else
        {
            priceText.text = currentSelectedItem.sellingPrice.ToString();
        }
    }

    public void UpdateUI()
    {
        if(isShop)
        {
            sellMode.SetActive(true);
        } else
        {
            if (buySellMode)
            {
                sellMode.SetActive(true);
            }
            else
            {
                sellMode.SetActive(false);
            }
        }
    }
}
