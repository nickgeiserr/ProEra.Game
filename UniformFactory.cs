// Decompiled with JetBrains decompiler
// Type: UniformFactory
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using System.Collections.Generic;
using UnityEngine;

public class UniformFactory
{
  private Dictionary<string, Texture2D> userTeamJerseys;
  private Dictionary<string, Texture2D> compTeamJerseys;
  private Dictionary<int, UniformSet> uniformSets;

  public UniformFactory()
  {
    this.uniformSets = new Dictionary<int, UniformSet>();
    this.userTeamJerseys = new Dictionary<string, Texture2D>();
    this.compTeamJerseys = new Dictionary<string, Texture2D>();
  }

  public Texture2D GetSavedJersey(string playerNameNumber, UniformAssetType type)
  {
    switch (type)
    {
      case UniformAssetType.USER:
        if (this.userTeamJerseys.ContainsKey(playerNameNumber))
          return this.userTeamJerseys[playerNameNumber];
        break;
      case UniformAssetType.COMP:
        if (this.compTeamJerseys.ContainsKey(playerNameNumber))
          return this.compTeamJerseys[playerNameNumber];
        break;
    }
    return (Texture2D) null;
  }

  public void SaveJersey(string playerNameNumber, Texture2D jerseyTexture, UniformAssetType type)
  {
    switch (type)
    {
      case UniformAssetType.USER:
        if (this.userTeamJerseys.ContainsKey(playerNameNumber))
          break;
        this.userTeamJerseys.Add(playerNameNumber, jerseyTexture);
        break;
      case UniformAssetType.COMP:
        if (this.compTeamJerseys.ContainsKey(playerNameNumber))
          break;
        this.compTeamJerseys.Add(playerNameNumber, jerseyTexture);
        break;
    }
  }

  public void ClearUniformSets()
  {
    this.uniformSets = new Dictionary<int, UniformSet>();
    Resources.UnloadUnusedAssets();
  }

  public void ClearSavedJerseys()
  {
    this.userTeamJerseys = new Dictionary<string, Texture2D>();
    this.compTeamJerseys = new Dictionary<string, Texture2D>();
    Resources.UnloadUnusedAssets();
  }
}
