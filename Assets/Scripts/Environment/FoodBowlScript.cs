using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBowlScript : InteractableBaseClass
//This is a foodbowl in the starting area
//that'll constantly dispense food and allow
//player to keep trying things
//
{
  [SerializeField] bool _StarterArea; //if starter area, will keep refilling
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void PopUp()
    {
      Debug.Log("PopUp start!");
      //code here to do canvas? TMPro around object that's always facing player with stylized letter
      //"press e!" "do this!"
    }
    public override void AbleInteract(float _distanceNeeded, Vector3 _playerTransform)
    {
      PopUp();
    }
    public override void IsInteracting()
    {
      Debug.Log("");
    }
    public override void Broken()
    {
      Debug.Log("");
    }
}
