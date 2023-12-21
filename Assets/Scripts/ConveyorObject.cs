using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An Oobject that can be updated by landing on a Conveyor
public class ConveyorObject : MonoBehaviour
{
    GameManager gameManager;
    private bool onConveyor = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    
    // Update is called once per frame
    void Update()
    {
        if(onConveyor) 
        {
        transform.Translate(gameManager.currentDifficulty.conveyorSpeed * Vector2.right * Time.deltaTime);
        }
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collison with " + other.collider.tag);
        if (!other.collider.CompareTag("Conveyor")) {
            return;
        }

        var conveyor = other.collider.GetComponent<ConveyorController>();

        // Start moving this object
        onConveyor = true;
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
}
