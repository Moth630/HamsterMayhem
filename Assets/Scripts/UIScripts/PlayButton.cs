using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayButton : MonoBehaviour
{
  [SerializeField] string GameScreen;
  public void OnButtonClick()
  {
      Time.timeScale = 1f;
      SceneManager.LoadScene(GameScreen);
  }
}
