using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : GameManagerObservable
{
    private PlayerInput playerInput;

    [SerializeField] AnvilSpawner anvilSpawner;
    [SerializeField] float cooldown;

    private float cooldownTimer;
    private bool anvilReady = true;
    
    void Awake()
    {
        playerInput = new PlayerInput();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        playerInput.Map.Enable();
        PlayerInput();
    }

    // Update is called once per frame
    void Update()
    {
        // Anvil Drop Cooldown Timer
        if(!anvilReady) 
        {
            cooldownTimer += Time.deltaTime;
            if(cooldownTimer > cooldown)
            {
                anvilReady = true;
                cooldownTimer = 0;
            } 
        }
    }

    private void PlayerInput() 
    {
        playerInput.Map.Drop.started += OnDropPressed;
        playerInput.Map.Restart.started += OnRestartPressed;
    }

    private void OnDropPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Drop pressed");
        if(!anvilReady) 
        { 
            Debug.Log("Anvil not ready..");
            return; 
        }

        anvilSpawner.DropAnvil();

        // Start cooldown
        anvilReady = false;

    }

    private void OnRestartPressed(InputAction.CallbackContext context)
    {
        gameManager.RestartLevel();
    }
}
