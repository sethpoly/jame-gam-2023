using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum DifficultyLevel {
    One,
    Two,
    Three,
    Four,
    Five,
     Six,
    Seven,
    Eight,
    Nine,
    Expert
}

public enum Level 
{
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten
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
        new Difficulty(conveyorSpeed: 8, minItemSpawnTime: .2f, maxItemSpawnTime: 1f, levelTimer: 15f, anvilSpawnerLocation: Vector2.zero),
        new Difficulty(conveyorSpeed: 8.5f, minItemSpawnTime: .15f, maxItemSpawnTime: .9f, levelTimer: 15f, anvilSpawnerLocation: Vector2.zero),
        new Difficulty(conveyorSpeed: 9, minItemSpawnTime: .15f, maxItemSpawnTime: .9f, levelTimer: 15f, anvilSpawnerLocation: Vector2.zero),
        new Difficulty(conveyorSpeed: 9.5f, minItemSpawnTime: .15f, maxItemSpawnTime: .9f, levelTimer: 15f, anvilSpawnerLocation: Vector2.zero),
        new Difficulty(conveyorSpeed: 10, minItemSpawnTime: .2f, maxItemSpawnTime: 1f, levelTimer: 15f, anvilSpawnerLocation: Vector2.zero),
        new Difficulty(conveyorSpeed: 11, minItemSpawnTime: .15f, maxItemSpawnTime: .9f, levelTimer: 15f, anvilSpawnerLocation: Vector2.zero),
        new Difficulty(conveyorSpeed: 11.5f, minItemSpawnTime: .15f, maxItemSpawnTime: .9f, levelTimer: 15f, anvilSpawnerLocation: Vector2.zero)
    };

    private static readonly List<LevelInstance> levels = new()
    {
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.One), anvilSpawnLocation: AnvilSpawnLocation.End, itemsToCrush: 5),
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.Two), anvilSpawnLocation: AnvilSpawnLocation.End, itemsToCrush: 6),
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.Three), anvilSpawnLocation: AnvilSpawnLocation.End, itemsToCrush: 7),
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.Four), anvilSpawnLocation: AnvilSpawnLocation.End, itemsToCrush: 8),
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.Five), anvilSpawnLocation: AnvilSpawnLocation.End, itemsToCrush: 5),
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.Five), anvilSpawnLocation: AnvilSpawnLocation.End, itemsToCrush: 5),
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.Five), anvilSpawnLocation: AnvilSpawnLocation.End, itemsToCrush: 6),
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.Five), anvilSpawnLocation: AnvilSpawnLocation.End, itemsToCrush: 7),
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.Five), anvilSpawnLocation: AnvilSpawnLocation.End, itemsToCrush: 7),
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.Expert), anvilSpawnLocation: AnvilSpawnLocation.End, itemsToCrush: 3)

    };
    [SerializeField] Camera mainCamera;

    public UnityEvent onNewLevelStart = new();
    public Level _currentLevel;
    public LevelInstance currentLevel;
    public ItemType currentItemToCrush;
    public bool conveyorBeltOn = false;
    public int crushedItemsCount = 0;
    public bool currentLevelFailed = false;
    public AudioSource coalIncrementClip;
    public AudioSource levelFailClip;
    public AudioSource levelCompleteClip;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        onNewLevelStart.Invoke();
        StartCoroutine(StartConveyor());
    }

    // Update is called once per frame
    void Update()
    {
        currentLevel = levels[(int)_currentLevel];
    }

    public void IncrementCrushedItemsCount() 
    {
        crushedItemsCount++;
        coalIncrementClip.Play();

        if(crushedItemsCount >= currentLevel.itemsToCrush)
        {
            conveyorBeltOn = false;
            levelCompleteClip.Play();
            NextLevel();
            crushedItemsCount = 0;
        }
    }

    public void ScreenShake() 
    {
        var shaker = mainCamera.GetComponent<CameraShake>();
        StartCoroutine(shaker.Shake(duration: .1f, magnitude: .3f));
    }

    private void NextLevel() 
    {
        crushedItemsCount = 0;

        // Last level was reached, invoke end game
        if(_currentLevel == Level.Ten)
        {
            EndGame();
            return;
        }
        Debug.Log("Starting Next Level: " + _currentLevel.Next());

        currentLevelFailed = false;

        // Change current level
        _currentLevel = _currentLevel.Next();

        // Invoke level start event
        onNewLevelStart.Invoke();

        // Start conveyor after delay
        StartCoroutine(StartConveyor());
    }

    // Stop level & show restart UI
    public void LevelFailed()
    {
        conveyorBeltOn = false;
        currentLevelFailed = true;
        levelFailClip.Play();
    }

    // Restart current level
    public void RestartLevel()
    {
        crushedItemsCount = 0;
        currentLevelFailed = false;
        onNewLevelStart.Invoke();
        StartCoroutine(StartConveyor());
    }

    private void EndGame()
    {
        Debug.Log("Last level complete. Ending game");
        StartCoroutine(LoadEndGameScene());
    }

    private IEnumerator StartConveyor()
    {
        yield return new WaitForSecondsRealtime(2);
        conveyorBeltOn = true;
    }

    private IEnumerator LoadEndGameScene()
    {
        levelCompleteClip.Play();
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("GameEnd");
    }

    static Difficulty GetDifficulty(DifficultyLevel difficultyLevel)
    {
        var index = (int)difficultyLevel;
        return difficulties[index];
    }
}