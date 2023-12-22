using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public LevelInstance(Difficulty difficulty, AnvilSpawnLocation anvilSpawnLocation)
    {
        this.difficulty = difficulty;
        this.anvilSpawnLocation = anvilSpawnLocation;
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
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.One), anvilSpawnLocation: AnvilSpawnLocation.End),
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.Three), anvilSpawnLocation: AnvilSpawnLocation.End),
        new LevelInstance(difficulty: GetDifficulty(DifficultyLevel.Five), anvilSpawnLocation: AnvilSpawnLocation.End)
    };

    public DifficultyLevel difficultyLevel;
    public Difficulty currentDifficulty;
    public Level _currentLevel;
    public LevelInstance currentLevel;
    public AnvilSpawnLocation anvilSpawnLocation;
    public ItemType currentItemToCrush;


    // Start is called before the first frame update
    void Start()
    {
        currentDifficulty = GetDifficulty(difficultyLevel);
    }

    // Update is called once per frame
    void Update()
    {
        currentDifficulty = difficulties[(int)difficultyLevel];
    }

    void nextLevel() 
    {
        Debug.Log("");

        // Destroy existing anvils/items
        // Change current level
        // Show level name UI
        // Start conveyor
        // Start item spawner
        // Invoke level start event

    }

    static Difficulty GetDifficulty(DifficultyLevel difficultyLevel)
    {
        var index = (int)difficultyLevel;
        return difficulties[index];
    }
}
