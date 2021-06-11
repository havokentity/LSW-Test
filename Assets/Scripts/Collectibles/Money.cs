using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Money : MonoBehaviour
{
    private Vector3 startPosition;
    private SpriteRenderer spriteRenderer;

    public float animationRadius, amplitude, cash, animationDuration;
    public bool horizontalType;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameController.instance.money += cash;
            GameController.instance.UpdateMoney();
            SoundController.instance.collectSound.Play();

            var sequence = DOTween.Sequence();
            //sequence.Append(transform.DOMoveY(Camera.main.transform.position.y + 5.0f, animationDuration));
            sequence.Append(transform.DOMove(new Vector3(Camera.main.transform.position.x + 8.0f, Camera.main.transform.position.y + 5.0f, 0), animationDuration));
            sequence.Insert(0, spriteRenderer.DOFade(0, animationDuration));
            sequence.AppendCallback(() => GameObject.Destroy(this.gameObject));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var toCamera = startPosition - Camera.main.transform.position;
        float mag = toCamera.sqrMagnitude;

        if (mag < animationRadius)
        {
            if(horizontalType)
            {
                transform.position = startPosition + new Vector3(0, Mathf.Sin(Time.time + startPosition.x) * amplitude, 0);
            } else
            {
                transform.position = startPosition + new Vector3(Mathf.Sin(Time.time + startPosition.y) * amplitude, 0, 0);                
            }
            
        }
    }
}
