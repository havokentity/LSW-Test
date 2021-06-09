using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject cellsParent, sellMode;
    public bool buySellMode, isShop;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
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
