// Decompiled with JetBrains decompiler
// Type: TeamUniformConfigs
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL;
using DDL.UniformData;
using FootballVR;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTeamUniformConfigs", menuName = "Data/Uniforms/Team Uniforms Configs")]
public class TeamUniformConfigs : ScriptableObject
{
  public Material paintedUniformMat;
  public Material customizedUniformMat;
  public UniformConfig[] configs;

  public void ApplyConfig(Renderer renderer, int configIndex) => this.configs[configIndex].ApplyConfig(renderer);

  public void GetIndicesForGroup(ref List<int> indices, TeamUniformConfigs.EUniformGroup group)
  {
    int length = this.configs.Length;
    for (int index = 0; index < length; ++index)
    {
      if (this.configs[index].group == group)
        indices.Add(index);
    }
  }

  public void GetIndicesForGroup(
    ref List<int> indices,
    TeamUniformConfigs.EUniformGroup group,
    ECustomerType customer)
  {
    int length = this.configs.Length;
    for (int index = 0; index < length; ++index)
    {
      if (this.configs[index].group == group && (this.configs[index].teamType == customer || customer == ECustomerType.kAny))
        indices.Add(index);
    }
  }

  public int GetIndex(
    ETeamUniformId teamId,
    ETeamUniformFlags flag,
    ETeamUniformId defaultTeamId,
    ETeamUniformFlags defaultFlags)
  {
    if (teamId == ETeamUniformId.Unknown)
    {
      if (defaultTeamId == ETeamUniformId.Unknown)
        defaultTeamId = ETeamUniformId.Custom_1;
      teamId = defaultTeamId;
    }
    if (flag == ETeamUniformFlags.Unknown)
    {
      if (defaultFlags == ETeamUniformFlags.Unknown)
        defaultFlags = ETeamUniformFlags.Home;
      flag = defaultFlags;
    }
    int length = this.configs.Length;
    for (int index = 0; index < length; ++index)
    {
      if (this.configs[index].team == teamId && (this.configs[index].uniFlags == flag || this.configs[index].uniFlags == ETeamUniformFlags.Any))
        return index;
    }
    Debug.LogWarning((object) string.Format("[TeamUniformDatas] - Failed to extract exact team uniform {0}, {1}", (object) teamId, (object) flag));
    return this.get_any_index(teamId);
  }

  public UniformConfig GetConfig(
    ETeamUniformId teamId,
    ETeamUniformFlags flag,
    ETeamUniformId defaultTeamId,
    ETeamUniformFlags defaultFlags)
  {
    return this.configs[this.GetIndex(teamId, flag, defaultTeamId, defaultFlags)];
  }

  private int get_any_index(ETeamUniformId teamId)
  {
    int length = this.configs.Length;
    for (int anyIndex = 0; anyIndex < length; ++anyIndex)
    {
      if (this.configs[anyIndex].team == teamId)
        return anyIndex;
    }
    return -1;
  }

  public enum EUniformGroup
  {
    Unknown,
    AFC_East,
    AFC_North,
    AFC_South,
    AFC_West,
    NFC_East,
    NFC_North,
    NFC_South,
    NFC_West,
    Other,
    Custom,
  }
}
