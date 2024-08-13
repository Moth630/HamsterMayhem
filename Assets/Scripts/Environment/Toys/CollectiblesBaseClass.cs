using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectiblesBaseClass : MonoBehaviour
{
  public GameObject _prefab;
  public string _name;

  public virtual string PrefabName()
  {
      return gameObject.name;

  }

  public virtual void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.tag == "Player")
    {

    }
  }
//  public virtual void Gravity()
//  {
//    transform.
//  }
}
