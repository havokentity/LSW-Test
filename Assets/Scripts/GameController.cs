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

    public Transform objectToTrack;
    public float cameraEasing;

    public bool bShopZone;

    // Start is called before the first frame update
    void Start()
    {
        bShopZone = false;  
        GameController.instance = this;
        UpdateMoney();
        systemMessage.ShowMessage("Hola... WASD to move, I for inventory", 7.0f);
        HideShop();
    }

    public void HideShop()
    {
        shopInventory.Hide();
        playerInventory.Hide();
    }

    public void ShowShop()
    {
        playerInventory.buySellMode = true;
        shopInventory.Show();
        playerInventory.Show();
    }

    public void ShowInventory()
    {
        playerInventory.buySellMode = false;
        playerInventory.Show();
    }

    public void HideInventory()
    {
        playerInventory.Hide();
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

    private void Update()
    {
        if (bShopZone)
        {
            if (!shopInventory.gameObject.activeSelf)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    ShowShop();
                }
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.I))
            {
                print("I TOGGL");
                if (playerInventory.gameObject.activeSelf)
                {
                    HideInventory();
                }
                else
                {
                    ShowInventory();
                }

            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector2 cameraMovement = (objectToTrack.position - Camera.main.transform.position) * cameraEasing;
        Camera.main.transform.position += (Vector3)cameraMovement;
    }
}
