using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class NewPlayerMove : MonoBehaviour
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
    // Start is called before the first frame update
    private void Start()
    {
      _vertSpeed = _minFall;
      _charController = GetComponent<CharacterController>();
      _charController.radius = 0.4f;
      _groundCheckDistance =
          (_charController.height + _charController.radius) /
          _charController.height * 0.95f;

    }

    // Update is called once per frame
    private void Update()
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
            //  _animator.SetBool("Jumping", false);
          }

      else
      {
          /*
           * Gravity is applied as acceleration, which will constantly
           * increase the velocity of an object while it free falls.
           * This results in an arched jump, which could be graphed as
           * a parabola.
           *
           * Velocity is clamped once ir reaches the terminal velocity.
           */
          _vertSpeed += _gravity * 5 * Time.deltaTime;

          if (_vertSpeed < _terminalVelocity)
              _vertSpeed = _terminalVelocity;

          // This condition prevents immediately transitioning to the
          // jumping state right at the beginning of the level.
          if (_contact != null)
          //   _animator.SetBool("Jumping", true);

          // This will execute when raycasting didn't detect a
          // collision in range, but the collider might.
          if (_charController.isGrounded)
          {
              // Respond differently depending on whether the
              // player is facing the contact point or not.
              if (Vector3.Dot(movement, _contact.normal) < 0)
                  // If the direction of movement and the normal are
                  // opposite, meaning I'm going up the slope, push
                  // character downward.
                  movement = _contact.normal * _moveSpeed;
              else
                  // If the direction of movement and the normal are
                  // in the same direction, meaning I'm going down the
                  // slope, add contact normal resistance to movement.
                  movement += _contact.normal * _moveSpeed;
          }
      }

      /*
       * Assign the downward velocity (y-component) of the movement
       * vector, and then apply frame rate independent movement via
       * the `Move` method of the CharacterController. This will
       * apply both horizontal and vertical movement to the player.
       */
      movement.y = _vertSpeed;
      _charController.Move(movement * Time.deltaTime);

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;
    }
}
