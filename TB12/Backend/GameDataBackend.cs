// Decompiled with JetBrains decompiler
// Type: TB12.Backend.GameDataBackend
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace TB12.Backend
{
  public static class GameDataBackend
  {
    private const string backendApi = "https://dev-tb12.platform.bytecubedlabs.co/";
    private const string authToken = "40f8b409-712e-4631-824d-0b6a289debe0";
    private const int version = 3;

    private static async Task<string> GetLastTimeStampAsync(CancellationToken cancellationToken)
    {
      UnityWebRequest www = UnityWebRequest.Head(string.Format("{0}gamedata/{1}", (object) "https://dev-tb12.platform.bytecubedlabs.co/", (object) 3));
      www.SetRequestHeader("Authorization", "40f8b409-712e-4631-824d-0b6a289debe0");
      UnityWebRequestAsyncOperation op = www.SendWebRequest();
      while (!op.isDone && !cancellationToken.IsCancellationRequested)
        await Task.Delay(25, cancellationToken);
      if (cancellationToken.IsCancellationRequested)
        return (string) null;
      int num = www.result == UnityWebRequest.Result.ConnectionError ? 1 : (www.result == UnityWebRequest.Result.ProtocolError ? 1 : 0);
      if (num != 0)
        Debug.LogError((object) www.error);
      return num != 0 ? (string) null : www.GetResponseHeader("Timestamp");
    }

    private static async Task<string> GetDataAsync(
      string apiCall,
      CancellationToken cancellationToken)
    {
      UnityWebRequest www = UnityWebRequest.Get("https://dev-tb12.platform.bytecubedlabs.co/" + apiCall);
      www.SetRequestHeader("Authorization", "40f8b409-712e-4631-824d-0b6a289debe0");
      UnityWebRequestAsyncOperation op = www.SendWebRequest();
      while (!op.isDone && !cancellationToken.IsCancellationRequested)
        await Task.Delay(25, cancellationToken);
      int num = www.result == UnityWebRequest.Result.ConnectionError ? 1 : (www.result == UnityWebRequest.Result.ProtocolError ? 1 : 0);
      if (num != 0)
        Debug.LogError((object) www.error);
      string text = num != 0 ? (string) null : www.downloadHandler.text;
      www = (UnityWebRequest) null;
      op = (UnityWebRequestAsyncOperation) null;
      return text;
    }

    private static async Task<bool> PostDataAsync(
      string apiCall,
      string json,
      CancellationToken cancellationToken)
    {
      UnityWebRequest request = new UnityWebRequest("https://dev-tb12.platform.bytecubedlabs.co/" + apiCall, "POST");
      byte[] data = (byte[]) null;
      if (!string.IsNullOrEmpty(json))
        data = Encoding.UTF8.GetBytes(json);
      UnityWebRequest unityWebRequest = request;
      UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(data);
      uploadHandlerRaw.contentType = "application/json";
      unityWebRequest.uploadHandler = (UploadHandler) uploadHandlerRaw;
      request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
      request.SetRequestHeader("Authorization", "40f8b409-712e-4631-824d-0b6a289debe0");
      UnityWebRequestAsyncOperation op = request.SendWebRequest();
      while (!op.isDone && !cancellationToken.IsCancellationRequested)
        await Task.Delay(25, cancellationToken);
      if (cancellationToken.IsCancellationRequested)
        return false;
      int num = request.result == UnityWebRequest.Result.ConnectionError ? 1 : (request.result == UnityWebRequest.Result.ProtocolError ? 1 : 0);
      if (num != 0)
        Debug.LogError((object) request.error);
      return num == 0;
    }

    public static async Task<long> GetLatestTimestamp(CancellationToken cancellationToken)
    {
      long timeStamp = 0;
      string lastTimeStampAsync = await GameDataBackend.GetLastTimeStampAsync(cancellationToken);
      if (lastTimeStampAsync != null)
        long.TryParse(lastTimeStampAsync, out timeStamp);
      return timeStamp;
    }

    public static async Task<bool> SaveToBackendAsync(
      string json,
      long timeStamp,
      CancellationToken cancellationToken)
    {
      string json1 = JsonConvert.SerializeObject((object) new GameDataBackend.JsonEntry()
      {
        timestamp = timeStamp,
        data = json
      });
      return await GameDataBackend.PostDataAsync(string.Format("gamedata/{0}", (object) 3), json1, cancellationToken);
    }

    public static async Task<GameDataBackend.JsonEntry> GetLatestGameplayData(
      CancellationToken cancellationToken)
    {
      string dataAsync = await GameDataBackend.GetDataAsync(string.Format("gamedata/{0}", (object) 3), cancellationToken);
      try
      {
        return JsonConvert.DeserializeObject<GameDataBackend.JsonEntry>(dataAsync);
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ex.Message);
        return (GameDataBackend.JsonEntry) null;
      }
    }

    public class JsonEntry
    {
      public long timestamp;
      public string data;
    }
  }
}
