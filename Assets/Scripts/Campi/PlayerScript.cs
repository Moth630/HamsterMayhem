using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
  [SerializeField] Canvas _stats;
  [SerializeField] TextMeshProUGUI _numTreats;
  [SerializeField] TextMeshProUGUI _numToys;
  [SerializeField] GameObject _throw;
  [SerializeField] GameObject _eat;
  [SerializeField] PlayerMove _moveScript;
  [SerializeField] float _eatDuration = 4f;
  [SerializeField] float _throwDuration = 3f;
  public int _treats;
  public int _toys;
  private List<CollectiblesBaseClass> _pickups = new List<CollectiblesBaseClass>();
  //keep list of toys picked up and spit them out in opposite order
  //max num of toys is 3
  private bool _isFire1ButtonHeld = false;
  private float _buttonHoldTime=0f;
  private Slider _slider;

  //for eating and speeding
  private bool _isFire2ButtonHeld = false;
  private float _eatButtonHoldTime =0f;
  private Slider _eatSlider;

    // Start is called before the first frame update
    void Start()
    {
      _moveScript = GetComponent<PlayerMove>();
      _stats = GameObject.Find("StatsScreen").GetComponent<Canvas>();
      _numTreats =
        _stats.transform.Find("Food").GetComponent<TextMeshProUGUI>();
      _numToys =
        _stats.transform.Find("Toys").GetComponent<TextMeshProUGUI>();
        UpdateTexts();
      _throw =
        _stats.transform.Find("ToyThrow").gameObject;
        _throw.SetActive(false);
        _slider = _throw.transform.GetComponent<Slider>();
        _slider.maxValue = _throwDuration;
      _eat =
        _stats.transform.Find("Eating").gameObject;
        _eat.SetActive(false);
        _eatSlider = _eat.transform.GetComponent<Slider>();
        _eatSlider.maxValue = _eatDuration;
    }

    // Update is called once per frame
    void Update()
    {
      if(_toys >0)
        ActionThrow();
      if(_treats >0)
        ActionEat();
    }

    public void ActionEat()
    {
      if(Input.GetButtonDown("Fire2"))
      {
        if(!_isFire2ButtonHeld)
        {
          _isFire2ButtonHeld = true;
          _eatButtonHoldTime = 0f;
          _eat.SetActive(true);
          StartCoroutine(EatingFood());
        }
        if(_eatButtonHoldTime < _eatDuration)
        {
          _eatButtonHoldTime += Time.deltaTime;
          Debug.Log("eat time is " + _eatButtonHoldTime);
        }
      }
      if(Input.GetButtonUp("Fire2"))
      {
        _isFire2ButtonHeld =false;
        _eat.SetActive(false);
      }
    }

    public IEnumerator EatingFood()
    {
      float abcd = 0f;
      while(_isFire2ButtonHeld)
      {
        _eatButtonHoldTime += Time.deltaTime;
        _eatSlider.value = Mathf.Clamp(_eatButtonHoldTime,0f,_eatDuration);
//        for (int aib=0; aib <7; aib++)
//        {
          yield return null;
//        }
        abcd = _eatButtonHoldTime;
      }
      if(abcd > _eatDuration)
      {
        Debug.Log("made it to boost");
        _moveScript.SpeedBoost();
        _treats-=1;
        UpdateTexts();
      }
      Debug.Log("diff is " + (abcd -_eatDuration)+", abcd is " + abcd +", eatbuttonhold is " + _eatButtonHoldTime);
    }


    public IEnumerator ThrowingToys()
    {
      while(_isFire1ButtonHeld)
      {
        _buttonHoldTime += Time.deltaTime;
        _slider.value =Mathf.Clamp(_buttonHoldTime,0f,_throwDuration);
//       for (int i = 0; i <5; i++)
//        {
          yield return null;
//        }

      }

        Debug.Log("made it to throw");
        //toy script for instantiating based on name from list, and throwing
        _toys-=1;
        UpdateTexts();
    }

    public void ActionThrow()
    {
      if(Input.GetButtonDown("Fire1"))
      {
        if(!_isFire1ButtonHeld)
        {
          _isFire1ButtonHeld = true;
          _buttonHoldTime = 0f;
          _throw.SetActive(true);
          StartCoroutine(ThrowingToys());
        }
      }
      if(Input.GetButtonUp("Fire1"))
      {
        _isFire1ButtonHeld =false;
        _throw.SetActive(false);
      }
    }

    public void OnTriggerEnter(Collider other)
    {
      CollectiblesBaseClass _toyPickup = other.GetComponent<CollectiblesBaseClass>();
      if (_toyPickup != null && _toys <3)
      {
        _pickups.Add(_toyPickup);
        MoreTreats();
        Destroy(other.gameObject);
        Debug.Log("Picked up " + _toyPickup.Name() + " toy");
      }
    }

    public void SpitOutToys()
    {
      int i = _pickups.Count - 1;

      _pickups.RemoveAt(i);
    }



    public void UpdateTexts()
    {
      _numTreats.text = "Food: " + _treats;
      _numToys.text = "Toys: " + _toys;
    }
    public void MoreTreats()
    {
      if(_treats < 3)
      {
        _treats++;
        UpdateTexts();
      }
      else
      {
        Debug.Log("Too many treats!");
      }
    }
    public int ReturnTreats()
    {
      return _treats;
    }
    public void MoreToys()
    {
      if(_toys <3)
      {
        _toys++;
        UpdateTexts();
      }
      else
      {
        Debug.Log("Too many toys!!");
      }
    }
    public int ReturnToys()
    {
      return _toys;
    }
}
