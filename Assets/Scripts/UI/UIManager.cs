using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : GameManagerObservable
{
    public GameObject levelTextObj;
    public GameObject coalCountTextObj;
    public GameObject restartLevelObj;
    public GameObject levelStartObj;

    private TextMeshProUGUI levelText;
    private TextMeshProUGUI coalCountText;
    public TextMeshProUGUI levelStartText;


    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
        levelText = levelTextObj.GetComponent<TextMeshProUGUI>();
        coalCountText = coalCountTextObj.GetComponent<TextMeshProUGUI>();

        gameManager.onNewLevelStart.AddListener(OnNewLevelStart);
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

    private void OnNewLevelStart()
    {
        var levelNumber = (int)gameManager._currentLevel + 1;
        levelStartObj.SetActive(true);
        levelStartText.text = "Level " + levelNumber;
        StartCoroutine(DismissLevelStartPanel());
    }

    private IEnumerator DismissLevelStartPanel()
    {
        yield return new WaitForSecondsRealtime(1.3f);
        levelStartObj.SetActive(false);
    }

}
