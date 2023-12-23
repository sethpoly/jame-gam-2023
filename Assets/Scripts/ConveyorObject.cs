using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An Oobject that can be updated by landing on a Conveyor
public class ConveyorObject : GameManagerObservable
{
    private bool onConveyor = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Initialize();
        gameManager.onNewLevelStart.AddListener(OnNewLevelStart);
    }

    
    // Update is called once per frame
    void Update()
    {
        if(onConveyor) 
        {
            transform.Translate(gameManager.currentLevel.difficulty.conveyorSpeed * Vector2.right * Time.deltaTime);
        }
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        // Colliding with conveyor
        if (other.collider.CompareTag("Conveyor")) {
            // Start moving this object
            onConveyor = true;

            // If object is anvil, initiate camera shake
            if(CompareTag("Anvil"))
            {
                gameManager.ScreenShake();
            }
        }

        // Colliding with sack
        if (other.collider.CompareTag("Sack"))
        {
            Destroy(gameObject);
        }
    }

    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        if (!other.collider.CompareTag("Conveyor")) {
            return;
        }

        // Stop moving object
        onConveyor = false;
    }

    private void OnNewLevelStart()
    {
        gameManager.onNewLevelStart.RemoveListener(OnNewLevelStart);
        Destroy(gameObject);
    }
}
