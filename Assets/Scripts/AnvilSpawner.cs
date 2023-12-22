using Unity.Collections;
using UnityEngine;

public class AnvilSpawner : GameManagerObservable
{
    public GameObject anvilPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        // Fetch spawn location from gameManager
        var locationX = gameManager.currentLevel.anvilSpawnLocation.GetAttributeOfType<AnvilSpawnLocationAttribute>().x;
        var locationY = gameManager.currentLevel.anvilSpawnLocation.GetAttributeOfType<AnvilSpawnLocationAttribute>().y;

        var position = new Vector3(x: locationX, y: locationY);
        transform.position = position;
    }

    public void DropAnvil()
    {
        Debug.Log("Dropping anvil...");

        Instantiate(anvilPrefab, transform);
    }
}
