using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private Transform segmentPrefab;
    [SerializeField] private int initialSize = 4;
    [SerializeField] private float moveRate = 0.2f;

    private Vector2 direction = Vector2.right;
    private List<Transform> segments = new();

    private float nextMoveTime;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        // ? Only take input when playing
        if (GameManager.Instance.currentState != GameState.Playing)
            return;

        HandleInput();
    }

    private void FixedUpdate()
    {
        // ? STOP movement unless game is Playing
        if (GameManager.Instance.currentState != GameState.Playing)
            return;

        if (Time.time >= nextMoveTime)
        {
            Move();
            nextMoveTime = Time.time + moveRate;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
            direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
            direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
            direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
            direction = Vector2.right;
    }

    private void Move()
    {
        for (int i = segments.Count - 1; i > 0; i--)
            segments[i].position = segments[i - 1].position;

        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + direction.x,
            Mathf.Round(transform.position.y) + direction.y,
            0f
        );
    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[^1].position;
        segment.tag = "Obstacle";
        segments.Add(segment);
    }

    private void ResetState()
    {
        foreach (var segment in segments)
        {
            if (segment != transform)
                Destroy(segment.gameObject);
        }

        segments.Clear();
        segments.Add(transform);

        for (int i = 1; i < initialSize; i++)
            Grow();

        transform.position = Vector3.zero;
        direction = Vector2.right;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Grow();
            GameEvents.OnFoodCollected?.Invoke();
        }
        else if (other.CompareTag("Obstacle"))
        {
            GameEvents.OnGameOver?.Invoke();
        }
    }
}