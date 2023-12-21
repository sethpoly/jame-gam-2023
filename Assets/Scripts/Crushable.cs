using UnityEngine;

public class Crushable : MonoBehaviour
{
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collison with " + other.collider.tag);
        if (!other.collider.CompareTag("Anvil")) {
            return;
        }
        Destroy(gameObject);
    }
}   
