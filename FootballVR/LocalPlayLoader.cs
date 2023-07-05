// Decompiled with JetBrains decompiler
// Type: FootballVR.LocalPlayLoader
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework;
using StreamingAssetsUtils;
using TB12;
using UnityEngine;

namespace FootballVR
{
  [CreateAssetMenu(fileName = "LocalPlayLoader", menuName = "TB12/Managers/LocalPlayLoader", order = 1)]
  public class LocalPlayLoader : ScriptableSingleton<LocalPlayLoader>
  {
    [SerializeField]
    private UniformStore _uniformStore;
    [SerializeField]
    private string[] _localPlayNames;
    [SerializeField]
    private string _receiverPlayName;
    [SerializeField]
    private PlayMeta[] _playMetas;
    private int _currPlayIndexInternal;
    public static PlayMeta LoadedPlayMeta;
    private const float _middleOfFieldPos = 26.65f;

    public int CurrentPlayIndex
    {
      get => this._currPlayIndexInternal;
      set
      {
        this._currPlayIndexInternal = value;
        if (this._currPlayIndexInternal >= this._localPlayNames.Length)
        {
          this._currPlayIndexInternal = 0;
        }
        else
        {
          if (this._currPlayIndexInternal >= 0)
            return;
          this._currPlayIndexInternal = this._localPlayNames.Length - 1;
        }
      }
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      this.Clear();
    }

    public void LoadPlayByIndex(int index)
    {
      this.CurrentPlayIndex = index;
      this.LoadCurrentPlay();
    }

    public void LoadReceiverPlay()
    {
      SerializedDataManager.Clear();
      LocalPlayLoader.LoadedPlayMeta = this._playMetas[2];
      this.LoadPlay(this._receiverPlayName);
    }

    public void LoadCurrentPlay(float yardLine = -1f)
    {
      SerializedDataManager.Clear();
      LocalPlayLoader.LoadedPlayMeta = this._playMetas[this._currPlayIndexInternal];
      this.LoadPlay(this._localPlayNames[this._currPlayIndexInternal], yardLine);
    }

    public void LoadNextPlay()
    {
      ++this.CurrentPlayIndex;
      this.LoadCurrentPlay();
    }

    public void LoadPlay(string playName, float yardLine = -1f)
    {
      string path = "/LocalPlays/" + playName;
      if (!BetterStreamingAssets.FileExists(path))
        Debug.LogError((object) ("[ConfigHandler] Can't find app config file at '" + path + "' - this is fatal"));
      else
        this.LoadPlay(JsonUtility.FromJson<DataSensitiveStructs_v5.PlayData>(BetterStreamingAssets.ReadAllText(path)), yardLine);
    }

    public void LoadPlay(DataSensitiveStructs_v5.PlayData play, float yardLine = -1f)
    {
      this.ClosePlay();
      if ((double) yardLine >= 0.0)
      {
        Vector2 vector2 = Utilities.YardsToGamePosVec2(new Vector2(yardLine + 0.2f, 26.65f)) - play.ballData.placement;
        play.ballData.placement += vector2;
        foreach (DataSensitiveStructs_v5.PlayerData player in play.players)
          player.placement = player.placement + vector2;
      }
      SerializedDataManager.DeserializeData(play, this._uniformStore);
    }

    public void LoadPreviousPlay()
    {
      --this.CurrentPlayIndex;
      this.LoadCurrentPlay();
    }

    public void ClosePlay() => SerializedDataManager.Clear();

    public void Clear() => this._currPlayIndexInternal = 0;
  }
}
