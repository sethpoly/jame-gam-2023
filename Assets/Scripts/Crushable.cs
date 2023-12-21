using UnityEngine;

public class Crushable : MonoBehaviour
{
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
        Destroy(gameObject);
        
    }
}   
