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
      _popupDisplay = this.GetComponentInChildren<Canvas>();
      mainCamera = Camera.main;
      _interactable=false;
      if(_popupDisplay != null)
      {
        _popupText = _popupDisplay.GetComponentInChildren<TextMeshProUGUI>();
        if(_popupText!= null)
        {
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

    // Update is called once per frame
    void Update()
    {
      if(_interactable) //if in distance based on collider of sphere
      {
        Debug.Log("interactable is " + _interactable);
        PopUp(); //show pop up
        if(Input.GetKeyUp(KeyCode.E))
        {
          Interacted();
        }
      }
    }
    public override void PopUp()
    {
      Debug.Log("PopUp start!");
      if(_popupText != null && !_empty)
      {
        _popupText.text = "press E to use!\n"+ _treats +" left";
        _popupText.gameObject.SetActive(true);
        StartCoroutine(PopUpFaceCamera());
      }
      else {
      if(_popupText = null)
      {
        Debug.Log("bigger problems");
      }
      else
      {
        Debug.Log("gg");
      }
    }
      //code here to do canvas? TMPro around object that's always facing player with stylized letter
      //"press e!" "do this!"
    }
    public override void IsInteracting() //no need
    {
      Debug.Log("");
    }
    public override void Interacted() //if player's food count not at max, add one
    {
      //change based on Collider // only called when interactable is true, so no need to check?
      if(_playerScript != null && _playerScript.ReturnTreats() <3)
      {
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
/*    public override void OnTriggerEnter(Collider other)
    {

    }
    public override void OnTriggerExit(Collider other)
    {}*/
}
