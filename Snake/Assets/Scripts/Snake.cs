using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    //Reminder: Clear fonksiyonundaki sorunu kontrol et

    private Vector2 movementDirection = Vector2.right;
    private List<Transform> snakeSegments;
    public Transform segmentPrefab;

    void Start()
    {
        snakeSegments = new List<Transform>();
        snakeSegments.Add(this.transform);
    }

    
    void Update()
    {
        //Snake Controller
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            movementDirection = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            movementDirection = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movementDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movementDirection = Vector2.right;
        }

    }

    private void FixedUpdate()
    {
        for (int i = snakeSegments.Count - 1; i > 0 ; i--)
        //Reminder: harekete tersten baþlamasý için negatif kullanýyorum böylece hareket ederken arada boþluk oluþmayacak
        {
            snakeSegments[i].position = snakeSegments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x + movementDirection.x),
            Mathf.Round(this.transform.position.y + movementDirection.y),
            0.0f);
    }

    private void Grow()
    {
        //Snake Size
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = snakeSegments[snakeSegments.Count - 1].position;

        snakeSegments.Add(segment);
    }

    private void ResetGame()
    {
        for (int i = 1; i < snakeSegments.Count; i++)
        {
            Destroy(snakeSegments[i].gameObject);

            snakeSegments.Clear();
            snakeSegments.Add(this.transform);

            this.transform.position = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        }

        else if (other.CompareTag("Obstacle"))
        {
            
            ResetGame();
        }
    }

}
