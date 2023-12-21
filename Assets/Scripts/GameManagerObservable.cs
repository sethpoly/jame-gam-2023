using UnityEngine;

public class GameManagerObservable: MonoBehaviour {
    protected GameManager gameManager;

    protected void Initialize() {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }
}