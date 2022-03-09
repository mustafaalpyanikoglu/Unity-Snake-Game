using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apple : MonoBehaviour
{
    private GameObject snake;
    void Start()
    {
        snake = GameObject.Find("Snake");
    }

    
    void Update()
    {
        //If the position of the apple and the snake is the same, the snake eats the apple.
        if (new Vector2(transform.position.x, transform.position.y) == new Vector2(snake.transform.position.x, snake.transform.position.y))
        {
            snake.GetComponent<SneakeMovement>().eatApple();
            Destroy(this.gameObject);
        }
    }
}
