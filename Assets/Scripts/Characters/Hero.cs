using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    protected override void Initialize()
    {
        base.Initialize();
        goal = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Shop"))
        {
            GameController.instance.systemMessage.ShowMessage("Press E to talk to shop keep", 7.0f);
            GameController.instance.bShopZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameController.instance.bShopZone = false;
        GameController.instance.systemMessage.HideMessage();
        GameController.instance.HideShop();
    }

    public override void RunCharacterLogic()
    {
        Vector2 movement = Vector2.zero;
        if(Input.GetKey(KeyCode.W))
        {
            movement += Vector2.up;
        } else if (Input.GetKey(KeyCode.S))
        {
            movement += Vector2.down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement += Vector2.right;
        }

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit(0);
            return;
        }

        rigidBody2D.AddForceAtPosition(movement * speed, Vector2.zero, ForceMode2D.Impulse);
        base.RunCharacterLogic();
    }
}
