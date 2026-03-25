using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private int width = 20;
    [SerializeField] private int height = 20;

    private void Awake()
    {
        Instance = this;
    }

    public Vector3 GetRandomPosition()
    {
        int x = UnityEngine.Random.Range(-width / 2, width / 2);
        int y = UnityEngine.Random.Range(-height / 2, height / 2);

        return new Vector3(x, y, 0f);
    }
}