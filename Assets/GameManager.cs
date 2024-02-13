using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject selectedskin;
    public GameObject Player;


    void Start()
    {
        selectedskin = GameObject.FindGameObjectWithTag("MainMenuSkin");
            Player.GetComponent<SpriteRenderer>().sprite = selectedskin.GetComponent<SpriteRenderer>().sprite;
            Player.GetComponent<Animator>().runtimeAnimatorController = selectedskin.GetComponent<Animator>().runtimeAnimatorController;
   
    }
    private void OnEnable()
    {
        selectedskin = GameObject.FindGameObjectWithTag("MainMenuSkin");
        selectedskin.GetComponent<Renderer>().enabled = false;
    }
    private void OnDisable()
    {
       
    }
}
