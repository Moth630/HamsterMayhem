using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FriendHamster : InteractableBaseClass
/*
Notes:
Player can go up to Friend and click E (which will popup) to ask for advice
in addition, each time the player makes headway, the Friend will yell out and let player know what to do next
and at random intervals (that are spaced out so as to not be annoying)
Friend will shout out (regardless of distance) to remind player what to do
*/
{
  //screen text
  [SerializeField] Canvas _MessageBubble;
  private GameObject _panel;
  private TextMeshProUGUI _textBox;
  //tests for story progression
  public bool _boxTouched; //box has been interacted with
  public bool _boxPulled; //box has been pulled around
  public bool _hasJumped; //jumped onto first platform
  public bool _hasEaten; //player has eaten at least once and figured out
  public bool _eatenTooMuch; //player has eaten too much without doing anything, friend will say "Hey! save some for me!"

    void Start()
    {
      if(_MessageBubble != null)
      {
        Debug.Log("a");
      }
      else
      {
        Debug.Log("no Canvas available!");
      }
    }
    // Update is called once per frame
    void Update()
    {
      if(_interactable)
      {
        PopUp();
      }
    }
    public override void PopUp()
    {
      //_MessageBubble.gameObject.Getcomponent<Text>();
    }
    public override void Interacted()
    {
      //pop up text using GetComponent method

    }
    public override void Broken()
    {
      Debug.Log("Friend will never break!");
    }
}
