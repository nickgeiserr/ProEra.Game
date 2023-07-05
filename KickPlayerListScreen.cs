// Decompiled with JetBrains decompiler
// Type: KickPlayerListScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using FootballVR.UI;
using System.Collections.Generic;
using UnityEngine;

public class KickPlayerListScreen : MonoBehaviour
{
  [SerializeField]
  private KickPlayerPanelItem[] _playersKickItems;
  [SerializeField]
  private TouchButton _backButton;

  private void OnEnable() => this._backButton.onClick += new System.Action(this.TurnOff);

  private void OnDisable() => this._backButton.onClick -= new System.Action(this.TurnOff);

  public void InjectData(
    List<int> players,
    Dictionary<int, PlayerCustomization> playerProfiles)
  {
    for (int index = 0; index < players.Count; ++index)
    {
      int player = players[index];
      PlayerCustomization playerCustomization;
      if (playerProfiles.TryGetValue(player, out playerCustomization))
      {
        this._playersKickItems[index].gameObject.SetActive(true);
        this._playersKickItems[index].InjectData((string) playerCustomization.LastName, player);
      }
    }
    for (int count = players.Count; count < this._playersKickItems.Length; ++count)
      this._playersKickItems[count].gameObject.SetActive(false);
  }

  private void TurnOff() => this.gameObject.SetActive(false);
}
