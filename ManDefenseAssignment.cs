// Decompiled with JetBrains decompiler
// Type: ManDefenseAssignment
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using System;

[Serializable]
public class ManDefenseAssignment : PlayAssignment
{
  private PlayerAI coverageOn;
  private ManDefenseAssignment.EManCoverTypeLeverage leverage;
  private ManDefenseAssignment.EManCoverTypeTechnique technique;
  private bool IsQBSpy;

  public ManDefenseAssignment(
    RouteGraphicData graphicData,
    float[] r,
    bool shouldSpy = false,
    ManDefenseAssignment.EManCoverTypeLeverage leverageParam = ManDefenseAssignment.EManCoverTypeLeverage.Inside,
    ManDefenseAssignment.EManCoverTypeTechnique techniqueParam = ManDefenseAssignment.EManCoverTypeTechnique.Under)
    : base(EPlayAssignmentId.ManCoverage, graphicData, r)
  {
    this.coverageOn = (PlayerAI) null;
    this.leverage = leverageParam;
    this.technique = techniqueParam;
    this.IsQBSpy = shouldSpy;
  }

  public ManDefenseAssignment(ManDefenseAssignment copyDefAssign)
    : base((PlayAssignment) copyDefAssign)
  {
    this.coverageOn = copyDefAssign.coverageOn;
    this.leverage = copyDefAssign.leverage;
    this.technique = copyDefAssign.technique;
    this.IsQBSpy = copyDefAssign.IsQBSpy;
  }

  public ManDefenseAssignment.EManCoverTypeLeverage GetLeverage() => this.leverage;

  public ManDefenseAssignment.EManCoverTypeTechnique GetTechnique() => this.technique;

  public void SetCoverageOn(PlayerAI coverPlayer) => this.coverageOn = coverPlayer;

  public PlayerAI GetCoverageOn() => this.coverageOn;

  public bool GetIsQBSpy() => this.IsQBSpy;

  public enum EManCoverTypeLeverage
  {
    Inside,
    Outside,
  }

  public enum EManCoverTypeTechnique
  {
    Over,
    Under,
  }
}
