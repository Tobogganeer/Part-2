using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        // Just load the current scene again
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextScene()
    {
        // The method will handle wrapping it around
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void LoadScene(int unclampedIndex)
    {
        // Wrap around the index if it's gone out of bounds
        int numScenes = SceneManager.sceneCountInBuildSettings;
        if (unclampedIndex < 0)
            unclampedIndex = numScenes - 1;
        else if (unclampedIndex >= numScenes)
            unclampedIndex = 0;

        // Index is now clamped, load it
        SceneManager.LoadScene(unclampedIndex);
    }
}
