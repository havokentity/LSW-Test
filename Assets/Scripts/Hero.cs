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
        print(movement);
        rigidBody2D.AddForceAtPosition(movement * speed, Vector2.zero, ForceMode2D.Impulse);
        base.RunCharacterLogic();
    }
}
