using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class controller : MonoBehaviour
{
    [SerializeField] public float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    [SerializeField] private float _jumpForce = 5;
    private Vector3 _input;

    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Transform _model;

    bool isGrounded;
    public Animator playerAnim;
    bool isJumping;

    public PlayerInput playerInput;
    public bool gamepad;

    void Start()
    {
      
    }
    private void Awake()
    {
        playerInput = new PlayerInput();
        if (!gamepad)
        {
            playerInput.player.moveKey.performed += inputKeyBoard;
            playerInput.player.moveKey.canceled += inputKeyBoard;
        }

        

    }

    private void OnEnable()
    {
        playerInput.Enable();

        

    }

    private void OnDisable()
    {
        playerInput.Disable();


    }
    Vector2 valuekey;
    void Update()
    {
        if (gamepad)
        {
            GatherInput();
        }
       
        Look();
    }
    private void FixedUpdate()
    {
        Move();

        if(canMove)
        {
           
            _input = new Vector3(valuekey.x, 0, valuekey.y);

            playerAnim.SetBool(("move"), true);
        }
        else
        {
            playerAnim.SetBool(("move"), false);
            _input = Vector3.zero;
        }
    }

    public void MoveAnim()
    {
        playerAnim.SetBool(("move"), true);
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
       
     
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground" && isJumping)
        {
            playerAnim.SetTrigger("jumpEnd");
            isJumping = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = false;
            playerAnim.SetBool(("move"), false);
        }
    }
    public void Jump()
    {
        if (isGrounded)
        {
            _rigidBody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("jump");

            isJumping =true;
        }
        
    }

    private void GatherInput()
    {

        Vector2 stickValue = Gamepad.current.leftStick.ReadValue();
        _input = new Vector3(stickValue.x, 0, stickValue.y);

        

        if(_input.x != 0 || _input.y != 0)
        {
            playerAnim.SetBool(("move"), true);
        }
        else
        {
            playerAnim.SetBool(("move"), false);
        }
    }

    public bool canMove;
    float valueKey;
    public void inputKeyBoard(InputAction.CallbackContext value)
    {
       
        if (value.performed)
        {
            valuekey = value.ReadValue<Vector2>();
            canMove = true;
        }
        if (value.canceled)
        {
            
            canMove = false;

           
        }
    }

    private void Move()
    {
        _rigidBody.MovePosition(transform.position + _input.ToIso() * _input.normalized.magnitude * _speed * Time.deltaTime);
    }
    private void Look()
    {
        if (_input == Vector3.zero) return;

        Quaternion rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        _model.rotation = Quaternion.RotateTowards(_model.rotation, rot, _turnSpeed * Time.deltaTime);
    }
}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}

