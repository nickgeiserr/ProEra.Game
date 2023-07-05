// Decompiled with JetBrains decompiler
// Type: FootballVR.PlayerAvatar
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using Framework;
using Framework.Data;
using System;
using System.Collections.Generic;
using UDB;
using UnityEngine;
using Vars;

namespace FootballVR
{
  public class PlayerAvatar : MonoBehaviour
  {
    public static PlayerAvatar Instance;
    [SerializeField]
    private HeadTiltHandler _headTiltHandler;
    [SerializeField]
    protected PlayerBodyController _bodyController;
    [SerializeField]
    protected PlayerUniformsStore _uniformsStore;
    public HelmetController HelmetController;
    public HandController LeftController;
    public HandController RightController;
    public Transform headTransform;
    protected readonly LinksHandler _linksHandler = new LinksHandler();
    private bool _initialized;
    private bool _deinitialized;
    public AppEvent OnAvatarInitializationComplete = new AppEvent();

    protected PlayerProfile _playerProfile => SaveManager.GetPlayerProfile();

    public PlayerBodyController BodyController => this._bodyController;

    public virtual void Awake()
    {
      PlayerAvatar.Instance = this;
      this._playerProfile.Initialize();
      this.Initialize();
      this._headTiltHandler.SetState(true);
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
    }

    public virtual void OnDestroy()
    {
      if ((UnityEngine.Object) PlayerAvatar.Instance == (UnityEngine.Object) this)
        PlayerAvatar.Instance = (PlayerAvatar) null;
      this.Deinitialize(true);
    }

    public static HandController GetLeftHand() => (UnityEngine.Object) PlayerAvatar.Instance == (UnityEngine.Object) null ? (HandController) null : PlayerAvatar.Instance.LeftController;

    public static HandController GetRightHand() => (UnityEngine.Object) PlayerAvatar.Instance == (UnityEngine.Object) null ? (HandController) null : PlayerAvatar.Instance.RightController;

    public bool Initialize()
    {
      if (this._initialized)
        return true;
      this.OnInitialize();
      this._initialized = true;
      this.OnAvatarInitializationComplete?.Trigger();
      return true;
    }

    protected virtual void OnInitialize()
    {
      this.HelmetController.Initialize();
      this.LeftController.Initialize();
      this.RightController.Initialize();
      this._bodyController.Initialize();
      this._bodyController.SetVisible(true);
      this._bodyController.SetBodyGraphics(this._playerProfile.Customization.BodyType.Value).ConfigureAwait(false);
      this._bodyController.SetUniformData(-1, this._playerProfile.Customization);
      this._linksHandler.SetLinks(new List<EventHandle>()
      {
        this._playerProfile.Customization.GloveId.Link<EGlovesId>((Action<EGlovesId>) (x => this.HandleGlovesChanged())),
        this._playerProfile.Customization.BodyType.Link<global::EBodyType>(new Action<global::EBodyType>(this.GenderChangeHandler)),
        ScriptableSingleton<VRSettings>.Instance.ControllerAnnotations.Link<bool>(new Action<bool>(this.ApplyControllerAnnotations)),
        VRState.ControllerGraphics.Link<bool>(new Action<bool>(this.ApplyControllerGraphics)),
        VRState.HelmetEnabled.Link<bool>((Action<bool>) (state => this.HelmetController.SetRenderState(state && !VRSettings.forceHideHelmet))),
        VREvents.AdjustPlayerHeight.Link(new System.Action(this.RecaltulatePlayerHeight))
      });
      this._bodyController.InitializeLinks();
    }

    protected void ApplyControllerGraphics(bool controllerGraphics)
    {
      this.LeftController.Renderer.SetGraphics(controllerGraphics);
      this.RightController.Renderer.SetGraphics(controllerGraphics);
    }

    private void ApplyControllerAnnotations(bool state)
    {
      this.LeftController.Renderer.SetControllerAnnotations(state);
      this.RightController.Renderer.SetControllerAnnotations(state);
    }

    public void SyncBodyPosition() => this._bodyController.SyncPosition();

    private void Update() => this._bodyController.UpdateHeightCheck();

    private void RecaltulatePlayerHeight() => this._playerProfile.Customization.HeightScale = this._bodyController.RecalculateHeight();

    protected void HandleGloves(EGlovesId glovesId, ETeamUniformId uniformID = ETeamUniformId.Army)
    {
      int configId = (int) glovesId;
      if (uniformID == ETeamUniformId.Army)
        uniformID = this._playerProfile.Customization.Uniform.Value;
      this.LeftController.Renderer.ApplyCustomization(configId, uniformID);
      this.RightController.Renderer.ApplyCustomization(configId, uniformID);
    }

    public virtual void Deinitialize(bool alreadyBeingDestroyed = false)
    {
      if (this._deinitialized)
        return;
      this._linksHandler.Clear();
      this._deinitialized = true;
      if ((UnityEngine.Object) this.LeftController != (UnityEngine.Object) null)
        Objects.SafeDestroy((UnityEngine.Object) this.LeftController.gameObject);
      if ((UnityEngine.Object) this.RightController != (UnityEngine.Object) null)
        Objects.SafeDestroy((UnityEngine.Object) this.RightController.gameObject);
      if ((UnityEngine.Object) this.HelmetController != (UnityEngine.Object) null)
        Objects.SafeDestroy((UnityEngine.Object) this.HelmetController.gameObject);
      if (alreadyBeingDestroyed)
        return;
      Objects.SafeDestroy((UnityEngine.Object) this.gameObject);
    }

    private void GenderChangeHandler(global::EBodyType value)
    {
      PlayerCustomization customization = this._playerProfile.Customization;
      this._bodyController.SetBodyGraphics(value).ConfigureAwait(false);
      this._bodyController.SetCustomization((ETeamUniformId) (Variable<ETeamUniformId>) customization.Uniform, (bool) customization.HomeUniform);
    }

    private void HandleUniformNumberChanged(int number) => this._uniformsStore.RegenerateUniformsForPlayer(-1);

    private void HandleUniformChanged()
    {
      PlayerCustomization customization = this._playerProfile.Customization;
      this._bodyController.SetCustomization((ETeamUniformId) (Variable<ETeamUniformId>) customization.Uniform, (bool) customization.HomeUniform);
      this._uniformsStore.RegenerateUniformsForPlayer(-1);
    }

    public void HandleGameTeamUniformChanged()
    {
      Debug.Log((object) ("HandleGameTeamUniformChanged() " + Time.realtimeSinceStartup.ToString()));
      if (!SingletonBehaviour<PersistentData, MonoBehaviour>.Exists())
        return;
      if (PersistentData.userIsHome)
        this._bodyController.SetCustomization(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.HomeTeamUniform.Value, true);
      else
        this._bodyController.SetCustomization(SingletonBehaviour<PersistentData, MonoBehaviour>.instance.AwayTeamUniform.Value, false);
      this._uniformsStore.RegenerateUniformsForPlayer(-1);
    }

    private void HandleGlovesChanged() => this.HandleGloves((EGlovesId) (Variable<EGlovesId>) this._playerProfile.Customization.GloveId);
  }
}
