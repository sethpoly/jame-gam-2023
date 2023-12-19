using UnityEngine;

public class AnvilSpawner : MonoBehaviour
{
    public GameObject anvilPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropAnvil()
    {
        Debug.Log("Dropping anvil...");
        // Instantiate at position (0, 0, 0) and zero rotation.
        Instantiate(anvilPrefab, transform);
    }
}
