// Decompiled with JetBrains decompiler
// Type: PlayDataOff
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using ProEra.Game;
using System;

[Serializable]
public class PlayDataOff : PlayData
{
  private PlayType playType;
  private PlayTypeSpecific playTypeSpecific;
  private HandoffType handoffType;
  private DropbackType dropbackType;
  private int targetPlayerIndex;
  private int runnerHoleOffset;
  private int playActionTargetIndex;

  public PlayDataOff()
  {
  }

  public PlayDataOff(
    FormationPositions f,
    PlayType pType,
    PlayTypeSpecific pTypeSpecific,
    DropbackType dType,
    HandoffType hType,
    int trgtPlayerIndex,
    string playN,
    PlayConcept concept,
    int rHoleOffsetOrPATarget,
    PlayAssignment p1Route,
    PlayAssignment p2Route,
    PlayAssignment p3Route,
    PlayAssignment p4Route,
    PlayAssignment p5Route,
    PlayAssignment p6Route,
    PlayAssignment p7Route,
    PlayAssignment p8Route,
    PlayAssignment p9Route,
    PlayAssignment p10Route,
    PlayAssignment p11Route)
    : base(f, playN, concept)
  {
    this.playType = pType;
    this.playTypeSpecific = pTypeSpecific;
    this.dropbackType = dType;
    this.handoffType = hType;
    this.targetPlayerIndex = trgtPlayerIndex;
    if (pTypeSpecific == PlayTypeSpecific.PlayAction)
      this.playActionTargetIndex = rHoleOffsetOrPATarget;
    else
      this.runnerHoleOffset = rHoleOffsetOrPATarget;
    this.routes = new PlayAssignment[11]
    {
      p1Route,
      p2Route,
      p3Route,
      p4Route,
      p5Route,
      p6Route,
      p7Route,
      p8Route,
      p9Route,
      p10Route,
      p11Route
    };
  }

  public PlayType GetPlayType() => this.playType;

  public PlayTypeSpecific GetPlayTypeSpecific() => this.playTypeSpecific;

  public HandoffType GetHandoffType() => this.handoffType;

  public DropbackType GetDropbackType()
  {
    if (global::Game.IsPlayAction)
      return DropbackType.OneStep;
    bool flag = false;
    if (!FieldState.IsBallInOpponentTerritory() && Field.GetYardLineByFieldLocation(MatchManager.ballOn) < 10)
      flag = true;
    return flag ? DropbackType.ThreeStep : this.dropbackType;
  }

  public int GetHandoffTarget() => this.playTypeSpecific == PlayTypeSpecific.PlayAction ? this.playActionTargetIndex : this.targetPlayerIndex;

  public int GetPlayTarget() => this.targetPlayerIndex;

  public int GetPlayActionTargetIndex() => this.playActionTargetIndex;

  public int GetRunnerHoleOffset() => this.runnerHoleOffset;

  public int GetPrimaryReceiver() => this.targetPlayerIndex;

  public bool IsUnderCenterPlay()
  {
    BaseFormation baseFormation = this.GetFormation().GetBaseFormation();
    switch (baseFormation)
    {
      case BaseFormation.Pistol:
      case BaseFormation.Hail_Mary:
        return false;
      default:
        return baseFormation != BaseFormation.Shotgun;
    }
  }
}
