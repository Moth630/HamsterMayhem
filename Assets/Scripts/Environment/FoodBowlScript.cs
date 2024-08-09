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
  bool interactable;
  [SerializeField] bool _StarterArea; //if starter area, will keep refilling
  [SerializeField] int _treats = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(interactable) //if in distance based on collider of sphere
      {
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

      _treats--;
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
      interactable = false;
    }
/*    public override void OnTriggerEnter(Collider other)
    {

    }
    public override void OnTriggerExit(Collider other)
    {}*/
}
