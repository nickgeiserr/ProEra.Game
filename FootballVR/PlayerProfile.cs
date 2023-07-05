// Decompiled with JetBrains decompiler
// Type: FootballVR.PlayerProfile
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FootballVR
{
  [CreateAssetMenu(menuName = "TB12/Data/PlayerProfile")]
  [AppStore]
  public class PlayerProfile : ScriptableObject
  {
    [NonSerialized]
    public PlayerCustomization Customization = new PlayerCustomization();
    [NonSerialized]
    private bool _initialized;
    [NonSerialized]
    private bool _isNewProfile;
    [NonSerialized]
    private bool _isLamarVo;

    public string PlayerFirstName => (string) this.Customization.FirstName;

    public string PlayerLastName => (string) this.Customization.LastName;

    public string UserName => (string) this.Customization.UserName;

    public int PlayerNumber => (int) this.Customization.UniformNumber;

    public string FirstInitialAndLastName => string.Format("{0}. {1}", (object) (char) (this.PlayerFirstName.Length > 0 ? (int) this.PlayerFirstName[0] : (int) ' '), (object) this.PlayerLastName);

    public Dictionary<int, PlayerCustomization> Profiles { get; } = new Dictionary<int, PlayerCustomization>();

    public bool IsNewProfile
    {
      get => this._isNewProfile;
      set => this._isNewProfile = value;
    }

    private void OnEnable() => this.Initialize();

    public bool IsInitialized() => this._initialized;

    public void Initialize()
    {
      if (this._initialized)
        return;
      this._initialized = true;
    }

    public bool IsLamarVO() => this._isLamarVo;

    public void SetIsLamarVO(bool value) => this._isLamarVo = value;

    public void NewProfile()
    {
      Debug.Log((object) "Set new profile");
      this.IsNewProfile = true;
      this.Customization.Reset();
    }

    public void RefreshGloves(ETeamUniformId teamUniformId)
    {
      PlayerAvatar instance = PlayerAvatar.Instance;
      if (!((UnityEngine.Object) instance != (UnityEngine.Object) null))
        return;
      int configId = (int) this.Customization.GloveId.Value;
      if (this.Customization.GloveId.Value != EGlovesId.TeamAlt && this.Customization.GloveId.Value != EGlovesId.TeamColor)
        return;
      instance.LeftController.Renderer.ApplyCustomization(configId, teamUniformId);
      instance.RightController.Renderer.ApplyCustomization(configId, teamUniformId);
    }
  }
}
