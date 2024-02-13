using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public string email;
    public string username;
    public int highscore;
    public string password;
    public int currentSelectedSkin;

    public static PlayerData Parse(string json)
    {
        return JsonUtility.FromJson<PlayerData>(json);
    }
    public string Stringify()
    {
        return JsonUtility.ToJson(this);
    }
}






