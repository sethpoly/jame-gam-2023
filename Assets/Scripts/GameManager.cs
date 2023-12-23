using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public enum DifficultyLevel {
    One,
    Two,
    Three,
    Four,
    Five
}

public enum Level 
{
    One,
    Two,
    Three,
    Four,
    Five,
}

public class LevelInstance
{
    public Difficulty difficulty;
    public AnvilSpawnLocation anvilSpawnLocation;
    public int itemsToCrush;
    
    public LevelInstance(Difficulty difficulty, AnvilSpawnLocation anvilSpawnLocation, int itemsToCrush)
    {
        this.difficulty = difficulty;
        this.anvilSpawnLocation = anvilSpawnLocation;
        this.itemsToCrush = itemsToCrush;
    }
}

public class GameManager : MonoBehaviour
{
    private static readonly List<Difficulty> difficulties = new()
    {
        new Difficulty(conveyorSpeed: 5, minItemSpawnTime: .5f, maxItemSpawnTime: 2.5f, levelTimer: 15f, anvilSpawnerLocation: Vector2.zero),
        new Difficulty(conveyorSpeed: 6, minItemSpawnTime: .4f, maxItemSpawnTime: 2.0f, levelTimer: 15f, anvilSpawnerLocation: Vector2.zero),
        new Difficulty(conveyorSpeed: 7, minItemSpawnTime: .3f, maxItemSpawnTime: 1.5f, levelTimer: 15f, anvilSpawnerLocation: Vector2.zero),
        new Difficulty(conveyorSpeed: 8, minItemSpawnTime: .2f, maxItemSpawnTime: 1.5f, levelTimer: 15f, anvilSpawnerLocation: Vector2.zero),
        new Difficulty(conveyorSpeed: 9, minItemSpawnTime: .15f, maxItemSpawnTime: 1f, levelTimer: 15f, anvilSpawnerLocation: Vector2.zero)
    };

    private static readonly List<LevelInstance> levels = new()
    {
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.One), anvilSpawnLocation: AnvilSpawnLocation.End, itemsToCrush: 8),
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.Two), anvilSpawnLocation: AnvilSpawnLocation.End, itemsToCrush: 8),
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.Three), anvilSpawnLocation: AnvilSpawnLocation.End, itemsToCrush: 10),
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.Four), anvilSpawnLocation: AnvilSpawnLocation.End, itemsToCrush: 10),
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.Five), anvilSpawnLocation: AnvilSpawnLocation.End, itemsToCrush: 10)
    };
    [SerializeField] Camera mainCamera;

    public UnityEvent onNewLevelStart = new();
    public Level _currentLevel;
    public LevelInstance currentLevel;
    public ItemType currentItemToCrush;
    private int crushedItemsCount = 0;
    private int incorrectCrushedItemsCount = 0;

    // Update is called once per frame
    void Update()
    {
        currentLevel = levels[(int)_currentLevel];
    }

    public void IncrementCrushedItemsCount() 
    {
        crushedItemsCount++;

        if(crushedItemsCount >= currentLevel.itemsToCrush)
        {
            NextLevel();
            crushedItemsCount = 0;
        }
    }

    public void IncrementIncorrectCrushedItemsCount() 
    {
        incorrectCrushedItemsCount++;

        // TODO: Max incorrect items?
    }

    public void ScreenShake() 
    {
        var shaker = mainCamera.GetComponent<CameraShake>();
        StartCoroutine(shaker.Shake(duration: .1f, magnitude: .3f));
    }

    private void NextLevel() 
    {
        incorrectCrushedItemsCount = 0;
        crushedItemsCount = 0;
        
        // Last level was reached, invoke end game
        if(_currentLevel == Level.Five)
        {
            EndGame();
            return;
        }
        Debug.Log("Starting Next Level: " + _currentLevel.Next());

        // Invoke level start event
        onNewLevelStart.Invoke();

        // Change current level
        _currentLevel = _currentLevel.Next();

        // TODO: Show level name UI
    }

    // TODO: Show restart UI
    private void LevelFailed()
    {
        
    }

    // TODO: Restart current level
    private void RestartLevel()
    {

    }

    private void EndGame()
    {
        Debug.Log("Last level complete. Ending game");
        // TODO:
    }

    static Difficulty GetDifficulty(DifficultyLevel difficultyLevel)
    {
        var index = (int)difficultyLevel;
        return difficulties[index];
    }
}