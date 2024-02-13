using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public static class ApiCall 
{
    public  static IEnumerator GetLeaderboard(string uri, System.Action<List<PlayerData>> callback = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
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
                    PlayerData[] playerlist = JsonHelper.FromJson<PlayerData>("{\"Items\":" + request.downloadHandler.text + "}");
                    if (callback != null)
                    {
                        callback.Invoke(playerlist.ToList());
                    }
                    break; 
                    
            }
        }
     
    }
}
