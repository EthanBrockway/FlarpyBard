using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Skins : MonoBehaviour
{

    public RuntimeAnimatorController animator;
    public List<RuntimeAnimatorController> animations = new List<RuntimeAnimatorController>();
    public List<Sprite> skins = new List<Sprite>();
    public SpriteRenderer sr;
    
    public List<Sprite> unlockedSkins = new List<Sprite>();
    private int skinIndex = 0;
    
    


    public GameObject playerskin;
    public GameObject SkinsScreen;
    public GameObject MenuScreen;
    public GameObject lockVariable;
    public GameObject selectButton;

    public PlayerData _playerData;

    void Start()
    {
        
        playerskin.GetComponent<Animator>().enabled = false;
        
        var dictionary = new Dictionary<int, Sprite>()
        {
              {-1, skins[0] },
              {5, skins[1] }, 
              {10, skins[2] }, 
              {25, skins[3] },
              {50, skins[4] },
              {60, skins[5] },
              {75, skins[6] },
              {80, skins[7] },
              {90, skins[8] },
              {100, skins[9] },
              {175, skins[10] },
              {250, skins[11] },
              {500, skins[12] },
        };

        var skinUnlockScore = PlayerPrefs.GetInt("HighScore", 0);
        for (int i = -1; i < skinUnlockScore; i++) {
            Sprite unlockedSkin;

        if (dictionary.TryGetValue(i, out unlockedSkin)){
                unlockedSkins.Add(unlockedSkin);
        }
    }
       
    }
    void OnEnable()
    {
        var playerSkinNumber = PlayerPrefs.GetInt("currentSelectedSkin", 0);
        sr.sprite = skins[playerSkinNumber];
        skinIndex = playerSkinNumber;
        playerskin.SetActive(true);
    }
    private void OnDisable()
    {
        playerskin.SetActive(false);
    }
    public void NextOption()
    {
        
        skinIndex++;
        if (skinIndex == skins.Count)
        {
            skinIndex = 0;
        }
        if (unlockedSkins.Contains(skins[skinIndex]))
        {
            lockVariable.SetActive(false);
            selectButton.GetComponent<Button>().interactable = true;
        }
        else 
        {
            lockVariable.SetActive(true);
            selectButton.GetComponent<Button>().interactable = false;
        }
        animator = animations[skinIndex];
        sr.sprite = skins[skinIndex];
    }
    public void BackOption()
    {
        skinIndex--;
        if (skinIndex < 0)
        {
            skinIndex = skins.Count - 1;
        }
        if (unlockedSkins.Contains(skins[skinIndex]))
        {
            lockVariable.SetActive(false);
            selectButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            lockVariable.SetActive(true);
            selectButton.GetComponent<Button>().interactable = false;
        }
        animator = animations[skinIndex];
        sr.sprite = skins[skinIndex];
    }
    public void leaveSkins()
    {
       
        lockVariable.SetActive(false);
        SkinsScreen.SetActive(false);
        MenuScreen.SetActive(true);
    }
    public void selectSkin()
    {
        lockVariable.SetActive(false);
        animator = animations[skinIndex];
        PlayerPrefs.SetInt("currentSelectedSkin", skinIndex);
        playerskin.GetComponent<Animator>().runtimeAnimatorController = animator;
        SkinsScreen.SetActive(false);
        MenuScreen.SetActive(true);
    }
}
