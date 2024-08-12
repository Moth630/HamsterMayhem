using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class PauseMenu : MonoBehaviour
{
  [SerializeField] GameObject StatScreen;
  [SerializeField] GameObject PauseScreen;
  [SerializeField] AudioMixer _masterMixer;
  [SerializeField] AudioMixerSnapshot snapshot1;
  [SerializeField] AudioMixerSnapshot snapshot2;
  AudioMixerSnapshot _currSnapshot;
  bool _paused = false;


    // Start is called before the first frame update
    void Start()
    {
        StatScreen = GameObject.Find("StatsScreen");
        StatScreen.SetActive(!_paused);
        PauseScreen = GameObject.Find("PauseMenu");
        PauseScreen.SetActive(_paused);
        _currSnapshot = snapshot1;
        _currSnapshot.TransitionTo(1f);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
          TogglePause();
        }
    }

    public void TogglePause()
    {
        _paused = !_paused;
        Time.timeScale = _paused ? 0f : 1f;
        PauseScreen.SetActive(_paused);
        StatScreen.SetActive(!_paused);
        Debug.Log("all going good?");
        if(_paused)
        {
          snapshot2.TransitionTo(4f);
          _currSnapshot = snapshot2;
          Debug.Log("current snapshot is " + snapshot2);
        }
        else
        {
          snapshot1.TransitionTo(4f);
          _currSnapshot = snapshot1;
          Debug.Log("current snapshot is " + snapshot1);
        }
        // Optionally lock/unlock cursor
        Cursor.lockState = _paused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = _paused;
    }

}
