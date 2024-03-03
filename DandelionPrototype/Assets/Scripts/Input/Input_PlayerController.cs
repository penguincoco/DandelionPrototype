using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Input_PlayerController : MonoBehaviour
{
    private static Input_PlayerController _instance;
    public static Input_PlayerController Instance { get { return _instance; } }

    [SerializeField] private Rigidbody _rb;
    private Vector3 _input;
    private float movementX;
    private float movementY;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 720;

    [SerializeField] private int cameraOffset;

    private bool isMoving = false;

    private float xVelocity;
    private float zVelocity;

    void Awake()
    {
        //singleton pattern
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    //INPUT FUNCTIONS
    private void GetInput()
    {
        xVelocity = movementX * _speed * Time.deltaTime;
        zVelocity = movementY * _speed * Time.deltaTime;

        _input = new Vector3(movementX, 0.0f, movementY);
    }

    /*
    private void OnRestart(InputValue inputValue)
    {
        SceneManager.LoadScene(0);
    } */

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void Update()
    {
        GetInput();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 moveDirection = transform.forward * _input.magnitude;
        Vector3 velocity = moveDirection * _speed;
        _rb.velocity = velocity;
        //_rb.velocity = new Vector3(xVelocity, 0f, zVelocity) ;
        //_rb.MovePosition(transform.position + (transform.forward * _input.magnitude) * _speed * Time.deltaTime);
    }

    public bool GetMovementStatus()
    {
        if (new Vector2(movementX, movementY) == Vector2.zero)
            isMoving = false;
        else
            isMoving = true;

        return isMoving;
    }

    /*
    //for smooth rotation
    private void Look()
    {
        if (_input != Vector3.zero)
        {
            Matrix4x4 matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

            Vector3 skewedInput = matrix.MultiplyPoint3x4(_input);

            Vector3 relative = (transform.position + skewedInput) - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
        }
    } */

    
    //for 180 degree turning
    private void Look()
    {
        if (_input != Vector3.zero)
        {
            Matrix4x4 matrix = Matrix4x4.Rotate(Quaternion.Euler(0, cameraOffset, 0));

            Vector3 skewedInput = matrix.MultiplyPoint3x4(_input);

            Vector3 relative = (transform.position + skewedInput) - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relative, Vector3.up);

            // Set the rotation instantly without smoothly rotating
            transform.rotation = rotation;
        }
    } 
}

