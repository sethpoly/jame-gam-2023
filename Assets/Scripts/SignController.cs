using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour
{
    public ItemType itemType;
    public SpriteRenderer spriteRenderer;
    public Sprite giftSprite;
    public Sprite coalSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update sprite based on item to show
        switch(itemType) 
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
