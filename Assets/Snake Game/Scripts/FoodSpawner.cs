using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    // Reference to the food prefab that will be spawned
    [SerializeField] private GameObject foodPrefab;

    // Keeps track of the currently spawned food object
    private GameObject currentFood;

    // Subscribe to the food collected event when object is enabled
    private void OnEnable()
    {
        GameEvents.OnFoodCollected += SpawnFood;
    }

    // Unsubscribe from the event when object is disabled (important to avoid memory leaks)
    private void OnDisable()
    {
        GameEvents.OnFoodCollected -= SpawnFood;
    }

    // Called when the game starts
    private void Start()
    {
        // Spawn the first food at the beginning of the game
        SpawnFood();
    }

    // Handles spawning of food
    private void SpawnFood()
    {
        // If a food already exists, destroy it before spawning a new one
        if (currentFood != null)
        {
            Destroy(currentFood);
        }

        // Get a random position from the GridManager
        Vector3 spawnPosition = GridManager.Instance.GetRandomPosition();

        // Instantiate the food prefab at the random position
        currentFood = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
    }
}