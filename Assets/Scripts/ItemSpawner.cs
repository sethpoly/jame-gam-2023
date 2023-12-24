using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random=UnityEngine.Random;

public class ItemSpawner : GameManagerObservable
{
    public GameObject giftPrefab;
    public GameObject coalPrefab;
    private float timePassed = 0f;
    private float timeToWait = 0f;

    private ItemType lastItemDropped = ItemType.Coal;
    private int identicalItemDroppedInSequenceCount = 0;
    private readonly int maxIdenticalItemsToDrop = 5; // The max identical items we may drop in sequence of the conveyor

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
        if(!gameManager.conveyorBeltOn) { return; }
        timePassed += Time.deltaTime;
        if(timePassed > timeToWait)
        {
            DropRandom();
            timeToWait = GetRandomTimeToWait();
            timePassed = 0f;
        } 
    }

    private void DropRandom() {
        ItemType nextItem = ItemType.Coal;

        // Check if we dropped the same item the past X times
        // If so, drop the opposite item type
        if(identicalItemDroppedInSequenceCount >= maxIdenticalItemsToDrop) {
            switch(lastItemDropped) {
                case ItemType.Gift:
                nextItem = ItemType.Coal;
                break;
                case ItemType.Coal:
                nextItem = ItemType.Gift;
                break;
            }
            identicalItemDroppedInSequenceCount = 0;
        } else {
            // Else choose a random ItemType to drop
            Array values = Enum.GetValues(typeof(ItemType));
            System.Random random = new();
            nextItem = (ItemType)values.GetValue(random.Next(values.Length));
        }

        switch(nextItem) {
            case ItemType.Gift: 
            DropGift();
            break;
            case ItemType.Coal:
            DropCoal();
            break;
        }

        // Increment flag so we can ensure we don't 
        // keep spamming the same item on the conveyor
        if(lastItemDropped == nextItem) {
            identicalItemDroppedInSequenceCount++;
        }

        lastItemDropped = nextItem;
    }

    private void DropGift() {
        Debug.Log("Dropping gift...");
        Instantiate(giftPrefab, transform);
    }

    private void DropCoal() {
        Debug.Log("Dropping coal...");
        Instantiate(coalPrefab, transform);
    }

    private float GetRandomTimeToWait() {
        return Random.Range(gameManager.currentLevel.difficulty.minItemSpawnTime, gameManager.currentLevel.difficulty.maxItemSpawnTime);
    }
}
