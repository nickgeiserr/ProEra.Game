// Decompiled with JetBrains decompiler
// Type: WristQuickSimScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.UI;
using System;
using TB12;
using UnityEngine;

public class WristQuickSimScreen : UIView
{
  [SerializeField]
  private TouchButton _simulationButton;

  public override Enum ViewId => (Enum) EScreens.kWristQuickSim;

  protected override void DidAppear()
  {
    if ((UnityEngine.Object) this._simulationButton != (UnityEngine.Object) null)
      this._simulationButton.onClick += new System.Action(this.HandleSimulationButton);
    else
      Debug.LogError((object) "_simulationButton is null in LeftTopWristScreen3.Awake");
  }

  protected override void DidDisappear()
  {
    base.DidDisappear();
    if ((UnityEngine.Object) this._simulationButton != (UnityEngine.Object) null)
      this._simulationButton.onClick -= new System.Action(this.HandleSimulationButton);
    else
      Debug.LogError((object) "_simulationButton is null in LeftTopWristScreen3.OnDestroy");
  }

  private void HandleSimulationButton()
  {
    MatchManager.instance.ShouldSimulate.Toggle();
    this._simulationButton.gameObject.GetComponent<ButtonText>().text = ((bool) MatchManager.instance.ShouldSimulate ? "STOP" : "START") + " SIM";
  }
}
