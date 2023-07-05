// Decompiled with JetBrains decompiler
// Type: KickPlayerPanelItem
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using System;
using TMPro;
using UnityEngine;

public class KickPlayerPanelItem : MonoBehaviour
{
  [SerializeField]
  private KickPlayerConfirmationScreen _kickPlayerConfirmationScreen;
  [SerializeField]
  private TextMeshProUGUI _playerNameText;
  [SerializeField]
  private TouchButton _kickButton;
  private GameObject _parentScreen;

  private void OnEnable() => this._kickButton.onClickInfo += new Action<TouchButton>(this.InjectConfirmationScreenWithData);

  private void OnDisable() => this._kickButton.onClickInfo -= new Action<TouchButton>(this.InjectConfirmationScreenWithData);

  private void InjectConfirmationScreenWithData(TouchButton obj)
  {
    this._kickPlayerConfirmationScreen.InjectData(this._playerNameText, obj.GetID());
    this._kickPlayerConfirmationScreen.gameObject.SetActive(true);
  }

  public void InjectData(string playerName, int playerID)
  {
    this._playerNameText.text = playerName;
    this._kickButton.SetID(playerID);
  }
}
