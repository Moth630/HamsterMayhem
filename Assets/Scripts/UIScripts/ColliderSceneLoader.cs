using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderSceneLoader : MonoBehaviour
{
  public string _goScene;
  public void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Player")
    {
      Debug.Log("next");
      SceneManager.LoadScene(_goScene);
    }
  }
}
