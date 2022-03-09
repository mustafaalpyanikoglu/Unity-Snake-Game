using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SneakeMovement : MonoBehaviour
{
    private float timer1,snakeWidth;
    public float timer2;
    private string direction,nowDirection;
    public GameObject apple,tail,poison;
    [HideInInspector] public int totalTail;
    [HideInInspector] public Vector2[] snakeTails;
    private bool tailSpawn;
    kalanHakManager kalanHakManager;
    private int kalanHak;
    [SerializeField]
    public Text puanText;

    void Start()
    {
        transform.position = new Vector2(0, 0);

        timer2 /= 100;

        snakeWidth = gameObject.GetComponent<SpriteRenderer>().size.x;

        snakeTails = new Vector2[22 * 22];

        tailSpawn = false;
    }
    private void Awake()
    {
        kalanHak = 3;
        kalanHakManager = Object.FindObjectOfType<kalanHakManager>();
        kalanHakManager.KalanHaklariKontrolEt(kalanHak);
    }
    void Update()
    {
        //enter from the right and exit from the left
        if (transform.position.x>11.5*snakeWidth)
        {
            transform.position = new Vector2(-11*snakeWidth, transform.position.y);
        }
        if (transform.position.x < -11.5 * snakeWidth)
        {
            transform.position = new Vector2(11*snakeWidth, transform.position.y);
        }
        if (transform.position.y > 11.5 * snakeWidth)
        {
            transform.position = new Vector2(transform.position.x, -11 * snakeWidth);
        }
        if (transform.position.y < -11.5 * snakeWidth)
        {
            transform.position = new Vector2(transform.position.x, 11 * snakeWidth);
        }

        if (GameObject.FindGameObjectWithTag("Apple") == null)//generate the apple in a random position
        {
            Instantiate(apple, new Vector2(Random.Range(-11, 12) * snakeWidth, Random.Range(-11, 12) * snakeWidth), Quaternion.identity);
        }

        if (GameObject.FindGameObjectWithTag("Poison") == null)//generate the apple in a random position
        {
            Instantiate(poison, new Vector2(Random.Range(-11, 12) * snakeWidth, Random.Range(-11, 12) * snakeWidth), Quaternion.identity);
        }

        //bi anda ters yöne dönmemesi için kullanılan koşul
        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            if(nowDirection != "down")
            {
                direction = "up";
            }
        }
        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            if (nowDirection != "up")
            {
                direction = "down";
            }
        }
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            if (nowDirection != "right")
            {
                direction = "left";
            }
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            if (nowDirection != "left")
            {
                direction = "right";
            }
        }
        azUpdate();
    }
    private void azUpdate()
    {
        //kodlar daha az sıklıkla denetlenecek
        if (Time.time > timer1)
        {
            snakeTails[0] = transform.position;
            for (int i = 0; i < totalTail; i++)
            {
                snakeTails[totalTail - i] = snakeTails[totalTail -1 -i];
            }
            


            if (direction == "right")
            {
                nowDirection = direction;
                transform.position = new Vector2(transform.position.x + snakeWidth, transform.position.y);
            }
            if (direction == "left")
            {
                nowDirection = direction;
                transform.position = new Vector2(transform.position.x - snakeWidth, transform.position.y);
            }
            if (direction == "up")
            {
                nowDirection = direction;
                transform.position = new Vector2(transform.position.x, transform.position.y + snakeWidth);
            }
            if (direction == "down")
            {
                nowDirection = direction;
                transform.position = new Vector2(transform.position.x, transform.position.y - snakeWidth);
            }

            if(tailSpawn==true)
            {
                Instantiate(tail, transform.position, Quaternion.identity);
                tailSpawn = false;
            }

            timer1 = timer2 + Time.time;
        }
    }
    public void eatApple()
    {
        totalTail += 1;
        puanText.text = "Puan:" + totalTail.ToString();
        tailSpawn = true;
    }
    public void eatPoison()
    {
        kalanHak--;
        if(kalanHak<0)
        {
            SceneManager.LoadScene("End");
        }
        kalanHakManager.KalanHaklariKontrolEt(kalanHak);
    }
}
