using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConveyorController : GameManagerObservable
{
    public SpriteRenderer powerLightRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        powerLightRenderer.enabled = gameManager.conveyorBeltOn;
    }
}
