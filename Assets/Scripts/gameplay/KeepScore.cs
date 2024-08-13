using UnityEngine;

public class KeepScore : MonoBehaviour
{
    public static KeepScore Instance { get; private set; }

    public int playerScore;
    public int playerTime;

    private void Awake()
    {
        // Check if instance already exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object when loading new scenes
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }
}
