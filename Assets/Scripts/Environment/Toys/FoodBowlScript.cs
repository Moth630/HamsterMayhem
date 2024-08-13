using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodBowlScript : InteractableBaseClass
//This is a foodbowl in the starting area
//that'll constantly dispense food and allow
//player to keep trying things
//
{
  [SerializeField] bool _StarterArea; //if starter area, will keep refilling
  [SerializeField] int _treats = 5;
  private bool _empty = false;
  //private Renderer
  //[SerializeField] Canvas _popupDisplay;
  //private TextMeshPro _treatText;
    // Start is called before the first frame update
    void Start()
    {
      BasicSetup();
    }

    // Update is called once per frame
    void Update()
    {
      if(!_empty&& _interactable) //if in distance based on collider of sphere
      {
        _popupText.text = "press E to use!\n"+ _treats +" left";
        if(!_poppedUp)
        {
          Debug.Log("making it popup");
          PopUp(); //show pop up
        }
        if(Input.GetKeyUp(KeyCode.E))
        {
          Interacted();
          Debug.Log("interacteded!");
        }
      }
    }

    public override void Interacted() //if player's food count not at max, add one
    {
      //change based on Collider // only called when interactable is true, so no need to check?
      if(_playerScript != null && _playerScript.ReturnTreats() <3)
      {
        Debug.Log("playerscript found");
        _playerScript.MoreTreats();
        _treats--;
      }
      if(_StarterArea)
        _treats++;
      if(_treats <=0)
      {
        Broken();
      }
    }
    public override void Broken() //no need
    {
      Debug.Log("");
      _empty = true;
      DePopUp();
      Renderer _foodDispenser = this.GetComponentInChildren<Renderer>();
      if (_foodDispenser != null)
      {
        _foodDispenser.material.color = new Color(175, 125,114);
      }
    }
}
