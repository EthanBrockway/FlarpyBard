using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BirdScript : MonoBehaviour
{
   
    public Rigidbody2D myRidgedBody;
    public float flapStrength;
    public LogicScript logic;
    public bool birdIsAlive = true;
    public Animator birdAnimation;
    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()

    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) ||Input.GetKeyDown(KeyCode.Mouse0) && birdIsAlive)
            
        {

            birdAnimation.enabled = true;
            myRidgedBody.velocity = Vector2.up * flapStrength;
            timer = 0;
        }
        if (timer > 0.4f)
        {
            birdAnimation.enabled = false;
        }
        if (transform.position.y > 23 | transform.position.y < -21.8)
        {
            logic.gameOver();
        } ;
    }
 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        logic.gameOver();
    }
}
