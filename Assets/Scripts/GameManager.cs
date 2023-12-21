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

public class GameManager : MonoBehaviour
{
    private List<Difficulty> difficulties = new()
    {
        new Difficulty(conveyorSpeed: 5, minItemSpawnTime: .5f, maxItemSpawnTime: 2.5f, levelTimer: 15f, anvilSpawnerLocation: Vector2.zero),
        new Difficulty(conveyorSpeed: 5, minItemSpawnTime: .5f, maxItemSpawnTime: 2.5f, levelTimer: 15f, anvilSpawnerLocation: Vector2.zero),
        new Difficulty(conveyorSpeed: 5, minItemSpawnTime: .5f, maxItemSpawnTime: 2.5f, levelTimer: 15f, anvilSpawnerLocation: Vector2.zero),
        new Difficulty(conveyorSpeed: 5, minItemSpawnTime: .5f, maxItemSpawnTime: 2.5f, levelTimer: 15f, anvilSpawnerLocation: Vector2.zero),
        new Difficulty(conveyorSpeed: 5, minItemSpawnTime: .5f, maxItemSpawnTime: 2.5f, levelTimer: 15f, anvilSpawnerLocation: Vector2.zero)
    };

    public DifficultyLevel difficultyLevel;
    public Difficulty currentDifficulty;

    // Start is called before the first frame update
    void Start()
    {
        var index = (int)difficultyLevel;
        currentDifficulty = difficulties[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
