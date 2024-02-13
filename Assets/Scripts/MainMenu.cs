using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;


public class MainMenu : MonoBehaviour
    

{
   
    public GameObject optionsScreen;
    public GameObject mainMenuScreen;
    public GameObject skinsMenuScreen;
    public GameObject leaderBoardsMenu;
    public GameObject highScore;
    public GameObject Player;
    public SpriteRenderer sr;
    public RuntimeAnimatorController controller;
    public GameObject loginScreen;

    public List<Sprite> skins = new List<Sprite>();
    public List<RuntimeAnimatorController> animations = new List<RuntimeAnimatorController>();

    
    private RuntimeAnimatorController playerAnimator;
    
    public AudioSource GameMusic;
    public Slider voiceSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public PlayerData _playerData;

    void Start()
    {
        
        _playerData.username = PlayerPrefs.GetString("username");
        _playerData.email = PlayerPrefs.GetString("email");
        _playerData.password = PlayerPrefs.GetString("password");
        _playerData.highscore = PlayerPrefs.GetInt("HighScore", 0);
        
      
        LoadSettings();
        GameMusic = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        highScore.GetComponent<TextMeshProUGUI>().text = $"HighScore: {PlayerPrefs.GetInt("HighScore", 0)}";
        if (GameMusic.isPlaying)
        {
            return;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Play();
        }
    }
    public void OnEnable()
    {
        Player.SetActive(true);
        _playerData.currentSelectedSkin = PlayerPrefs.GetInt("currentSelectedSkin", 0);
        Player.GetComponent<SpriteRenderer>().sprite = skins[_playerData.currentSelectedSkin];
        playerAnimator = animations[_playerData.currentSelectedSkin];
        Player.GetComponent<Animator>().runtimeAnimatorController = playerAnimator;
    }
  

    public void leaveLeaderBoards()
    {
        mainMenuScreen.SetActive(true);
        leaderBoardsMenu.SetActive(false);
    }
    public void leaderBoards()
    {
        Player.SetActive(false);
        leaderBoardsMenu.SetActive(true);
        mainMenuScreen.SetActive(false);
    }
  
    
    public void LoadSettings()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicvolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxvolume");
        voiceSlider.value = PlayerPrefs.GetFloat("voicevolume");
    }
    public void playGame()
    {
        DontDestroyOnLoad(Player);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void activateOptions()
    {
        Player.SetActive(false);
        optionsScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
    }
   public void LogOut()
    {
        Player.SetActive(false);
        _playerData.currentSelectedSkin = PlayerPrefs.GetInt("currentSelectedSkin", 0);
        StartCoroutine(LogicScript.CreateUser(_playerData.Stringify(),  result => {
            Debug.Log(result);
            if (result)
            {
                PlayerPrefs.SetString("username", "");
                PlayerPrefs.SetInt("HighScore", 0);
                PlayerPrefs.SetInt("currentSelectedSkin", 0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }));
      
    }
    public void openSkins() 
    {
        Player.SetActive(false);
        skinsMenuScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
    }
    
    public void quitGame()
    {
        Debug.Log("Quitting!");
        UnityEngine.Application.Quit();
    }
}
