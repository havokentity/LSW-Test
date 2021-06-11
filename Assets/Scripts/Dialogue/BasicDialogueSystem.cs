using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasicDialogueSystem : MonoBehaviour
{
    public string salutation;
    public TextMeshProUGUI messageText;
    public List<string> jokes;
    private int currentJokeIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentJokeIndex = 0;
    }

    private void OnEnable()
    {
        messageText.text = salutation;
    }

    public void ShowMessage(string text)
    {
        messageText.text = text;
    }

    public void TellJoke()
    {
        if (currentJokeIndex >= jokes.Count)
        {
            SoundController.instance.dangerSound.Play();
            messageText.text = "Sorry I got no more jokes :(";
        }
        else
        {
            messageText.text = jokes[currentJokeIndex++];
        }
    }

    public void ShowGameObject(GameObject target)
    {
        target.SetActive(true);
    }

    public void HideGameObject(GameObject target)
    {
        target.SetActive(false);
    }
}
