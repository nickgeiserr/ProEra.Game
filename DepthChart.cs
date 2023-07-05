// Decompiled with JetBrains decompiler
// Type: DepthChart
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using MessagePack;
using System;
using System.Collections.Generic;
using UnityEngine;

[MessagePackObject(false)]
[Serializable]
public class DepthChart
{
  [IgnoreMember]
  private int[] noFormation;
  [Key(0)]
  public int[] goallinePlayers_Heavy;
  [Key(1)]
  public int[] goallinePlayers_TwinsOver;
  [Key(2)]
  public int[] iFormPlayers_Normal;
  [Key(3)]
  public int[] iFormPlayers_Tight;
  [Key(4)]
  public int[] iFormPlayers_SlotFlex;
  [Key(5)]
  public int[] iFormPlayers_TwinTE;
  [Key(6)]
  public int[] iFormPlayers_Twins;
  [Key(7)]
  public int[] iFormPlayers_YTrips;
  [Key(8)]
  public int[] strongIPlayers_Close;
  [Key(9)]
  public int[] strongIPlayers_Normal;
  [Key(10)]
  public int[] strongIPlayers_Tight;
  [Key(11)]
  public int[] strongIPlayers_TwinTE;
  [Key(12)]
  public int[] strongIPlayers_Twins;
  [Key(13)]
  public int[] strongIPlayers_TwinsFlex;
  [Key(14)]
  public int[] weakIPlayers_CloseTwins;
  [Key(15)]
  public int[] weakIPlayers_TwinsFlex;
  [Key(16)]
  public int[] weakIPlayers_Normal;
  [Key(17)]
  public int[] weakIPlayers_TwinTE;
  [Key(18)]
  public int[] weakIPlayers_Twins;
  [Key(19)]
  public int[] splitBackPlayers;
  [Key(20)]
  public int[] singleBackPlayers_Big;
  [Key(21)]
  public int[] singleBackPlayers_BigTwins;
  [Key(22)]
  public int[] singleBackPlayers_Bunch;
  [Key(23)]
  public int[] singleBackPlayers_Slot;
  [Key(24)]
  public int[] singleBackPlayers_Spread;
  [Key(25)]
  public int[] singleBackPlayers_TreyOpen;
  [Key(26)]
  public int[] singleBackPlayers_Trio;
  [Key(27)]
  public int[] singleBackPlayers_Trio4WR;
  [Key(28)]
  public int[] emptyPlayers_TreyOpen;
  [Key(29)]
  public int[] emptyPlayers_FlexTrips;
  [Key(30)]
  public int[] pistolPlayers_Ace;
  [Key(31)]
  public int[] pistolPlayers_Bunch;
  [Key(32)]
  public int[] pistolPlayers_Slot;
  [Key(33)]
  public int[] pistolPlayers_SpreadFlex;
  [Key(34)]
  public int[] pistolPlayers_TreyOpen;
  [Key(35)]
  public int[] pistolPlayers_Trio;
  [Key(36)]
  public int[] pistolPlayers_YTrips;
  [Key(37)]
  public int[] shotgunPlayers_Normal;
  [Key(38)]
  public int[] shotgunPlayers_NormalDimeDropping;
  [Key(39)]
  public int[] shotgunPlayers_NormalYFlex;
  [Key(40)]
  public int[] shotgunPlayers_QuadsTrio;
  [Key(41)]
  public int[] shotgunPlayers_SlotOffset;
  [Key(42)]
  public int[] shotgunPlayers_SplitOffset;
  [Key(43)]
  public int[] shotgunPlayers_Spread;
  [Key(44)]
  public int[] shotgunPlayers_Tight;
  [Key(45)]
  public int[] shotgunPlayers_Trey;
  [Key(46)]
  public int[] shotgunPlayers_Trips;
  [Key(47)]
  public int[] shotgunPlayers_Spread5WR;
  [Key(48)]
  public int[] shotgunPlayers_Bunch5WR;
  [Key(49)]
  public int[] hailMaryPlayers_Normal;
  [Key(50)]
  public int[] qbKneelPlayers_Normal;
  [Key(51)]
  public int[] fieldGoalPlayers;
  [Key(52)]
  public int[] puntPlayers;
  [Key(53)]
  public int[] fgBlockPlayers;
  [Key(54)]
  public int[] puntRetPlayers;
  [Key(55)]
  public int[] kickoffPlayers;
  [Key(56)]
  public int[] kickRetPlayers;
  [Key(57)]
  public int[] kickRetOnsidePlayers;
  [Key(58)]
  public int[] threeFourPlayers;
  [Key(59)]
  public int[] fourThreePlayers;
  [Key(60)]
  public int[] nickelPlayers;
  [Key(61)]
  public int[] dimePlayers;
  [Key(62)]
  public int[] fourFourPlayers;
  [Key(63)]
  public int[] fiveThreePlayers;
  [Key(64)]
  public int[] sixTwoPlayers;
  [IgnoreMember]
  private List<int[]> allFormations;
  [Key(67)]
  public List<int> playerList = new List<int>();
  [Key(68)]
  public List<int> playerOVR = new List<int>();
  [Key(69)]
  public RosterData MainRoster;
  [Key(70)]
  public RosterData PracticeSquad;
  [Key(71)]
  public Dictionary<string, int> DefaultPlayers;
  [Key(72)]
  public int[] shotgunPlayers_TightWideBack;
  [Key(73)]
  public int[] nickelPlayers_TwoFour;

  [Key(65)]
  public int NumberOfDLUsed { get; private set; }

  [Key(66)]
  public int NumberOfLBUsed { get; private set; }

  public DepthChart()
  {
  }

  public DepthChart(
    RosterData _mainRoster,
    RosterData _practiceSquad,
    string defensivePlaybook,
    Dictionary<string, int> defaultPlayers)
  {
    this.MainRoster = _mainRoster;
    this.PracticeSquad = _practiceSquad;
    this.DefaultPlayers = defaultPlayers;
    this.SetDefaultPlayers(defaultPlayers);
    this.allFormations = new List<int[]>();
    this.SetAllFormationReferences();
    if (defensivePlaybook.ToUpper() == "THREE FOUR")
    {
      this.NumberOfDLUsed = 3;
      this.NumberOfLBUsed = 4;
    }
    else
    {
      this.NumberOfDLUsed = 4;
      this.NumberOfLBUsed = 3;
    }
  }

  public void SetDepthChartMainRoster(RosterData roster) => this.MainRoster = roster;

  public void SetAllFormationReferences()
  {
    this.allFormations.Clear();
    this.allFormations.Add(this.goallinePlayers_Heavy);
    this.allFormations.Add(this.goallinePlayers_TwinsOver);
    this.allFormations.Add(this.iFormPlayers_Normal);
    this.allFormations.Add(this.iFormPlayers_Tight);
    this.allFormations.Add(this.iFormPlayers_SlotFlex);
    this.allFormations.Add(this.iFormPlayers_TwinTE);
    this.allFormations.Add(this.iFormPlayers_Twins);
    this.allFormations.Add(this.iFormPlayers_YTrips);
    this.allFormations.Add(this.strongIPlayers_Close);
    this.allFormations.Add(this.strongIPlayers_Normal);
    this.allFormations.Add(this.strongIPlayers_Tight);
    this.allFormations.Add(this.strongIPlayers_TwinTE);
    this.allFormations.Add(this.strongIPlayers_Twins);
    this.allFormations.Add(this.strongIPlayers_TwinsFlex);
    this.allFormations.Add(this.weakIPlayers_CloseTwins);
    this.allFormations.Add(this.weakIPlayers_TwinsFlex);
    this.allFormations.Add(this.weakIPlayers_Normal);
    this.allFormations.Add(this.weakIPlayers_TwinTE);
    this.allFormations.Add(this.weakIPlayers_Twins);
    this.allFormations.Add(this.splitBackPlayers);
    this.allFormations.Add(this.singleBackPlayers_Big);
    this.allFormations.Add(this.singleBackPlayers_BigTwins);
    this.allFormations.Add(this.singleBackPlayers_Bunch);
    this.allFormations.Add(this.singleBackPlayers_Slot);
    this.allFormations.Add(this.singleBackPlayers_Spread);
    this.allFormations.Add(this.singleBackPlayers_TreyOpen);
    this.allFormations.Add(this.singleBackPlayers_Trio);
    this.allFormations.Add(this.singleBackPlayers_Trio4WR);
    this.allFormations.Add(this.emptyPlayers_TreyOpen);
    this.allFormations.Add(this.emptyPlayers_FlexTrips);
    this.allFormations.Add(this.pistolPlayers_Ace);
    this.allFormations.Add(this.pistolPlayers_Bunch);
    this.allFormations.Add(this.pistolPlayers_Slot);
    this.allFormations.Add(this.pistolPlayers_SpreadFlex);
    this.allFormations.Add(this.pistolPlayers_TreyOpen);
    this.allFormations.Add(this.pistolPlayers_Trio);
    this.allFormations.Add(this.pistolPlayers_YTrips);
    this.allFormations.Add(this.shotgunPlayers_Normal);
    this.allFormations.Add(this.shotgunPlayers_NormalYFlex);
    this.allFormations.Add(this.shotgunPlayers_QuadsTrio);
    this.allFormations.Add(this.shotgunPlayers_SlotOffset);
    this.allFormations.Add(this.shotgunPlayers_SplitOffset);
    this.allFormations.Add(this.shotgunPlayers_Spread);
    this.allFormations.Add(this.shotgunPlayers_Tight);
    this.allFormations.Add(this.shotgunPlayers_Trey);
    this.allFormations.Add(this.shotgunPlayers_Trips);
    this.allFormations.Add(this.shotgunPlayers_Spread5WR);
    this.allFormations.Add(this.shotgunPlayers_Bunch5WR);
    this.allFormations.Add(this.shotgunPlayers_NormalDimeDropping);
    this.allFormations.Add(this.hailMaryPlayers_Normal);
    this.allFormations.Add(this.qbKneelPlayers_Normal);
    this.allFormations.Add(this.fieldGoalPlayers);
    this.allFormations.Add(this.puntPlayers);
    this.allFormations.Add(this.fgBlockPlayers);
    this.allFormations.Add(this.puntRetPlayers);
    this.allFormations.Add(this.kickoffPlayers);
    this.allFormations.Add(this.kickRetPlayers);
    this.allFormations.Add(this.kickRetOnsidePlayers);
    this.allFormations.Add(this.threeFourPlayers);
    this.allFormations.Add(this.fourThreePlayers);
    this.allFormations.Add(this.nickelPlayers);
    this.allFormations.Add(this.dimePlayers);
    this.allFormations.Add(this.fourFourPlayers);
    this.allFormations.Add(this.fiveThreePlayers);
    this.allFormations.Add(this.sixTwoPlayers);
    this.allFormations.Add(this.shotgunPlayers_TightWideBack);
    this.allFormations.Add(this.nickelPlayers_TwoFour);
  }

  public List<int[]> GetPlaybookFormations() => this.allFormations;

  public PlayerData GetStartingQB() => this.MainRoster.GetPlayer(this.singleBackPlayers_Slot[5]);

  public PlayerData GetStartingWRX() => this.MainRoster.GetPlayer(this.singleBackPlayers_Slot[8]);

  public PlayerData GetStartingWRZ() => this.MainRoster.GetPlayer(this.singleBackPlayers_Slot[9]);

  public PlayerData GetStartingWRY() => this.MainRoster.GetPlayer(this.singleBackPlayers_Slot[10]);

  public PlayerData GetStartingWRA() => this.MainRoster.GetPlayer(this.shotgunPlayers_Spread5WR[7]);

  public PlayerData GetStartingWRB() => this.MainRoster.GetPlayer(this.shotgunPlayers_Spread5WR[6]);

  public PlayerData GetStartingTE() => this.MainRoster.GetPlayer(this.singleBackPlayers_Slot[7]);

  public PlayerData GetStartingTE2() => this.MainRoster.GetPlayer(this.iFormPlayers_Tight[8]);

  public PlayerData GetStartingRB() => this.MainRoster.GetPlayer(this.singleBackPlayers_Slot[6]);

  public PlayerData GetStartingFB() => this.MainRoster.GetPlayer(this.iFormPlayers_Normal[10]);

  public PlayerData GetStartingLT() => this.MainRoster.GetPlayer(this.singleBackPlayers_Slot[0]);

  public PlayerData GetStartingLG() => this.MainRoster.GetPlayer(this.singleBackPlayers_Slot[1]);

  public PlayerData GetStartingC() => this.MainRoster.GetPlayer(this.singleBackPlayers_Slot[2]);

  public PlayerData GetStartingRG() => this.MainRoster.GetPlayer(this.singleBackPlayers_Slot[3]);

  public PlayerData GetStartingRT() => this.MainRoster.GetPlayer(this.singleBackPlayers_Slot[4]);

  public PlayerData GetStartingLDE_43() => this.MainRoster.GetPlayer(this.fourThreePlayers[0]);

  public PlayerData GetStartingLDT() => this.MainRoster.GetPlayer(this.fourThreePlayers[1]);

  public PlayerData GetStartingRDT() => this.MainRoster.GetPlayer(this.fourThreePlayers[2]);

  public PlayerData GetStartingRDE_43() => this.MainRoster.GetPlayer(this.fourThreePlayers[3]);

  public PlayerData GetStartingLDE_34() => this.MainRoster.GetPlayer(this.threeFourPlayers[0]);

  public PlayerData GetStartingNT() => this.MainRoster.GetPlayer(this.threeFourPlayers[1]);

  public PlayerData GetStartingRDE_34() => this.MainRoster.GetPlayer(this.threeFourPlayers[2]);

  public PlayerData GetStartingLOLB() => this.MainRoster.GetPlayer(this.threeFourPlayers[3]);

  public PlayerData GetStartingLILB() => this.MainRoster.GetPlayer(this.threeFourPlayers[4]);

  public PlayerData GetStartingRILB() => this.MainRoster.GetPlayer(this.threeFourPlayers[5]);

  public PlayerData GetStartingROLB() => this.MainRoster.GetPlayer(this.threeFourPlayers[6]);

  public PlayerData GetStartingWLB() => this.MainRoster.GetPlayer(this.fourThreePlayers[4]);

  public PlayerData GetStartingMLB() => this.MainRoster.GetPlayer(this.fourThreePlayers[5]);

  public PlayerData GetStartingSLB() => this.MainRoster.GetPlayer(this.fourThreePlayers[6]);

  public PlayerData GetStartingLCB() => this.MainRoster.GetPlayer(this.threeFourPlayers[7]);

  public PlayerData GetStartingSS() => this.MainRoster.GetPlayer(this.threeFourPlayers[8]);

  public PlayerData GetStartingFS() => this.MainRoster.GetPlayer(this.threeFourPlayers[9]);

  public PlayerData GetStartingRCB() => this.MainRoster.GetPlayer(this.threeFourPlayers[10]);

  public PlayerData GetStartingNickelBack() => this.MainRoster.GetPlayer(this.nickelPlayers[6]);

  public PlayerData GetStartingDimeBack() => this.MainRoster.GetPlayer(this.dimePlayers[5]);

  public PlayerData GetStartingKicker() => this.MainRoster.GetPlayer(this.fieldGoalPlayers[6]);

  public PlayerData GetStartingPunter() => this.MainRoster.GetPlayer(this.puntPlayers[6]);

  public PlayerData GetStartingKickReturner() => this.MainRoster.GetPlayer(this.kickRetPlayers[9]);

  public PlayerData GetStartingPuntReturner() => this.MainRoster.GetPlayer(this.puntRetPlayers[9]);

  public int GetStartingQBIndex() => this.singleBackPlayers_Slot[5];

  public int GetStartingWRXIndex() => this.singleBackPlayers_Slot[8];

  public int GetStartingWRZIndex() => this.singleBackPlayers_Slot[9];

  public int GetStartingWRYIndex() => this.singleBackPlayers_Slot[10];

  public int GetStartingWRAIndex() => this.shotgunPlayers_Spread5WR[7];

  public int GetStartingWRBIndex() => this.shotgunPlayers_Spread5WR[6];

  public int GetStartingTEIndex() => this.singleBackPlayers_Slot[7];

  public int GetStartingTE2Index() => this.iFormPlayers_Tight[8];

  public int GetStartingRBIndex() => this.singleBackPlayers_Slot[6];

  public int GetStartingFBIndex() => this.iFormPlayers_Normal[10];

  public int GetStartingLTIndex() => this.singleBackPlayers_Slot[0];

  public int GetStartingLGIndex() => this.singleBackPlayers_Slot[1];

  public int GetStartingCIndex() => this.singleBackPlayers_Slot[2];

  public int GetStartingRGIndex() => this.singleBackPlayers_Slot[3];

  public int GetStartingRTIndex() => this.singleBackPlayers_Slot[4];

  public int GetStartingLDEIndex_43() => this.fourThreePlayers[0];

  public int GetStartingLDTIndex() => this.fourThreePlayers[1];

  public int GetStartingRDTIndex() => this.fourThreePlayers[2];

  public int GetStartingRDEIndex_43() => this.fourThreePlayers[3];

  public int GetStartingLDEIndex_34() => this.threeFourPlayers[0];

  public int GetStartingNTIndex() => this.threeFourPlayers[1];

  public int GetStartingRDEIndex_34() => this.threeFourPlayers[2];

  public int GetStartingLOLBIndex() => this.threeFourPlayers[3];

  public int GetStartingLILBIndex() => this.threeFourPlayers[4];

  public int GetStartingRILBIndex() => this.threeFourPlayers[5];

  public int GetStartingROLBIndex() => this.threeFourPlayers[6];

  public int GetStartingWLBIndex() => this.fourThreePlayers[4];

  public int GetStartingMLBIndex() => this.fourThreePlayers[5];

  public int GetStartingSLBIndex() => this.fourThreePlayers[6];

  public int GetStartingLCBIndex() => this.threeFourPlayers[7];

  public int GetStartingSSIndex() => this.threeFourPlayers[8];

  public int GetStartingFSIndex() => this.threeFourPlayers[9];

  public int GetStartingRCBIndex() => this.threeFourPlayers[10];

  public int GetStartingNickelBackIndex() => this.nickelPlayers[6];

  public int GetStartingDimeBackIndex() => this.dimePlayers[5];

  public int GetStartingKickerIndex() => this.fieldGoalPlayers[6];

  public int GetStartingPunterIndex() => this.puntPlayers[6];

  public int GetStartingKickReturnerIndex() => this.kickRetPlayers[9];

  public int GetStartingPuntReturnerIndex() => this.puntRetPlayers[9];

  private ref int[] GetRefToFormation(FormationPositions formation)
  {
    BaseFormation baseFormation = formation.GetBaseFormation();
    SubFormation subFormation = formation.GetSubFormation();
    switch (baseFormation)
    {
      case BaseFormation.I_Form:
        switch (subFormation)
        {
          case SubFormation.Normal:
            return ref this.iFormPlayers_Normal;
          case SubFormation.Slot_Flex:
            return ref this.iFormPlayers_SlotFlex;
          case SubFormation.Tight:
            return ref this.iFormPlayers_Tight;
          case SubFormation.Twin_TE:
            return ref this.iFormPlayers_TwinTE;
          case SubFormation.Twins:
            return ref this.iFormPlayers_Twins;
          case SubFormation.Y_Trips:
            return ref this.iFormPlayers_YTrips;
        }
        break;
      case BaseFormation.Split_Back:
        if (subFormation == SubFormation.Normal)
          return ref this.splitBackPlayers;
        break;
      case BaseFormation.Single_Back:
        switch (subFormation)
        {
          case SubFormation.Trey_Open:
            return ref this.singleBackPlayers_TreyOpen;
          case SubFormation.Bunch:
            return ref this.singleBackPlayers_Bunch;
          case SubFormation.Slot:
            return ref this.singleBackPlayers_Slot;
          case SubFormation.Trio:
            return ref this.singleBackPlayers_Trio;
          case SubFormation.Big:
            return ref this.singleBackPlayers_Big;
          case SubFormation.Spread:
            return ref this.singleBackPlayers_Spread;
          case SubFormation.Big_Twins:
            return ref this.singleBackPlayers_BigTwins;
          case SubFormation.Trio_4WR:
            return ref this.singleBackPlayers_Trio4WR;
        }
        break;
      case BaseFormation.Shotgun:
        switch (subFormation)
        {
          case SubFormation.Normal:
            return ref this.shotgunPlayers_Normal;
          case SubFormation.Tight:
            return ref this.shotgunPlayers_Tight;
          case SubFormation.Trips:
            return ref this.shotgunPlayers_Trips;
          case SubFormation.Spread_5WR:
            return ref this.shotgunPlayers_Spread5WR;
          case SubFormation.Bunch_5WR:
            return ref this.shotgunPlayers_Bunch5WR;
          case SubFormation.Normal_Y_Flex:
            return ref this.shotgunPlayers_NormalYFlex;
          case SubFormation.Quads_Trio:
            return ref this.shotgunPlayers_QuadsTrio;
          case SubFormation.Slot_Offset:
            return ref this.shotgunPlayers_SlotOffset;
          case SubFormation.Split_Offset:
            return ref this.shotgunPlayers_SplitOffset;
          case SubFormation.Trey:
            return ref this.shotgunPlayers_Trey;
          case SubFormation.NormalDimeDropping:
            return ref this.shotgunPlayers_NormalDimeDropping;
          case SubFormation.TightWideBack:
            return ref this.shotgunPlayers_TightWideBack;
          case SubFormation.Spread:
            return ref this.shotgunPlayers_Spread;
        }
        break;
      case BaseFormation.Pistol:
        switch (subFormation)
        {
          case SubFormation.Y_Trips:
            return ref this.pistolPlayers_YTrips;
          case SubFormation.Trey_Open:
            return ref this.pistolPlayers_TreyOpen;
          case SubFormation.Ace:
            return ref this.pistolPlayers_Ace;
          case SubFormation.Bunch:
            return ref this.pistolPlayers_Bunch;
          case SubFormation.Slot:
            return ref this.pistolPlayers_Slot;
          case SubFormation.Spread_Flex:
            return ref this.pistolPlayers_SpreadFlex;
          case SubFormation.Trio:
            return ref this.pistolPlayers_Trio;
        }
        break;
      case BaseFormation.Empty:
        if (subFormation == SubFormation.Trey_Open)
          return ref this.emptyPlayers_TreyOpen;
        if (subFormation == SubFormation.Flex_Trips)
          return ref this.emptyPlayers_FlexTrips;
        break;
      case BaseFormation.Goalline:
        if (subFormation == SubFormation.Heavy)
          return ref this.goallinePlayers_Heavy;
        break;
      case BaseFormation.Hail_Mary:
        if (subFormation == SubFormation.Normal)
          return ref this.hailMaryPlayers_Normal;
        break;
      case BaseFormation.Strong_I:
        switch (subFormation)
        {
          case SubFormation.Normal:
            return ref this.strongIPlayers_Normal;
          case SubFormation.Tight:
            return ref this.strongIPlayers_Tight;
          case SubFormation.Twin_TE:
            return ref this.strongIPlayers_TwinTE;
          case SubFormation.Twins:
            return ref this.strongIPlayers_Twins;
          case SubFormation.Close:
            return ref this.strongIPlayers_Close;
          case SubFormation.Twins_Flex:
            return ref this.strongIPlayers_TwinsFlex;
        }
        break;
      case BaseFormation.Weak_I:
        switch (subFormation)
        {
          case SubFormation.Normal:
            return ref this.weakIPlayers_Normal;
          case SubFormation.Twin_TE:
            return ref this.weakIPlayers_TwinTE;
          case SubFormation.Twins:
            return ref this.weakIPlayers_Twins;
          case SubFormation.Twins_Flex:
            return ref this.weakIPlayers_TwinsFlex;
          case SubFormation.Close_Twins:
            return ref this.weakIPlayers_CloseTwins;
        }
        break;
      case BaseFormation.QB_Kneel:
        if (subFormation == SubFormation.Normal)
          return ref this.qbKneelPlayers_Normal;
        break;
      case BaseFormation.Special_Teams:
        switch (subFormation)
        {
          case SubFormation.Field_Goal:
            return ref this.fieldGoalPlayers;
          case SubFormation.Punt:
            return ref this.puntPlayers;
          case SubFormation.Kickoff:
            return ref this.kickoffPlayers;
          case SubFormation.Onside_Kick:
            return ref this.kickoffPlayers;
          case SubFormation.Field_Goal_Block:
            return ref this.fgBlockPlayers;
          case SubFormation.Punt_Return:
            return ref this.puntRetPlayers;
          case SubFormation.Kick_Return:
            return ref this.kickRetPlayers;
          case SubFormation.Onside_Kick_Return:
            return ref this.kickRetOnsidePlayers;
        }
        break;
      case BaseFormation.Six_Two:
        return ref this.sixTwoPlayers;
      case BaseFormation.Five_Three:
        return ref this.fiveThreePlayers;
      case BaseFormation.Four_Four:
        return ref this.fourFourPlayers;
      case BaseFormation.Four_Three:
        return ref this.fourThreePlayers;
      case BaseFormation.Three_Four:
        return ref this.threeFourPlayers;
      case BaseFormation.Nickel:
        return ref (subFormation == SubFormation.Two_Four ? ref this.nickelPlayers_TwoFour : ref this.nickelPlayers);
      case BaseFormation.Dime:
        return ref this.dimePlayers;
    }
    return ref this.noFormation;
  }

  public int[] GetPlayersInFormation(FormationPositions formation)
  {
    ref int[] local1 = ref this.GetRefToFormation(formation);
    if (local1 == null && this.DefaultPlayers != null)
    {
      DepthChart depthChart = new DepthChart();
      depthChart.SetDefaultPlayers(this.DefaultPlayers);
      ref int[] local2 = ref depthChart.GetRefToFormation(formation);
      local1 = (int[]) local2.Clone();
    }
    if (local1 == null)
      Debug.Log((object) ("Unable to retrieve list of players in this formation. DepthChart.GetPlayersInFormation (FormationPosition formation). Formation: " + formation.GetBaseFormationString() + " " + formation.GetSubFormationString()));
    return local1;
  }

  public bool IsPlayerAStarter(int playerIndex)
  {
    if (playerIndex == this.GetStartingQBIndex() || playerIndex == this.GetStartingRBIndex() || playerIndex == this.GetStartingWRXIndex() || playerIndex == this.GetStartingWRYIndex() || playerIndex == this.GetStartingWRZIndex() || playerIndex == this.GetStartingTEIndex() || playerIndex == this.GetStartingLTIndex() || playerIndex == this.GetStartingLGIndex() || playerIndex == this.GetStartingCIndex() || playerIndex == this.GetStartingRGIndex() || playerIndex == this.GetStartingRTIndex())
      return true;
    if (this.NumberOfDLUsed == 3)
    {
      if (playerIndex == this.GetStartingLDEIndex_34() || playerIndex == this.GetStartingNTIndex() || playerIndex == this.GetStartingLDEIndex_34())
        return true;
    }
    else if (playerIndex == this.GetStartingLDEIndex_43() || playerIndex == this.GetStartingLDTIndex() || playerIndex == this.GetStartingRDTIndex() || playerIndex == this.GetStartingLDEIndex_43())
      return true;
    if (this.NumberOfLBUsed == 3)
    {
      if (playerIndex == this.GetStartingWLBIndex() || playerIndex == this.GetStartingMLBIndex() || playerIndex == this.GetStartingSLBIndex())
        return true;
    }
    else if (playerIndex == this.GetStartingLOLBIndex() || playerIndex == this.GetStartingLILBIndex() || playerIndex == this.GetStartingRILBIndex() || playerIndex == this.GetStartingROLBIndex())
      return true;
    return playerIndex == this.GetStartingLCBIndex() || playerIndex == this.GetStartingSSIndex() || playerIndex == this.GetStartingFSIndex() || playerIndex == this.GetStartingRCBIndex() || playerIndex == this.GetStartingKickerIndex() || playerIndex == this.GetStartingPunterIndex();
  }

  public void SetDefaultPlayers(Dictionary<string, int> defaultPlayers)
  {
    int defaultPlayer1 = defaultPlayers["QB"];
    int defaultPlayer2 = defaultPlayers["RB1"];
    int defaultPlayer3 = defaultPlayers["FB"];
    int defaultPlayer4 = defaultPlayers["WRX"];
    int defaultPlayer5 = defaultPlayers["WRZ"];
    int defaultPlayer6 = defaultPlayers["WRY"];
    int defaultPlayer7 = defaultPlayers["WRA"];
    int defaultPlayer8 = defaultPlayers["WRB"];
    int defaultPlayer9 = defaultPlayers["TE1"];
    int defaultPlayer10 = defaultPlayers["TE2"];
    int defaultPlayer11 = defaultPlayers["LT"];
    int defaultPlayer12 = defaultPlayers["LG"];
    int defaultPlayer13 = defaultPlayers["C"];
    int defaultPlayer14 = defaultPlayers["RG"];
    int defaultPlayer15 = defaultPlayers["RT"];
    int defaultPlayer16 = defaultPlayers["DL1"];
    int defaultPlayer17 = defaultPlayers["DL2"];
    int defaultPlayer18 = defaultPlayers["DL3"];
    int defaultPlayer19 = defaultPlayers["DL4"];
    int defaultPlayer20 = defaultPlayers["LB1"];
    int defaultPlayer21 = defaultPlayers["LB2"];
    int defaultPlayer22 = defaultPlayers["LB3"];
    int defaultPlayer23 = defaultPlayers["LB4"];
    int defaultPlayer24 = defaultPlayers["LCB"];
    int defaultPlayer25 = defaultPlayers["SS"];
    int defaultPlayer26 = defaultPlayers["FS"];
    int defaultPlayer27 = defaultPlayers["RCB"];
    int defaultPlayer28 = defaultPlayers["NB"];
    int defaultPlayer29 = defaultPlayers["DB"];
    int defaultPlayer30 = defaultPlayers["KCK"];
    int defaultPlayer31 = defaultPlayers["PNT"];
    int defaultPlayer32 = defaultPlayers["HOL"];
    int defaultPlayer33 = defaultPlayers["RET"];
    int defaultPlayer34 = defaultPlayers["LS"];
    this.goallinePlayers_Heavy = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer10,
      defaultPlayer3
    };
    this.goallinePlayers_TwinsOver = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer10,
      defaultPlayer5,
      defaultPlayer3
    };
    this.iFormPlayers_Normal = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer3
    };
    this.iFormPlayers_Tight = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer10,
      defaultPlayer5,
      defaultPlayer3
    };
    this.iFormPlayers_SlotFlex = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer6,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer3
    };
    this.iFormPlayers_TwinTE = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer10,
      defaultPlayer3
    };
    this.iFormPlayers_Twins = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer6,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer3
    };
    this.iFormPlayers_YTrips = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer6,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer3
    };
    this.strongIPlayers_Close = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer3
    };
    this.strongIPlayers_Normal = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer3
    };
    this.strongIPlayers_Tight = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer10,
      defaultPlayer5,
      defaultPlayer3
    };
    this.strongIPlayers_TwinTE = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer10,
      defaultPlayer3
    };
    this.strongIPlayers_Twins = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer3
    };
    this.strongIPlayers_TwinsFlex = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer3
    };
    this.weakIPlayers_CloseTwins = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer3
    };
    this.weakIPlayers_TwinsFlex = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer3
    };
    this.weakIPlayers_Normal = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer3
    };
    this.weakIPlayers_TwinTE = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer10,
      defaultPlayer3
    };
    this.weakIPlayers_Twins = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer3
    };
    this.splitBackPlayers = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer3
    };
    this.singleBackPlayers_Big = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer10
    };
    this.singleBackPlayers_BigTwins = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer10
    };
    this.singleBackPlayers_Bunch = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.singleBackPlayers_Slot = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.singleBackPlayers_Spread = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer7,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.singleBackPlayers_TreyOpen = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.singleBackPlayers_Trio = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.singleBackPlayers_Trio4WR = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer7,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.emptyPlayers_TreyOpen = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer9,
      defaultPlayer7,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.emptyPlayers_FlexTrips = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer8,
      defaultPlayer7,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.pistolPlayers_Ace = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer10
    };
    this.pistolPlayers_Bunch = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.pistolPlayers_Slot = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.pistolPlayers_SpreadFlex = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer7,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.pistolPlayers_TreyOpen = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.pistolPlayers_Trio = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.pistolPlayers_YTrips = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.shotgunPlayers_Normal = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.shotgunPlayers_Trips = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer6,
      defaultPlayer7,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer2
    };
    this.shotgunPlayers_Spread5WR = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer8,
      defaultPlayer7,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.shotgunPlayers_NormalYFlex = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.shotgunPlayers_QuadsTrio = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer9,
      defaultPlayer7,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.shotgunPlayers_SlotOffset = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer3,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.shotgunPlayers_SplitOffset = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer3,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.shotgunPlayers_Spread = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer7,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.shotgunPlayers_Tight = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.shotgunPlayers_TightWideBack = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.shotgunPlayers_Trey = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.shotgunPlayers_Bunch5WR = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer8,
      defaultPlayer7,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.shotgunPlayers_NormalDimeDropping = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.hailMaryPlayers_Normal = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer6
    };
    this.qbKneelPlayers_Normal = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer1,
      defaultPlayer2,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer10
    };
    this.fieldGoalPlayers = new int[11]
    {
      defaultPlayer11,
      defaultPlayer12,
      defaultPlayer13,
      defaultPlayer14,
      defaultPlayer15,
      defaultPlayer31,
      defaultPlayer30,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer3
    };
    this.puntPlayers = new int[11]
    {
      defaultPlayer22,
      defaultPlayer23,
      defaultPlayer34,
      defaultPlayer28,
      defaultPlayer29,
      defaultPlayer2,
      defaultPlayer31,
      defaultPlayer9,
      defaultPlayer4,
      defaultPlayer5,
      defaultPlayer3
    };
    this.kickRetPlayers = new int[11]
    {
      defaultPlayer10,
      defaultPlayer29,
      defaultPlayer7,
      defaultPlayer8,
      defaultPlayer28,
      defaultPlayer9,
      defaultPlayer24,
      defaultPlayer3,
      defaultPlayer5,
      defaultPlayer33,
      defaultPlayer6
    };
    this.fourThreePlayers = new int[11]
    {
      defaultPlayer16,
      defaultPlayer17,
      defaultPlayer18,
      defaultPlayer19,
      defaultPlayer20,
      defaultPlayer21,
      defaultPlayer22,
      defaultPlayer24,
      defaultPlayer25,
      defaultPlayer26,
      defaultPlayer27
    };
    this.threeFourPlayers = new int[11]
    {
      defaultPlayer16,
      defaultPlayer17,
      defaultPlayer18,
      defaultPlayer20,
      defaultPlayer21,
      defaultPlayer22,
      defaultPlayer23,
      defaultPlayer24,
      defaultPlayer25,
      defaultPlayer26,
      defaultPlayer27
    };
    this.nickelPlayers = new int[11]
    {
      defaultPlayer16,
      defaultPlayer17,
      defaultPlayer18,
      defaultPlayer19,
      defaultPlayer20,
      defaultPlayer21,
      defaultPlayer28,
      defaultPlayer24,
      defaultPlayer25,
      defaultPlayer26,
      defaultPlayer27
    };
    this.nickelPlayers_TwoFour = new int[11]
    {
      defaultPlayer20,
      defaultPlayer16,
      defaultPlayer17,
      defaultPlayer23,
      defaultPlayer21,
      defaultPlayer22,
      defaultPlayer28,
      defaultPlayer24,
      defaultPlayer25,
      defaultPlayer26,
      defaultPlayer27
    };
    this.dimePlayers = new int[11]
    {
      defaultPlayer16,
      defaultPlayer17,
      defaultPlayer18,
      defaultPlayer19,
      defaultPlayer21,
      defaultPlayer29,
      defaultPlayer28,
      defaultPlayer24,
      defaultPlayer25,
      defaultPlayer26,
      defaultPlayer27
    };
    this.fourFourPlayers = new int[11]
    {
      defaultPlayer16,
      defaultPlayer17,
      defaultPlayer18,
      defaultPlayer19,
      defaultPlayer20,
      defaultPlayer21,
      defaultPlayer22,
      defaultPlayer23,
      defaultPlayer24,
      defaultPlayer25,
      defaultPlayer27
    };
    this.fiveThreePlayers = new int[11]
    {
      defaultPlayer16,
      defaultPlayer17,
      defaultPlayer18,
      defaultPlayer19,
      defaultPlayer23,
      defaultPlayer20,
      defaultPlayer21,
      defaultPlayer22,
      defaultPlayer24,
      defaultPlayer25,
      defaultPlayer27
    };
    this.sixTwoPlayers = new int[11]
    {
      defaultPlayer20,
      defaultPlayer16,
      defaultPlayer17,
      defaultPlayer18,
      defaultPlayer19,
      defaultPlayer23,
      defaultPlayer21,
      defaultPlayer22,
      defaultPlayer24,
      defaultPlayer25,
      defaultPlayer27
    };
    this.fgBlockPlayers = new int[11]
    {
      defaultPlayer20,
      defaultPlayer16,
      defaultPlayer17,
      defaultPlayer18,
      defaultPlayer19,
      defaultPlayer23,
      defaultPlayer21,
      defaultPlayer22,
      defaultPlayer24,
      defaultPlayer25,
      defaultPlayer27
    };
    this.puntRetPlayers = new int[11]
    {
      defaultPlayer29,
      defaultPlayer28,
      defaultPlayer7,
      defaultPlayer8,
      defaultPlayer10,
      defaultPlayer23,
      defaultPlayer21,
      defaultPlayer22,
      defaultPlayer24,
      defaultPlayer33,
      defaultPlayer27
    };
    this.kickoffPlayers = new int[11]
    {
      defaultPlayer29,
      defaultPlayer28,
      defaultPlayer7,
      defaultPlayer21,
      defaultPlayer10,
      defaultPlayer23,
      defaultPlayer30,
      defaultPlayer22,
      defaultPlayer25,
      defaultPlayer26,
      defaultPlayer8
    };
    this.kickRetOnsidePlayers = new int[11]
    {
      defaultPlayer29,
      defaultPlayer28,
      defaultPlayer7,
      defaultPlayer8,
      defaultPlayer24,
      defaultPlayer9,
      defaultPlayer30,
      defaultPlayer10,
      defaultPlayer25,
      defaultPlayer26,
      defaultPlayer27
    };
  }

  public void SubstitutePlayers(
    int outPlayer,
    int inPlayer,
    bool takePlayerOutOfReturnPositions = false,
    int[] subtituteInThisFormationOnly = null)
  {
    if (this.allFormations == null)
      return;
    if (subtituteInThisFormationOnly == null)
    {
      for (int index1 = 0; index1 < this.allFormations.Count; ++index1)
      {
        int[] allFormation = this.allFormations[index1];
        for (int index2 = 0; index2 < allFormation.Length; ++index2)
        {
          if (takePlayerOutOfReturnPositions || (allFormation != this.kickRetPlayers || index2 != 9) && (allFormation != this.puntRetPlayers || index2 != 9))
          {
            if (allFormation[index2] == outPlayer)
              allFormation[index2] = inPlayer;
            else if (allFormation[index2] == inPlayer)
              allFormation[index2] = outPlayer;
          }
        }
      }
    }
    else
    {
      for (int index = 0; index < subtituteInThisFormationOnly.Length; ++index)
      {
        if (subtituteInThisFormationOnly[index] == outPlayer)
          subtituteInThisFormationOnly[index] = inPlayer;
        else if (subtituteInThisFormationOnly[index] == inPlayer)
          subtituteInThisFormationOnly[index] = outPlayer;
      }
    }
  }

  public void SubInNextPlayerOnDepthChartForThisStarter(int playerIndexToSubOut)
  {
    Position startingPositionGeneric = this.GetStartingPosition_Generic(playerIndexToSubOut);
    int num = playerIndexToSubOut == this.GetStartingKickReturnerIndex() ? 1 : 0;
    bool flag1 = playerIndexToSubOut == this.GetStartingPuntReturnerIndex();
    if (num != 0)
      this.SetBestStartersFor_KR(Position.KR);
    if (flag1)
      this.SetBestStartersFor_PR(Position.PR);
    if (startingPositionGeneric == Position.None)
      return;
    this.playerList.Clear();
    this.playerOVR.Clear();
    this.OrderPlayersByOverall(startingPositionGeneric);
    bool flag2 = false;
    for (int index = 0; index < this.playerList.Count; ++index)
    {
      if (this.GetStartingPosition_Generic(this.playerList[index]) == Position.None && this.MainRoster.GetPlayer(this.playerList[index]).CurrentInjury == null && this.MainRoster.GetPlayer(playerIndexToSubOut).PlayerPosition == this.MainRoster.GetPlayer(this.playerList[index]).PlayerPosition)
      {
        this.SubstitutePlayers(playerIndexToSubOut, this.playerList[index]);
        flag2 = true;
        break;
      }
    }
    if (flag2)
      return;
    for (int index = 0; index < this.playerList.Count; ++index)
    {
      if (this.GetStartingPosition_Generic(this.playerList[index]) == Position.None && this.MainRoster.GetPlayer(this.playerList[index]).CurrentInjury == null)
      {
        this.SubstitutePlayers(playerIndexToSubOut, this.playerList[index]);
        break;
      }
    }
  }

  private void OrderPlayersByOverall(Position position)
  {
    for (int playerIndex = 0; playerIndex < TeamAssetManager.NUMBER_OF_PLAYERS_ON_ROSTER; ++playerIndex)
    {
      if (this.MainRoster.GetPlayer(playerIndex) != null)
      {
        this.playerList.Add(playerIndex);
        this.playerOVR.Add(this.MainRoster.GetOverall(playerIndex, position));
      }
    }
    for (int index1 = 0; index1 < this.playerList.Count - 1; ++index1)
    {
      int index2 = index1;
      for (int index3 = index1 + 1; index3 < this.playerList.Count; ++index3)
      {
        if (this.playerOVR[index3] > this.playerOVR[index2])
          index2 = index3;
      }
      int num = this.playerOVR[index1];
      this.playerOVR[index1] = this.playerOVR[index2];
      this.playerOVR[index2] = num;
      int player = this.playerList[index1];
      this.playerList[index1] = this.playerList[index2];
      this.playerList[index2] = player;
    }
  }

  public int GetBestBenchPlayerAtPosition(Position position)
  {
    this.playerList.Clear();
    this.playerOVR.Clear();
    this.OrderPlayersByOverall(position);
    for (int index = 0; index < this.playerList.Count; ++index)
    {
      if (this.GetStartingPosition_Generic(this.playerList[index]) == Position.None && this.MainRoster.GetPlayer(this.playerList[index]).CurrentInjury == null && position == this.MainRoster.GetPlayer(this.playerList[index]).PlayerPosition)
        return this.playerList[index];
    }
    for (int index = 0; index < this.playerList.Count; ++index)
    {
      if (this.GetStartingPosition_Generic(this.playerList[index]) == Position.None && this.MainRoster.GetPlayer(this.playerList[index]).CurrentInjury == null)
        return this.playerList[index];
    }
    Debug.Log((object) ("No available substitutes for position: " + position.ToString()));
    return 0;
  }

  public void SetBestStartersForAllPositions()
  {
    this.SetBestStartersFor_QB(Position.QB);
    this.SetBestStartersFor_RB(Position.RB);
    this.SetBestStartersFor_WR(Position.WR);
    this.SetBestStartersFor_TE(Position.TE);
    this.SetBestStartersFor_OL(Position.OL);
    this.SetBestStartersFor_DL(Position.DL);
    this.SetBestStartersFor_LB(Position.LB);
    this.SetBestStartersFor_DB(Position.DB);
    this.SetBestStartersFor_K(Position.K);
    this.SetBestStartersFor_P(Position.P);
    this.SetBestStartersFor_KR(Position.KR);
    this.SetBestStartersFor_PR(Position.PR);
  }

  public void SetBestStartersForPosition(Position p)
  {
    switch (p)
    {
      case Position.QB:
        this.SetBestStartersFor_QB(p);
        break;
      case Position.RB:
        this.SetBestStartersFor_RB(p);
        break;
      case Position.WR:
        this.SetBestStartersFor_WR(p);
        break;
      case Position.TE:
        this.SetBestStartersFor_TE(p);
        break;
      case Position.OL:
        this.SetBestStartersFor_OL(p);
        break;
      case Position.K:
        this.SetBestStartersFor_K(p);
        break;
      case Position.P:
        this.SetBestStartersFor_P(p);
        break;
      case Position.KR:
        this.SetBestStartersFor_KR(p);
        break;
      case Position.PR:
        this.SetBestStartersFor_PR(p);
        break;
      case Position.DL:
        this.SetBestStartersFor_DL(p);
        break;
      case Position.LB:
        this.SetBestStartersFor_LB(p);
        break;
      case Position.DB:
        this.SetBestStartersFor_DB(p);
        break;
    }
  }

  public void SetBestStartersFor_QB(Position position)
  {
    this.playerList.Clear();
    this.playerOVR.Clear();
    this.OrderPlayersByOverall(position);
    int nextPlayerInList = this.FindNextPlayerInList(0, position);
    if (nextPlayerInList == -1)
      return;
    this.SubstitutePlayers(this.GetStartingQBIndex(), this.playerList[nextPlayerInList]);
  }

  public void SetBestStartersFor_RB(Position position)
  {
    this.playerList.Clear();
    this.playerOVR.Clear();
    this.OrderPlayersByOverall(position);
    int nextPlayerInList1 = this.FindNextPlayerInList(0, position);
    if (nextPlayerInList1 != -1)
      this.SubstitutePlayers(this.GetStartingRBIndex(), this.playerList[nextPlayerInList1]);
    int nextPlayerInList2 = this.FindNextPlayerInList(nextPlayerInList1 + 1, position);
    if (nextPlayerInList2 == -1)
      return;
    this.SubstitutePlayers(this.GetStartingFBIndex(), this.playerList[nextPlayerInList2]);
  }

  public void SetBestStartersFor_TE(Position position)
  {
    this.playerList.Clear();
    this.playerOVR.Clear();
    this.OrderPlayersByOverall(position);
    int nextPlayerInList1 = this.FindNextPlayerInList(0, position);
    if (nextPlayerInList1 != -1)
      this.SubstitutePlayers(this.GetStartingTEIndex(), this.playerList[nextPlayerInList1]);
    int nextPlayerInList2 = this.FindNextPlayerInList(nextPlayerInList1 + 1, position);
    if (nextPlayerInList2 == -1)
      return;
    this.SubstitutePlayers(this.GetStartingTE2Index(), this.playerList[nextPlayerInList2]);
  }

  public void SetBestStartersFor_WR(Position position)
  {
    this.playerList.Clear();
    this.playerOVR.Clear();
    this.OrderPlayersByOverall(position);
    int nextPlayerInList1 = this.FindNextPlayerInList(0, position);
    if (nextPlayerInList1 != -1)
      this.SubstitutePlayers(this.GetStartingWRXIndex(), this.playerList[nextPlayerInList1]);
    int nextPlayerInList2 = this.FindNextPlayerInList(nextPlayerInList1 + 1, position);
    if (nextPlayerInList2 != -1)
      this.SubstitutePlayers(this.GetStartingWRZIndex(), this.playerList[nextPlayerInList2]);
    int nextPlayerInList3 = this.FindNextPlayerInList(nextPlayerInList2 + 1, position);
    if (nextPlayerInList3 != -1)
      this.SubstitutePlayers(this.GetStartingWRYIndex(), this.playerList[nextPlayerInList3]);
    int nextPlayerInList4 = this.FindNextPlayerInList(nextPlayerInList3 + 1, position);
    if (nextPlayerInList4 != -1)
      this.SubstitutePlayers(this.GetStartingWRAIndex(), this.playerList[nextPlayerInList4]);
    int nextPlayerInList5 = this.FindNextPlayerInList(nextPlayerInList4 + 1, position);
    if (nextPlayerInList5 == -1)
      return;
    this.SubstitutePlayers(this.GetStartingWRBIndex(), this.playerList[nextPlayerInList5]);
  }

  public void SetBestStartersFor_OL(Position position)
  {
    this.playerList.Clear();
    this.playerOVR.Clear();
    this.OrderPlayersByOverall(position);
    int nextPlayerInList1 = this.FindNextPlayerInList(0, position);
    if (nextPlayerInList1 != -1)
      this.SubstitutePlayers(this.GetStartingLTIndex(), this.playerList[nextPlayerInList1]);
    int nextPlayerInList2 = this.FindNextPlayerInList(nextPlayerInList1 + 1, position);
    if (nextPlayerInList2 != -1)
      this.SubstitutePlayers(this.GetStartingRTIndex(), this.playerList[nextPlayerInList2]);
    int nextPlayerInList3 = this.FindNextPlayerInList(nextPlayerInList2 + 1, position);
    if (nextPlayerInList3 != -1)
      this.SubstitutePlayers(this.GetStartingCIndex(), this.playerList[nextPlayerInList3]);
    int nextPlayerInList4 = this.FindNextPlayerInList(nextPlayerInList3 + 1, position);
    if (nextPlayerInList4 != -1)
      this.SubstitutePlayers(this.GetStartingLGIndex(), this.playerList[nextPlayerInList4]);
    int nextPlayerInList5 = this.FindNextPlayerInList(nextPlayerInList4 + 1, position);
    if (nextPlayerInList5 == -1)
      return;
    this.SubstitutePlayers(this.GetStartingRGIndex(), this.playerList[nextPlayerInList5]);
  }

  public void SetBestStartersFor_DL(Position position)
  {
    this.playerList.Clear();
    this.playerOVR.Clear();
    this.OrderPlayersByOverall(position);
    int startIndex = 0;
    if (this.NumberOfDLUsed == 3)
    {
      int nextPlayerInList1 = this.FindNextPlayerInList(startIndex, position);
      if (nextPlayerInList1 != -1)
        this.SubstitutePlayers(this.GetStartingLDEIndex_34(), this.playerList[nextPlayerInList1]);
      int nextPlayerInList2 = this.FindNextPlayerInList(nextPlayerInList1 + 1, position);
      if (nextPlayerInList2 != -1)
        this.SubstitutePlayers(this.GetStartingNTIndex(), this.playerList[nextPlayerInList2]);
      int nextPlayerInList3 = this.FindNextPlayerInList(nextPlayerInList2 + 1, position);
      if (nextPlayerInList3 == -1)
        return;
      this.SubstitutePlayers(this.GetStartingRDEIndex_34(), this.playerList[nextPlayerInList3]);
    }
    else
    {
      int nextPlayerInList4 = this.FindNextPlayerInList(startIndex, position);
      if (nextPlayerInList4 != -1)
        this.SubstitutePlayers(this.GetStartingLDEIndex_43(), this.playerList[nextPlayerInList4]);
      int nextPlayerInList5 = this.FindNextPlayerInList(nextPlayerInList4 + 1, position);
      if (nextPlayerInList5 != -1)
        this.SubstitutePlayers(this.GetStartingLDTIndex(), this.playerList[nextPlayerInList5]);
      int nextPlayerInList6 = this.FindNextPlayerInList(nextPlayerInList5 + 1, position);
      if (nextPlayerInList6 != -1)
        this.SubstitutePlayers(this.GetStartingRDTIndex(), this.playerList[nextPlayerInList6]);
      int nextPlayerInList7 = this.FindNextPlayerInList(nextPlayerInList6 + 1, position);
      if (nextPlayerInList7 == -1)
        return;
      this.SubstitutePlayers(this.GetStartingRDEIndex_43(), this.playerList[nextPlayerInList7]);
    }
  }

  public void SetBestStartersFor_LB(Position position)
  {
    this.playerList.Clear();
    this.playerOVR.Clear();
    this.OrderPlayersByOverall(position);
    int startIndex = 0;
    if (this.NumberOfLBUsed == 3)
    {
      int nextPlayerInList1 = this.FindNextPlayerInList(startIndex, position);
      if (nextPlayerInList1 != -1)
        this.SubstitutePlayers(this.GetStartingWLBIndex(), this.playerList[nextPlayerInList1]);
      int nextPlayerInList2 = this.FindNextPlayerInList(nextPlayerInList1 + 1, position);
      if (nextPlayerInList2 != -1)
        this.SubstitutePlayers(this.GetStartingMLBIndex(), this.playerList[nextPlayerInList2]);
      int nextPlayerInList3 = this.FindNextPlayerInList(nextPlayerInList2 + 1, position);
      if (nextPlayerInList3 == -1)
        return;
      this.SubstitutePlayers(this.GetStartingSLBIndex(), this.playerList[nextPlayerInList3]);
    }
    else
    {
      int nextPlayerInList4 = this.FindNextPlayerInList(startIndex, position);
      if (nextPlayerInList4 != -1)
        this.SubstitutePlayers(this.GetStartingLOLBIndex(), this.playerList[nextPlayerInList4]);
      int nextPlayerInList5 = this.FindNextPlayerInList(nextPlayerInList4 + 1, position);
      if (nextPlayerInList5 != -1)
        this.SubstitutePlayers(this.GetStartingROLBIndex(), this.playerList[nextPlayerInList5]);
      int nextPlayerInList6 = this.FindNextPlayerInList(nextPlayerInList5 + 1, position);
      if (nextPlayerInList6 != -1)
        this.SubstitutePlayers(this.GetStartingLILBIndex(), this.playerList[nextPlayerInList6]);
      int nextPlayerInList7 = this.FindNextPlayerInList(nextPlayerInList6 + 1, position);
      if (nextPlayerInList7 == -1)
        return;
      this.SubstitutePlayers(this.GetStartingRILBIndex(), this.playerList[nextPlayerInList7]);
    }
  }

  public void SetBestStartersFor_DB(Position position)
  {
    this.playerList.Clear();
    this.playerOVR.Clear();
    this.OrderPlayersByOverall(position);
    int nextPlayerInList1 = this.FindNextPlayerInList(0, position);
    if (nextPlayerInList1 != -1)
      this.SubstitutePlayers(this.GetStartingLCBIndex(), this.playerList[nextPlayerInList1]);
    int nextPlayerInList2 = this.FindNextPlayerInList(nextPlayerInList1 + 1, position);
    if (nextPlayerInList2 != -1)
      this.SubstitutePlayers(this.GetStartingRCBIndex(), this.playerList[nextPlayerInList2]);
    int nextPlayerInList3 = this.FindNextPlayerInList(nextPlayerInList2 + 1, position);
    if (nextPlayerInList3 != -1)
      this.SubstitutePlayers(this.GetStartingSSIndex(), this.playerList[nextPlayerInList3]);
    int nextPlayerInList4 = this.FindNextPlayerInList(nextPlayerInList3 + 1, position);
    if (nextPlayerInList4 != -1)
      this.SubstitutePlayers(this.GetStartingFSIndex(), this.playerList[nextPlayerInList4]);
    int nextPlayerInList5 = this.FindNextPlayerInList(nextPlayerInList4 + 1, position);
    if (nextPlayerInList5 != -1)
      this.SubstitutePlayers(this.GetStartingNickelBackIndex(), this.playerList[nextPlayerInList5]);
    int nextPlayerInList6 = this.FindNextPlayerInList(nextPlayerInList5 + 1, position);
    if (nextPlayerInList6 == -1)
      return;
    this.SubstitutePlayers(this.GetStartingDimeBackIndex(), this.playerList[nextPlayerInList6]);
  }

  public void SetBestStartersFor_K(Position position)
  {
    this.playerList.Clear();
    this.playerOVR.Clear();
    this.OrderPlayersByOverall(position);
    int nextPlayerInList = this.FindNextPlayerInList(0, position);
    if (nextPlayerInList == -1)
      return;
    this.SubstitutePlayers(this.GetStartingKickerIndex(), this.playerList[nextPlayerInList]);
  }

  public void SetBestStartersFor_P(Position position)
  {
    this.playerList.Clear();
    this.playerOVR.Clear();
    this.OrderPlayersByOverall(position);
    int nextPlayerInList = this.FindNextPlayerInList(0, position);
    if (nextPlayerInList == -1)
      return;
    this.SubstitutePlayers(this.GetStartingPunterIndex(), this.playerList[nextPlayerInList]);
  }

  public void SetBestStartersFor_KR(Position position)
  {
    this.playerList.Clear();
    this.playerOVR.Clear();
    this.OrderPlayersByOverall(position);
    int nextPlayerInList = this.FindNextPlayerInList(0, position, true);
    if (nextPlayerInList == -1)
      return;
    this.SubstitutePlayers(this.GetStartingKickReturnerIndex(), this.playerList[nextPlayerInList], subtituteInThisFormationOnly: this.kickRetPlayers);
  }

  public void SetBestStartersFor_PR(Position position)
  {
    this.playerList.Clear();
    this.playerOVR.Clear();
    this.OrderPlayersByOverall(position);
    int nextPlayerInList = this.FindNextPlayerInList(0, position, true);
    if (nextPlayerInList == -1)
      return;
    this.SubstitutePlayers(this.GetStartingPuntReturnerIndex(), this.playerList[nextPlayerInList], subtituteInThisFormationOnly: this.puntRetPlayers);
  }

  private int FindNextPlayerInList(
    int startIndex,
    Position position,
    bool allowOtherPositionsToStart = false,
    int[] ignorePlayersInThisFormation = null)
  {
    for (int index1 = startIndex; index1 < this.playerList.Count; ++index1)
    {
      if (this.MainRoster.GetPlayer(this.playerList[index1]).CurrentInjury == null)
      {
        bool flag = false;
        if (ignorePlayersInThisFormation != null)
        {
          for (int index2 = 0; index2 < ignorePlayersInThisFormation.Length; ++index2)
          {
            if (this.playerList[index1] == ignorePlayersInThisFormation[index2])
            {
              flag = true;
              break;
            }
          }
        }
        if (!flag && (allowOtherPositionsToStart || this.MainRoster.GetPlayer(this.playerList[index1]).PlayerPosition == position))
          return index1;
      }
    }
    for (int index = 0; index < this.playerList.Count; ++index)
    {
      if (this.MainRoster.GetPlayer(this.playerList[index]).CurrentInjury == null && this.GetStartingPosition_Generic(this.playerList[index]) == Position.None)
        return index;
    }
    Debug.Log((object) "Unable to find any available players.");
    return -1;
  }

  public Position GetStartingPosition_Generic(int playerIndex)
  {
    if (playerIndex == this.GetStartingQBIndex())
      return Position.QB;
    if (playerIndex == this.GetStartingWRXIndex() || playerIndex == this.GetStartingWRZIndex() || playerIndex == this.GetStartingWRYIndex() || playerIndex == this.GetStartingWRAIndex() || playerIndex == this.GetStartingWRBIndex())
      return Position.WR;
    if (playerIndex == this.GetStartingTEIndex() || playerIndex == this.GetStartingTE2Index())
      return Position.TE;
    if (playerIndex == this.GetStartingRBIndex() || playerIndex == this.GetStartingFBIndex())
      return Position.RB;
    if (playerIndex == this.GetStartingLTIndex() || playerIndex == this.GetStartingLGIndex() || playerIndex == this.GetStartingCIndex() || playerIndex == this.GetStartingRGIndex() || playerIndex == this.GetStartingRTIndex())
      return Position.OL;
    if (playerIndex == this.GetStartingLDEIndex_43() || playerIndex == this.GetStartingLDTIndex() || playerIndex == this.GetStartingRDTIndex() || playerIndex == this.GetStartingRDEIndex_43() || playerIndex == this.GetStartingLDEIndex_34() || playerIndex == this.GetStartingNTIndex() || playerIndex == this.GetStartingRDEIndex_34())
      return Position.DL;
    if (playerIndex == this.GetStartingLOLBIndex() || playerIndex == this.GetStartingLILBIndex() || playerIndex == this.GetStartingRILBIndex() || playerIndex == this.GetStartingROLBIndex() || playerIndex == this.GetStartingWLBIndex() || playerIndex == this.GetStartingMLBIndex() || playerIndex == this.GetStartingSLBIndex())
      return Position.LB;
    if (playerIndex == this.GetStartingLCBIndex() || playerIndex == this.GetStartingSSIndex() || playerIndex == this.GetStartingFSIndex() || playerIndex == this.GetStartingRCBIndex() || playerIndex == this.GetStartingNickelBackIndex() || playerIndex == this.GetStartingDimeBackIndex())
      return Position.DB;
    if (playerIndex == this.GetStartingKickerIndex())
      return Position.K;
    return playerIndex == this.GetStartingPunterIndex() ? Position.P : Position.None;
  }

  public StartingPosition GetStartingPosition_Specific(int playerIndex)
  {
    if (playerIndex == this.GetStartingQBIndex())
      return StartingPosition.QB;
    if (playerIndex == this.GetStartingWRXIndex())
      return StartingPosition.WRX;
    if (playerIndex == this.GetStartingWRZIndex())
      return StartingPosition.WRZ;
    if (playerIndex == this.GetStartingWRYIndex())
      return StartingPosition.WRY;
    if (playerIndex == this.GetStartingWRAIndex())
      return StartingPosition.WRA;
    if (playerIndex == this.GetStartingWRBIndex())
      return StartingPosition.WRB;
    if (playerIndex == this.GetStartingTEIndex())
      return StartingPosition.TE;
    if (playerIndex == this.GetStartingTE2Index())
      return StartingPosition.TE2;
    if (playerIndex == this.GetStartingRBIndex())
      return StartingPosition.RB;
    if (playerIndex == this.GetStartingFBIndex())
      return StartingPosition.FB;
    if (playerIndex == this.GetStartingLTIndex())
      return StartingPosition.LT;
    if (playerIndex == this.GetStartingLGIndex())
      return StartingPosition.LG;
    if (playerIndex == this.GetStartingCIndex())
      return StartingPosition.C;
    if (playerIndex == this.GetStartingRGIndex())
      return StartingPosition.RG;
    if (playerIndex == this.GetStartingRTIndex())
      return StartingPosition.RT;
    if (playerIndex == this.GetStartingLDEIndex_43())
      return StartingPosition.LDE_43;
    if (playerIndex == this.GetStartingLDTIndex())
      return StartingPosition.LDT;
    if (playerIndex == this.GetStartingRDTIndex())
      return StartingPosition.RDT;
    if (playerIndex == this.GetStartingRDEIndex_43())
      return StartingPosition.RDE_43;
    if (playerIndex == this.GetStartingLDEIndex_34())
      return StartingPosition.LDE_34;
    if (playerIndex == this.GetStartingNTIndex())
      return StartingPosition.NT;
    if (playerIndex == this.GetStartingRDEIndex_34())
      return StartingPosition.RDE_34;
    if (playerIndex == this.GetStartingLOLBIndex())
      return StartingPosition.LOLB;
    if (playerIndex == this.GetStartingLILBIndex())
      return StartingPosition.LILB;
    if (playerIndex == this.GetStartingRILBIndex())
      return StartingPosition.RILB;
    if (playerIndex == this.GetStartingROLBIndex())
      return StartingPosition.ROLB;
    if (playerIndex == this.GetStartingWLBIndex())
      return StartingPosition.WLB;
    if (playerIndex == this.GetStartingMLBIndex())
      return StartingPosition.MLB;
    if (playerIndex == this.GetStartingSLBIndex())
      return StartingPosition.SLB;
    if (playerIndex == this.GetStartingLCBIndex())
      return StartingPosition.LCB;
    if (playerIndex == this.GetStartingSSIndex())
      return StartingPosition.SS;
    if (playerIndex == this.GetStartingFSIndex())
      return StartingPosition.FS;
    if (playerIndex == this.GetStartingRCBIndex())
      return StartingPosition.RCB;
    if (playerIndex == this.GetStartingNickelBackIndex())
      return StartingPosition.NB;
    if (playerIndex == this.GetStartingDimeBackIndex())
      return StartingPosition.DB;
    if (playerIndex == this.GetStartingKickerIndex())
      return StartingPosition.K;
    return playerIndex == this.GetStartingPunterIndex() ? StartingPosition.P : StartingPosition.None;
  }

  public int GetPlayerIndexOfStarter(StartingPosition position)
  {
    switch (position)
    {
      case StartingPosition.QB:
        return this.GetStartingQBIndex();
      case StartingPosition.WRX:
        return this.GetStartingWRXIndex();
      case StartingPosition.WRZ:
        return this.GetStartingWRZIndex();
      case StartingPosition.WRY:
        return this.GetStartingWRYIndex();
      case StartingPosition.WRA:
        return this.GetStartingWRAIndex();
      case StartingPosition.WRB:
        return this.GetStartingWRBIndex();
      case StartingPosition.TE:
        return this.GetStartingTEIndex();
      case StartingPosition.TE2:
        return this.GetStartingTE2Index();
      case StartingPosition.RB:
        return this.GetStartingRBIndex();
      case StartingPosition.FB:
        return this.GetStartingFBIndex();
      case StartingPosition.LT:
        return this.GetStartingLTIndex();
      case StartingPosition.LG:
        return this.GetStartingLGIndex();
      case StartingPosition.C:
        return this.GetStartingCIndex();
      case StartingPosition.RG:
        return this.GetStartingRGIndex();
      case StartingPosition.RT:
        return this.GetStartingRTIndex();
      case StartingPosition.LDE_43:
        return this.GetStartingLDEIndex_43();
      case StartingPosition.LDT:
        return this.GetStartingLDTIndex();
      case StartingPosition.RDT:
        return this.GetStartingRDTIndex();
      case StartingPosition.RDE_43:
        return this.GetStartingRDEIndex_43();
      case StartingPosition.LDE_34:
        return this.GetStartingLDEIndex_34();
      case StartingPosition.NT:
        return this.GetStartingNTIndex();
      case StartingPosition.RDE_34:
        return this.GetStartingRDEIndex_34();
      case StartingPosition.LOLB:
        return this.GetStartingLOLBIndex();
      case StartingPosition.LILB:
        return this.GetStartingLILBIndex();
      case StartingPosition.RILB:
        return this.GetStartingRILBIndex();
      case StartingPosition.ROLB:
        return this.GetStartingROLBIndex();
      case StartingPosition.WLB:
        return this.GetStartingWLBIndex();
      case StartingPosition.MLB:
        return this.GetStartingMLBIndex();
      case StartingPosition.SLB:
        return this.GetStartingSLBIndex();
      case StartingPosition.LCB:
        return this.GetStartingLCBIndex();
      case StartingPosition.SS:
        return this.GetStartingSSIndex();
      case StartingPosition.FS:
        return this.GetStartingFSIndex();
      case StartingPosition.RCB:
        return this.GetStartingRCBIndex();
      case StartingPosition.NB:
        return this.GetStartingNickelBackIndex();
      case StartingPosition.DB:
        return this.GetStartingDimeBackIndex();
      case StartingPosition.K:
        return this.GetStartingKickerIndex();
      case StartingPosition.P:
        return this.GetStartingPunterIndex();
      default:
        Debug.Log((object) ("Unknown Starting Position Specified: " + position.ToString()));
        return 0;
    }
  }

  public PlayerData GetStarter(StartingPosition position)
  {
    switch (position)
    {
      case StartingPosition.QB:
        return this.GetStartingQB();
      case StartingPosition.WRX:
        return this.GetStartingWRX();
      case StartingPosition.WRZ:
        return this.GetStartingWRZ();
      case StartingPosition.WRY:
        return this.GetStartingWRY();
      case StartingPosition.WRA:
        return this.GetStartingWRA();
      case StartingPosition.WRB:
        return this.GetStartingWRB();
      case StartingPosition.TE:
        return this.GetStartingTE();
      case StartingPosition.TE2:
        return this.GetStartingTE2();
      case StartingPosition.RB:
        return this.GetStartingRB();
      case StartingPosition.FB:
        return this.GetStartingFB();
      case StartingPosition.LT:
        return this.GetStartingLT();
      case StartingPosition.LG:
        return this.GetStartingLG();
      case StartingPosition.C:
        return this.GetStartingC();
      case StartingPosition.RG:
        return this.GetStartingRG();
      case StartingPosition.RT:
        return this.GetStartingRT();
      case StartingPosition.LDE_43:
        return this.GetStartingLDE_43();
      case StartingPosition.LDT:
        return this.GetStartingLDT();
      case StartingPosition.RDT:
        return this.GetStartingRDT();
      case StartingPosition.RDE_43:
        return this.GetStartingRDE_43();
      case StartingPosition.LDE_34:
        return this.GetStartingLDE_34();
      case StartingPosition.NT:
        return this.GetStartingNT();
      case StartingPosition.RDE_34:
        return this.GetStartingRDE_34();
      case StartingPosition.LOLB:
        return this.GetStartingLOLB();
      case StartingPosition.LILB:
        return this.GetStartingLILB();
      case StartingPosition.RILB:
        return this.GetStartingRILB();
      case StartingPosition.ROLB:
        return this.GetStartingROLB();
      case StartingPosition.WLB:
        return this.GetStartingWLB();
      case StartingPosition.MLB:
        return this.GetStartingMLB();
      case StartingPosition.SLB:
        return this.GetStartingSLB();
      case StartingPosition.LCB:
        return this.GetStartingLCB();
      case StartingPosition.SS:
        return this.GetStartingSS();
      case StartingPosition.FS:
        return this.GetStartingFS();
      case StartingPosition.RCB:
        return this.GetStartingRCB();
      case StartingPosition.NB:
        return this.GetStartingNickelBack();
      case StartingPosition.DB:
        return this.GetStartingDimeBack();
      case StartingPosition.K:
        return this.GetStartingKicker();
      case StartingPosition.P:
        return this.GetStartingPunter();
      default:
        Debug.Log((object) ("Unknown Starting Position Specified: " + position.ToString()));
        return (PlayerData) null;
    }
  }
}
