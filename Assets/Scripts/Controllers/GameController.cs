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
        if (shopInventory.gameObject.activeSelf)
        {
            SoundController.instance.tweepSound.Play();
        }
        shopInventory.Hide();
        playerInventory.Hide();
    }

    public void ShowShop()
    {        
        SoundController.instance.pleepSound.Play();
        playerInventory.buySellMode = true;
        shopInventory.Show();
        playerInventory.Show();
        playerInventory.equippedInventory.Hide();
    }

    public void ShowInventory()
    {
        SoundController.instance.pleepSound.Play();
        playerInventory.buySellMode = false;
        playerInventory.Show();
        playerInventory.equippedInventory.Show();
    }

    public bool IsInventoryMode()
    {
        if(playerInventory.gameObject.activeSelf && playerInventory.buySellMode == false &&
            !shopInventory.gameObject.activeSelf)
        {
            return true;
        }

        return false;
    }

    public void HideInventory()
    {
        SoundController.instance.tweepSound.Play();
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
