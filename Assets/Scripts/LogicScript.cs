using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using UnityEngine.Networking;
using UnityEngine.Profiling;
using System.Runtime.CompilerServices;
using System;
using System.Net;
using Random = UnityEngine.Random;
using System.Linq;
using UnityEditor;
public class LogicScript : MonoBehaviour
{
    public BirdScript bird;
    public int playerScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject score;
    public GameObject gameOverScreen;
    public AudioSource dingSFX;
    public List<AudioSource> oneSFX = new List<AudioSource>();
    public List<AudioSource> fiveSFX = new List<AudioSource>();
    public List<AudioSource> twentyFiveSFX = new List<AudioSource>();
    public List<AudioSource> fiftySFX = new List<AudioSource>();
    public List<AudioSource> seventyFiveSFX = new List<AudioSource>();
    public List<AudioSource> oneHundredSFX = new List<AudioSource>();

    public GameObject Player;
    public GameObject highscore;

    public PlayerData _playerData;

    [ContextMenu("Increase Score")]

    private void Start()
    {
        _playerData.username = PlayerPrefs.GetString("username");
        _playerData.email = PlayerPrefs.GetString("email");
        _playerData.password = PlayerPrefs.GetString("password");   
        _playerData.highscore = PlayerPrefs.GetInt("HighScore", 0);
        _playerData.currentSelectedSkin = PlayerPrefs.GetInt("currentSelectedSkin", 0);
        highScoreText.text = $"HighScore: {PlayerPrefs.GetInt("HighScore", 0)}";
        Cursor.visible = false;

        Player = GameObject.FindGameObjectWithTag("MainMenuSkin");
       
    }
   
    public static IEnumerator CreateUser(string profile, System.Action<bool> callback = null)
    {
        using (UnityWebRequest request = new UnityWebRequest("https://flarpybard.onrender.com/users", "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("ApiKey", "f4c670ca09d8610798ab32897d698aa684daefb8ee05139c8e8fd7625ae0d37064a6ca91a914bbacb140b40859f7bb994f6a15d469a41fe2cbb25e4656e4b672");
             
            byte[] bodyRaw = Encoding.UTF8.GetBytes(profile);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();

            if (request.error != null)
            {
                Debug.Log(request.error);
                if (callback != null)
                {
                    callback.Invoke(false);
                }
            }
            else
            {
                if (callback != null)
                {
                    callback.Invoke(request.downloadHandler.text != "{}");
                }
            }
        }
    }
    public static IEnumerator Login(string profile, System.Action<bool> callback = null)
    {
        using (UnityWebRequest request = new UnityWebRequest("https://flarpybard.onrender.com/users/login", "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(profile);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.error != null)
            {
                Debug.Log(request.error);
                if (callback != null)
                {
                    callback.Invoke(false);
                }
            }
            else
            {
                if (callback != null)
                {
                    callback.Invoke(request.downloadHandler.text != "{}");
                }
            }
        }
    }
    public static IEnumerator GetUser(string id, System.Action<PlayerData> callback = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Get("https://flarpybard.onrender.com/users/" + id))
        {
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + request.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + request.error);
                    break;
                case UnityWebRequest.Result.Success:
                    if (callback != null)
                    {
                        callback.Invoke(PlayerData.Parse(request.downloadHandler.text));
                    }
                    break;

            }
        }
    }

    public void addScore(int scoreToAdd)
    {
        playerScore = playerScore + scoreToAdd;
        scoreText.text = playerScore.ToString();
        dingSFX.Play(0);
    }
    public void playSound()
    {
        if (playerScore == 1)
        {
            int i = Random.Range(0, oneSFX.Count);
            if (playerScore == 1)
            {
                oneSFX[i].Play();
            }
        }
       else if (playerScore == 5)
        {
            int i = Random.Range(0, fiveSFX.Count);
            fiveSFX[i].Play();
        }
        else if (playerScore == 25)
        {
            int i = Random.Range(0, twentyFiveSFX.Count);
            twentyFiveSFX[i].Play();
        }
        else if (playerScore == 50)
        {
            int i = Random.Range(0, fiftySFX.Count);
            fiftySFX[i].Play();
        }
        else if (playerScore == 75)
        {
            int i = Random.Range(0, seventyFiveSFX.Count);
            seventyFiveSFX[i].Play();
        }
        else if (playerScore == 100)
        {
            int i = Random.Range(0, oneHundredSFX.Count);
            oneHundredSFX[i].Play();
        }
    }
    public void resetScore()
    {
        playerScore = 0;
        PlayerPrefs.SetInt("HighScore", playerScore);
    }

    public void CheckHighScore()
    {
        if (playerScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
            UpdateHighScore(playerScore);
        }
    }
    public void UpdateHighScore(int Highscore)
    {
        Debug.Log("updating highscore");
        _playerData.highscore = Highscore;
        highScoreText.text = $"HighScore: {PlayerPrefs.GetInt("HighScore", 0)}";
        StartCoroutine(CreateUser(_playerData.Stringify(), result => {
            Debug.Log(result);
        }));
    }

    public void restartGame()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       
    }
    public void mainMenu()
    {
        Destroy(Player);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    
    public void gameOver() {
        Cursor.visible = true;
        bird.birdIsAlive = false;
        gameOverScreen.SetActive(true);
        score.SetActive(false);
        CheckHighScore();
    }
}
