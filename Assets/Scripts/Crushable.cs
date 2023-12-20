using UnityEngine;

public class Crushable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
