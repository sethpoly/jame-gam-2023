using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : GameManagerObservable
{
    public GameObject levelTextObj;
    public GameObject coalCountTextObj;
    public GameObject restartLevelObj;

    private TextMeshProUGUI levelText;
    private TextMeshProUGUI coalCountText;


    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
        levelText = levelTextObj.GetComponent<TextMeshProUGUI>();
        coalCountText = coalCountTextObj.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update level number
        if(levelText != null)
        {
            var levelNumber = (int)gameManager._currentLevel + 1;
            levelText.SetText("Level " + levelNumber);
        }

        // Update coal count
        if(coalCountText != null)
        {
            coalCountText.SetText(gameManager.crushedItemsCount + "/" + gameManager.currentLevel.itemsToCrush);
        }

        // Show Restart Level UI
        restartLevelObj.SetActive(gameManager.currentLevelFailed);
    }
}
