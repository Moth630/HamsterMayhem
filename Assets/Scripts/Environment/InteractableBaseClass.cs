using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBaseClass : MonoBehaviour
{
//base class for interactable environment items

//method for pop up text
public virtual void PopUp()
{
  Debug.Log("PopUp start!");
}
//method for see if able to start interaction because player close
public virtual void AbleInteract(float _distanceNeeded, Vector3 _playerTransform)
{
  PopUp();
}
//method for starting interaction
//just switch this for Update() with a bool for ready to interact
//method for while interacting? in update, if interacting is true do this
public virtual void IsInteracting()
{
  Debug.Log("Interactioning!");
}
//method for not interactable anymore for things that can be broken/runout
public abstract void Broken();
//method for transitioning between


}
