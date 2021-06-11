using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private Vector3 startPosition;

    public float animationRadius, amplitude, cash;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.instance.money += cash;
            GameController.instance.UpdateMoney();
            GameObject.Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var toCamera = startPosition - Camera.main.transform.position;
        float mag = toCamera.sqrMagnitude;

        if (mag < animationRadius)
        {
            transform.position = startPosition + new Vector3(0, Mathf.Sin(Time.time) * amplitude, 0);
        }
    }
}
