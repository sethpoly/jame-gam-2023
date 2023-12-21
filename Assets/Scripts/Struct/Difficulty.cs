// Defines the difficulty of the current level to play
using UnityEngine;

public class Difficulty
{
    float conveyorSpeed;
    float minItemSpawnTime;
    float maxItemSpawnTime;
    float levelTimer;
    Vector2 anvilSpawnerLocation;

    public Difficulty(float conveyorSpeed, float minItemSpawnTime, float maxItemSpawnTime, float levelTimer, Vector2 anvilSpawnerLocation) {
        this.conveyorSpeed = conveyorSpeed;
        this.minItemSpawnTime = minItemSpawnTime;
        this.maxItemSpawnTime = maxItemSpawnTime;
        this.levelTimer = levelTimer;
        this.anvilSpawnerLocation = anvilSpawnerLocation;

    }

}
