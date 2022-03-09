using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tailMov : MonoBehaviour
{
    private GameObject snake;
    private int tailNumber;
    private bool checkDeath;
    void Start()
    {
        checkDeath = false;
        snake = GameObject.Find("Snake");
        tailNumber = snake.GetComponent<SneakeMovement>().totalTail;
    }


    void Update()
    {
        if(checkDeath==true)
        {
            if (new Vector2(transform.position.x, transform.position.y) == new Vector2(snake.transform.position.x, snake.transform.position.y))
            {
                SceneManager.LoadScene("End");
            }
        }
        else
        {
            checkDeath = true;
        }
        transform.position = snake.GetComponent<SneakeMovement>().snakeTails[tailNumber];
    }
}
