using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhysicsPrediction : MonoBehaviour
{
    // This is just a bit of fun
    // I want to experiment with a thing I've only briefly had the chance to try before

    public GameObject[] trackedObjects;

    // https://docs.unity3d.com/ScriptReference/PhysicsScene.Simulate.html
    public SimulatedScene Simulate(float time)
    {
        // I assume doing this once per frame will be dreadful for performance
        Scene current = SceneManager.GetActiveScene();

        Scene simScene = SceneManager.CreateScene("PhysicsSim", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        // Set the sim to be the current scene
        SceneManager.SetActiveScene(simScene);

        Dictionary<GameObject, GameObject> actualToCopy = CopyObjects();

        {
            PhysicsScene2D phys = simScene.GetPhysicsScene2D();
            // Make physics execute due to scripts
            SimulationMode2D currentSimMode = Physics2D.simulationMode;
            Physics2D.simulationMode = SimulationMode2D.Script;

            for (float i = 0; i < time; i += Time.fixedDeltaTime)
            {
                // Simulate each step
                phys.Simulate(Time.fixedDeltaTime);
            }
            // Reset the sim mode
            Physics2D.simulationMode = currentSimMode;
        }

        // Reset the scene
        SceneManager.SetActiveScene(current);

        return new SimulatedScene(simScene, actualToCopy);
    }

    private Dictionary<GameObject, GameObject> CopyObjects()
    {
        Dictionary<GameObject, GameObject> actualToCopy = new Dictionary<GameObject, GameObject>();
        foreach (GameObject obj in trackedObjects)
        {
            // Just copy every object in our list
            actualToCopy.Add(obj, Instantiate(obj));
        }
        return actualToCopy;
    }
}

public class SimulatedScene
{
    private Scene sceneHandle;
    private Dictionary<GameObject, GameObject> actualToCopy;

    public SimulatedScene(Scene sceneHandle, Dictionary<GameObject, GameObject> actualToCopy)
    {
        this.sceneHandle = sceneHandle;
        this.actualToCopy = actualToCopy;
    }

    /// <summary>
    /// Tries to get the copied object from this scene.
    /// </summary>
    /// <param name="realObj"></param>
    /// <param name="copy"></param>
    /// <returns></returns>
    public bool TryGetCopiedObject(GameObject realObj, out GameObject copy)
    {
        return actualToCopy.TryGetValue(realObj, out copy);
    }

    /// <summary>
    /// Destroys this scene.
    /// </summary>
    /// <returns></returns>
    public AsyncOperation Destroy()
    {
        return SceneManager.UnloadSceneAsync(sceneHandle);
    }
}
