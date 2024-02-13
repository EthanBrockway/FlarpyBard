using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject Pipe;
    public GameObject smallerPipe;
    public float spawnRate = 1.7f;
    private float timer = 0;
    public float heightOffset = 11;
    public int playerScore;
    public LogicScript logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate) {
            timer = timer + Time.deltaTime;
        }
        else
        {
            playerScore = logic.playerScore;
            if (playerScore == 24)
            {
                spawnRate = 1.5f;
                timer = 0;
            }
            if(playerScore == 74)
            {
                spawnRate = 1.48f;
            }
            if(playerScore == 99)
            {
                spawnRate = 1.46f;
            }
            if (playerScore == 149)
            {
                spawnRate = 1.45f;
                timer = 0;
            }
            else
            {
                spawnPipe();
                timer = 0;
            }
        };
    }
    void spawnPipe ()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
         if (playerScore > 99) {
            Instantiate(smallerPipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
         }
         else 
         { 
             Instantiate(Pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation); 
         }
    }
}
