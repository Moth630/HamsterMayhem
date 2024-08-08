using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class InteractableBaseClass : MonoBehaviour
{
//base class for interactable environment items

//public float _interactionDistance = 5f; //dont need, instead of calculating distance, just make a sphere collider
public bool _interactable;
public GameObject contact;


//method for pop up text
public virtual void PopUp()
{
  Debug.Log("PopUp start!");
}
//method for see if able to start interaction because player close
/*public virtual void AbleInteract(float _distanceNeeded, Vector3 _playerTransform)
{
  PopUp();
}*/ //dont need, just use update to check
//method for starting interaction
//just switch this for Update() with a bool for ready to interact
//method for while interacting? in update, if interacting is true do this
public virtual void IsInteracting()
{
  Debug.Log("Interactioning!");
}
public abstract void Interacted(); //finished interacting
//method for not interactable anymore for things that can be broken/runout
public abstract void Broken();
//method for transitioning between
public virtual void OnTriggerEnter(Collider other)
{
  if(other.gameObject.tag == "Player")
  {
    _interactable = true;
    //contact = other.gameObject;
  }
}

public virtual void OnTriggerExit(Collider other)
{
  if(other.gameObject.tag == "Player")
  {
    _interactable = false;
  }
}
}
