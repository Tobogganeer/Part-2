using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    public string sceneName;

    public void Load()
    {
        // The most complicated script ever written
        SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
        // Stop playing if in editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

/*

SceneButton.cs
- Just contains functions used by buttons to load scenes
- Used in main menu and game
- Pseudocode:
  - Variable for scene name
  - fn Load() -> loads the desired scene

*/