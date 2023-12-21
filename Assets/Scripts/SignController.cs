using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : GameManagerObservable
{
    public SpriteRenderer spriteRenderer;
    public Sprite giftSprite;
    public Sprite coalSprite;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        // Update sprite based on item to show
        switch(gameManager.currentItemToCrush) 
        {
            case ItemType.Gift:
            spriteRenderer.sprite = giftSprite;
            break;
            case ItemType.Coal:
            spriteRenderer.sprite = coalSprite;
            break;
        }
    }
}
