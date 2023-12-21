using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random=UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    public GameObject giftPrefab;
    public GameObject coalPrefab;
    private float timePassed = 0f;
    private float timeToWait = 0f;

    private ItemType lastItemDropped = ItemType.Coal;
    private int identicalItemDroppedInSequenceCount = 0;
    private readonly int maxIdenticalItemsToDrop = 3; // The max identical items we may drop in sequence of the conveyor

    // Update is called once per frame
    void Update()
    {
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
        return Random.Range(.5f, 2.5f);
    }
}
