// Decompiled with JetBrains decompiler
// Type: SidelineManager
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using UnityEngine;

public class SidelineManager : MonoBehaviour
{
  public SidelinePlayer[] players;
  private TeamData teamData;
  private UniformSet uniformSet;
  private bool isUserTeam;

  public void Init(TeamData t, UniformSet u, bool isUser)
  {
    this.teamData = t;
    this.uniformSet = u;
    this.isUserTeam = isUser;
  }

  public void SetBaseUniforms()
  {
    UniformConfig uniformConfig = this.uniformSet.GetUniformConfig(this.uniformSet.GetLockedInUniform());
    for (int index = 0; index < this.players.Length; ++index)
      this.players[index].uniManager.SetUniform(this.uniformSet, uniformConfig, this.uniformSet.GetLockedInUniformPiece(UniformPiece.Helmets), this.uniformSet.GetLockedInUniformPiece(UniformPiece.Pants));
  }

  public void SetCustomUniform(int sidelineIndex, int indexOnTeam)
  {
    UniformAssetType type = this.isUserTeam ? UniformAssetType.USER : UniformAssetType.COMP;
    this.players[sidelineIndex].uniManager.SetJersey(this.uniformSet.GetLockedInUniformPiece(UniformPiece.Jerseys), this.teamData.GetPlayer(indexOnTeam).LastName, this.teamData.GetPlayer(indexOnTeam).Number, type);
    this.players[sidelineIndex].uniManager.SetSkinTone(this.teamData.GetPlayer(indexOnTeam).SkinColor);
    UniformConfig uniformConfig = this.uniformSet.GetUniformConfig(this.uniformSet.GetLockedInUniform());
    if (this.teamData.GetPlayer(indexOnTeam).Sleeves == 1)
    {
      this.players[sidelineIndex].uniManager.ShowArmSleeves(uniformConfig.GetArmSleevesColor());
    }
    else
    {
      this.players[sidelineIndex].uniManager.HideArmSleeves();
      if (this.teamData.GetPlayer(indexOnTeam).Bands == 1)
        this.players[sidelineIndex].uniManager.ShowArmBands(uniformConfig.GetArmBandColor());
      else
        this.players[sidelineIndex].uniManager.HideArmBands();
    }
    if (this.teamData.GetPlayer(indexOnTeam).Visor == 1)
      this.players[sidelineIndex].uniManager.ShowHelmetVisor(uniformConfig.GetHelmetVisorColor());
    else
      this.players[sidelineIndex].uniManager.HideHelmetVisor();
  }
}
