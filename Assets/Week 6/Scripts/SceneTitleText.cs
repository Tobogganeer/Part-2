using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SceneTitleText : MonoBehaviour
{
    void Start()
    {
        // Just get our text component and set it's text to the scene name
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.text = "Scene: " + SceneManager.GetActiveScene().name;
    }
}
