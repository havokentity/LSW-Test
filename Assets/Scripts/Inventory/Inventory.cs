using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public GameObject cellsParent, sellMode;
    public Inventory equippedInventory;
    public bool buySellMode, isShop;

    public InventoryItem currentSelectedItem;
    public TextMeshProUGUI priceText;

    public Character boundCharacter;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        if(!isShop)
        {
            if(sellMode !=null)
            {
                if(buySellMode)
                {
                    sellMode.SetActive(true);
                } else
                {
                    sellMode.SetActive(false);
                }
                
            }
        }
    }

    public void BuyAndSell()
    {
        if (currentSelectedItem != null)
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
                            SoundController.instance.cashRegisterSound.Play();
                        }
                    }
                }
                else
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
    }

    public void SwapItemWithEquipped(InventoryItem inventoryItem)
    {
        if(equippedInventory)
        {
            var tempItem = equippedInventory.GetInventoryItem(0);
            tempItem.SwapItem(inventoryItem);
            UpdateEquippedWithBoundCharacter();
        }
    }


    void UpdateEquippedWithBoundCharacter()
    {
        if (boundCharacter)
        {
            var firstEquippedItem = this.GetInventoryItem(0);
            boundCharacter.equippedHelmet = firstEquippedItem.item;
            boundCharacter.UpdateEquipped();
        }
        else
        {
            if (equippedInventory)
            {
                var firstEquippedItem = equippedInventory.GetInventoryItem(0);
                equippedInventory.boundCharacter.equippedHelmet = firstEquippedItem.item;
                equippedInventory.boundCharacter.UpdateEquipped();
            }
        }
    }
    public InventoryItem GetInventoryItem(int index)
    {
        var items = GetComponentsInChildren<InventoryItem>();
        return items[index];
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

    public void HandleCellClick(InventoryItem item)
    {
        if (sellMode == null && !isShop)
        {
            var firstEquippedItem = this.GetInventoryItem(0);
            GameController.instance.playerInventory.AddItem(firstEquippedItem);
            firstEquippedItem.RemoveItem();            
        } else if(!isShop)
        {
            if(GameController.instance.IsInventoryMode())
            {
                this.SwapItemWithEquipped(item);                
            }
        }

        UpdateEquippedWithBoundCharacter();
    }

    public void SetCurrentSelectedItem(InventoryItem inventoryItem)
    {
        currentSelectedItem = inventoryItem;

        //This means our inventory is the currently equipped inventory
        if (equippedInventory == null && sellMode == null && !isShop)
        {

        }
        else
        {
            if (GameController.instance.IsInventoryMode())
            {
                
            } else
            {
                if (priceText != null)
                {
                    if (isShop)
                    {
                        priceText.text = currentSelectedItem.shopPrice.ToString();
                    }
                    else
                    {
                        priceText.text = currentSelectedItem.sellingPrice.ToString();
                    }
                }
            }
        }
    }

    public void UpdateUI()
    {
        if (sellMode != null)
        {
            if (isShop)
            {
                sellMode.SetActive(true);
            }
            else
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
}
