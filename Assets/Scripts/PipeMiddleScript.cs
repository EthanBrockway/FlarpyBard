using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMiddleScript : MonoBehaviour
{
    public LogicScript logic;
   
  
    // Start is called before the first frame update
    void Start()
    {
       logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }
    private void Update()
    {
        
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.layer == 3)
        {
            
            logic.addScore(1);
            logic.playSound();
        }

        
    }
}
