// Decompiled with JetBrains decompiler
// Type: PgpToPlexUniformImporter
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using DDL.UniformData;
using FootballWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PgpToPlexUniformImporter : MonoBehaviour
{
  public Texture2D[] _textures;
  public UniformStore _UniformStore;
  public TeamUniformConfigs _pgpConfig;

  [ContextMenu("Execute")]
  public void Execute()
  {
    Debug.Log((object) "Executing..");
    foreach (UnityEngine.Object texture in this._textures)
    {
      string name = texture.name;
      if (name.Contains("Alt") || name.Contains("alt"))
      {
        Debug.LogWarning((object) ("Skipping " + name + " as alternate uniform"));
      }
      else
      {
        bool flag1 = name.Contains("Home");
        bool flag2 = name.Contains("Away");
        if (flag1 == flag2)
        {
          Debug.LogError((object) string.Format("{0} {1} {2} flags should be different.. skipping..", (object) name, (object) flag1, (object) flag2));
        }
        else
        {
          string teamName = name.Split('_', StringSplitOptions.None)[1];
          ETeamUniformId teamId = DataUtils.GetTeamId(teamName);
          if (teamId == ETeamUniformId.Unknown)
          {
            Debug.LogError((object) ("Failed to map " + name + " " + teamName));
          }
          else
          {
            ETeamUniformFlags eteamUniformFlags = flag1 ? ETeamUniformFlags.Home : ETeamUniformFlags.Away;
            Debug.Log((object) ("Mapped " + name + " to " + teamId.ToString() + " " + (flag1 ? "Home" : "Away")));
            if ((UnityEngine.Object) this.FindPgpConfig(teamId, eteamUniformFlags) == (UnityEngine.Object) null)
              Debug.LogError((object) string.Format("Failed to find pgp config for {0}", (object) teamId));
            else
              this.FindUniform(teamId, eteamUniformFlags);
          }
        }
      }
    }
  }

  private FootballWorld.UniformConfig FindUniform(
    ETeamUniformId teamId,
    ETeamUniformFlags teamFlags)
  {
    List<FootballWorld.UniformConfig> configs = this._UniformStore.Configs;
    foreach (FootballWorld.UniformConfig uniform in configs)
    {
      if (uniform.Team == teamId && uniform.Flags == teamFlags)
        return uniform;
    }
    FootballWorld.UniformConfig uniform1 = new FootballWorld.UniformConfig()
    {
      TeamName = string.Format("{0} - {1}", (object) teamId, (object) teamFlags),
      Team = teamId,
      Flags = teamFlags
    };
    configs.Add(uniform1);
    return uniform1;
  }

  private FootballVR.UniformConfig FindPgpConfig(
    ETeamUniformId teamUniformId,
    ETeamUniformFlags flags)
  {
    foreach (FootballVR.UniformConfig config in this._pgpConfig.configs)
    {
      if (config.team == teamUniformId && config.uniFlags == flags)
        return config;
    }
    return (FootballVR.UniformConfig) null;
  }
}
