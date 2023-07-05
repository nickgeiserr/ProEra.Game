// Decompiled with JetBrains decompiler
// Type: DebugTextEnvironment
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR;
using Framework;
using Framework.Networked;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;

public class DebugTextEnvironment : TransitionEnvironment
{
  [SerializeField]
  private TextMeshProUGUI _text;
  private string version = "0.3.2";
  private const string LocalizationAlphaThrowingOn = "Transition_Text_AlphaThrowON";
  private const string LocalizationAlphaThrowingOff = "Transition_Text_AlphaThrowOFF";
  private const string LocalizationByeWeek = "Transition_Text_ByeWeek";

  public void ShowText(TransitionScreenController.ETransitionType type)
  {
    LocalizeStringEvent component = this._text.GetComponent<LocalizeStringEvent>();
    if (!((Object) component != (Object) null))
      return;
    component.StringReference.Arguments = (IList<object>) new string[0];
    switch (type)
    {
      case TransitionScreenController.ETransitionType.ByeWeek:
        component.StringReference.TableEntryReference = (TableEntryReference) "Transition_Text_ByeWeek";
        break;
      case TransitionScreenController.ETransitionType.DebugThrowing:
        component.StringReference.Arguments = (IList<object>) new string[1]
        {
          this.version.ToString()
        };
        component.StringReference.TableEntryReference = (TableEntryReference) (ScriptableSingleton<VRSettings>.Instance.AlphaThrowing.Value ? "Transition_Text_AlphaThrowON" : "Transition_Text_AlphaThrowOFF");
        break;
      case TransitionScreenController.ETransitionType.NetworkMessage:
        component.StringReference.TableEntryReference = (TableEntryReference) NetworkState.GetCurrentNetworkStatusMessage();
        break;
    }
  }
}
