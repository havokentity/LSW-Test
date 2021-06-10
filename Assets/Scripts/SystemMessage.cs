using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class SystemMessage : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    private Vector3 originalPosition;
    private RectTransform rectTransform;

    public float hiddenPos, shownPos;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        rectTransform = GetComponent<RectTransform>();
    }

    public void ShowMessage(string text, float duration)
    {
        var sequence = DOTween.Sequence();
        messageText.text = text;
        sequence.Append(rectTransform.DOAnchorPosY(shownPos, 1.0f));
        sequence.AppendInterval(duration);
        sequence.Append(rectTransform.DOAnchorPosY(hiddenPos, 1.0f));

        //var tween =  DOTween.To(() => transform.position, x => transform.position = x, new Vector3(0, 1, 0), 1);
        //tween.a DOTween.To(() => transform.position, x => transform.position = x, originalPosition, 1);

    }
}
