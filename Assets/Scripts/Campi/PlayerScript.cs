using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class PlayerScript : MonoBehaviour
{
  //Audio
  [SerializeField] AudioSource _campiSource;//done
  [SerializeField] AudioClip _coinClip;//done
  [SerializeField] AudioClip _nuhUh; //use hithurt //done
  //Stats
  [SerializeField] Canvas _stats;//done
  [SerializeField] TextMeshProUGUI _numTreats;//done
  [SerializeField] TextMeshProUGUI _numToys;//done
  [SerializeField] TextMeshProUGUI _numPoints;//done
  [SerializeField] GameObject _throw;//done
  [SerializeField] GameObject _eat;//done
  [SerializeField] PlayerMove _moveScript;//done
  [SerializeField] float _eatDuration = 4f;//done
  [SerializeField] float _throwDuration = 3f;//done
  //TREATS
  public int _treats;//done
  //for eating and speeding
  private bool _isFire2ButtonHeld = false;//done
  private float _eatButtonHoldTime =0f;//done
  private Slider _eatSlider;//deon
  //TOYS
  public int _toys;//done
  private List<GameObject> _pickups = new List<GameObject>();
  private bool _isFire1ButtonHeld = false;
  private float _buttonHoldTime=0f;
  private Slider _slider;//done
  [SerializeField] float _strMulti = 3f;
  private Vector3 _throwLocation;
  //keep list of toys picked up and spit them out in opposite order
  //max num of toys is 3
  //still need to implement code for toys so they can get picked up

  //POINTS
  public int _points;//done

    // Start is called before the first frame update
    void Start()
    {
      _throwLocation = transform.position + new Vector3(4,4,0);
      _campiSource = this.GetComponent<AudioSource>();
      _moveScript = GetComponent<PlayerMove>();
      _stats = GameObject.Find("StatsScreen").GetComponent<Canvas>();
      _numTreats =
        _stats.transform.Find("Food").GetComponent<TextMeshProUGUI>();
      _numToys =
        _stats.transform.Find("Toys").GetComponent<TextMeshProUGUI>();
      _numPoints =
        _stats.transform.Find("Points").GetComponent<TextMeshProUGUI>();
      UpdateTexts();

      _throw =
        _stats.transform.Find("ToyThrow").gameObject;
      _throw.SetActive(false);

      _slider =
        _throw.transform.GetComponent<Slider>();
      _slider.maxValue = _throwDuration;

      _eat =
        _stats.transform.Find("Eating").gameObject;
      _eat.SetActive(false);

      _eatSlider =
        _eat.transform.GetComponent<Slider>();
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

    public void UpdateTexts()
    {
      _numTreats.text = "Food: " + _treats;
      _numToys.text = "Toys: " + _toys;
      _numPoints.text = "Points: " + _points;
    }

    //TREATS//
    public IEnumerator EatingFood()
    {
      float abcd = 0f;
      while(_isFire2ButtonHeld)
      {
        _eatButtonHoldTime += Time.deltaTime;
        _eatSlider.value = Mathf.Clamp(_eatButtonHoldTime,0f,_eatDuration);
          yield return null;
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

/* how should toys work?
toy throw and instantiation,pickup should be in toy class
toy pickup uses ontriggerenter and inputKeycodeE, when E is pressed
while player in trigger, runs MoreToys to see if player can
hold more. If player can, pickup and destroy.
MoreToys makes it so that I dont have to call ReturnToys and compare,
just get a bool straight away and also update num of toys held
list is list of gameobjects, so should call back the gameobject on
instantiate -- how to give it velocity? turn bool thrown on,
allows toy to create sound and destroy when hitting after thrown

maybe throw and instantiation will be on playerscript
need transform of right in front of campi so that instantiated object wont
cause physics issues
get quaternion of camera to find angle? or just set throw angle at 45-figure that out

*/    //TOYS//
    public void MoreToys(GameObject _thisToy) //make it a bool so that when it gets called, if cant pickup the toy wont get destroyed
    {
      if(_toys <3) //should only be called when less than 3, but keep here still
      {
        _toys++;
        UpdateTexts();
        Debug.Log("added");
        _pickups.Add(_thisToy); //adds to list
      }
      else
      {
        Debug.Log("Too many toys!!");
        if(_nuhUh != null)
        {
          _campiSource.clip = _nuhUh;
          _campiSource.Play();
        }
      }
    }
    public int ReturnToys()
    {
      return _toys;
    }

     //toy script for instantiating based on name from list, and throwing
    public void SpitOutToys()
    {
      int i = _pickups.Count - 1;
      Debug.Log("spitting");
      ThrowPrefab(_buttonHoldTime, _pickups[i], _throwLocation, Quaternion.identity);
      _pickups.RemoveAt(i);

      _toys-=1;
      UpdateTexts();
    }
    public void ThrowPrefab(float strength, GameObject prefab, Vector3 position, Quaternion rotation)
    {
      GameObject _thrownToy = Instantiate(prefab, position, rotation);
      Rigidbody _body = _thrownToy.GetComponent<Rigidbody>();
      if(_body!=null)
      {
        Debug.Log("thrown");
        Vector3 throwDirection = new Vector3(1, 1, 0);
        _body.AddForce(throwDirection * strength * _strMulti, ForceMode.Impulse);
        Destroy(prefab);
      }
      else
    {
      Debug.Log("not thrown");
    }
    }
    public void ActionThrow()//start charging, then buttonup do throw
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
        _throwLocation = transform.position + new Vector3(4,4,0);
        _isFire1ButtonHeld =false;
        _throw.SetActive(false);
        SpitOutToys();
      }
    }
    public IEnumerator ThrowingToys() //visual for power charge _buttonHoldTime
    {
      while(_isFire1ButtonHeld)
      {
        _buttonHoldTime += Time.deltaTime;
        _slider.value =Mathf.Clamp(_buttonHoldTime,0f,_throwDuration);
          yield return null;
      }
      //  Debug.Log("made it to throw");
    }

    //POINTS//
    public void AddPoints()
    {
      _points++;
      if(_coinClip !=null)
      {
        _campiSource.clip = _coinClip;
        _campiSource.Play();
      }
      UpdateTexts();
    }
}
