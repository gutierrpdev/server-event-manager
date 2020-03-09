using System.Collections;
using UnityEngine;

public class CoroutineLauncher
{
    /*
     * Helper Method to Start a coroutine from a C# class that does not extend MonoBehaviour in order to
     * provide an encapsulated way of handling coroutines from scripts that are not directly attached to 
     * GameObjects.
     */
    public static Coroutine StartCoroutine(IEnumerator coroutine)
    {
        // Create a new gameObject to hold our temporary MonoBehaviour.
        GameObject coroutineLauncher = new GameObject();
        // Add an instance of a MonoBehavior-extending component to the coroutineLauncher.
        CoroutineLauncherInstance routineHandler =
            coroutineLauncher.AddComponent(typeof(CoroutineLauncherInstance)) as CoroutineLauncherInstance;
        // Use our handler monobehavior to start the coroutine from an actual gameObject.
        return routineHandler.LaunchCoroutine(coroutine);
    }

    // Auxiliary class extending MonoBehaviour in order to actually be able to Start a Coroutine
    // from a physical GameObject in the scene.
    private class CoroutineLauncherInstance : MonoBehaviour
    {
        void Awake()
        {
            // We need to guarantee that this instance is not destroyed between scenes, since a coroutine might
            // be called at the end of a level and finish its completion after current scene gets changed.
            DontDestroyOnLoad(this);
        }

        public Coroutine LaunchCoroutine(IEnumerator coroutine)
        {
            // instead of directly starting our coroutine, we start DestroyWhenComplete to guarantee that the gameObject
            // holding our launcher does not persist in our scene after its work is done.
            return StartCoroutine(DestroyWhenComplete(coroutine));
        }

        public IEnumerator DestroyWhenComplete(IEnumerator coroutine)
        {
            yield return StartCoroutine(coroutine);
            Destroy(gameObject);
        }
    }
}
