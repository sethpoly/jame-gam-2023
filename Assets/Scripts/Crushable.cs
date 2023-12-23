using System.Collections;
using UnityEngine;

public class Crushable : GameManagerObservable
{
    public ItemType type;
    public GameObject explosionPrefab;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rb;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Initialize();
        animator = explosionPrefab.GetComponentInChildren<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
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
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        // Explosion animation
        spriteRenderer.enabled = false; // Hide this gameObject
        rb.isKinematic = true;
        boxCollider2D.enabled = false; // Disable collider
        Instantiate(explosionPrefab, transform);
        animator.Play("Explosion");
        yield return new WaitForSecondsRealtime(.6f);
        Destroy(gameObject);
    }
}   
