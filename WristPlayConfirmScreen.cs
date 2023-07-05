// Decompiled with JetBrains decompiler
// Type: WristPlayConfirmScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using Framework.UI;
using System;
using TB12;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;

public class WristPlayConfirmScreen : UIView
{
  [SerializeField]
  private TouchButton _confirmButton;
  [SerializeField]
  private Color _disabledColor;
  [SerializeField]
  private Color _enabledColor;
  [SerializeField]
  private LocalizeStringEvent LocalizeConfirmButton;
  private const string LocalizationAudible = "PlayCall_Button_Audible";
  private const string LocalizationHuddleBreak = "PlayCall_Button_Break";

  public override Enum ViewId { get; } = (Enum) EScreens.kWristPlayConfirm;

  public TouchButton ConfirmButton => this._confirmButton;

  public Color DisabledColor => this._disabledColor;

  public Color EnabledColor => this._enabledColor;

  public event System.Action OnConfirmed;

  protected override void WillAppear()
  {
    base.WillAppear();
    if (!(bool) (UnityEngine.Object) this._confirmButton || !MatchManager.Exists())
      return;
    if ((UnityEngine.Object) this.LocalizeConfirmButton != (UnityEngine.Object) null)
    {
      if (MatchManager.instance.playManager.canUserCallAudible)
        this.LocalizeConfirmButton.StringReference.TableEntryReference = (TableEntryReference) "PlayCall_Button_Audible";
      else
        this.LocalizeConfirmButton.StringReference.TableEntryReference = (TableEntryReference) "PlayCall_Button_Break";
    }
    if (!MatchManager.instance.playManager.IsUserRunningHurryUp)
      return;
    this._confirmButton.HighlighAsSelected(false, this._disabledColor, this._enabledColor);
    this._confirmButton.SetInteractible(false);
  }

  protected override void DidAppear()
  {
    base.DidAppear();
    if (!(bool) (UnityEngine.Object) this._confirmButton || !MatchManager.Exists())
      return;
    if (MatchManager.instance.playManager.IsUserRunningHurryUp)
    {
      this._confirmButton.HighlighAsSelected(false, this._disabledColor, this._enabledColor);
      this._confirmButton.SetInteractible(false);
    }
    else
    {
      this._confirmButton.onClick += new System.Action(this.ConfirmButtonOnClick);
      bool selected = true;
      this._confirmButton.HighlighAsSelected(selected, this._disabledColor, this._enabledColor);
      this._confirmButton.SetInteractible(selected);
    }
  }

  public void ResetConfirmHandler() => this.OnConfirmed = (System.Action) null;

  private void ConfirmButtonOnClick()
  {
    System.Action onConfirmed = this.OnConfirmed;
    if (onConfirmed != null)
      onConfirmed();
    this._confirmButton.onClick -= new System.Action(this.ConfirmButtonOnClick);
  }

  public void OnDestroy() => this._confirmButton.onClick -= new System.Action(this.ConfirmButtonOnClick);
}
