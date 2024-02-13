using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderBoardsScript : MonoBehaviour
{
    public List<TextMeshProUGUI> usernames = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> highscores = new List<TextMeshProUGUI>();

    private PlayerData _playerData;
    public void OnEnable()
    {
        _playerData = new PlayerData();
        _playerData.username = PlayerPrefs.GetString("username");
        _playerData.highscore = PlayerPrefs.GetInt("HighScore", 0);
        StartCoroutine(ApiCall.GetLeaderboard("https://flarpybard.onrender.com/users", players =>
        {
            for (int i = 0; i < usernames.Count; i++)
            {
                if (i == players.Count)
                { break; }
                else
                {
                    usernames[i].text = $"{i + 1}" + ". " + $"{players[i].username}";
                    highscores[i].text = $"Highscore:  {players[i].highscore}";
                }

            }
        }));
    }
   
   
}
