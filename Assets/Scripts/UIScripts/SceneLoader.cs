using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    
    public void SetScene(string Name)
    {
      sceneName = Name;
    }
    public void LoadScene()
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Start loading the scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // While the scene is loading, you can display a loading screen or perform other tasks
        while (!asyncOperation.isDone)
        {
            // Optionally update a loading progress bar or UI
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            Debug.Log($"Loading Progress: {progress * 100}%");

            yield return null; // Wait until the next frame
        }

        // Scene is fully loaded and ready
        Debug.Log("Scene Loaded");
    }
}
