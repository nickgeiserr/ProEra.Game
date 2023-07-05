// Decompiled with JetBrains decompiler
// Type: DeveloperMode
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Framework.Data;
using Framework.DeveloperSettings;
using System;
using System.Collections.Generic;
using TB12.UI;
using UnityEngine;
using Vars;

public class DeveloperMode : MonoBehaviour
{
  public static readonly VariableBool Activated = new VariableBool();
  [EditorSetting(ESettingType.FeatureToggle)]
  private static bool StartInDeveloperMode;
  private readonly VariableBool accelerate = new VariableBool();
  private readonly LinksHandler _linksHandler = new LinksHandler();
  private float _devSettingsTriggerTime;
  private bool _devModeButtonsDown;

  private void Awake()
  {
    this._linksHandler.SetLinks(new List<EventHandle>()
    {
      DeveloperMode.Activated.Link<bool>(new Action<bool>(this.HandleDevelopmentMode)),
      this.accelerate.Link<bool>((Action<bool>) (state => Time.timeScale = state ? 5f : 1f))
    });
    DeveloperMode.Activated.SetValue(DeveloperMode.StartInDeveloperMode);
  }

  private void OnDestroy() => this._linksHandler.Clear();

  private void HandleDevelopmentMode(bool state)
  {
    DevSettingsActivator.CanEnableDevSettings = state;
    if (state)
    {
      GameplayUI.ShowText("Developer Mode");
      OVRManager.cpuLevel = 2;
      OVRManager.gpuLevel = 2;
    }
    DevLogViewer.Verbose = state;
  }

  private void Update()
  {
    if (!(bool) DeveloperMode.Activated)
    {
      if (this.DevModeButtonsDown())
      {
        if (this._devModeButtonsDown)
        {
          if ((double) Time.unscaledTime < (double) this._devSettingsTriggerTime)
            return;
          this._devModeButtonsDown = false;
          DeveloperMode.Activated.SetValue(true);
        }
        else
        {
          this._devModeButtonsDown = true;
          this._devSettingsTriggerTime = Time.unscaledTime + 3f;
        }
      }
      else
        this._devModeButtonsDown = false;
    }
    else
      this.accelerate.SetValue((double) VRInputManager.Get(VRInputManager.Axis2D.Primary2DAxis, VRInputManager.Controller.RightHand).x > 0.9 && (double) VRInputManager.Get(VRInputManager.Axis2D.Primary2DAxis, VRInputManager.Controller.LeftHand).x > 0.9);
  }

  private bool DevModeButtonsDown() => (double) VRInputManager.Get(VRInputManager.Axis1D.Grip, VRInputManager.Controller.RightHand) > 0.89999997615814209 && (double) VRInputManager.Get(VRInputManager.Axis1D.Trigger, VRInputManager.Controller.RightHand) > 0.89999997615814209 && (double) VRInputManager.Get(VRInputManager.Axis1D.Grip, VRInputManager.Controller.LeftHand) > 0.89999997615814209 && (double) VRInputManager.Get(VRInputManager.Axis1D.Trigger, VRInputManager.Controller.LeftHand) > 0.89999997615814209 && VRInputManager.Get(VRInputManager.Button.Primary2DAxisClick, VRInputManager.Controller.LeftHand) && VRInputManager.Get(VRInputManager.Button.Primary2DAxisClick, VRInputManager.Controller.RightHand);
}
