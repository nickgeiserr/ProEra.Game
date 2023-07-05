// Decompiled with JetBrains decompiler
// Type: TB12.UI.Screens.OnboardingOptions
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework;
using Framework.Data;
using Framework.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TB12.UI.Screens
{
  public class OnboardingOptions : UIView
  {
    [SerializeField]
    private TouchButton _standingButton;
    [SerializeField]
    private TouchButton _sittingButton;
    [SerializeField]
    private TouchButton _acceptButton;
    [SerializeField]
    private TouchButton _acceptPSVRSafetyButton;
    [SerializeField]
    private TouchButton _openEULAButton;
    [SerializeField]
    private GameObject _playModeMenu;
    [SerializeField]
    private GameObject _EULAMenu;
    [SerializeField]
    private GameObject _PSVRSafetyMenu;
    [SerializeField]
    private GameObject _RunTutorial;
    [SerializeField]
    private float _lastScreenDelay = 3f;
    [SerializeField]
    private GameObject _RunTutorialOculusText;
    [SerializeField]
    private GameObject _RunTutorialPSVRText;
    [SerializeField]
    private GameObject _RunTutorialIndexText;
    [SerializeField]
    private GameObject _RunTutorialViveText;
    [SerializeField]
    private GameObject[] _RunTutorialOculusImages;
    [SerializeField]
    private GameObject[] _RunTutorialPSVRImages;
    [SerializeField]
    private GameObject[] _RunTutorialViveImages;
    [SerializeField]
    private GameObject[] _RunTutorialIndexImages;
    private float orginalUIDistanceValue;
    private bool isCleaning;

    public override Enum ViewId { get; } = (Enum) EScreens.kOnboardingOptions;

    protected override void OnInitialize()
    {
      this.linksHandler.SetLinks(new List<EventHandle>()
      {
        (EventHandle) this._standingButton.Link(new System.Action(this.HandleStanding)),
        (EventHandle) this._sittingButton.Link(new System.Action(this.HandleSitting))
      });
      this.ShowPlayModeMenu();
    }

    private void OnDestroy() => this.linksHandler.Clear();

    private void ShowPlayModeMenu()
    {
      this._EULAMenu.SetActive(false);
      this._playModeMenu.SetActive(true);
      this._RunTutorial.SetActive(false);
    }

    private void ShowRunTutorial()
    {
      this._EULAMenu.SetActive(false);
      this._playModeMenu.SetActive(false);
      this._RunTutorial.SetActive(true);
      this._RunTutorialOculusText.SetActive(false);
      this._RunTutorialPSVRText.SetActive(false);
      for (int index = 0; index < this._RunTutorialOculusImages.Length; ++index)
      {
        this._RunTutorialOculusImages[index].SetActive(false);
        this._RunTutorialPSVRImages[index].SetActive(false);
        this._RunTutorialViveImages[index].SetActive(false);
        this._RunTutorialIndexImages[index].SetActive(false);
      }
      switch (VRUtils.GetDeviceType())
      {
        case VRUtils.DeviceType.PSVR2:
          this._RunTutorialPSVRText.SetActive(true);
          for (int index = 0; index < this._RunTutorialOculusImages.Length; ++index)
            this._RunTutorialPSVRImages[index].SetActive(true);
          break;
        case VRUtils.DeviceType.Vive:
          this._RunTutorialViveText.SetActive(true);
          for (int index = 0; index < this._RunTutorialViveImages.Length; ++index)
            this._RunTutorialViveImages[index].SetActive(true);
          break;
        case VRUtils.DeviceType.Index:
          this._RunTutorialIndexText.SetActive(true);
          for (int index = 0; index < this._RunTutorialIndexImages.Length; ++index)
            this._RunTutorialIndexImages[index].SetActive(true);
          break;
        default:
          this._RunTutorialOculusText.SetActive(true);
          for (int index = 0; index < this._RunTutorialOculusImages.Length; ++index)
            this._RunTutorialOculusImages[index].SetActive(true);
          break;
      }
      this.HandleCleanupDelay(this._lastScreenDelay);
    }

    private void HandleStanding()
    {
      ScriptableSingleton<VRSettings>.Instance.SeatedMode.SetValue(false);
      this.ShowRunTutorial();
    }

    private void HandleSitting()
    {
      ScriptableSingleton<VRSettings>.Instance.SeatedMode.SetValue(true);
      this.ShowRunTutorial();
    }

    private void HandleCleanupDelay(float timeDelay) => this.StartCoroutine(this.IHandleCleanupDelayRoutinte(timeDelay));

    private IEnumerator IHandleCleanupDelayRoutinte(float timeDelay)
    {
      if (!this.isCleaning)
      {
        this.isCleaning = true;
        yield return (object) new WaitForSeconds(timeDelay);
        this.HandleCleanup();
        this.isCleaning = false;
      }
    }

    private void HandleCleanup()
    {
      UIDispatch.FrontScreen.HideView(EScreens.kOnboardingOptions);
      AppState.SeasonMode.Value = ESeasonMode.kUnknown;
      PersistentData.userIsHome = true;
      PersistentData.SetUserTeam(TeamDataCache.GetTeam(2));
      PersistentData.SetCompTeam(TeamDataCache.GetTeam(26));
      GameplayManager.LoadLevelActivation(EGameMode.kTunnel, ETimeOfDay.Night);
    }
  }
}
