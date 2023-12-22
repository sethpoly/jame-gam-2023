using UnityEngine;

public class Crushable : GameManagerObservable
{
    public ItemType type;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collison with " + other.attachedRigidbody.tag);
        if (!other.attachedRigidbody.CompareTag("Anvil")) {
            return;
        }
        // Increment crushed item count if it matches
        if(gameManager.currentItemToCrush == type) 
        {
            gameManager.IncrementCrushedItemsCount();
        }

        Destroy(gameObject);
        
    }
}   
