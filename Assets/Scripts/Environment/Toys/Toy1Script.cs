using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(Rigidbody))]
public class Toy1Script : InteractableBaseClass
{

    // Start is called before the first frame update
    void Start()
    {
      this.GetComponent<Rigidbody>().useGravity = true;

      //stuff they all have
      BasicSetup();
    }

    // Update is called once per frame
    void Update()
    {
      if(_interactable) //if in distance based on collider of sphere
      {
        _popupText.text = "Hi Campi! Press E!"; //the toys talk to you
        if(!_poppedUp)
        {
          Debug.Log("making it popup");
          PopUp(); //show pop up
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
          IsInteracting();
          Debug.Log("interacteded!");
        }
      }
    }
    public override void IsInteracting()
    {
      if(_playerScript.ReturnToys() <3) //if player can pick up more
      {
        _playerScript.MoreToys(gameObject);
        Debug.Log(this.name + "is interacting");
        transform.position = new Vector3(0,200,0);
      }
    }
    //throw and instantiation here

}
