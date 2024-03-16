using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionMapManager : MonoBehaviour
{
    private static ActionMapManager _instance;
    public static ActionMapManager Instance { get { return _instance; } }

    [SerializeField] private PlayerInput input;
    [SerializeField] private string mapName;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("activating map");
            SetActionMap(mapName);
        } */
    }

    private void OnEnable()
    {
        input.actions["SwitchActionMap"].performed += SwitchActionMap;
    }

    private void OnDisable()
    {
        input.actions["SwitchActionMap"].performed -= SwitchActionMap;
    }

    private void SwitchActionMap(InputAction.CallbackContext context)
    {
        //input.SwitchCurrentActionMap(mapName);
        input.actions.FindActionMap(mapName).Enable();
    }

    public void SetActionMap(string mapToActive)
    {
        this.mapName = mapToActive;
        input.actions.FindActionMap(mapName).Enable();
    }
}
