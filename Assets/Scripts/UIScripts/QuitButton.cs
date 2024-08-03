using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
  public void Quit()
     {
       Time.timeScale = 1f;
       #if UNITY_EDITOR
       // Stop playing the game in the editor
       UnityEditor.EditorApplication.isPlaying = false;
       #else
       // Quit the game application
       Application.Quit();
       #endif //code from chatgpt
     }
}
