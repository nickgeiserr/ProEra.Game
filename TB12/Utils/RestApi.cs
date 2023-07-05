// Decompiled with JetBrains decompiler
// Type: TB12.Utils.RestApi
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace TB12.Utils
{
  public static class RestApi
  {
    public static IEnumerator GetRequest(string uri, Action<string> callback)
    {
      using (UnityWebRequest req = UnityWebRequest.Get(uri))
      {
        yield return (object) req.SendWebRequest();
        if (req.result == UnityWebRequest.Result.ConnectionError)
        {
          Debug.Log((object) ("[Error]: " + req.error));
          callback(string.Empty);
        }
        else
          callback(req.downloadHandler.text);
      }
    }

    public static IEnumerator PostRequest(string uri, string json)
    {
      using (UnityWebRequest req = UnityWebRequest.Post(uri, json))
      {
        yield return (object) req.SendWebRequest();
        if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
          Debug.Log((object) req.error);
        else
          Debug.Log((object) "POSTED");
      }
    }
  }
}
