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

    public static PhysicsPrediction Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public GameObject[] trackedObjects;
    private static Scene simScene;
    private static List<GameObject> goBuffer;
    const float DT = 0.02f;

    private void Start()
    {
        simScene = SceneManager.CreateScene("PhysicsSim", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        goBuffer = new List<GameObject>(trackedObjects.Length);
    }

    // https://docs.unity3d.com/ScriptReference/PhysicsScene.Simulate.html
    public static SimulatedScene CreateSimulation()
    {
        // I assume doing this once per frame will be dreadful for performance
        Scene current = SceneManager.GetActiveScene();

        // Set the sim to be the current scene
        SceneManager.SetActiveScene(simScene);

        // Clear out the sim scene
        ClearScene(simScene);

        // Copy in fresh objects
        Dictionary<GameObject, GameObject> actualToCopy = SpawnObjectCopies();

        // Reset the scene
        SceneManager.SetActiveScene(current);

        return new SimulatedScene(simScene, actualToCopy);
    }

    

    public static void Simulate(SimulatedScene scene, float time)
    {
        PhysicsScene2D phys = scene.GetSceneHandle().GetPhysicsScene2D();
        // Make physics execute due to scripts
        SimulationMode2D currentSimMode = Physics2D.simulationMode;
        Physics2D.simulationMode = SimulationMode2D.Script;

        for (float i = 0; i < time; i += DT)
        {
            // Simulate each step
            if (phys.Simulate(DT) == false)
                Debug.LogWarning("Physics not run!");
        }
        // Reset the sim mode
        Physics2D.simulationMode = currentSimMode;
    }

    private static Dictionary<GameObject, GameObject> SpawnObjectCopies()
    {
        Dictionary<GameObject, GameObject> actualToCopy = new Dictionary<GameObject, GameObject>();
        foreach (GameObject obj in Instance.trackedObjects)
        {
            // Just copy every object in our list
            actualToCopy.Add(obj, Instantiate(obj));
        }
        return actualToCopy;
    }

    public static void ClearScene(Scene scene)
    {
        scene.GetRootGameObjects(goBuffer);
        for (int i = 0; i < scene.rootCount; i++)
            Destroy(goBuffer[i]);
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

    public void Simulate(float time) => PhysicsPrediction.Simulate(this, time);

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

    public Scene GetSceneHandle() => sceneHandle;

    public void Destroy()
    {
        PhysicsPrediction.ClearScene(sceneHandle);
    }
}
