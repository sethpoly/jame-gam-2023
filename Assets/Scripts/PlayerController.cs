using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;

    [SerializeField] AnvilSpawner anvilSpawner;
    
    void Awake()
    {
        playerInput = new PlayerInput();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInput.Map.Enable();
        PlayerInput();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PlayerInput() 
    {
        playerInput.Map.Drop.started += OnDropPressed;
    }

    private void OnDropPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Drop pressed");
        anvilSpawner.DropAnvil();
    }
}
