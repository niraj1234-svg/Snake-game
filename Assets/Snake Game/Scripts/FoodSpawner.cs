using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;

    private GameObject currentFood;

    private void OnEnable()
    {
        GameEvents.OnFoodCollected += SpawnFood;
    }

    private void OnDisable()
    {
        GameEvents.OnFoodCollected -= SpawnFood;
    }

    private void Start()
    {
        SpawnFood();
    }

    private void SpawnFood()
    {
        if (currentFood != null)
        {
            Destroy(currentFood);
        }

        Vector3 spawnPosition = GridManager.Instance.GetRandomPosition();

        currentFood = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
    }
}