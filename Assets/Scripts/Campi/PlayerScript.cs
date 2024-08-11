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
  [SerializeField] float _MaxTimeForThrow = 3f;
  public int _treats;
  public int _toys;
  private List<CollectiblesBaseClass> _pickups = new List<CollectiblesBaseClass>();
  //keep list of toys picked up and spit them out in opposite order
  //max num of toys is 3

  private bool _isButtonHeld = false;
  private float _buttonHoldTime=0f;
  private float _throwStrength= 0f;

    // Start is called before the first frame update
    void Start()
    {
      _stats = GameObject.Find("StatsScreen").GetComponent<Canvas>();
      _numTreats =
        _stats.transform.Find("Food").GetComponent<TextMeshProUGUI>();
      _numToys =
        _stats.transform.Find("Toys").GetComponent<TextMeshProUGUI>();
        UpdateTexts();
      _throw =
        _stats.transform.Find("ToyThrow").gameObject;
        _throw.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Q))
      {
        if(!_isButtonHeld)
        {
          _isButtonHeld = true;
          _buttonHoldTime = 0f;
          _throwStrength = 0f;
          _throw.SetActive(true);
          StartCoroutine(ThrowingToys());
        }
      }
      if(Input.GetKeyUp(KeyCode.Q))
      {
        _isButtonHeld =false;
        _throw.SetActive(false);
      }
    }
    public IEnumerator ThrowingToys()
    {
      Slider _slider = _throw.transform.GetComponent<Slider>();
      while(_isButtonHeld)
      {
        _buttonHoldTime += Time.deltaTime;
        _throwStrength = Mathf.Clamp(_buttonHoldTime,0f,3f);
        _slider.value = _throwStrength;
      }
      yield return null;
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
