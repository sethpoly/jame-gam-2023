using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject giftPrefab;
    public GameObject coalPrefab;

    private float timePassed = 0f;
    private float timeToWait = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed > timeToWait)
        {
            DropRandom();
            timePassed = 0f;
        } 
    }

    private void DropRandom() {
        Array values = Enum.GetValues(typeof(ItemType));
        System.Random random = new();
        ItemType randomItem = (ItemType)values.GetValue(random.Next(values.Length));

        switch(randomItem) {
            case ItemType.Gift: 
            DropGift();
            break;
            case ItemType.Coal:
            DropCoal();
            break;
        }
    }

    private void DropGift() {
        Debug.Log("Dropping gift...");
        Instantiate(giftPrefab, transform);
    }

    private void DropCoal() {
        Debug.Log("Dropping coal...");
        Instantiate(coalPrefab, transform);
    }
}
