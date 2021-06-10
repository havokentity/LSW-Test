using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public float money = 100;
    public TextMeshProUGUI moneyText;

    public static GameController instance;
    public List<ItemInventoryImageMapping> itemsMappingList;

    public Inventory shopInventory, playerInventory;
    public SystemMessage systemMessage;

    // Start is called before the first frame update
    void Start()
    {
        GameController.instance = this;
        UpdateMoney();
        systemMessage.ShowMessage("Hola... WASD move, I for inventory", 7.0f);
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

    public bool CheckCost(int cost)
    {
        if (money < cost)
        {
            systemMessage.ShowMessage("Not enough money bruh", 7.0f);
            return false;
        }

        return true;
    }

    public void IncurCost(int cost)
    {
        money -= cost;
        UpdateMoney();
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
