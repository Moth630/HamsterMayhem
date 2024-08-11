using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
  [SerializeField] Canvas _stats;
  [SerializeField] TextMeshProUGUI _numTreats;
  [SerializeField] TextMeshProUGUI _numToys;
  public int _treats;
  public int _toys;

    // Start is called before the first frame update
    void Start()
    {
      _stats = GameObject.Find("StatsScreen").GetComponent<Canvas>();
      _numTreats =
        _stats.transform.Find("Food").GetComponent<TextMeshProUGUI>();
      _numToys =
        _stats.transform.Find("Toys").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }




    
    public void MoreTreats()
    {
      if(_treats < 3)
      {
        _treats++;

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
