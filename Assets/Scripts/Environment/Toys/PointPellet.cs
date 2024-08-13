using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PointPellet : MonoBehaviour
{
  [SerializeField] float _newY;
  [SerializeField] float _moveDist = 1f;
  bool _floatDirection;
    // Start is called before the first frame update
    void Start()
    {
      Vector3 currentPosition = transform.position;
      Physics.Raycast(
                                transform.position,
                                Vector3.down,
                                out RaycastHit hit);
      if(hit.distance >= 10f)
      {
        _newY += currentPosition.y - hit.distance + 12f;
      }
      else
        _newY += hit.distance;
      transform.position = new Vector3(currentPosition.x, _newY, currentPosition.z);

    }

    // Update is called once per frame
    void Update()
    {
      //create script for making them float
      //StartCoroutine(Floating());
      if(this.transform.position.y < -10f)
      {
        Debug.Log("went too low");
        Destroy(gameObject);
      }
    }
    public void OnTriggerEnter(Collider other)
    {
      if(other.gameObject.tag == "Player")
      {
        other.GetComponent<PlayerScript>().AddPoints();
        Destroy(gameObject);
      }
    }
    private IEnumerator Floating()
    {
      while(true) //prob dont need this but just in case
      {
      for(int i = 0; i < 8; i++)
        {
          if(_floatDirection)
          {
            transform.position += new Vector3(0,_moveDist*Time.deltaTime,0);
          }
          else
          {
            transform.position -= new Vector3(0,_moveDist*Time.deltaTime,0);
          }
          if(i ==7)
          {
            _floatDirection=!_floatDirection;
            i=0;
          }
          for(int ia = 0; ia <1; ia++)
            yield return null; //notes for when I get back: things to do: toy pickup behavior, point display done, highscore scene, work on getting scenes to load better?
        }
      }
    }
}
