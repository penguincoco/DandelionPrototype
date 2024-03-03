using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_PlayerFlyController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float jumpForce;

    private void OnFly(InputValue inputValue)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
