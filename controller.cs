using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.InputSystem;

public class controller : MonoBehaviour
{
    [SerializeField] public float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    private Vector3 _input;

    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Transform _model;


    public GameObject modelGameObject;

    void Start()
    {
        // at the start of the game
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
        Look();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void GatherInput()
    {

        Vector2 stickValue = Gamepad.current.leftStick.ReadValue();
        _input = new Vector3(stickValue.x, 0, stickValue.y);
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

