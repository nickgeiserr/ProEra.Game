// Decompiled with JetBrains decompiler
// Type: FootballVR.VRModule
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballWorld;
using Framework;
using Framework.Data;
using StreamingAssetsUtils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TB12.UI;
using UnityEngine;

namespace FootballVR
{
  public class VRModule : MonoBehaviour
  {
    protected static VRModule _instance;
    [SerializeField]
    private PlayerProfile _playerProfile;
    private UniformStore _uniformStore;
    private readonly ThumbstickLocomotion _thumbstickLocomotion = new ThumbstickLocomotion();
    private readonly LinksHandler _linksHandler = new LinksHandler();

    public static VRModule Instance
    {
      get
      {
        if ((UnityEngine.Object) VRModule._instance == (UnityEngine.Object) null)
          VRModule._instance = UnityEngine.Object.FindObjectOfType(typeof (VRModule)) as VRModule;
        return VRModule._instance;
      }
    }

    private void Awake()
    {
      VRModule._instance = this;
      this._uniformStore = SaveManager.GetUniformStore();
      this._uniformStore.Initialize();
      this._playerProfile.Initialize();
      ScriptableSettings.Initialiation.Trigger();
      this._thumbstickLocomotion.Setup();
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        VRState.LaserEnabled.Link<bool>(new Action<bool>(VRModule.SetLaserState)),
        VRState.Muted.Link<bool>(new Action<bool>(GameplayUI.SetMicMute)),
        VRState.LocomotionEnabled.Link<bool>(new Action<bool>(this._thumbstickLocomotion.SetState))
      });
      this.DelayedAssetsInit();
    }

    public string GetPlayerName()
    {
      if (!this._playerProfile.IsInitialized())
      {
        Debug.Log((object) "Player Profile was not initialized -- Initializing now ");
        this._playerProfile.Initialize();
      }
      return this._playerProfile.PlayerLastName;
    }

    private void OnDestroy()
    {
      VRModule._instance = (VRModule) null;
      this._uniformStore.Deinitialize();
      this._linksHandler.Clear();
    }

    private async void DelayedAssetsInit()
    {
      await Task.Delay(3000);
      string dataPath = Application.dataPath;
      string persistentDataPath = Application.streamingAssetsPath;
      await Task.Run((System.Action) (() => VRModule.InitializeAssets(dataPath, persistentDataPath)));
    }

    private static void InitializeAssets(string dataPath, string persistentAssetsPath)
    {
      try
      {
        BetterStreamingAssets.InitializeWithData(dataPath, persistentAssetsPath);
      }
      catch (Exception ex)
      {
        Debug.LogError((object) ex.Message);
      }
    }

    private static void SetLaserState(bool state)
    {
      LaserPointer.LaserEnabled = state;
      OVRRaycaster.InputEnabled = state;
    }
  }
}
