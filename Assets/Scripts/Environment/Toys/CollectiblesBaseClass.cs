using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectiblesBaseClass : MonoBehaviour
{
  public GameObject _prefab;
  public string _name;

  public virtual string Name()
  {
    if(_name != null)
      return _name;
    else
      return null;
  }
  public virtual void Throw(float _strength, Vector3 position, Quaternion rotation)
  {
    Debug.Log("");
  }
//  public virtual void Gravity()
//  {
//    transform.
//  }
}
