using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]


public class PlayerMove : MonoBehaviour
{
  [SerializeField] Transform _target;

  [SerializeField] float _moveSpeed = 6.0f;
  [SerializeField] float _rotSpeed = 15.0f;

  [SerializeField] float _jumpSpeed = 15.0f;
  [SerializeField] float _gravity = -9.8f;
  [SerializeField] float _terminalVelocity = -20.0f;
  [SerializeField] float _minFall = -1.5f;

  float _vertSpeed;
  float _groundCheckDistance;
  ControllerColliderHit _contact;

  CharacterController _charController;

  //Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
      _vertSpeed = _minFall;
      _charController = GetComponent<CharacterController>();
      _charController.radius = 8f;
      _groundCheckDistance =
          (_charController.height + _charController.radius) /
          _charController.height * 0.9f;
    }

    // Update is called once per frame
    void Update()
    {
      Vector3 movement = Vector3.zero;

      float horInput = Input.GetAxis("Horizontal");
      float vertInput = Input.GetAxis("Vertical");
      if (horInput != 0 || vertInput != 0)
      {
        Vector3 right = _target.right;
        Vector3 forward = Vector3.Cross(right, Vector3.up);
        movement = (right * horInput) + (forward * vertInput);
        movement *= _moveSpeed;
        movement = Vector3.ClampMagnitude(movement, _moveSpeed);
        Quaternion direction = Quaternion.LookRotation(movement);
        transform.rotation = Quaternion.Lerp(
            transform.rotation, direction, _rotSpeed * Time.deltaTime);
    }
//    _animator.SetFloat("Speed", movement.sqrMagnitude);
    bool hitGround = false;
    if (_vertSpeed < 0 && Physics.Raycast(
                              transform.position,
                              Vector3.down,
                              out RaycastHit hit))
        hitGround = hit.distance <= _groundCheckDistance;

    if (hitGround)
        if (Input.GetButtonDown("Jump"))
            _vertSpeed = _jumpSpeed;
        else
        {
            _vertSpeed = _minFall;
    //        _animator.SetBool("Jumping", false);
        }

    else
    {
      _vertSpeed += _gravity * 5 * Time.deltaTime;

      if (_vertSpeed < _terminalVelocity)
          _vertSpeed = _terminalVelocity;
      if (_contact != null)
      {
         //_animator.SetBool("Jumping", true);
       }
       if (_charController.isGrounded)
       {
         if (Vector3.Dot(movement, _contact.normal) < 0)
         {
         movement = _contact.normal * _moveSpeed;
        }
        else{
          movement += _contact.normal * _moveSpeed;
          }
        }
      }
    movement.y = _vertSpeed;
    _charController.Move(movement * Time.deltaTime);
  }

  private void OnControllerColliderHit(ControllerColliderHit hit)
  {
      _contact = hit;
  }
}
