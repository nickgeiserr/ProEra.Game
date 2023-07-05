// Decompiled with JetBrains decompiler
// Type: SeasonTabletAwardScreen
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeasonTabletAwardScreen : MonoBehaviour
{
  [SerializeField]
  private Transform _mvpHolder;

  private void Awake() => SeasonModeManager.self.OnInitComplete += new System.Action(this.Init);

  private void OnDestroy()
  {
    if (!(bool) (UnityEngine.Object) SeasonModeManager.self)
      return;
    SeasonModeManager.self.OnInitComplete -= new System.Action(this.Init);
  }

  private void Init()
  {
    List<int[]> conferenceMvp = PlayerStats.GetConferenceMVP(0, StatDuration.CurrentSeason, MVPType.Overall, 10);
    TeamDataStore[] teamData = SeasonTeamDataHolder.GetTeamData();
    SeasonModeManager self = SeasonModeManager.self;
    SGD_SeasonModeData seasonModeData = self.seasonModeData;
    if (!((UnityEngine.Object) this._mvpHolder != (UnityEngine.Object) null))
      return;
    for (int index = 0; index < this._mvpHolder.childCount; ++index)
    {
      PlayerData player = self.GetTeamData(conferenceMvp[index][0]).GetPlayer(conferenceMvp[index][1]);
      Transform child = this._mvpHolder.GetChild(index);
      child.Find("PlayerIcon").GetComponent<Image>().sprite = teamData[conferenceMvp[index][0]].Logo;
      child.Find("RankText").GetComponent<TextMeshProUGUI>().text = (index + 1).ToString();
      child.Find("Name").GetComponent<TextMeshProUGUI>().text = player.FullName;
      child.Find("Position").GetComponent<TextMeshProUGUI>().text = player.PlayerPosition.ToString();
    }
  }
}
