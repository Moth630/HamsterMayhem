using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]


public class PlayerMove : MonoBehaviour
{
  [SerializeField] Transform _target;

  [SerializeField] float _moveSpeed = 6.0f;
  [SerializeField] float _runTime = 10.0f;
  private float _currSpeed;
  [SerializeField] float _rotSpeed = 15.0f;

  [SerializeField] float _jumpSpeed = 25.0f;
  [SerializeField] AudioClip _jumpSound;
  [SerializeField] AudioSource _campiSource;
  private float _currJump;
  [SerializeField] float _gravity = -9.8f;
  [SerializeField] float _terminalVelocity = -20.0f;
  [SerializeField] float _minFall = -1.5f;

  [SerializeField] GameObject _JumpStrength;
  [SerializeField] float _chargeDuration;
  [SerializeField] float _maxDuration =2f;
  bool _isJumpButtonHeld = false;

  [SerializeField] GameObject _BoostShower;


  float _vertSpeed;
  float _groundCheckDistance;
  ControllerColliderHit _contact;

  CharacterController _charController;

  //Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
      _currSpeed = _moveSpeed;
      _vertSpeed = _minFall;
      _currJump = _jumpSpeed;
      _BoostShower = GameObject.Find("OnFire");
      _BoostShower.SetActive(false);
      _charController = GetComponent<CharacterController>();
      _charController.radius = 8f;
      _groundCheckDistance =
          (_charController.height + _charController.radius) /
          _charController.height * 1.1f;
      _JumpStrength = GameObject.Find("JumpStrength");
      _JumpStrength.SetActive(false);
      _JumpStrength.GetComponent<Slider>().maxValue = _maxDuration;
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
        movement *= _currSpeed;
        movement = Vector3.ClampMagnitude(movement, _currSpeed);
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
                              {
        hitGround = hit.distance <= _groundCheckDistance;
  //      Debug.Log("hitground is " + hitGround);
  //      Debug.Log("hit distance is " + hit.distance + ", ground check is " + _groundCheckDistance);
      }
    if (hitGround){
      ActionJump();
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
         movement = _contact.normal * _currSpeed;
        }
        else{
          movement += _contact.normal * _currSpeed;
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

  private void ActionJump()
  {
    if(Input.GetButtonDown("Jump"))
    {
      if(!_isJumpButtonHeld)//checks to see if starting new jump
      {
        _isJumpButtonHeld = true;
        _chargeDuration= 0f;
        _JumpStrength.SetActive(true);
        StartCoroutine(Jumping());
      }
    }
    else if (Input.GetButtonUp("Jump"))
    {
      _isJumpButtonHeld=false;
      _campiSource.clip = _jumpSound;
      _campiSource.Play();
      _vertSpeed = _currJump * (_chargeDuration / _maxDuration);
      _JumpStrength.SetActive(false);
    }
    else
    {
      _vertSpeed = _minFall;
    }
  }

  public IEnumerator Jumping()
  {
    float abcd = 0f;
    while(_isJumpButtonHeld)
    {
      abcd += Time.deltaTime * 2;
      _JumpStrength.GetComponent<Slider>().value
        = Mathf.Clamp(abcd, 0f, _maxDuration);
      _chargeDuration = Mathf.Clamp(abcd, _maxDuration *0.7f, _maxDuration);
      yield return null;
    }
  }
  public void SpeedBoost()
  {
    Debug.Log("Speedboost");
    StartCoroutine(Boosting());
  }

  public IEnumerator Boosting()
  {
    Debug.Log("boosting!");
    _currSpeed = _moveSpeed * 2;
    _currJump = _jumpSpeed *2;
    _BoostShower.SetActive(true);
    yield return new WaitForSeconds(_runTime);
    _currSpeed = _moveSpeed;
    _currJump = _jumpSpeed;
    _BoostShower.SetActive(false);
  }
}
