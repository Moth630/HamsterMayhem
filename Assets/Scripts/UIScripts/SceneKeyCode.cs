using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneKeyCode : MonoBehaviour //when press button go to next screen
//go to new screen upon button press
{
  [SerializeField] KeyCode _nextScene = KeyCode.E;
  [SerializeField] string _gamePlay;

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(_nextScene))
      {
        SceneManager.LoadScene(_gamePlay);
      }
    }
}
