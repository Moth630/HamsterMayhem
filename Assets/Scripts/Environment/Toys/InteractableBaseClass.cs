using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(SphereCollider))]
public abstract class InteractableBaseClass : MonoBehaviour
{
//base class for interactable environment items

//public float _interactionDistance = 5f; //dont need, instead of calculating distance, just make a sphere collider
public GameObject _prefab;
public bool _interactable;
public bool _poppedUp;
public Canvas _popupDisplay;
public TextMeshProUGUI _popupText;
public Camera mainCamera;
public PlayerScript _playerScript;


public virtual void BasicSetup()
{
  this.GetComponent<SphereCollider>().isTrigger = true;
  MeshFilter meshFilter = GetComponent<MeshFilter>();

         if (meshFilter != null)
         {
             // Get the bounds of the Mesh
             Bounds bounds = meshFilter.sharedMesh.bounds;

             // Calculate the radius for the SphereCollider
             float radius = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) / 2 + 5f;
             this.GetComponent<SphereCollider>().radius = radius;
           }
  _popupDisplay = this.GetComponentInChildren<Canvas>();
  mainCamera = Camera.main;
  Debug.Log("1" +this.name);
  _interactable=false;
  if(_popupDisplay != null)
  {
    Debug.Log("2" +this.name);
    _popupText = _popupDisplay.GetComponentInChildren<TextMeshProUGUI>();
    if(_popupText!= null)
    {
      Debug.Log("3" +this);
      _popupText.gameObject.SetActive(false);
      Debug.Log("everything working here at " +this.name);
    }
    else
    {
      Debug.Log("error with tmpro of " + this.name);
    }
  }
  else
  {
    Debug.Log("Canvas issue with gameobject" + this.name);
  }
}
public virtual string PrefabName()
{
    return gameObject.name; //for getting name of toys
}
//method for pop up text
//just edit _popupText and it'll show it without needing to override this
public virtual void PopUp()
{
  if(_popupDisplay != null)
  {
    Debug.Log("PopUp start!");
    if(_popupText!= null)
    {
      _popupText.gameObject.SetActive(true);
      _poppedUp = true;
      StartCoroutine(PopUpFaceCamera());
    }
  }
}
//method for getting rid of Popup
public virtual void DePopUp()
{
  if(_popupText!= null)
  {
    _popupText.gameObject.SetActive(false);
    _poppedUp = false;
  }
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

public virtual void Interacted()
{
  Debug.Log("samething");
} //finished interacting
//method for not interactable anymore for things that can be broken/runout

public virtual void Broken()
{
  Debug.Log("doesn't always need to be implemented");
}
//method for transitioning between

/*triggerenter makes thing interactable
and also calls interactioning
and set _playerScript so it's usable
*/
public virtual void OnTriggerEnter(Collider other)
{
  if(other.gameObject.tag == "Player")
  {
    Debug.Log("player found");
    _interactable = true;
    _playerScript = other.GetComponent<PlayerScript>();
    if (_playerScript != null)
    {
      IsInteracting();
      Debug.Log("playerscript is found");
    }
    else
    {
      Debug.Log("Playerscript not found");
    }
  }
}
public virtual IEnumerator PopUpFaceCamera()//make text face camera
{
  while(_interactable)
  {
    if (mainCamera != null)
   {
       // Make the text face the camera
       _popupDisplay.transform.LookAt(mainCamera.transform);
       _popupText.transform.LookAt(_popupText.transform.position + mainCamera.transform.forward);
   }
   else
   {
     Debug.Log("camera not captured");
   }
   yield return new WaitForSeconds(0.1f);
  }
}

public virtual void OnTriggerExit(Collider other)
  {
    if(other.gameObject.tag == "Player")
    {
      _interactable = false;
      DePopUp();
    }
  }
}
/*ability to be interacted with is checked with Trigger
actual interaction with pressing E is checked with update
*/
