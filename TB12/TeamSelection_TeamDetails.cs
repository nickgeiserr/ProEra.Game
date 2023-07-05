// Decompiled with JetBrains decompiler
// Type: TB12.TeamSelection_TeamDetails
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using System;
using TMPro;
using UnityEngine;

namespace TB12
{
  public class TeamSelection_TeamDetails : MonoBehaviour
  {
    [SerializeField]
    private TextMeshProUGUI _teamName;
    [SerializeField]
    private TextMeshProUGUI _teamCity;
    [SerializeField]
    private TextMeshProUGUI _teamOff;
    [SerializeField]
    private TextMeshProUGUI _teamDef;
    [SerializeField]
    private TextMeshProUGUI _offGrade;
    [SerializeField]
    private TextMeshProUGUI _defGrade;
    [SerializeField]
    private TextMeshProUGUI _spcGrade;
    [NonSerialized]
    public int TeamID;

    public void SetTeam(ETeamUniformId teamId)
    {
      TeamData team = TeamDataCache.GetTeam(TeamDataCache.ToTeamIndex(teamId));
      if (team == null)
        return;
      this._teamName.text = team.GetName();
      this._teamCity.text = team.GetCity();
    }

    public void SetTeam(int teamId)
    {
      this.TeamID = teamId;
      TeamData team = TeamDataCache.GetTeam(teamId);
      if (team == null)
        return;
      this._teamName.text = team.GetName();
      this._teamCity.text = team.GetCity();
    }
  }
}
