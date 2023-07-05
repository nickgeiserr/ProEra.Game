// Decompiled with JetBrains decompiler
// Type: KickPlayerConfirmationScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.Multiplayer;
using FootballVR.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

public class KickPlayerConfirmationScreen : MonoBehaviour
{
  [SerializeField]
  private KickPlayerListScreen _kickPlayerListScreen;
  [SerializeField]
  private LocalizeStringEvent _explanationText;
  [SerializeField]
  private TouchButton _backButton;
  [SerializeField]
  private TouchButton _confirmButton;
  private int _playerToKick;

  private void OnEnable()
  {
    this._confirmButton.onClick += new System.Action(this.OnConfirmClick);
    this._backButton.onClick += new System.Action(this.TurnOff);
  }

  private void OnDisable()
  {
    this._confirmButton.onClick -= new System.Action(this.OnConfirmClick);
    this._backButton.onClick -= new System.Action(this.TurnOff);
  }

  public void InjectData(TextMeshProUGUI playerNameText, int iD)
  {
    this._playerToKick = iD;
    this._explanationText.StringReference.Arguments = (IList<object>) new string[1]
    {
      playerNameText.text
    };
  }

  private void OnConfirmClick()
  {
    MultiplayerEvents.KickPlayer.Trigger(this._playerToKick);
    this._kickPlayerListScreen.gameObject.SetActive(false);
    this.TurnOff();
  }

  private void TurnOff() => this.gameObject.SetActive(false);
}
