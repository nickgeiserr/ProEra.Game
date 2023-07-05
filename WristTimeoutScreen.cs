// Decompiled with JetBrains decompiler
// Type: WristTimeoutScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using FootballVR.UI;
using Framework;
using Framework.UI;
using ProEra.Game;
using System;
using TB12;
using UnityEngine;

public class WristTimeoutScreen : UIView
{
  [SerializeField]
  private TouchButton _timeoutButton;
  [SerializeField]
  private Color _timeoutDisabledColor;
  [SerializeField]
  private Color _timeoutEnabledColor;
  [SerializeField]
  private Transform _normalPosition;
  [SerializeField]
  private Transform _oneHandOnFieldPosition;
  [SerializeField]
  private Transform _oneHandSidelinePosition;

  public override Enum ViewId { get; } = (Enum) EScreens.kWristTimeout;

  public TouchButton TimeoutButton => this._timeoutButton;

  public Color TimeoutDisabledColor => this._timeoutDisabledColor;

  public Color TimeoutEnabledColor => this._timeoutEnabledColor;

  protected override void DidAppear()
  {
    base.DidAppear();
    this.TimeoutButton.onClick += new System.Action(this.TimeoutButtonOnClick);
    PEGameplayEventManager.PlayerOnSideline.OnValueChanged += new Action<bool>(this.PlayerPositionChanged);
    if (!(bool) (UnityEngine.Object) this._timeoutButton)
      return;
    bool selected = GameTimeoutState.UserTimeouts.Value > 0;
    this._timeoutButton.HighlighAsSelected(selected, this._timeoutDisabledColor, this._timeoutEnabledColor);
    this._timeoutButton.SetInteractible(selected);
  }

  private void TimeoutButtonOnClick()
  {
    if (GameTimeoutState.TimeoutCalledP1.Value)
      return;
    TimeoutManager.CallTimeOut(Team.Player1);
  }

  protected override void DidDisappear()
  {
    base.DidDisappear();
    this.TimeoutButton.onClick -= new System.Action(this.TimeoutButtonOnClick);
    PEGameplayEventManager.PlayerOnSideline.OnValueChanged -= new Action<bool>(this.PlayerPositionChanged);
  }

  private void PlayerPositionChanged(bool sideline)
  {
    Debug.Log((object) ("WristTimeoutScreen: PlayerPositionChanged: " + sideline.ToString()));
    if ((bool) ScriptableSingleton<VRSettings>.Instance.OneHandedMode)
    {
      if ((bool) PEGameplayEventManager.PlayerOnSideline)
        this.transform.SetParent(this._oneHandSidelinePosition);
      else
        this.transform.SetParent(this._oneHandOnFieldPosition);
    }
    else
      this.transform.SetParent(this._normalPosition);
    this.transform.ResetTransform();
  }
}
