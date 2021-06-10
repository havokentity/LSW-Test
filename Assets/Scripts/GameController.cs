using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public float money = 1000;
    public TextMeshProUGUI moneyText;

    public static GameController instance;
    public List<ItemInventoryImageMapping> itemsMappingList;

    public Inventory shopInventory, playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        GameController.instance = this;
        UpdateMoney();
    }

    public void HideInventory()
    {
        shopInventory.gameObject.SetActive(false);
        playerInventory.gameObject.SetActive(false);
    }

    public void ShowInventory()
    {
        shopInventory.gameObject.SetActive(true);
        playerInventory.gameObject.SetActive(true);
    }

    public void UpdateMoney()
    {
        moneyText.text = money.ToString();
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
