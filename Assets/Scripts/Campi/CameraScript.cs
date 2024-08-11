using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
  [SerializeField] Transform _target;

  [SerializeField] float _rotSpeed = 1.5f;

  float _rotY;
  Vector3 _offset;


  private void Start()
  {
      // Store current Y rotation
      _rotY = transform.eulerAngles.y;

      // Store the starting offset based on the positional
      // difference between the camera and the target
      _offset = _target.position - transform.position;
  }

  private void LateUpdate()
  {
      // Increment rotation values based on input
      float horInput = Input.GetAxis("Horizontal");

      // Rotate based on the keyboard if pressed

          _rotY += Input.GetAxis("Mouse X") * _rotSpeed * 3;

      // Maintain starting offset, shifted according to the
      // camera's rotation
      Quaternion rotation = Quaternion.Euler(0, _rotY, 0);

      // Multiplying a position vector with a quaternion results in
      // a position that is shifted over according to that rotation
      transform.position = _target.position - (rotation * _offset);

      // The camera will always be facing the target
      // The LookAt() method is used to point one object towards
      // another object, not just cameras
      transform.LookAt(_target);
  }
}
