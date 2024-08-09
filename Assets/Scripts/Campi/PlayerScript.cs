using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
  public int _treats;
  public int _toys;

    // Start is called before the first frame update
    void Start()
    {

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
}
