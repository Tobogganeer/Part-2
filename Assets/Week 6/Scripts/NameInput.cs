using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class NameInput : MonoBehaviour
{
    public SceneLoader sceneLoader;
    public TMP_Text messageText; // Tells them to press enter/enter a valid name

    TMP_InputField input;
    float messageTimer;

    const int MinCharacters = 2;

    private void Start()
    {
        input = GetComponent<TMP_InputField>();
        messageText.text = string.Empty;
    }

    private void Update()
    {
        // Clear the message text when the timer has elapsed
        if (messageTimer > 0)
        {
            messageTimer -= Time.deltaTime;
            if (messageTimer <= 0 )
            {
                messageText.text = string.Empty;
            }
        }
    }

    public void OnTyping()
    {
        // Let them know how to submit their name
        if (input.text.Length >= MinCharacters)
            Message("Press Enter to submit...", 3f);
        else
            messageText.text = string.Empty;
    }

    public void OnNameInput()
    {
        if (input.text.Length < MinCharacters)
            Message($"Name must be at least {MinCharacters} characters long.");
        else
        {
            // TODO: Save the name/do something here
            Debug.Log("Name input: " + input.text);
            sceneLoader.LoadNextScene();
        }
    }

    void Message(string message, float length = 3f)
    {
        messageText.text = message;
        messageTimer = length;
    }
}
