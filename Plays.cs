// Decompiled with JetBrains decompiler
// Type: Plays
// Assembly: ProEra.Game, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A251AB60-A6EC-4F45-B61A-221E02FF094C
// Assembly location: C:\Users\nicke\Desktop\Folders\pro era modding again lol\pcversion\NFL Pro Era\NFL PRO ERA_Data\Managed\ProEra.Game.dll

using Axis;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Plays : MonoBehaviour
{
  [HideInInspector]
  public FormationData specialDefPlays;
  [HideInInspector]
  public FormationData kickReturnPlays;
  [HideInInspector]
  public PlayDataDef dspc_fgBlock;
  [HideInInspector]
  public PlayDataDef dspc_puntBlock;
  [HideInInspector]
  public PlayDataDef dspc_puntReturnLeft;
  [HideInInspector]
  public PlayDataDef dspc_puntReturnRight;
  [HideInInspector]
  public PlayDataDef dspc_kickRetLeft;
  [HideInInspector]
  public PlayDataDef dspc_kickRetMid;
  [HideInInspector]
  public PlayDataDef dspc_kickRetRight;
  [HideInInspector]
  public PlayDataDef dspc_kickRetOnside;
  [HideInInspector]
  public PlayDataDef dspc_kickRetPinch;
  [HideInInspector]
  public FormationData dimePlays;
  [HideInInspector]
  public FormationData fiveThreePlays;
  [HideInInspector]
  public FormationData fourFourPlays;
  [HideInInspector]
  public FormationData fourThreePlays;
  [HideInInspector]
  public FormationData nickelPlays;
  [HideInInspector]
  public FormationData sixTwoPlays;
  [HideInInspector]
  public FormationData threeFourPlays;
  [HideInInspector]
  public FormationData twoFourNickelPlays;
  [HideInInspector]
  public FormationData emptyPlays_FlexTrips;
  [HideInInspector]
  public FormationData emptyPlays_TreyOpen;
  [HideInInspector]
  public FormationData goallinePlays_Heavy;
  [HideInInspector]
  public FormationData hailMaryPlays_Normal;
  [HideInInspector]
  public FormationData iFormPlays_Normal;
  [HideInInspector]
  public FormationData iFormPlays_SlotFlex;
  [HideInInspector]
  public FormationData iFormPlays_Tight;
  [HideInInspector]
  public FormationData iFormPlays_Twins;
  [HideInInspector]
  public FormationData iFormPlays_TwinTE;
  [HideInInspector]
  public FormationData iFormPlays_YTrips;
  [HideInInspector]
  public FormationData specialOffPlays;
  [HideInInspector]
  public PlayDataOff spc_fieldGoal;
  [HideInInspector]
  public PlayDataOff spc_puntLeft;
  [HideInInspector]
  public PlayDataOff spc_puntRight;
  [HideInInspector]
  public PlayDataOff spc_puntProtect;
  [HideInInspector]
  public FormationData kickoffPlays;
  [HideInInspector]
  public PlayDataOff spc_kickoffMid;
  [HideInInspector]
  public PlayDataOff spc_kickoffRight;
  [HideInInspector]
  public PlayDataOff spc_kickoffLeft;
  [HideInInspector]
  public PlayDataOff spc_onsideKick;
  [HideInInspector]
  public FormationData clockManagementPlays;
  [HideInInspector]
  public static readonly string CLOCK_CONTROL_FORMATION_GROUP_NAME = "CLOCK CONTROL";
  [HideInInspector]
  public FormationData pistolPlays_Ace;
  [HideInInspector]
  public FormationData pistolPlays_Bunch;
  [HideInInspector]
  public FormationData pistolPlays_Slot;
  [HideInInspector]
  public FormationData pistolPlays_SpreadFlex;
  [HideInInspector]
  public FormationData pistolPlays_TreyOpen;
  [HideInInspector]
  public FormationData pistolPlays_Trio;
  [HideInInspector]
  public FormationData pistolPlays_YTrips;
  [HideInInspector]
  public FormationData shotgunPlays_Bunch5WR;
  [HideInInspector]
  public FormationData shotgunPlays_Normal;
  [HideInInspector]
  public FormationData shotgunPlays_NormalDimeDropping;
  [HideInInspector]
  public FormationData shotgunPlays_NormalYFlex;
  [HideInInspector]
  public FormationData shotgunPlays_QuadsTrio;
  [HideInInspector]
  public FormationData shotgunPlays_SlotOffset;
  [HideInInspector]
  public FormationData shotgunPlays_SplitOffset;
  [HideInInspector]
  public FormationData shotgunPlays_Spread;
  [HideInInspector]
  public FormationData shotgunPlays_Spread5WR;
  [HideInInspector]
  public FormationData shotgunPlays_Tight;
  [HideInInspector]
  public FormationData shotgunPlays_TightWideBack;
  [HideInInspector]
  public FormationData shotgunPlays_Trey;
  [HideInInspector]
  public FormationData shotgunPlays_Trips;
  [HideInInspector]
  public FormationData singleBackPlays_Big;
  [HideInInspector]
  public FormationData singleBackPlays_BigTwins;
  [HideInInspector]
  public FormationData singleBackPlays_Bunch;
  [HideInInspector]
  public FormationData singleBackPlays_Slot;
  [HideInInspector]
  public FormationData singleBackPlays_Spread;
  [HideInInspector]
  public FormationData singleBackPlays_TreyOpen;
  [HideInInspector]
  public FormationData singleBackPlays_Trio;
  [HideInInspector]
  public FormationData singleBackPlays_Trio4WR;
  [HideInInspector]
  public FormationData splitBackPlays_Normal;
  [HideInInspector]
  public FormationData strongIPlays_Close;
  [HideInInspector]
  public FormationData strongIPlays_Normal;
  [HideInInspector]
  public FormationData strongIPlays_Tight;
  [HideInInspector]
  public FormationData strongIPlays_Twins;
  [HideInInspector]
  public FormationData strongIPlays_TwinsFlex;
  [HideInInspector]
  public FormationData strongIPlays_TwinTE;
  [HideInInspector]
  public FormationData weakIPlays_CloseTwins;
  [HideInInspector]
  public FormationData weakIPlays_Normal;
  [HideInInspector]
  public FormationData weakIPlays_Twins;
  [HideInInspector]
  public FormationData weakIPlays_TwinsFlex;
  [HideInInspector]
  public FormationData weakIPlays_TwinTE;
  public static Plays self;
  [HideInInspector]
  public FormationPositions goalline_Heavy;
  [HideInInspector]
  public FormationPositions iForm_Normal;
  [HideInInspector]
  public FormationPositions iForm_Tight;
  [HideInInspector]
  public FormationPositions iForm_SlotFlex;
  [HideInInspector]
  public FormationPositions iForm_TwinTE;
  [HideInInspector]
  public FormationPositions iForm_Twins;
  [HideInInspector]
  public FormationPositions iForm_YTrips;
  [HideInInspector]
  public FormationPositions strongI_Close;
  [HideInInspector]
  public FormationPositions strongI_Normal;
  [HideInInspector]
  public FormationPositions strongI_Tight;
  [HideInInspector]
  public FormationPositions strongI_TwinTE;
  [HideInInspector]
  public FormationPositions strongI_Twins;
  [HideInInspector]
  public FormationPositions strongI_TwinsFlex;
  [HideInInspector]
  public FormationPositions weakI_CloseTwins;
  [HideInInspector]
  public FormationPositions weakI_TwinsFlex;
  [HideInInspector]
  public FormationPositions weakI_Normal;
  [HideInInspector]
  public FormationPositions weakI_TwinTE;
  [HideInInspector]
  public FormationPositions weakI_Twins;
  [HideInInspector]
  public FormationPositions splitBack_Normal;
  [HideInInspector]
  public FormationPositions singleBack_Big;
  [HideInInspector]
  public FormationPositions singleBack_BigTwins;
  [HideInInspector]
  public FormationPositions singleBack_Bunch;
  [HideInInspector]
  public FormationPositions singleBack_Slot;
  [HideInInspector]
  public FormationPositions singleBack_Spread;
  [HideInInspector]
  public FormationPositions singleBack_TreyOpen;
  [HideInInspector]
  public FormationPositions singleBack_Trio;
  [HideInInspector]
  public FormationPositions singleBack_Trio4WR;
  [HideInInspector]
  public FormationPositions empty_TreyOpen;
  [HideInInspector]
  public FormationPositions empty_FlexTrips;
  [HideInInspector]
  public FormationPositions pistol_Ace;
  [HideInInspector]
  public FormationPositions pistol_Bunch;
  [HideInInspector]
  public FormationPositions pistol_Slot;
  [HideInInspector]
  public FormationPositions pistol_SpreadFlex;
  [HideInInspector]
  public FormationPositions pistol_TreyOpen;
  [HideInInspector]
  public FormationPositions pistol_Trio;
  [HideInInspector]
  public FormationPositions pistol_YTrips;
  [HideInInspector]
  public FormationPositions shotgun_Normal;
  [HideInInspector]
  public FormationPositions shotgun_XSlot;
  [HideInInspector]
  public FormationPositions shotgun_NormalYFlex;
  [HideInInspector]
  public FormationPositions shotgun_QuadsTrio;
  [HideInInspector]
  public FormationPositions shotgun_SlotOffset;
  [HideInInspector]
  public FormationPositions shotgun_SplitOffset;
  [HideInInspector]
  public FormationPositions shotgun_Spread;
  [HideInInspector]
  public FormationPositions shotgun_Tight;
  [HideInInspector]
  public FormationPositions shotgun_TightWideBack;
  [HideInInspector]
  public FormationPositions shotgun_Trey;
  [HideInInspector]
  public FormationPositions shotgun_Trips;
  [HideInInspector]
  public FormationPositions shotgun_Spread5WR;
  [HideInInspector]
  public FormationPositions shotgun_Bunch5WR;
  [HideInInspector]
  public FormationPositions shotgun_NormalDimeDropping;
  [HideInInspector]
  public FormationPositions qbKneel_Normal;
  [HideInInspector]
  public FormationPositions hailMary_Normal;
  [HideInInspector]
  public FormationPositions fieldGoalForm;
  [HideInInspector]
  public FormationPositions puntForm;
  [HideInInspector]
  public FormationPositions kickoffForm;
  [HideInInspector]
  public FormationPositions onsideKickForm;
  [HideInInspector]
  public FormationPositionsDef sixTwoForm;
  [HideInInspector]
  public FormationPositionsDef fiveThreeForm;
  [HideInInspector]
  public FormationPositionsDef fourFourForm;
  [HideInInspector]
  public FormationPositionsDef fourThreeForm;
  [HideInInspector]
  public FormationPositionsDef threeFourForm;
  [HideInInspector]
  public FormationPositionsDef nickelForm;
  [HideInInspector]
  public FormationPositionsDef dimeForm;
  [HideInInspector]
  public FormationPositionsDef twoFourNickelForm;
  [HideInInspector]
  public FormationPositionsDef fgBlockForm;
  [HideInInspector]
  public FormationPositionsDef puntRetForm;
  [HideInInspector]
  public FormationPositionsDef kickRetForm;
  [HideInInspector]
  public FormationPositionsDef kickRetOnsideForm;
  [HideInInspector]
  public List<FormationData> offPlaybookP1;
  [HideInInspector]
  public List<FormationData> offPlaybookP2;
  [HideInInspector]
  public List<FormationData> playbook_AirRaid;
  [HideInInspector]
  public List<FormationData> playbook_PowerRun;
  [HideInInspector]
  public List<FormationData> playbook_WestCoast;
  [HideInInspector]
  public List<FormationData> playbook_Pistol;
  [HideInInspector]
  public List<FormationData> playbook_Singleback;
  [HideInInspector]
  public List<FormationData> playbook_AllOffPlays;
  [HideInInspector]
  public List<FormationData> playbook_DimeDropping;
  [HideInInspector]
  public List<FormationData> playbook_DemoOffPlays;
  [HideInInspector]
  public List<FormationData> playbook_PistolLite;
  [HideInInspector]
  public List<FormationData> playbook_Tampa;
  [HideInInspector]
  public List<FormationData> playbook_SanFrancisco;
  public List<CustomPlaybook> customOffensivePlaybooks;
  public List<CustomPlaybook> customDefensivePlaybooks;
  public List<string> offensivePlaybookNames = new List<string>()
  {
    "AIR RAID",
    "POWER RUN",
    "WEST COAST",
    "PISTOL",
    "SINGLEBACK",
    "TAMPA",
    "PISTOL LITE",
    "SAN FRANCISCO"
  };
  [HideInInspector]
  public List<FormationData> defPlaybookP1;
  [HideInInspector]
  public List<FormationData> defPlaybookP2;
  [HideInInspector]
  public List<FormationData> playbook_ThreeFour;
  [HideInInspector]
  public List<FormationData> playbook_FourThree;
  [HideInInspector]
  public List<FormationData> playbook_AllDefPlays;
  public List<string> defensivePlaybookNames = new List<string>()
  {
    "THREE FOUR",
    "FOUR THREE"
  };
  private List<PlayDataDef> manCoverage_Close_P1;
  private List<PlayDataDef> manCoverage_Short_P1;
  private List<PlayDataDef> manCoverage_Medium_P1;
  private List<PlayDataDef> manCoverage_Long_P1;
  private List<PlayDataDef> manZoneDouble_Close_P1;
  private List<PlayDataDef> manZoneDouble_Short_P1;
  private List<PlayDataDef> manZoneDouble_Medium_P1;
  private List<PlayDataDef> manZoneDouble_Long_P1;
  private List<PlayDataDef> cover2_Close_P1;
  private List<PlayDataDef> cover2_Short_P1;
  private List<PlayDataDef> cover2_Medium_P1;
  private List<PlayDataDef> cover2_Long_P1;
  private List<PlayDataDef> cover3_Close_P1;
  private List<PlayDataDef> cover3_Short_P1;
  private List<PlayDataDef> cover3_Medium_P1;
  private List<PlayDataDef> cover3_Long_P1;
  private List<PlayDataDef> cover4_Close_P1;
  private List<PlayDataDef> cover4_Short_P1;
  private List<PlayDataDef> cover4_Medium_P1;
  private List<PlayDataDef> cover4_Long_P1;
  private List<PlayDataDef> manBlitz_Close_P1;
  private List<PlayDataDef> manBlitz_Short_P1;
  private List<PlayDataDef> manBlitz_Medium_P1;
  private List<PlayDataDef> manBlitz_Long_P1;
  private List<PlayDataDef> zoneBlitz_Close_P1;
  private List<PlayDataDef> zoneBlitz_Short_P1;
  private List<PlayDataDef> zoneBlitz_Medium_P1;
  private List<PlayDataDef> zoneBlitz_Long_P1;
  private List<PlayDataDef> qbSpy_P1;
  private List<PlayDataDef> manCoverage_Close_P2;
  private List<PlayDataDef> manCoverage_Short_P2;
  private List<PlayDataDef> manCoverage_Medium_P2;
  private List<PlayDataDef> manCoverage_Long_P2;
  private List<PlayDataDef> manZoneDouble_Close_P2;
  private List<PlayDataDef> manZoneDouble_Short_P2;
  private List<PlayDataDef> manZoneDouble_Medium_P2;
  private List<PlayDataDef> manZoneDouble_Long_P2;
  private List<PlayDataDef> cover2_Close_P2;
  private List<PlayDataDef> cover2_Short_P2;
  private List<PlayDataDef> cover2_Medium_P2;
  private List<PlayDataDef> cover2_Long_P2;
  private List<PlayDataDef> cover3_Close_P2;
  private List<PlayDataDef> cover3_Short_P2;
  private List<PlayDataDef> cover3_Medium_P2;
  private List<PlayDataDef> cover3_Long_P2;
  private List<PlayDataDef> cover4_Close_P2;
  private List<PlayDataDef> cover4_Short_P2;
  private List<PlayDataDef> cover4_Medium_P2;
  private List<PlayDataDef> cover4_Long_P2;
  private List<PlayDataDef> manBlitz_Close_P2;
  private List<PlayDataDef> manBlitz_Short_P2;
  private List<PlayDataDef> manBlitz_Medium_P2;
  private List<PlayDataDef> manBlitz_Long_P2;
  private List<PlayDataDef> zoneBlitz_Close_P2;
  private List<PlayDataDef> zoneBlitz_Short_P2;
  private List<PlayDataDef> zoneBlitz_Medium_P2;
  private List<PlayDataDef> zoneBlitz_Long_P2;
  private List<PlayDataDef> qbSpy_P2;
  private Dictionary<PlayConcept, List<PlayDataDef>> defPlaysByConcept_P1;
  private Dictionary<PlayConcept, List<PlayDataDef>> defPlaysByConcept_P2;
  private Dictionary<BaseFormation, List<PlayDataDef>> defPlaysByFormation_P1;
  private Dictionary<BaseFormation, List<PlayDataDef>> defPlaysByFormation_P2;
  private List<PlayDataOff> insideRunPlays_P1;
  private List<PlayDataOff> outsideRunPlays_P1;
  private List<PlayDataOff> qbKeeperPlays_P1;
  private List<PlayDataOff> readOptionPlays_P1;
  private List<PlayDataOff> shortPassPlays_P1;
  private List<PlayDataOff> midPassPlays_P1;
  private List<PlayDataOff> deepPassPlays_P1;
  private List<PlayDataOff> screenPassPlays_P1;
  private List<PlayDataOff> playActionPassPlays_P1;
  private List<PlayDataOff> goalLinePassPlays_P1;
  private List<PlayDataOff> goalLineRunPlays_P1;
  private List<PlayDataOff> insideRunPlays_P2;
  private List<PlayDataOff> outsideRunPlays_P2;
  private List<PlayDataOff> qbKeeperPlays_P2;
  private List<PlayDataOff> readOptionPlays_P2;
  private List<PlayDataOff> shortPassPlays_P2;
  private List<PlayDataOff> midPassPlays_P2;
  private List<PlayDataOff> deepPassPlays_P2;
  private List<PlayDataOff> screenPassPlays_P2;
  private List<PlayDataOff> playActionPassPlays_P2;
  private List<PlayDataOff> goalLinePassPlays_P2;
  private List<PlayDataOff> goalLineRunPlays_P2;
  private string CurOffPlaybookP1 = "";
  private string CurDefPlaybookP1 = "";
  private string CurOffPlaybookP2 = "";
  private string CurDefPlaybookP2 = "";
  private string _offAtlasNameP1 = "";
  private string _defAtlasNameP1 = "";
  private int runPos_actualPosition;
  private int runPos_sweepRight = 15;
  private int runPos_sweepLeft = -15;
  private int runPos_offTackleRight = 10;
  private int runPos_offTackleLeft = -10;
  private int runPos_reverseToLeft = -5;
  private int runPos_reverseToRight = 5;
  private int runPos_screenRight = 32;
  private int runPos_screenLeft = -32;
  [HideInInspector]
  public PlayAssignment runBlockT;
  [HideInInspector]
  public PlayAssignment runBlockG;
  [HideInInspector]
  public PlayAssignment runBlockC;
  [HideInInspector]
  public PlayAssignment runBlockWR;
  [HideInInspector]
  public PlayAssignment runBlockTE;
  [HideInInspector]
  public PlayAssignment runBlockRB;
  [HideInInspector]
  public PlayAssignment pullBlockOut;
  [HideInInspector]
  public PlayAssignment pullBlockIn;
  [HideInInspector]
  public PlayAssignment passBlockT;
  [HideInInspector]
  public PlayAssignment passBlockG;
  [HideInInspector]
  public PlayAssignment passBlockC;
  [HideInInspector]
  public PlayAssignment passBlockTE;
  [HideInInspector]
  public PlayAssignment passBlockRB;
  [HideInInspector]
  public PlayAssignment isoBlockIn;
  [HideInInspector]
  public PlayAssignment isoBlockOut;
  [HideInInspector]
  public PlayAssignment rbSweepBlockOut;
  [HideInInspector]
  public PlayAssignment rbSweepBlockIn;
  [HideInInspector]
  public PlayAssignment rbGunPABlock;
  [HideInInspector]
  public PlayAssignment passBlockLT_Short;
  [HideInInspector]
  public PlayAssignment passBlockLG_Short;
  [HideInInspector]
  public PlayAssignment passBlockRT_Short;
  [HideInInspector]
  public PlayAssignment passBlockRG_Short;
  [HideInInspector]
  public PlayAssignment passBlockC_Short;
  [HideInInspector]
  public PlayAssignment passBlockLT_Mid;
  [HideInInspector]
  public PlayAssignment passBlockLG_Mid;
  [HideInInspector]
  public PlayAssignment passBlockRT_Mid;
  [HideInInspector]
  public PlayAssignment passBlockRG_Mid;
  [HideInInspector]
  public PlayAssignment passBlockC_Mid;
  [HideInInspector]
  public PlayAssignment passBlockLT_Deep;
  [HideInInspector]
  public PlayAssignment passBlockLG_Deep;
  [HideInInspector]
  public PlayAssignment passBlockRT_Deep;
  [HideInInspector]
  public PlayAssignment passBlockRG_Deep;
  [HideInInspector]
  public PlayAssignment passBlockC_Deep;
  [HideInInspector]
  public PlayAssignment passBlockC_PA_Left;
  [HideInInspector]
  public PlayAssignment passBlockRG_PA_Left;
  [HideInInspector]
  public PlayAssignment passBlockRT_PA_Left;
  [HideInInspector]
  public PlayAssignment passBlockLG_PA_Left;
  [HideInInspector]
  public PlayAssignment passBlockLT_PA_Left;
  [HideInInspector]
  public PlayAssignment passBlockC_PA_Right;
  [HideInInspector]
  public PlayAssignment passBlockRG_PA_Right;
  [HideInInspector]
  public PlayAssignment passBlockRT_PA_Right;
  [HideInInspector]
  public PlayAssignment passBlockLG_PA_Right;
  [HideInInspector]
  public PlayAssignment passBlockLT_PA_Right;
  [HideInInspector]
  public PlayAssignment passBlockRB_Singleback;
  [HideInInspector]
  public PlayAssignment passBlockRB_Shotgun_Right;
  [HideInInspector]
  public PlayAssignment passBlockRB_Shotgun_Left;
  [HideInInspector]
  public PlayAssignment passBlockFB_GoLeft;
  [HideInInspector]
  public PlayAssignment screenBlockOLOut;
  [HideInInspector]
  public PlayAssignment screenBlockOLIn;
  [HideInInspector]
  public PlayAssignment screenBlockRBIn;
  [HideInInspector]
  public PlayAssignment screenBlockRBOut;
  [HideInInspector]
  public PlayAssignment kickoffMid;
  [HideInInspector]
  public PlayAssignment kickoffLeft;
  [HideInInspector]
  public PlayAssignment kickoffIn;
  [HideInInspector]
  public PlayAssignment kickoffKicker;
  [HideInInspector]
  public PlayAssignment diveBlockIn;
  [HideInInspector]
  public PlayAssignment diveBlockOut;
  [HideInInspector]
  public PlayAssignment passBlockC_Shotgun;
  [HideInInspector]
  public PlayAssignment passBlockC_PA;
  [HideInInspector]
  public PlayAssignment passBlockG_PA;
  [HideInInspector]
  public PlayAssignment passBlockT_PA;
  [HideInInspector]
  public PlayAssignment qbToss;
  [HideInInspector]
  public PlayAssignment qbRBDiveOut;
  [HideInInspector]
  public PlayAssignment qbRBDiveIn;
  [HideInInspector]
  public PlayAssignment qbIsoOut;
  [HideInInspector]
  public PlayAssignment qbIsoIn;
  [HideInInspector]
  public PlayAssignment qbOffTackleIn;
  [HideInInspector]
  public PlayAssignment qbOffTackleOut;
  [HideInInspector]
  public PlayAssignment qbFBDiveOut;
  [HideInInspector]
  public PlayAssignment qbFBDiveIn;
  [HideInInspector]
  public PlayAssignment qbCounterOut;
  [HideInInspector]
  public PlayAssignment qbCounterIn;
  [HideInInspector]
  public PlayAssignment qbEndAroundToLeft;
  [HideInInspector]
  public PlayAssignment qbEndAroundToRight;
  [HideInInspector]
  public PlayAssignment qbSlotAroundToLeft_GL;
  [HideInInspector]
  public PlayAssignment qbSlotAroundToRight_GL;
  [HideInInspector]
  public PlayAssignment qbSlotAroundToLeft_SB;
  [HideInInspector]
  public PlayAssignment qbSlotAroundToRight_SB;
  [HideInInspector]
  public PlayAssignment qbGunHOAcross;
  [HideInInspector]
  public PlayAssignment qbGunSlotHOIn;
  [HideInInspector]
  public PlayAssignment qbGunSlotHOOut;
  [HideInInspector]
  public PlayAssignment qbLHBAcrossIso;
  [HideInInspector]
  public PlayAssignment qbRHBAcrossIso;
  [HideInInspector]
  public PlayAssignment qbGunEndAround;
  [HideInInspector]
  public PlayAssignment qbPistolOffTackleOut;
  [HideInInspector]
  public PlayAssignment qbPistolOffTackleIn;
  [HideInInspector]
  public PlayAssignment qbPistolPAIn;
  [HideInInspector]
  public PlayAssignment qbPistolPAOut;
  [HideInInspector]
  public PlayAssignment qbReadOptionRight;
  [HideInInspector]
  public PlayAssignment qbReadOptionLeft;
  [HideInInspector]
  public PlayAssignment qbPassPlay;
  [HideInInspector]
  public PlayAssignment qbKneel;
  [HideInInspector]
  public PlayAssignment rbCounterOut;
  [HideInInspector]
  public PlayAssignment rbCounterIn;
  [HideInInspector]
  public PlayAssignment rbDiveOut;
  [HideInInspector]
  public PlayAssignment rbDiveIn;
  [HideInInspector]
  public PlayAssignment fbDiveIn;
  [HideInInspector]
  public PlayAssignment fbDiveOut;
  [HideInInspector]
  public PlayAssignment rbIsoOut;
  [HideInInspector]
  public PlayAssignment rbIsoIn;
  [HideInInspector]
  public PlayAssignment rbTossOut;
  [HideInInspector]
  public PlayAssignment rbTossIn;
  [HideInInspector]
  public PlayAssignment rbGunHOAcross;
  [HideInInspector]
  public PlayAssignment rbGunToss;
  [HideInInspector]
  public PlayAssignment rbSplitDive;
  [HideInInspector]
  public PlayAssignment rhbSplitAcrossIso;
  [HideInInspector]
  public PlayAssignment lhbSplitAcrossIso;
  [HideInInspector]
  public PlayAssignment rbOffTackleIn;
  [HideInInspector]
  public PlayAssignment rbOffTackleOut;
  [HideInInspector]
  public PlayAssignment teAround;
  [HideInInspector]
  public PlayAssignment teAroundGun;
  [HideInInspector]
  public PlayAssignment slotAround;
  [HideInInspector]
  public PlayAssignment wrZInsideRun;
  [HideInInspector]
  public PlayAssignment qbRHBDive;
  [HideInInspector]
  public PlayAssignment qbLHBDive;
  [HideInInspector]
  public PlayAssignment qbSplitDiveOut;
  [HideInInspector]
  public PlayAssignment fbDive_OffsetRight;
  [HideInInspector]
  public PlayAssignment fbDive_OffsetLeft;
  [HideInInspector]
  public PlayAssignment qbKeeperRight;
  [HideInInspector]
  public PlayAssignment qbKeeperLeft;
  [HideInInspector]
  public PlayAssignment out5;
  [HideInInspector]
  public PlayAssignment out10;
  [HideInInspector]
  public PlayAssignment out15;
  [HideInInspector]
  public PlayAssignment in5;
  [HideInInspector]
  public PlayAssignment in10;
  [HideInInspector]
  public PlayAssignment in15;
  [HideInInspector]
  public PlayAssignment slantIn;
  [HideInInspector]
  public PlayAssignment slantOut;
  [HideInInspector]
  public PlayAssignment slantInHitch;
  [HideInInspector]
  public PlayAssignment post5;
  [HideInInspector]
  public PlayAssignment post10;
  [HideInInspector]
  public PlayAssignment post5skinny;
  [HideInInspector]
  public PlayAssignment post10skinny;
  [HideInInspector]
  public PlayAssignment post5flat;
  [HideInInspector]
  public PlayAssignment post10flat;
  [HideInInspector]
  public PlayAssignment corner5;
  [HideInInspector]
  public PlayAssignment corner10;
  [HideInInspector]
  public PlayAssignment corner5skinny;
  [HideInInspector]
  public PlayAssignment corner10skinny;
  [HideInInspector]
  public PlayAssignment corner5flat;
  [HideInInspector]
  public PlayAssignment corner10flat;
  [HideInInspector]
  public PlayAssignment hitch5in;
  [HideInInspector]
  public PlayAssignment hitch5out;
  [HideInInspector]
  public PlayAssignment hitch10in;
  [HideInInspector]
  public PlayAssignment hitch10out;
  [HideInInspector]
  public PlayAssignment hitch15out;
  [HideInInspector]
  public PlayAssignment hitchInFly;
  [HideInInspector]
  public PlayAssignment fly;
  [HideInInspector]
  public PlayAssignment teScreenRoute;
  [HideInInspector]
  public PlayAssignment wrScreenRoute;
  [HideInInspector]
  public PlayAssignment slantInLowPost;
  [HideInInspector]
  public PlayAssignment slantInLowPostSkinny;
  [HideInInspector]
  public PlayAssignment slantInPostFlat;
  [HideInInspector]
  public PlayAssignment slantInPostSkinny;
  [HideInInspector]
  public PlayAssignment slantInHighPost;
  [HideInInspector]
  public PlayAssignment slantInHighPostFlat;
  [HideInInspector]
  public PlayAssignment slantInQuick;
  [HideInInspector]
  public PlayAssignment rbFlatOut_Fly;
  [HideInInspector]
  public PlayAssignment rbFlatIn_Fly;
  [HideInInspector]
  public PlayAssignment rbOffTackleOut_In;
  [HideInInspector]
  public PlayAssignment rbDiveIn_Out;
  [HideInInspector]
  public PlayAssignment rbDiveIn_In;
  [HideInInspector]
  public PlayAssignment fbDiveIn_Out;
  [HideInInspector]
  public PlayAssignment fbDiveIn_In;
  [HideInInspector]
  public PlayAssignment rbScreenIn;
  [HideInInspector]
  public PlayAssignment rbScreenOut;
  [HideInInspector]
  public PlayAssignment underIn;
  [HideInInspector]
  public PlayAssignment underOut;
  [HideInInspector]
  public PlayAssignment rbGunOffTackleIn_Post;
  [HideInInspector]
  public PlayAssignment rbOffTackleIn_In;
  [HideInInspector]
  public PlayAssignment slantInUpHitchOut;
  [HideInInspector]
  public PlayAssignment slantInUpCorner;
  [HideInInspector]
  public PlayAssignment slantInUpPost;
  [HideInInspector]
  public PlayAssignment upCornerFly;
  [HideInInspector]
  public PlayAssignment underOutAndUp;
  [HideInInspector]
  public PlayAssignment underOutUpCorner;
  [HideInInspector]
  public PlayAssignment underInUpPost;
  [HideInInspector]
  public PlayAssignment upPostHitch;
  [HideInInspector]
  public PlayAssignment upPostIn;
  [HideInInspector]
  public PlayAssignment upPostOut;
  [HideInInspector]
  public PlayAssignment upOutFly;
  [HideInInspector]
  public PlayAssignment upCornerHitch;
  [HideInInspector]
  public PlayAssignment slantInFly;
  [HideInInspector]
  public PlayAssignment slantOutFly;
  [HideInInspector]
  public PlayAssignment upInPost;
  [HideInInspector]
  public PlayAssignment slantInIn;
  [HideInInspector]
  public PlayAssignment post5Corner;
  [HideInInspector]
  public PlayAssignment post5Out;
  [HideInInspector]
  public PlayAssignment dragIn;
  [HideInInspector]
  public PlayAssignment dragInFromSlot;
  [HideInInspector]
  public PlayAssignment dragOut;
  [HideInInspector]
  public PlayAssignment dragOutFromSlot;
  [HideInInspector]
  public PlayAssignment rbIsoIn_Out;
  [HideInInspector]
  public PlayAssignment rbIsoIn_In;
  [HideInInspector]
  public PlayAssignment rbIsoOut_Out;
  [HideInInspector]
  public PlayAssignment rbIsoOut_In;
  [HideInInspector]
  public PlayAssignment rbIsoIn_Fly;
  [HideInInspector]
  public PlayAssignment rbIsoOut_Fly;
  [HideInInspector]
  public PlayAssignment splitBackIso_Out;
  [HideInInspector]
  public PlayAssignment splitBackIso_In;
  [HideInInspector]
  public PlayAssignment rbGunOffTackleIn_Fly;
  [HideInInspector]
  public PlayAssignment rbGunOffTackleIn_Out;
  [HideInInspector]
  public PlayAssignment rbGunOffTackleIn_In;
  [HideInInspector]
  public PlayAssignment rbFlatIn;
  [HideInInspector]
  public PlayAssignment rbFlatOut;
  [HideInInspector]
  public PlayAssignment rbIsoIn_HitchIn;
  [HideInInspector]
  public PlayAssignment deepZone1of1;
  [HideInInspector]
  public PlayAssignment deepZone1of3;
  [HideInInspector]
  public PlayAssignment deepZone2of3;
  [HideInInspector]
  public PlayAssignment deepZone3of3;
  [HideInInspector]
  public PlayAssignment deepZone1of4;
  [HideInInspector]
  public PlayAssignment deepZone2of4;
  [HideInInspector]
  public PlayAssignment deepZone3of4;
  [HideInInspector]
  public PlayAssignment deepZone4of4;
  [HideInInspector]
  public PlayAssignment deepZone1of2;
  [HideInInspector]
  public PlayAssignment deepZone2of2;
  [HideInInspector]
  public PlayAssignment midZone1of2;
  [HideInInspector]
  public PlayAssignment midZone2of2;
  [HideInInspector]
  public PlayAssignment midZone1of3;
  [HideInInspector]
  public PlayAssignment midZone2of3;
  [HideInInspector]
  public PlayAssignment midZone3of3;
  [HideInInspector]
  public PlayAssignment midZone1of4;
  [HideInInspector]
  public PlayAssignment midZone2of4;
  [HideInInspector]
  public PlayAssignment midZone3of4;
  [HideInInspector]
  public PlayAssignment midZone4of4;
  [HideInInspector]
  public PlayAssignment flatZoneLeft;
  [HideInInspector]
  public PlayAssignment flatZoneRight;
  [HideInInspector]
  public PlayAssignment blitzHole9;
  [HideInInspector]
  public PlayAssignment blitzHole7;
  [HideInInspector]
  public PlayAssignment blitzHole5;
  [HideInInspector]
  public PlayAssignment blitzHole3;
  [HideInInspector]
  public PlayAssignment blitzHole1;
  [HideInInspector]
  public PlayAssignment blitzHole0;
  [HideInInspector]
  public PlayAssignment blitzHole2;
  [HideInInspector]
  public PlayAssignment blitzHole4;
  [HideInInspector]
  public PlayAssignment blitzHole6;
  [HideInInspector]
  public PlayAssignment blitzHole8;
  [HideInInspector]
  public PlayAssignment blitzHole10;
  [HideInInspector]
  public PlayAssignment linemanHole9;
  [HideInInspector]
  public PlayAssignment linemanHole7;
  [HideInInspector]
  public PlayAssignment linemanHole5;
  [HideInInspector]
  public PlayAssignment linemanHole3;
  [HideInInspector]
  public PlayAssignment linemanHole1;
  [HideInInspector]
  public PlayAssignment linemanHole0;
  [HideInInspector]
  public PlayAssignment linemanHole2;
  [HideInInspector]
  public PlayAssignment linemanHole4;
  [HideInInspector]
  public PlayAssignment linemanHole6;
  [HideInInspector]
  public PlayAssignment linemanHole8;
  [HideInInspector]
  public PlayAssignment linemanHole10;
  [HideInInspector]
  public PlayAssignment stuntLeftOut;
  [HideInInspector]
  public PlayAssignment stuntRightOut;
  [HideInInspector]
  public PlayAssignment stuntLeftIn;
  [HideInInspector]
  public PlayAssignment stuntRightIn;
  [HideInInspector]
  public PlayAssignment manCoverage;
  [HideInInspector]
  public PlayAssignment manCoverageInsideOver;
  [HideInInspector]
  public PlayAssignment manCoverageOutsideUnder;
  [HideInInspector]
  public PlayAssignment manCoverageOutsideOver;
  [HideInInspector]
  public PlayAssignment spyQB;
  [HideInInspector]
  public PlayAssignment puntReturn;
  [HideInInspector]
  public PlayAssignment kickRetBlockerIn;
  [HideInInspector]
  public PlayAssignment kickRetBlockerOut;
  [HideInInspector]
  public PlayAssignment upBlocker1;
  [HideInInspector]
  public PlayAssignment upBlocker2;
  [HideInInspector]
  public PlayAssignment upBlocker3;
  [HideInInspector]
  public PlayAssignment upBlocker4;
  [HideInInspector]
  public PlayAssignment kickReturn;
  [HideInInspector]
  public PlayAssignment kickRetBlockerBack;
  [HideInInspector]
  public PlayAssignment kickRetBlockerBackRT;
  [HideInInspector]
  public PlayAssignment kickRetBlockerBackRG;
  [HideInInspector]
  public PlayAssignment kickRetBlockerBackC;
  [HideInInspector]
  public PlayAssignment kickRetBlockerBackLG;
  [HideInInspector]
  public PlayAssignment kickRetBlockerBackLT;
  [HideInInspector]
  public PlayAssignment puntGunner;
  [HideInInspector]
  public PlayAssignment puntRetBlockerOut;
  [HideInInspector]
  public PlayAssignment puntRetBlockerIn;
  [HideInInspector]
  public PlayAssignment puntRetBlockerCB;
  [HideInInspector]
  public PlayAssignment onsideKickRet;
  private float yard_1;
  private float yards_2;
  private float yards_3;
  private float yards_4;
  private float yards_5;
  private float yards_6;
  private float yards_7;
  private float yards_8;
  private float yards_9;
  private float yards_10;
  private float yards_11;
  private float yards_12;
  private float yards_13;
  private float yards_14;
  private float yards_15;
  private float yards_16;
  private float yards_17;
  private float yards_18;
  private float yards_19;
  private float yards_20;
  private float yards_30;
  private float yards_40;
  private float yards_50;
  private float yards_60;
  private float yards_70;
  private float yards_80;
  private float yards_90;
  private float yards_100;
  private float yards_21;
  private float yards_22;
  private float yards_23;
  private float yards_24;
  private float yards_25;
  private float yards_26;
  private float yards_27;
  private float yards_28;
  private float yards_29;
  private float yards_31;
  private float yards_32;
  private float yards_33;
  private float yards_34;
  private float yards_35;
  private float yards_36;
  private float yards_37;
  private float yards_38;
  private float yards_39;
  private float yards_41;
  private float yards_42;
  private float yards_43;
  private float yards_44;
  private float yards_45;
  private float yards_46;
  private float yards_47;
  private float yards_48;
  private float yards_49;
  private float speed_0;
  private float speed_1;
  private float speed_2;
  private float speed_3;
  private float speed_4;
  private float speed_5;
  [HideInInspector]
  public RouteGraphicData empty;
  [HideInInspector]
  public RouteGraphicData rgd_deep1of1;
  [HideInInspector]
  public RouteGraphicData rgd_deep1of2;
  [HideInInspector]
  public RouteGraphicData rgd_deep2of2;
  [HideInInspector]
  public RouteGraphicData rgd_deep1of3;
  [HideInInspector]
  public RouteGraphicData rgd_deep2of3;
  [HideInInspector]
  public RouteGraphicData rgd_deep3of3;
  [HideInInspector]
  public RouteGraphicData rgd_deep1of4;
  [HideInInspector]
  public RouteGraphicData rgd_deep2of4;
  [HideInInspector]
  public RouteGraphicData rgd_deep3of4;
  [HideInInspector]
  public RouteGraphicData rgd_deep4of4;
  [HideInInspector]
  public RouteGraphicData rgd_mid1of2;
  [HideInInspector]
  public RouteGraphicData rgd_mid2of2;
  [HideInInspector]
  public RouteGraphicData rgd_mid1of3;
  [HideInInspector]
  public RouteGraphicData rgd_mid2of3;
  [HideInInspector]
  public RouteGraphicData rgd_mid3of3;
  [HideInInspector]
  public RouteGraphicData rgd_mid1of4;
  [HideInInspector]
  public RouteGraphicData rgd_mid2of4;
  [HideInInspector]
  public RouteGraphicData rgd_mid3of4;
  [HideInInspector]
  public RouteGraphicData rgd_mid4of4;
  [HideInInspector]
  public RouteGraphicData rgd_flatLeft;
  [HideInInspector]
  public RouteGraphicData rgd_flatRight;
  [HideInInspector]
  public RouteGraphicData rgd_blitz9;
  [HideInInspector]
  public RouteGraphicData rgd_blitz7;
  [HideInInspector]
  public RouteGraphicData rgd_blitz5;
  [HideInInspector]
  public RouteGraphicData rgd_blitz3;
  [HideInInspector]
  public RouteGraphicData rgd_blitz1;
  [HideInInspector]
  public RouteGraphicData rgd_blitz0;
  [HideInInspector]
  public RouteGraphicData rgd_blitz2;
  [HideInInspector]
  public RouteGraphicData rgd_blitz4;
  [HideInInspector]
  public RouteGraphicData rgd_blitz6;
  [HideInInspector]
  public RouteGraphicData rgd_blitz8;
  [HideInInspector]
  public RouteGraphicData rgd_blitz10;
  [HideInInspector]
  public RouteGraphicData rgd_kickRetBlockIn;
  [HideInInspector]
  public RouteGraphicData rgd_kickRetBlockOut;
  [HideInInspector]
  public RouteGraphicData rgd_kickRetBlockBack;
  [HideInInspector]
  public RouteGraphicData rgd_kickRetBlockBackLT;
  [HideInInspector]
  public RouteGraphicData rgd_kickRetBlockBackLG;
  [HideInInspector]
  public RouteGraphicData rgd_kickRetBlockBackC;
  [HideInInspector]
  public RouteGraphicData rgd_kickRetBlockBackRG;
  [HideInInspector]
  public RouteGraphicData rgd_kickRetBlockBackRT;
  [HideInInspector]
  public RouteGraphicData rgd_onsideKickRet;
  [HideInInspector]
  public RouteGraphicData rgd_lineman;
  [HideInInspector]
  public RouteGraphicData rgd_stuntOut;
  [HideInInspector]
  public RouteGraphicData rgd_stuntIn;
  public RouteGraphicData rgd_kickoffMid;
  public RouteGraphicData rgd_kickoffIn;
  public RouteGraphicData rgd_kickoffLeft;
  public RouteGraphicData rgd_runBlock;
  public RouteGraphicData rgd_passBlock;
  public RouteGraphicData rgd_passBlockRB;
  public RouteGraphicData rgd_pullBlockIn;
  public RouteGraphicData rgd_pullBlockOut;
  public RouteGraphicData rgd_diveBlockIn;
  public RouteGraphicData rgd_diveBlockOut;
  public RouteGraphicData rgd_isoBlockIn;
  public RouteGraphicData rgd_isoBlockOut;
  public RouteGraphicData rgd_sweepBlockIn;
  public RouteGraphicData rgd_sweepBlockOut;
  public RouteGraphicData rgd_rbScreenBlockIn;
  public RouteGraphicData rgd_rbScreenBlockOut;
  public RouteGraphicData rgd_olScreenBlockIn;
  public RouteGraphicData rgd_olScreenBlockOut;
  public RouteGraphicData rgd_counterOut;
  public RouteGraphicData rgd_counterIn;
  public RouteGraphicData rgd_diveIn;
  public RouteGraphicData rgd_diveOut;
  public RouteGraphicData rgd_isoIn;
  public RouteGraphicData rgd_isoOut;
  public RouteGraphicData rgd_offTackleIn;
  public RouteGraphicData rgd_offTackleOut;
  public RouteGraphicData rgd_tossIn;
  public RouteGraphicData rgd_tossOut;
  public RouteGraphicData rgd_gunAcross;
  public RouteGraphicData rgd_gunToss;
  public RouteGraphicData rgd_splitDive;
  public RouteGraphicData rgd_splitAcross;
  public RouteGraphicData rgd_reverse;
  public RouteGraphicData rgd_slotReverse;
  public RouteGraphicData rgd_fbDiveIn;
  public RouteGraphicData rgd_fbDiveOut;
  public RouteGraphicData rgd_out5;
  public RouteGraphicData rgd_out10;
  public RouteGraphicData rgd_out15;
  public RouteGraphicData rgd_in5;
  public RouteGraphicData rgd_in10;
  public RouteGraphicData rgd_in15;
  public RouteGraphicData rgd_slantIn;
  public RouteGraphicData rgd_slantOut;
  public RouteGraphicData rgd_slantInHitch;
  public RouteGraphicData rgd_post5;
  public RouteGraphicData rgd_post10;
  public RouteGraphicData rgd_post5flat;
  public RouteGraphicData rgd_post10flat;
  public RouteGraphicData rgd_post5skinny;
  public RouteGraphicData rgd_post10skinny;
  public RouteGraphicData rgd_corner5;
  public RouteGraphicData rgd_corner10;
  public RouteGraphicData rgd_corner5flat;
  public RouteGraphicData rgd_corner10flat;
  public RouteGraphicData rgd_corner5skinny;
  public RouteGraphicData rgd_corner10skinny;
  public RouteGraphicData rgd_fly;
  public RouteGraphicData rgd_rbFlatIn;
  public RouteGraphicData rgd_rbFlatOut;
  public RouteGraphicData rgd_rbFlatOut_Fly;
  public RouteGraphicData rgd_rbFlatIn_Fly;
  public RouteGraphicData rgd_rbOffTackleOut_In;
  public RouteGraphicData rgd_rbDiveIn_In;
  public RouteGraphicData rgd_rbDiveIn_Out;
  public RouteGraphicData rgd_rbGunOffTackleIn_Post;
  public RouteGraphicData rgd_rbOffTackleIn_In;
  public RouteGraphicData rgd_rbScreenIn;
  public RouteGraphicData rgd_rbScreenOut;
  public RouteGraphicData rgd_fbDiveIn_In;
  public RouteGraphicData rgd_fbDiveIn_Out;
  public RouteGraphicData rgd_wrScreen;
  public RouteGraphicData rgd_underIn;
  public RouteGraphicData rgd_underOut;
  public RouteGraphicData rgd_hitch5in;
  public RouteGraphicData rgd_hitch5out;
  public RouteGraphicData rgd_hitch10in;
  public RouteGraphicData rgd_hitchInFly;
  public RouteGraphicData rgd_hitch10out;
  public RouteGraphicData rgd_slantInUpHitchOut;
  public RouteGraphicData rgd_slantInUpCorner;
  public RouteGraphicData rgd_slantInUpPost;
  public RouteGraphicData rgd_upCornerFly;
  public RouteGraphicData rgd_underOutUp;
  public RouteGraphicData rgd_underOutUpCorner;
  public RouteGraphicData rgd_underInUpPost;
  public RouteGraphicData rgd_upPostHitch;
  public RouteGraphicData rgd_upPostIn;
  public RouteGraphicData rgd_upPostOut;
  public RouteGraphicData rgd_upOutFly;
  public RouteGraphicData rgd_upCornerHitch;
  public RouteGraphicData rgd_slantInFly;
  public RouteGraphicData rgd_slantOutFly;
  public RouteGraphicData rgd_upInPost;
  public RouteGraphicData rgd_slantInIn;
  public RouteGraphicData rgd_post5corner;
  public RouteGraphicData rgd_post5out;
  public RouteGraphicData rgd_rbIsoIn_HitchIn;
  public RouteGraphicData rgd_dragIn;
  public RouteGraphicData rgd_dragInFromSlot;
  public RouteGraphicData rgd_dragOut;
  public RouteGraphicData rgd_dragOutFromSlot;
  public RouteGraphicData rgd_slantInLowPost;
  public RouteGraphicData rgd_slantInLowPostSkinny;
  public RouteGraphicData rgd_slantInPostFlat;
  public RouteGraphicData rgd_slantInPostSkinny;
  public RouteGraphicData rgd_slantInHighPost;
  public RouteGraphicData rgd_slantInHighPostFlat;
  public RouteGraphicData rgd_qbRun;
  public RouteGraphicData rgd_PAIsoOut_Out;
  public RouteGraphicData rgd_PAIsoOut_In;
  public RouteGraphicData rgd_PAIsoIn_In;
  public RouteGraphicData rgd_PAIsoIn_Out;
  public RouteGraphicData rgd_rbGunOffTackleIn_Out;
  public RouteGraphicData rgd_rbGunOffTackleIn_In;

  private void SetPlays_DefSpecialTeams()
  {
    this.specialDefPlays = new FormationData("SPECIAL TEAMS", FormationType.DefSpecial, (FormationPositions) this.fgBlockForm);
    this.kickReturnPlays = new FormationData("KICK RETURN", FormationType.KickReturn, (FormationPositions) this.kickRetForm);
    this.dspc_fgBlock = new PlayDataDef(this.fgBlockForm, "FG BLOCK", PlayConcept.Special_Teams, this.linemanHole5, this.linemanHole3, this.linemanHole1, this.linemanHole0, this.linemanHole2, this.linemanHole4, this.blitzHole3, this.blitzHole4, this.blitzHole7, this.linemanHole6, this.blitzHole8);
    this.dspc_puntBlock = new PlayDataDef(this.puntRetForm, "PUNT BLOCK", PlayConcept.Special_Teams, this.linemanHole5, this.linemanHole3, this.linemanHole1, this.linemanHole0, this.linemanHole2, this.linemanHole4, this.blitzHole3, this.blitzHole4, this.puntRetBlockerCB, this.puntReturn, this.puntRetBlockerCB);
    this.dspc_puntReturnLeft = new PlayDataDef(this.puntRetForm, "RETURN LEFT", PlayConcept.Special_Teams, this.linemanHole5, this.linemanHole3, this.linemanHole1, this.linemanHole0, this.linemanHole2, this.linemanHole4, this.puntRetBlockerOut, this.puntRetBlockerIn, this.puntRetBlockerCB, this.puntReturn, this.puntRetBlockerCB);
    this.dspc_puntReturnRight = new PlayDataDef(this.puntRetForm, "RETURN RIGHT", PlayConcept.Special_Teams, this.linemanHole5, this.linemanHole3, this.linemanHole1, this.linemanHole0, this.linemanHole2, this.linemanHole6, this.puntRetBlockerIn, this.puntRetBlockerOut, this.puntRetBlockerCB, this.puntReturn, this.puntRetBlockerCB);
    this.dspc_kickRetMid = new PlayDataDef(this.kickRetForm, "RETURN MID", PlayConcept.Kick_Return, this.kickRetBlockerBackLT, this.kickRetBlockerBackLG, this.kickRetBlockerBackC, this.kickRetBlockerBackC, this.kickRetBlockerBackRG, this.kickRetBlockerBackRT, this.upBlocker1, this.upBlocker2, this.upBlocker3, this.kickReturn, this.upBlocker4);
    this.dspc_kickRetLeft = new PlayDataDef(this.kickRetForm, "RETURN LEFT", PlayConcept.Kick_Return, this.kickRetBlockerBackLT, this.kickRetBlockerBackLG, this.kickRetBlockerBackC, this.kickRetBlockerBackC, this.kickRetBlockerBackRG, this.kickRetBlockerBackRT, this.upBlocker1, this.upBlocker2, this.upBlocker3, this.kickReturn, this.upBlocker4);
    this.dspc_kickRetRight = new PlayDataDef(this.kickRetForm, "RETURN RIGHT", PlayConcept.Kick_Return, this.kickRetBlockerBackLT, this.kickRetBlockerBackLG, this.kickRetBlockerBackC, this.kickRetBlockerBackC, this.kickRetBlockerBackRG, this.kickRetBlockerBackRT, this.upBlocker1, this.upBlocker2, this.upBlocker3, this.kickReturn, this.upBlocker4);
    this.dspc_kickRetPinch = new PlayDataDef(this.kickRetForm, "RETURN MID", PlayConcept.Kick_Return, this.kickRetBlockerBackLT, this.kickRetBlockerBackLG, this.kickRetBlockerBackC, this.kickRetBlockerBackC, this.kickRetBlockerBackRG, this.kickRetBlockerBackRT, this.upBlocker1, this.upBlocker2, this.upBlocker3, this.kickReturn, this.upBlocker4);
    this.dspc_kickRetOnside = new PlayDataDef(this.kickRetOnsideForm, "ONSIDE RETURN", PlayConcept.Kick_Return, this.onsideKickRet, this.onsideKickRet, this.onsideKickRet, this.onsideKickRet, this.onsideKickRet, this.onsideKickRet, this.onsideKickRet, this.onsideKickRet, this.onsideKickRet, this.onsideKickRet, this.onsideKickRet);
    this.specialDefPlays.SetPlays(new List<PlayData>()
    {
      (PlayData) this.dspc_fgBlock,
      (PlayData) this.dspc_puntBlock,
      (PlayData) this.dspc_puntReturnLeft,
      (PlayData) this.dspc_puntReturnRight
    });
    this.kickReturnPlays.SetPlays(new List<PlayData>()
    {
      (PlayData) this.dspc_kickRetLeft,
      (PlayData) this.dspc_kickRetMid,
      (PlayData) this.dspc_kickRetRight,
      (PlayData) this.dspc_kickRetPinch,
      (PlayData) this.dspc_kickRetOnside
    });
  }

  private void SetPlays_Dime()
  {
    this.dimePlays = new FormationData("DIME", FormationType.Defense, (FormationPositions) this.dimeForm);
    this.dimePlays.AddPlay((PlayData) new PlayDataDef(this.dimeForm, "BASE MAN", PlayConcept.Man_Coverage, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.spyQB, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.deepZone1of1, this.manCoverage));
    this.dimePlays.AddPlay((PlayData) new PlayDataDef(this.dimeForm, "FLATS COVER", PlayConcept.Man_Zone_Double, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.flatZoneRight, this.flatZoneLeft, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.dimePlays.AddPlay((PlayData) new PlayDataDef(this.dimeForm, "BASE COVER 3", PlayConcept.Cover_Three, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone2of4, this.midZone4of4, this.midZone1of4, this.deepZone1of3, this.midZone3of4, this.deepZone2of3, this.deepZone3of3));
    this.dimePlays.AddPlay((PlayData) new PlayDataDef(this.dimeForm, "FOIL", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole0, this.blitzHole8, this.midZone2of3, this.midZone1of3, this.deepZone2of2, this.deepZone1of2, this.midZone3of3));
    this.dimePlays.AddPlay((PlayData) new PlayDataDef(this.dimeForm, "STRONG BLITZ", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole2, this.manCoverage, this.manCoverage, this.manCoverage, this.blitzHole6, this.manCoverage, this.manCoverage));
    this.dimePlays.AddPlay((PlayData) new PlayDataDef(this.dimeForm, "COVER 3 STRONG SLIDE", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone2of2, this.blitzHole8, this.midZone1of2, this.deepZone1of3, this.blitzHole6, this.deepZone2of3, this.deepZone3of3));
    this.dimePlays.AddPlay((PlayData) new PlayDataDef(this.dimeForm, "COVER 4 MAX", PlayConcept.Cover_Four, this.flatZoneLeft, this.linemanHole3, this.linemanHole4, this.flatZoneRight, this.midZone2of3, this.midZone3of3, this.midZone1of3, this.deepZone1of4, this.deepZone3of4, this.deepZone2of4, this.deepZone4of4));
    this.dimePlays.AddPlay((PlayData) new PlayDataDef(this.dimeForm, "OUTSIDE STUFF", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.blitzHole8, this.blitzHole7, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.dimePlays.AddPlay((PlayData) new PlayDataDef(this.dimeForm, "BASE COVER 2", PlayConcept.Cover_Two, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.spyQB, this.midZone3of4, this.midZone2of4, this.midZone1of4, this.deepZone2of2, this.deepZone1of2, this.midZone4of4));
    this.dimePlays.AddPlay((PlayData) new PlayDataDef(this.dimeForm, "COVER 3 WEAK SLIDE", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone1of2, this.midZone2of2, this.blitzHole7, this.deepZone1of3, this.deepZone2of3, this.blitzHole5, this.deepZone3of3));
    this.dimePlays.AddPlay((PlayData) new PlayDataDef(this.dimeForm, "DOUBLE OUTS", PlayConcept.Cover_Two, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone2of3, this.flatZoneRight, this.flatZoneLeft, this.midZone1of3, this.deepZone2of2, this.deepZone1of2, this.midZone3of3));
    this.dimePlays.AddPlay((PlayData) new PlayDataDef(this.dimeForm, "DOUBLE DEEP", PlayConcept.Cover_Two, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.deepZone2of2, this.deepZone1of2, this.manCoverage));
    this.dimePlays.AddPlay((PlayData) new PlayDataDef(this.dimeForm, "PREVENT", PlayConcept.Cover_Four, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone2of3, this.midZone3of3, this.midZone1of3, this.deepZone1of4, this.deepZone3of4, this.deepZone2of4, this.deepZone4of4));
    this.dimePlays.AddPlay((PlayData) new PlayDataDef(this.dimeForm, "COVER 2 OUTSIDE BLITZ", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone2of3, this.blitzHole6, this.blitzHole5, this.midZone1of3, this.deepZone2of2, this.deepZone1of2, this.midZone3of3));
    this.dimePlays.AddPlay((PlayData) new PlayDataDef(this.dimeForm, "BALANCE", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole0, this.manCoverage, this.manCoverage, this.blitzHole7, this.manCoverage, this.manCoverage, this.manCoverage));
  }

  private void SetPlays_FiveThree()
  {
    this.fiveThreePlays = new FormationData("5-3", FormationType.Defense, (FormationPositions) this.fiveThreeForm);
    this.fiveThreePlays.AddPlay((PlayData) new PlayDataDef(this.fiveThreeForm, "BASE MAN", PlayConcept.Man_Coverage, this.linemanHole7, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.linemanHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.deepZone1of1, this.manCoverage));
    this.fiveThreePlays.AddPlay((PlayData) new PlayDataDef(this.fiveThreeForm, "OUTSIDE GAPS", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.linemanHole8, this.blitzHole7, this.midZone2of3, this.blitzHole8, this.midZone1of3, this.deepZone1of1, this.midZone3of3));
    this.fiveThreePlays.AddPlay((PlayData) new PlayDataDef(this.fiveThreeForm, "PINCH", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.linemanHole8, this.manCoverage, this.blitzHole1, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.fiveThreePlays.AddPlay((PlayData) new PlayDataDef(this.fiveThreeForm, "WEAK DOUBLE", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.linemanHole8, this.blitzHole5, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.fiveThreePlays.AddPlay((PlayData) new PlayDataDef(this.fiveThreeForm, "STRONG DOUBLE", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.linemanHole8, this.manCoverage, this.manCoverage, this.blitzHole6, this.manCoverage, this.manCoverage, this.manCoverage));
    this.fiveThreePlays.AddPlay((PlayData) new PlayDataDef(this.fiveThreeForm, "COVER 3 SMASH", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.linemanHole8, this.blitzHole5, this.midZone2of3, this.blitzHole6, this.deepZone1of3, this.deepZone2of3, this.deepZone3of3));
    this.fiveThreePlays.AddPlay((PlayData) new PlayDataDef(this.fiveThreeForm, "DOUBLE COVER 3", PlayConcept.Man_Zone_Double, this.manCoverage, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.deepZone1of3, this.deepZone2of3, this.deepZone3of3));
    this.fiveThreePlays.AddPlay((PlayData) new PlayDataDef(this.fiveThreeForm, "LOW COVER 3", PlayConcept.Man_Zone_Double, this.manCoverage, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.manCoverage, this.midZone1of3, this.midZone2of3, this.midZone3of3, this.manCoverage, this.manCoverage, this.manCoverage));
    this.fiveThreePlays.AddPlay((PlayData) new PlayDataDef(this.fiveThreeForm, "DOUBLE COVER 3", PlayConcept.Man_Zone_Double, this.manCoverage, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.deepZone1of3, this.deepZone2of3, this.deepZone3of3));
    this.fiveThreePlays.AddPlay((PlayData) new PlayDataDef(this.fiveThreeForm, "ZONE GAMBLE", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.linemanHole8, this.blitzHole7, this.blitzHole2, this.blitzHole8, this.midZone1of3, this.midZone2of3, this.midZone3of3));
    this.fiveThreePlays.AddPlay((PlayData) new PlayDataDef(this.fiveThreeForm, "STUNT SPIKE", PlayConcept.Man_Blitz, this.stuntLeftIn, this.linemanHole7, this.linemanHole0, this.linemanHole6, this.stuntRightIn, this.blitzHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.fiveThreePlays.AddPlay((PlayData) new PlayDataDef(this.fiveThreeForm, "ROCK SLIDE", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.linemanHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.blitzHole6, this.manCoverage));
  }

  private void SetPlays_FourFour()
  {
    this.fourFourPlays = new FormationData("4-4", FormationType.Defense, (FormationPositions) this.fourFourForm);
    this.fourFourPlays.AddPlay((PlayData) new PlayDataDef(this.fourFourForm, "BASE MAN", PlayConcept.Man_Coverage, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.spyQB, this.manCoverage, this.manCoverage, this.manCoverage, this.deepZone1of1, this.manCoverage));
    this.fourFourPlays.AddPlay((PlayData) new PlayDataDef(this.fourFourForm, "ZONE SPIKE", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone1of3, this.blitzHole3, this.midZone2of3, this.blitzHole8, this.deepZone1of2, this.deepZone2of2, this.midZone3of3));
    this.fourFourPlays.AddPlay((PlayData) new PlayDataDef(this.fourFourForm, "BASE COVER 3", PlayConcept.Cover_Three, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone1of4, this.midZone2of4, this.midZone3of4, this.midZone4of4, this.deepZone1of3, this.deepZone2of3, this.deepZone3of3));
    this.fourFourPlays.AddPlay((PlayData) new PlayDataDef(this.fourFourForm, "AVALANCHE", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole7, this.blitzHole3, this.midZone2of3, this.blitzHole8, this.midZone1of3, this.deepZone1of1, this.midZone3of3));
    this.fourFourPlays.AddPlay((PlayData) new PlayDataDef(this.fourFourForm, "COVER 3 WEAK BLITZ", PlayConcept.Cover_Three, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone1of3, this.blitzHole3, this.midZone2of3, this.midZone3of3, this.deepZone1of3, this.deepZone2of3, this.deepZone3of3));
    this.fourFourPlays.AddPlay((PlayData) new PlayDataDef(this.fourFourForm, "FLAT FIRE", PlayConcept.Cover_Four, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole5, this.midZone2of4, this.midZone3of4, this.flatZoneRight, this.midZone1of4, this.flatZoneLeft, this.midZone4of4));
    this.fourFourPlays.AddPlay((PlayData) new PlayDataDef(this.fourFourForm, "STRONG PRESS", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.manCoverage, this.blitzHole4, this.blitzHole6, this.manCoverage, this.manCoverage, this.manCoverage));
    this.fourFourPlays.AddPlay((PlayData) new PlayDataDef(this.fourFourForm, "COVER 3 STRONG BLITZ", PlayConcept.Cover_Three, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone1of3, this.midZone2of3, this.blitzHole4, this.midZone3of3, this.deepZone1of3, this.deepZone2of3, this.deepZone3of3));
    this.fourFourPlays.AddPlay((PlayData) new PlayDataDef(this.fourFourForm, "FLARE", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.blitzHole7, this.manCoverage, this.blitzHole8));
    this.fourFourPlays.AddPlay((PlayData) new PlayDataDef(this.fourFourForm, "OUTSIDE STUFF", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole5, this.manCoverage, this.manCoverage, this.blitzHole6, this.manCoverage, this.manCoverage, this.manCoverage));
    this.fourFourPlays.AddPlay((PlayData) new PlayDataDef(this.fourFourForm, "COVER 3 INSIDE BLITZ", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone1of2, this.blitzHole3, this.blitzHole4, this.midZone2of2, this.deepZone1of3, this.deepZone2of3, this.deepZone3of3));
    this.fourFourPlays.AddPlay((PlayData) new PlayDataDef(this.fourFourForm, "WEAK PRESS", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole5, this.blitzHole3, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.fourFourPlays.AddPlay((PlayData) new PlayDataDef(this.fourFourForm, "FLIP", PlayConcept.Zone_Blitz, this.midZone2of4, this.linemanHole3, this.stuntRightOut, this.linemanHole4, this.blitzHole7, this.blitzHole1, this.deepZone3of4, this.blitzHole2, this.deepZone1of4, this.blitzHole0, this.deepZone4of4));
    this.fourFourPlays.AddPlay((PlayData) new PlayDataDef(this.fourFourForm, "COVER 3 OUTSIDE BLITZ", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole5, this.midZone1of2, this.midZone2of2, this.blitzHole6, this.deepZone1of3, this.deepZone2of3, this.deepZone3of3));
    this.fourFourPlays.AddPlay((PlayData) new PlayDataDef(this.fourFourForm, "OUTSIDE DICE", PlayConcept.Man_Blitz, this.linemanHole3, this.stuntLeftOut, this.stuntRightOut, this.linemanHole4, this.blitzHole7, this.manCoverage, this.manCoverage, this.blitzHole8, this.manCoverage, this.manCoverage, this.manCoverage));
    this.fourFourPlays.AddPlay((PlayData) new PlayDataDef(this.fourFourForm, "INSIDE STUFF", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.blitzHole1, this.blitzHole2, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.fourFourPlays.AddPlay((PlayData) new PlayDataDef(this.fourFourForm, "CANNONBALL", PlayConcept.Man_Coverage, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.manCoverage, this.blitzHole2, this.manCoverage, this.manCoverage, this.manCoverage, this.blitzHole8));
  }

  private void SetPlays_FourThree()
  {
    this.fourThreePlays = new FormationData("4-3", FormationType.Defense, (FormationPositions) this.fourThreeForm);
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "BASE MAN", PlayConcept.Man_Coverage, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.spyQB, this.manCoverage, this.manCoverage, this.manCoverage, this.deepZone1of1, this.manCoverage));
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "BASE COVER 2", PlayConcept.Cover_Two, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.deepZone2of2, this.deepZone1of2, this.manCoverage));
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "BASE COVER 2", PlayConcept.Cover_Two, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone2of4, this.spyQB, this.midZone3of4, this.midZone1of4, this.deepZone2of2, this.deepZone1of2, this.midZone4of4));
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "WEAK BLITZ", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole7, this.blitzHole0, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "BASE COVER 3", PlayConcept.Cover_Three, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone1of4, this.midZone2of4, this.midZone4of4, this.deepZone1of3, this.midZone3of4, this.deepZone2of3, this.deepZone3of3));
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "COVER 3 RUSH", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole3, this.midZone1of3, this.blitzHole4, this.deepZone1of3, this.midZone3of4, this.deepZone2of3, this.deepZone3of3));
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "COVER 2 INSIDE STUFF", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole3, this.midZone2of3, this.blitzHole4, this.midZone1of3, this.deepZone2of2, this.deepZone1of2, this.midZone3of3));
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "STRONG BLITZ", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.blitzHole0, this.blitzHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "COVER 2 DOUBLE FLATS", PlayConcept.Cover_Two, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone1of3, this.midZone2of3, this.midZone3of3, this.flatZoneLeft, this.deepZone2of2, this.deepZone1of2, this.flatZoneRight));
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "COVER 2 CORNER BLITZ", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone1of3, this.midZone2of3, this.midZone3of3, this.blitzHole9, this.deepZone2of2, this.deepZone1of2, this.blitzHole10));
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "BASE COVER 4", PlayConcept.Cover_Four, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone1of3, this.midZone2of3, this.midZone3of3, this.deepZone1of4, this.deepZone3of4, this.deepZone2of4, this.deepZone4of4));
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "PRESS FLAT", PlayConcept.Man_Zone_Double, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.flatZoneRight, this.flatZoneLeft, this.manCoverage));
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "STRONG BLITZ", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole7, this.manCoverage, this.blitzHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "PUSH STRONG", PlayConcept.Man_Coverage, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.blitzHole0, this.blitzHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "CORNER SLIDE", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole7, this.manCoverage, this.manCoverage, this.blitzHole7, this.manCoverage, this.manCoverage, this.manCoverage));
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "INSIDE QB SPY", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.blitzHole0, this.spyQB, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "ZONE FLOOD", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone2of3, this.blitzHole0, this.midZone3of3, this.midZone1of3, this.deepZone1of1, this.blitzHole5, this.blitzHole8));
    this.fourThreePlays.AddPlay((PlayData) new PlayDataDef(this.fourThreeForm, "THUNDER", PlayConcept.Man_Coverage, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.manCoverage, this.blitzHole6, this.manCoverage, this.blitzHole4, this.manCoverage, this.manCoverage));
  }

  private void SetPlays_Nickel()
  {
    this.nickelPlays = new FormationData("NICKEL", FormationType.Defense, (FormationPositions) this.nickelForm);
    this.nickelPlays.AddPlay((PlayData) new PlayDataDef(this.nickelForm, "BASE MAN", PlayConcept.Man_Coverage, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.spyQB, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.deepZone1of1, this.manCoverage));
    this.nickelPlays.AddPlay((PlayData) new PlayDataDef(this.nickelForm, "BASE COVER 3", PlayConcept.Cover_Three, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.flatZoneLeft, this.flatZoneRight, this.midZone2of4, this.deepZone1of3, this.midZone3of4, this.deepZone2of3, this.deepZone3of3));
    this.nickelPlays.AddPlay((PlayData) new PlayDataDef(this.nickelForm, "COVER 3 INSIDE STUFF", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole3, this.blitzHole4, this.midZone1of2, this.deepZone1of3, this.midZone2of2, this.deepZone2of3, this.deepZone3of3));
    this.nickelPlays.AddPlay((PlayData) new PlayDataDef(this.nickelForm, "BASE COVER 4", PlayConcept.Cover_Four, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone2of3, this.midZone3of3, this.midZone1of3, this.deepZone1of4, this.deepZone3of4, this.deepZone2of4, this.deepZone4of4));
    this.nickelPlays.AddPlay((PlayData) new PlayDataDef(this.nickelForm, "BACKSLIDE", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole3, this.manCoverage, this.blitzHole7, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.nickelPlays.AddPlay((PlayData) new PlayDataDef(this.nickelForm, "COVER 3 BACK BLITZ", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.flatZoneLeft, this.flatZoneRight, this.blitzHole5, this.deepZone1of3, this.blitzHole6, this.deepZone2of3, this.deepZone3of3));
    this.nickelPlays.AddPlay((PlayData) new PlayDataDef(this.nickelForm, "MID COVER", PlayConcept.Man_Zone_Double, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone1of3, this.midZone3of3, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.nickelPlays.AddPlay((PlayData) new PlayDataDef(this.nickelForm, "SAFETY SQUEEZE", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.blitzHole4, this.manCoverage, this.manCoverage, this.blitzHole6, this.manCoverage, this.manCoverage));
    this.nickelPlays.AddPlay((PlayData) new PlayDataDef(this.nickelForm, "COVER 4 MAX", PlayConcept.Cover_Four, this.flatZoneLeft, this.linemanHole3, this.linemanHole4, this.flatZoneRight, this.midZone2of3, this.midZone3of3, this.midZone1of3, this.deepZone1of4, this.deepZone3of4, this.deepZone2of4, this.deepZone4of4));
    this.nickelPlays.AddPlay((PlayData) new PlayDataDef(this.nickelForm, "BASE COVER 2", PlayConcept.Cover_Two, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.deepZone2of2, this.deepZone1of2, this.manCoverage));
    this.nickelPlays.AddPlay((PlayData) new PlayDataDef(this.nickelForm, "COVER 4 WEAK BLITZ", PlayConcept.Cover_Four, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.midZone3of3, this.blitzHole5, this.midZone2of3, this.midZone1of3, this.deepZone1of4, this.deepZone3of4, this.deepZone2of4, this.deepZone4of4));
    this.nickelPlays.AddPlay((PlayData) new PlayDataDef(this.nickelForm, "CORNER BLITZ", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.blitzHole9, this.manCoverage, this.manCoverage, this.blitzHole10));
    this.nickelPlays.AddPlay((PlayData) new PlayDataDef(this.nickelForm, "SPLITSAW", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone2of3, this.blitzHole8, this.midZone1of3, this.blitzHole7, this.deepZone2of2, this.deepZone1of2, this.midZone3of3));
    this.nickelPlays.AddPlay((PlayData) new PlayDataDef(this.nickelForm, "BLINK", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole7, this.blitzHole8, this.midZone2of3, this.midZone1of3, this.midZone3of3, this.deepZone1of1, this.blitzHole8));
    this.nickelPlays.AddPlay((PlayData) new PlayDataDef(this.nickelForm, "COVER 2 WEAK COLLAPSE", PlayConcept.Cover_Two, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole1, this.midZone2of3, this.blitzHole3, this.midZone1of3, this.deepZone2of2, this.deepZone1of2, this.midZone3of3));
    this.nickelPlays.AddPlay((PlayData) new PlayDataDef(this.nickelForm, "SAFETY BLITZ", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.blitzHole6, this.blitzHole5, this.manCoverage));
    this.nickelPlays.AddPlay((PlayData) new PlayDataDef(this.nickelForm, "INSIDE BLITZ", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole3, this.blitzHole4, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
  }

  private void SetPlays_SixTwo()
  {
    this.sixTwoPlays = new FormationData("6-2", FormationType.Defense, (FormationPositions) this.sixTwoForm);
    this.sixTwoPlays.AddPlay((PlayData) new PlayDataDef(this.sixTwoForm, "BASE MAN", PlayConcept.Man_Coverage, this.linemanHole5, this.linemanHole3, this.linemanHole1, this.linemanHole2, this.linemanHole4, this.linemanHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.sixTwoPlays.AddPlay((PlayData) new PlayDataDef(this.sixTwoForm, "FLATS COVER", PlayConcept.Man_Zone_Double, this.flatZoneLeft, this.linemanHole3, this.linemanHole1, this.linemanHole2, this.linemanHole4, this.flatZoneRight, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.sixTwoPlays.AddPlay((PlayData) new PlayDataDef(this.sixTwoForm, "OUTSIDE SWAP", PlayConcept.Man_Blitz, this.manCoverage, this.linemanHole3, this.linemanHole1, this.linemanHole2, this.linemanHole4, this.manCoverage, this.manCoverage, this.manCoverage, this.blitzHole9, this.manCoverage, this.blitzHole8));
    this.sixTwoPlays.AddPlay((PlayData) new PlayDataDef(this.sixTwoForm, "SPLIT FIRE", PlayConcept.Zone_Blitz, this.linemanHole5, this.linemanHole3, this.linemanHole1, this.linemanHole2, this.linemanHole4, this.linemanHole8, this.blitzHole1, this.blitzHole8, this.deepZone1of3, this.deepZone2of3, this.deepZone3of3));
    this.sixTwoPlays.AddPlay((PlayData) new PlayDataDef(this.sixTwoForm, "SHORT COVER 3", PlayConcept.Cover_Three, this.midZone1of4, this.linemanHole3, this.linemanHole1, this.linemanHole2, this.linemanHole4, this.midZone4of4, this.midZone2of4, this.midZone3of4, this.deepZone1of3, this.deepZone2of3, this.deepZone3of3));
    this.sixTwoPlays.AddPlay((PlayData) new PlayDataDef(this.sixTwoForm, "COVER 2 LEVEL SWITCH", PlayConcept.Zone_Blitz, this.midZone1of3, this.linemanHole3, this.linemanHole1, this.linemanHole2, this.linemanHole4, this.midZone3of3, this.blitzHole3, this.blitzHole4, this.deepZone1of2, this.midZone2of3, this.deepZone2of2));
    this.sixTwoPlays.AddPlay((PlayData) new PlayDataDef(this.sixTwoForm, "BLANKET COVER 3", PlayConcept.Cover_Three, this.flatZoneLeft, this.linemanHole3, this.linemanHole1, this.linemanHole2, this.linemanHole4, this.flatZoneRight, this.midZone1of2, this.midZone2of2, this.deepZone1of3, this.deepZone2of3, this.deepZone3of3));
    this.sixTwoPlays.AddPlay((PlayData) new PlayDataDef(this.sixTwoForm, "COVER 2 CENTER CRASH", PlayConcept.Zone_Blitz, this.flatZoneLeft, this.linemanHole3, this.linemanHole1, this.linemanHole2, this.linemanHole4, this.flatZoneRight, this.blitzHole1, this.blitzHole2, this.deepZone1of2, this.blitzHole0, this.deepZone2of2));
    this.sixTwoPlays.AddPlay((PlayData) new PlayDataDef(this.sixTwoForm, "SHORT COVER 4", PlayConcept.Cover_Four, this.flatZoneLeft, this.linemanHole3, this.linemanHole1, this.linemanHole2, this.linemanHole4, this.flatZoneRight, this.midZone2of4, this.midZone3of4, this.midZone1of4, this.deepZone1of1, this.midZone4of4));
    this.sixTwoPlays.AddPlay((PlayData) new PlayDataDef(this.sixTwoForm, "COVER 4 SAFETY BLITZ", PlayConcept.Zone_Blitz, this.linemanHole5, this.linemanHole3, this.linemanHole1, this.linemanHole2, this.linemanHole4, this.linemanHole8, this.midZone2of4, this.midZone3of4, this.midZone1of4, this.blitzHole0, this.midZone4of4));
    this.sixTwoPlays.AddPlay((PlayData) new PlayDataDef(this.sixTwoForm, "COVER 3 PERIMETER", PlayConcept.Cover_Three, this.linemanHole5, this.linemanHole3, this.linemanHole1, this.linemanHole2, this.linemanHole4, this.linemanHole8, this.deepZone1of3, this.deepZone3of3, this.flatZoneLeft, this.deepZone1of1, this.flatZoneRight));
    this.sixTwoPlays.AddPlay((PlayData) new PlayDataDef(this.sixTwoForm, "COVER 3 OUTSIDE PRESS", PlayConcept.Zone_Blitz, this.linemanHole5, this.linemanHole3, this.linemanHole1, this.linemanHole2, this.linemanHole4, this.linemanHole8, this.midZone1of3, this.midZone3of3, this.blitzHole9, this.midZone2of3, this.blitzHole10));
    this.sixTwoPlays.AddPlay((PlayData) new PlayDataDef(this.sixTwoForm, "SLASH", PlayConcept.Zone_Blitz, this.linemanHole5, this.linemanHole3, this.linemanHole1, this.linemanHole2, this.linemanHole4, this.linemanHole8, this.blitzHole7, this.midZone2of3, this.blitzHole7, this.midZone1of3, this.midZone3of3));
    this.sixTwoPlays.AddPlay((PlayData) new PlayDataDef(this.sixTwoForm, "STUNT RUSH", PlayConcept.Man_Coverage, this.stuntLeftIn, this.linemanHole5, this.linemanHole3, this.linemanHole2, this.linemanHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.blitzHole10, this.manCoverage));
    this.sixTwoPlays.AddPlay((PlayData) new PlayDataDef(this.sixTwoForm, "SLIP SWITCH", PlayConcept.Zone_Blitz, this.deepZone1of3, this.linemanHole3, this.linemanHole1, this.linemanHole2, this.linemanHole4, this.deepZone3of3, this.blitzHole7, this.blitzHole8, this.blitzHole7, this.deepZone1of1, this.blitzHole8));
    this.sixTwoPlays.AddPlay((PlayData) new PlayDataDef(this.sixTwoForm, "CENTER STUFF", PlayConcept.Man_Blitz, this.manCoverage, this.linemanHole3, this.linemanHole1, this.linemanHole2, this.linemanHole4, this.manCoverage, this.blitzHole1, this.blitzHole4, this.manCoverage, this.manCoverage, this.manCoverage));
  }

  private void SetPlays_ThreeFour()
  {
    this.threeFourPlays = new FormationData("3-4", FormationType.Defense, (FormationPositions) this.threeFourForm);
    this.threeFourPlays.AddPlay((PlayData) new PlayDataDef(this.threeFourForm, "BASE COVER 2", PlayConcept.Cover_Two, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.manCoverage, this.spyQB, this.manCoverage, this.manCoverage, this.manCoverage, this.deepZone2of2, this.deepZone1of2, this.manCoverage));
    this.threeFourPlays.AddPlay((PlayData) new PlayDataDef(this.threeFourForm, "BASE COVER 3", PlayConcept.Cover_Three, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.deepZone1of3, this.manCoverage, this.deepZone2of3, this.deepZone3of3));
    this.threeFourPlays.AddPlay((PlayData) new PlayDataDef(this.threeFourForm, "COVER 3 WEAK BLITZ", PlayConcept.Cover_Three, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.blitzHole5, this.midZone1of4, this.midZone2of4, this.midZone3of4, this.deepZone1of3, this.midZone4of4, this.deepZone2of3, this.deepZone3of3));
    this.threeFourPlays.AddPlay((PlayData) new PlayDataDef(this.threeFourForm, "COVER 3 STRONG BLITZ", PlayConcept.Cover_Three, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.midZone1of4, this.midZone2of4, this.midZone3of4, this.blitzHole6, this.deepZone1of3, this.midZone4of4, this.deepZone2of3, this.deepZone3of3));
    this.threeFourPlays.AddPlay((PlayData) new PlayDataDef(this.threeFourForm, "SHORT COVER 2", PlayConcept.Cover_Two, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.flatZoneLeft, this.midZone2of4, this.midZone3of4, this.flatZoneRight, this.midZone1of4, this.deepZone2of2, this.deepZone1of2, this.midZone4of4));
    this.threeFourPlays.AddPlay((PlayData) new PlayDataDef(this.threeFourForm, "COUNTER BLITZ", PlayConcept.Man_Blitz, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.manCoverage, this.blitzHole6, this.blitzHole7, this.blitzHole2, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.threeFourPlays.AddPlay((PlayData) new PlayDataDef(this.threeFourForm, "WEAK OUTSIDE BLITZ", PlayConcept.Man_Blitz, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.blitzHole5, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.deepZone2of2, this.deepZone1of2, this.manCoverage));
    this.threeFourPlays.AddPlay((PlayData) new PlayDataDef(this.threeFourForm, "STRONG OUTSIDE BLITZ", PlayConcept.Man_Blitz, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.manCoverage, this.manCoverage, this.blitzHole6, this.manCoverage, this.manCoverage, this.deepZone2of2, this.deepZone1of2, this.manCoverage));
    this.threeFourPlays.AddPlay((PlayData) new PlayDataDef(this.threeFourForm, "COUNTER SWEEP", PlayConcept.Man_Blitz, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.manCoverage, this.blitzHole7, this.manCoverage, this.blitzHole6, this.blitzHole7, this.manCoverage, this.manCoverage, this.manCoverage));
    this.threeFourPlays.AddPlay((PlayData) new PlayDataDef(this.threeFourForm, "SAFETY DRIVE", PlayConcept.Man_Blitz, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.manCoverage, this.blitzHole1, this.manCoverage, this.manCoverage, this.manCoverage, this.blitzHole6, this.blitzHole5, this.manCoverage));
    this.threeFourPlays.AddPlay((PlayData) new PlayDataDef(this.threeFourForm, "ZONE BLITZ", PlayConcept.Zone_Blitz, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.blitzHole5, this.midZone2of4, this.blitzHole2, this.midZone3of4, this.midZone1of4, this.deepZone2of2, this.deepZone1of2, this.midZone4of4));
    this.threeFourPlays.AddPlay((PlayData) new PlayDataDef(this.threeFourForm, "BARRIER", PlayConcept.Man_Zone_Double, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.blitzHole5, this.midZone2of3, this.manCoverage, this.blitzHole6, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.threeFourPlays.AddPlay((PlayData) new PlayDataDef(this.threeFourForm, "WEAK INSIDE BLITZ", PlayConcept.Man_Blitz, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.manCoverage, this.blitzHole1, this.manCoverage, this.manCoverage, this.manCoverage, this.deepZone2of2, this.deepZone1of2, this.manCoverage));
    this.threeFourPlays.AddPlay((PlayData) new PlayDataDef(this.threeFourForm, "STRONG INSIDE BLITZ", PlayConcept.Man_Blitz, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.manCoverage, this.manCoverage, this.blitzHole2, this.manCoverage, this.manCoverage, this.deepZone2of2, this.deepZone1of2, this.manCoverage));
    this.threeFourPlays.AddPlay((PlayData) new PlayDataDef(this.threeFourForm, "WEAK FLOOD", PlayConcept.Man_Blitz, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.blitzHole5, this.blitzHole3, this.midZone2of3, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.threeFourPlays.AddPlay((PlayData) new PlayDataDef(this.threeFourForm, "STRONG FLOOD", PlayConcept.Man_Blitz, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.manCoverage, this.midZone2of3, this.blitzHole4, this.blitzHole6, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.threeFourPlays.AddPlay((PlayData) new PlayDataDef(this.threeFourForm, "QB SPY COVER 4", PlayConcept.Cover_Four, this.linemanHole3, this.linemanHole0, this.linemanHole4, this.midZone1of3, this.midZone2of3, this.spyQB, this.midZone3of3, this.deepZone1of4, this.deepZone3of4, this.deepZone2of4, this.deepZone4of4));
  }

  private void SetPlays_TwoFourNickel()
  {
    this.twoFourNickelPlays = new FormationData("2-4 NICKEL", FormationType.Defense, (FormationPositions) this.twoFourNickelForm);
    this.twoFourNickelPlays.AddPlay((PlayData) new PlayDataDef(this.twoFourNickelForm, "BASE MAN", PlayConcept.Man_Coverage, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.spyQB, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.deepZone1of1, this.manCoverage));
    this.twoFourNickelPlays.AddPlay((PlayData) new PlayDataDef(this.twoFourNickelForm, "BASE COVER 3", PlayConcept.Cover_Three, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.flatZoneLeft, this.flatZoneRight, this.midZone2of4, this.deepZone1of3, this.midZone3of4, this.deepZone2of3, this.deepZone3of3));
    this.twoFourNickelPlays.AddPlay((PlayData) new PlayDataDef(this.twoFourNickelForm, "COVER 3 INSIDE STUFF", PlayConcept.Zone_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole3, this.blitzHole4, this.midZone1of2, this.deepZone1of3, this.midZone2of2, this.deepZone2of3, this.deepZone3of3));
    this.twoFourNickelPlays.AddPlay((PlayData) new PlayDataDef(this.twoFourNickelForm, "BASE COVER 4", PlayConcept.Cover_Four, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone2of3, this.midZone3of3, this.midZone1of3, this.deepZone1of4, this.deepZone3of4, this.deepZone2of4, this.deepZone4of4));
    this.twoFourNickelPlays.AddPlay((PlayData) new PlayDataDef(this.twoFourNickelForm, "BACKSLIDE", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole3, this.manCoverage, this.blitzHole7, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.twoFourNickelPlays.AddPlay((PlayData) new PlayDataDef(this.twoFourNickelForm, "COVER 3 BACK BLITZ", PlayConcept.Cover_Three, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.flatZoneLeft, this.flatZoneRight, this.blitzHole5, this.deepZone1of3, this.blitzHole6, this.deepZone2of3, this.deepZone3of3));
    this.twoFourNickelPlays.AddPlay((PlayData) new PlayDataDef(this.twoFourNickelForm, "MID COVER", PlayConcept.Man_Zone_Double, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone1of3, this.midZone3of3, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
    this.twoFourNickelPlays.AddPlay((PlayData) new PlayDataDef(this.twoFourNickelForm, "SAFETY SQUEEZE", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.blitzHole4, this.manCoverage, this.manCoverage, this.blitzHole6, this.manCoverage, this.manCoverage));
    this.twoFourNickelPlays.AddPlay((PlayData) new PlayDataDef(this.twoFourNickelForm, "COVER 4 MAX", PlayConcept.Cover_Four, this.flatZoneLeft, this.linemanHole3, this.linemanHole4, this.flatZoneRight, this.midZone2of3, this.midZone3of3, this.midZone1of3, this.deepZone1of4, this.deepZone3of4, this.deepZone2of4, this.deepZone4of4));
    this.twoFourNickelPlays.AddPlay((PlayData) new PlayDataDef(this.twoFourNickelForm, "BASE COVER 2", PlayConcept.Cover_Two, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.deepZone2of2, this.deepZone1of2, this.manCoverage));
    this.twoFourNickelPlays.AddPlay((PlayData) new PlayDataDef(this.twoFourNickelForm, "COVER 4 WEAK BLITZ", PlayConcept.Cover_Four, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.midZone3of3, this.blitzHole5, this.midZone2of3, this.midZone1of3, this.deepZone1of4, this.deepZone3of4, this.deepZone2of4, this.deepZone4of4));
    this.twoFourNickelPlays.AddPlay((PlayData) new PlayDataDef(this.twoFourNickelForm, "CORNER BLITZ", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.blitzHole9, this.manCoverage, this.manCoverage, this.blitzHole10));
    this.twoFourNickelPlays.AddPlay((PlayData) new PlayDataDef(this.twoFourNickelForm, "SPLITSAW", PlayConcept.Cover_Two, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.midZone2of3, this.blitzHole8, this.midZone1of3, this.blitzHole7, this.deepZone2of2, this.deepZone1of2, this.midZone3of3));
    this.twoFourNickelPlays.AddPlay((PlayData) new PlayDataDef(this.twoFourNickelForm, "COVER 2 WEAK COLLAPSE", PlayConcept.Cover_Two, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole1, this.midZone2of3, this.blitzHole3, this.midZone1of3, this.deepZone2of2, this.deepZone1of2, this.midZone3of3));
    this.twoFourNickelPlays.AddPlay((PlayData) new PlayDataDef(this.twoFourNickelForm, "SAFETY BLITZ", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.blitzHole6, this.blitzHole5, this.manCoverage));
    this.twoFourNickelPlays.AddPlay((PlayData) new PlayDataDef(this.twoFourNickelForm, "INSIDE BLITZ", PlayConcept.Man_Blitz, this.linemanHole7, this.linemanHole3, this.linemanHole4, this.linemanHole8, this.blitzHole3, this.blitzHole4, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage, this.manCoverage));
  }

  private void SetPlays_Empty_FlexTrips()
  {
    this.emptyPlays_FlexTrips = new FormationData(FormationType.Offense, this.empty_FlexTrips);
    this.emptyPlays_FlexTrips.AddPlay((PlayData) new PlayDataOff(this.empty_FlexTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "CROSS RIGHT", PlayConcept.Mid_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.corner10flat, this.out5, this.post5skinny, this.in5, this.post5flat));
    this.emptyPlays_FlexTrips.AddPlay((PlayData) new PlayDataOff(this.empty_FlexTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 10, "INSIDE OUT", PlayConcept.Short_Pass, 0, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.out10, this.fly, this.in5, this.in5, this.out5));
    this.emptyPlays_FlexTrips.AddPlay((PlayData) new PlayDataOff(this.empty_FlexTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "FAKE SHUFFLE FLATS", PlayConcept.Mid_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.post5flat, this.fly, this.post5, this.in10, this.underIn));
    this.emptyPlays_FlexTrips.AddPlay((PlayData) new PlayDataOff(this.empty_FlexTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "SWING OUTS", PlayConcept.Short_Pass, 0, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.underOut, this.slantIn, this.post5flat, this.post5skinny, this.underOut));
    this.emptyPlays_FlexTrips.AddPlay((PlayData) new PlayDataOff(this.empty_FlexTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.SevenStep, HandoffType.None, 9, "SEAM ATTACK", PlayConcept.Deep_Pass, 0, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.slantInFly, this.out10, this.slantOutFly, this.slantOutFly, this.slantInFly));
    this.emptyPlays_FlexTrips.AddPlay((PlayData) new PlayDataOff(this.empty_FlexTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "REWIND", PlayConcept.Mid_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.corner10flat, this.slantIn, this.post5, this.post5, this.underOutAndUp));
    this.emptyPlays_FlexTrips.AddPlay((PlayData) new PlayDataOff(this.empty_FlexTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.SevenStep, HandoffType.None, 10, "FLYBIRD", PlayConcept.Deep_Pass, 0, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.slantInFly, this.hitch10in, this.in10, this.fly, this.upOutFly));
    this.emptyPlays_FlexTrips.AddPlay((PlayData) new PlayDataOff(this.empty_FlexTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "DELAYED FLAT", PlayConcept.Short_Pass, 0, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.upPostOut, this.in10, this.fly, this.post10, this.upPostOut));
    this.emptyPlays_FlexTrips.AddPlay((PlayData) new PlayDataOff(this.empty_FlexTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "SHORT CROSS", PlayConcept.Short_Pass, 0, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.dragOut, this.slantInIn, this.in5, this.slantIn, this.slantIn));
    this.emptyPlays_FlexTrips.AddPlay((PlayData) new PlayDataOff(this.empty_FlexTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "TRAIL UNDER", PlayConcept.Short_Pass, 0, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.underIn, this.corner10, this.hitch10in, this.upPostHitch, this.post5));
    this.emptyPlays_FlexTrips.AddPlay((PlayData) new PlayDataOff(this.empty_FlexTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 10, "ROTATE", PlayConcept.Mid_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.corner10, this.hitch5in, this.hitch10in, this.post5flat, this.fly));
    this.emptyPlays_FlexTrips.AddPlay((PlayData) new PlayDataOff(this.empty_FlexTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "UNDER CORNER", PlayConcept.Mid_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.underOutUpCorner, this.fly, this.fly, this.post5flat, this.underOutUpCorner));
    this.emptyPlays_FlexTrips.AddPlay((PlayData) new PlayDataOff(this.empty_FlexTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "PHASE SHIFT", PlayConcept.Mid_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.upCornerFly, this.slantInUpHitchOut, this.post5flat, this.in5, this.upOutFly));
  }

  private void SetPlays_Empty_TreyOpen()
  {
    this.emptyPlays_TreyOpen = new FormationData(FormationType.Offense, this.empty_TreyOpen);
    this.emptyPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.empty_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "CROSS RIGHT", PlayConcept.Mid_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.corner10flat, this.out5, this.post5skinny, this.in5, this.post5flat));
    this.emptyPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.empty_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 10, "INSIDE OUT", PlayConcept.Short_Pass, 0, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.out10, this.fly, this.in5, this.in5, this.out5));
    this.emptyPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.empty_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 10, "ROTATE", PlayConcept.Mid_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.corner10, this.hitch5in, this.hitch10in, this.post5flat, this.fly));
  }

  private void SetPlays_Goalline_Heavy()
  {
    this.goallinePlays_Heavy = new FormationData(FormationType.Offense, this.goalline_Heavy);
    this.goallinePlays_Heavy.AddPlay((PlayData) new PlayDataOff(this.goalline_Heavy, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "RB DIVE", PlayConcept.GL_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbDiveOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.goallinePlays_Heavy.AddPlay((PlayData) new PlayDataOff(this.goalline_Heavy, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "CORNERS", PlayConcept.GL_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatIn, this.corner5, this.corner5flat, this.in5, this.rbFlatOut_Fly));
    this.goallinePlays_Heavy.AddPlay((PlayData) new PlayDataOff(this.goalline_Heavy, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA SEAM", PlayConcept.GL_Run, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.slantIn, this.corner10flat, this.post5, this.rbFlatOut));
    this.goallinePlays_Heavy.AddPlay((PlayData) new PlayDataOff(this.goalline_Heavy, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "QUICK SLANTS", PlayConcept.GL_Pass, 0, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatIn_Fly, this.corner5, this.slantIn, this.hitch10in, this.rbFlatOut_Fly));
    this.goallinePlays_Heavy.AddPlay((PlayData) new PlayDataOff(this.goalline_Heavy, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "COUNTER LEAD", PlayConcept.GL_Run, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockIn));
    this.goallinePlays_Heavy.AddPlay((PlayData) new PlayDataOff(this.goalline_Heavy, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION", PlayConcept.GL_Run, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.goallinePlays_Heavy.AddPlay((PlayData) new PlayDataOff(this.goalline_Heavy, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "FADE", PlayConcept.GL_Pass, 0, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatOut, this.slantIn, this.slantOut, this.out10, this.isoBlockOut));
  }

  private void SetPlays_HailMary_Normal()
  {
    this.hailMaryPlays_Normal = new FormationData(FormationType.Offense, this.hailMary_Normal);
    this.hailMaryPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.hailMary_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "HAIL MARY", PlayConcept.Hail_Mary, 0, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.passBlockRB, this.post5skinny, this.fly, this.fly, this.fly));
  }

  private void SetPlays_IForm_Normal()
  {
    this.iFormPlays_Normal = new FormationData(FormationType.Offense, this.iForm_Normal);
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "RB LEAD", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockIn));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.passBlockLT_PA_Right, this.passBlockLG_PA_Right, this.passBlockC_PA_Right, this.passBlockRG_PA_Right, this.passBlockRT_PA_Right, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post10, this.in15, this.rbScreenOut));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "TE UNDER", PlayConcept.Short_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.in5, this.post10flat, this.fly, this.fbDiveIn_In));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "DEUCE", PlayConcept.Short_Pass, 0, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatIn_Fly, this.out5, this.in10, this.slantIn, this.passBlockFB_GoLeft));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "SWING BACKS", PlayConcept.Short_Pass, 0, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatIn, this.corner10, this.post5, this.in5, this.rbFlatOut));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SHUTTER", PlayConcept.Short_Pass, 0, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatIn, this.hitch10in, this.slantIn, this.post5skinny, this.passBlockFB_GoLeft));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA X COMEBACK", PlayConcept.Play_Action, 6, this.passBlockLT_PA_Right, this.passBlockLG_PA_Right, this.passBlockC_PA_Right, this.passBlockRG_PA_Right, this.passBlockRT_PA_Right, this.qbIsoIn, this.rbIsoIn_In, this.upOutFly, this.upPostHitch, this.slantIn, this.rbFlatOut));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Mid_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbOffTackleOut_In, this.out5, this.in10, this.slantInUpCorner, this.passBlockFB_GoLeft));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "CROSS IN", PlayConcept.Short_Pass, 0, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatIn, this.slantInIn, this.in10, this.slantInUpPost, this.passBlockFB_GoLeft));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURLS", PlayConcept.Mid_Pass, 0, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatIn, this.slantInHitch, this.hitch10in, this.hitch10in, this.rbFlatOut));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "RB BLAST", PlayConcept.Outside_Run, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbOffTackleOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB COUNTER", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.pullBlockIn, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "MID ATTACK", PlayConcept.Mid_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.passBlockRB, this.post10, this.out10, this.hitch10in, this.rbOffTackleOut_In));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA BOOT", PlayConcept.Play_Action, 6, this.passBlockLT_PA_Left, this.passBlockLG_PA_Left, this.passBlockC_PA_Left, this.passBlockRG_PA_Left, this.passBlockRT_PA_Left, this.qbIsoOut, this.rbIsoIn_In, this.underOut, this.slantInIn, this.hitch10out, this.passBlockFB_GoLeft));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA CANE", PlayConcept.Play_Action, 6, this.passBlockLT_PA_Right, this.passBlockLG_PA_Right, this.passBlockC_PA_Right, this.passBlockRG_PA_Right, this.passBlockRT_PA_Right, this.qbIsoIn, this.rbIsoIn_Out, this.post5, this.in5, this.post10, this.rbFlatIn));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA CLEAROUT", PlayConcept.Play_Action, 6, this.passBlockLT_PA_Left, this.passBlockLG_PA_Left, this.passBlockC_PA_Left, this.passBlockRG_PA_Left, this.passBlockRT_PA_Left, this.qbIsoOut, this.rbIsoOut_Out, this.fly, this.fly, this.fly, this.rbFlatIn));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA Z UNDER", PlayConcept.Play_Action, 6, this.passBlockLT_PA_Right, this.passBlockLG_PA_Right, this.passBlockC_PA_Right, this.passBlockRG_PA_Right, this.passBlockRT_PA_Right, this.qbIsoIn, this.rbIsoIn_In, this.corner10, this.post5, this.dragIn, this.rbFlatIn));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA POST WHEEL", PlayConcept.Play_Action, 6, this.passBlockLT_PA_Right, this.passBlockLG_PA_Right, this.passBlockC_PA_Right, this.passBlockRG_PA_Right, this.passBlockRT_PA_Right, this.qbIsoIn, this.rbIsoIn_In, this.in5, this.post10, this.underIn, this.rbFlatOut_Fly));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA STREAKS", PlayConcept.Play_Action, 6, this.passBlockLT_PA_Left, this.passBlockLG_PA_Left, this.passBlockC_PA_Left, this.passBlockRG_PA_Left, this.passBlockRT_PA_Left, this.qbIsoOut, this.rbIsoOut_Out, this.post5, this.fly, this.fly, this.passBlockFB_GoLeft));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.SevenStep, HandoffType.None, 9, "POST FLAGS", PlayConcept.Deep_Pass, 0, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn_Fly, this.post10, this.post5Corner, this.post5Corner, this.rbFlatOut));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "POWER O", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockIn));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, 0, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatOut_Fly, this.underOut, this.slantIn, this.slantIn, this.passBlockFB_GoLeft));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SEAM 678", PlayConcept.Mid_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.corner5, this.in5, this.post10, this.rbFlatIn));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS", PlayConcept.Mid_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatIn, this.slantInHitch, this.post5skinny, this.post5skinny, this.passBlockFB_GoLeft));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLANT AND GO", PlayConcept.Mid_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut_Fly, this.in5, this.slantInFly, this.slantInFly, this.rbFlatIn));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SLANTS", PlayConcept.Short_Pass, 0, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatIn, this.slantIn, this.post5flat, this.post5flat, this.passBlockFB_GoLeft));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.SevenStep, HandoffType.None, 9, "SLUGGOS", PlayConcept.Deep_Pass, 0, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut_Fly, this.fly, this.slantInFly, this.post5Corner, this.passBlockFB_GoLeft));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.in10, this.passBlockFB_GoLeft));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.SevenStep, HandoffType.None, 8, "CORNER FADES", PlayConcept.Deep_Pass, 0, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.fly, this.upCornerFly, this.upCornerFly, this.passBlockFB_GoLeft));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockIn));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB ISO WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.SevenStep, HandoffType.None, 9, "SHORT FADES", PlayConcept.Deep_Pass, 0, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.post10skinny, this.upCornerFly, this.upCornerFly, this.passBlockFB_GoLeft));
    this.iFormPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.iForm_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS TE DRAG", PlayConcept.Mid_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.post5skinny, this.post5skinny, this.passBlockFB_GoLeft));
  }

  private void SetPlays_iForm_SlotFlex()
  {
    this.iFormPlays_SlotFlex = new FormationData(FormationType.Offense, this.iForm_SlotFlex);
    this.iFormPlays_SlotFlex.AddPlay((PlayData) new PlayDataOff(this.iForm_SlotFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "BENCH", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.in10, this.out10, this.out10, this.diveBlockOut));
    this.iFormPlays_SlotFlex.AddPlay((PlayData) new PlayDataOff(this.iForm_SlotFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CURLS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInHitch, this.hitch10in, this.hitch10in, this.diveBlockOut));
    this.iFormPlays_SlotFlex.AddPlay((PlayData) new PlayDataOff(this.iForm_SlotFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 10, "FB DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbFBDiveIn, this.rbSweepBlockOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.fbDiveIn));
    this.iFormPlays_SlotFlex.AddPlay((PlayData) new PlayDataOff(this.iForm_SlotFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 10, "FLANKER DIG", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbDiveIn_In, this.fly, this.fly, this.in10, this.fbDiveIn_Out));
    this.iFormPlays_SlotFlex.AddPlay((PlayData) new PlayDataOff(this.iForm_SlotFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "GO'S RB OUT", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbDiveIn_In, this.post10, this.fly, this.fly, this.isoBlockOut));
    this.iFormPlays_SlotFlex.AddPlay((PlayData) new PlayDataOff(this.iForm_SlotFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "RB BLAST", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbIsoOut, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.iFormPlays_SlotFlex.AddPlay((PlayData) new PlayDataOff(this.iForm_SlotFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SLIP SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.pullBlockOut, this.pullBlockOut, this.qbPassPlay, this.rbScreenIn, this.corner10, this.in10, this.underInUpPost, this.diveBlockIn));
    this.iFormPlays_SlotFlex.AddPlay((PlayData) new PlayDataOff(this.iForm_SlotFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA POWER O", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner10flat, this.slantIn, this.fly, this.rbFlatIn));
    this.iFormPlays_SlotFlex.AddPlay((PlayData) new PlayDataOff(this.iForm_SlotFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA SCISSORS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.corner10, this.in10, this.post10, this.rbFlatIn));
    this.iFormPlays_SlotFlex.AddPlay((PlayData) new PlayDataOff(this.iForm_SlotFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "POWER O", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.rbSweepBlockIn));
    this.iFormPlays_SlotFlex.AddPlay((PlayData) new PlayDataOff(this.iForm_SlotFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SE POST FLAG", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockIn, this.post5, this.post5Corner, this.slantInFly, this.rbFlatOut));
    this.iFormPlays_SlotFlex.AddPlay((PlayData) new PlayDataOff(this.iForm_SlotFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLANTS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockIn, this.slantIn, this.slantIn, this.post5flat, this.rbFlatOut));
    this.iFormPlays_SlotFlex.AddPlay((PlayData) new PlayDataOff(this.iForm_SlotFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SMASH", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbOffTackleOut_In, this.corner5, this.in10, this.hitch10in, this.isoBlockIn));
    this.iFormPlays_SlotFlex.AddPlay((PlayData) new PlayDataOff(this.iForm_SlotFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "WR DRAG", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbDiveIn_In, this.in10, this.post5, this.slantInIn, this.diveBlockOut));
    this.iFormPlays_SlotFlex.AddPlay((PlayData) new PlayDataOff(this.iForm_SlotFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.post5flat, this.isoBlockIn));
  }

  private void SetPlays_IForm_Tight()
  {
    this.iFormPlays_Tight = new FormationData(FormationType.Offense, this.iForm_Tight);
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "MISDIRECTION", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.runBlockT, this.qbCounterOut, this.rbCounterOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockIn));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 10, "FB SMASH", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbFBDiveIn, this.rbSweepBlockOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.fbDiveIn));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "RB DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbDiveOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "CORNERS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner5, this.corner5flat, this.in5, this.rbFlatOut_Fly));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA SEAM", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.fly, this.corner10flat, this.post5, this.rbFlatOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 8, "PA FLEX", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.post5flat, this.out5, this.fly, this.rbFlatIn));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "SPLITS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantOut, this.in5, this.slantIn, this.rbFlatOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "INSIDE TRAP", PlayConcept.Inside_Run_With_Pulling_OL, this.runPos_actualPosition, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbDiveIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "CROSS HITCH", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.passBlockRB, this.in5, this.hitch10in, this.hitch10in, this.fbDiveIn_In));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 10, "FB SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.isoBlockIn, this.fly, this.runBlockWR, this.runBlockWR, this.rbScreenIn));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "WR UNDER SCREEN", PlayConcept.Screen_Pass, this.runPos_screenLeft, this.screenBlockOLOut, this.screenBlockOLOut, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.post5, this.slantIn, this.underIn, this.diveBlockOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "ISO FLAT", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantIn, this.post5, this.underIn, this.isoBlockIn));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "ANGLE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.corner5, this.out10, this.hitch10in, this.rbFlatOut_Fly));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "COUNTER LEAD", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockIn));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "GOALLINE FADE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB BLAST", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION", PlayConcept.Misdirection, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "INSIDE CROSS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.slantInIn, this.post5flat, this.in10, this.isoBlockOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "MID ATTACK", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.fly, this.hitch10in, this.hitch10in, this.rbFlatOut_Fly));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 8, "PA BOOT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.isoBlockOut, this.dragOut, this.slantInPostFlat, this.fly, this.isoBlockIn));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA BOOT SLIDE", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.slantInPostFlat, this.runBlockWR, this.slantInHighPost, this.rbFlatOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA SPLIT WAGGLE", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.dragOut, this.slantInPostFlat, this.corner10, this.isoBlockOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 7, "PA FB SLIDE", PlayConcept.Play_Action, 10, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbFBDiveIn, this.rbFlatOut, this.slantInIn, this.runBlockTE, this.slantInHighPost, this.diveBlockIn));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole1, 6, "PA RB WHEEL", PlayConcept.Play_Action, 10, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbFBDiveOut, this.rbFlatOut_Fly, this.dragOut, this.slantInHighPostFlat, this.fly, this.diveBlockOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA POWER O", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner10, this.in5, this.fly, this.rbFlatOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA SPLIT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.hitch10in, this.corner10, this.post5Corner, this.isoBlockOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA TE CORNER", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.corner10, this.post10, this.fly, this.rbFlatOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "POWER O", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.rbSweepBlockIn));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "SLANT", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragOut, this.hitch5out, this.slantIn, this.isoBlockOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE SLANT", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantIn, this.out5, this.out10, this.isoBlockOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "WR POST", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.hitch10in, this.corner10, this.post10, this.isoBlockOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "WEAK ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Z SLANT", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragOut, this.out5, this.slantIn, this.isoBlockOut));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "INSIDE ZONE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockIn));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbDiveIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockIn));
    this.iFormPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.iForm_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "TE SHAKE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbOffTackleIn_In, this.slantInHighPostFlat, this.out5, this.out10, this.isoBlockOut));
  }

  private void SetPlays_IForm_Twins()
  {
    this.iFormPlays_Twins = new FormationData(FormationType.Offense, this.iForm_Twins);
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "BUBBLE SCREEN", PlayConcept.Screen_Pass, this.runPos_screenLeft, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockIn, this.passBlockTE, this.runBlockWR, this.underOut, this.isoBlockIn));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "DEEP COMEBACK", PlayConcept.Deep_Pass, this.runPos_screenLeft, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantInHitch, this.hitch10out, this.post5, this.isoBlockIn));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "DOUBLE SLANT", PlayConcept.Short_Pass, this.runPos_screenLeft, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.slantOut, this.slantIn, this.slantIn, this.isoBlockIn));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 10, "FB DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbFBDiveIn, this.rbSweepBlockOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.fbDiveIn));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "DRIVE", PlayConcept.Short_Pass, this.runPos_screenLeft, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.out5, this.dragIn, this.in10, this.isoBlockIn));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "FB FLARE", PlayConcept.Short_Pass, this.runPos_screenLeft, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner10, this.in5, this.post10skinny, this.rbFlatIn));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "FADE SMASH", PlayConcept.Deep_Pass, this.runPos_screenLeft, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.post10, this.hitch5in, this.fly, this.isoBlockOut));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "FLANKER DIG", PlayConcept.Mid_Pass, this.runPos_screenLeft, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragIn, this.in10, this.fly, this.isoBlockOut));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "FORK", PlayConcept.Mid_Pass, this.runPos_screenLeft, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_In, this.in5, this.post5skinny, this.corner10, this.isoBlockOut));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB BLAST", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB COUNTER", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.runBlockT, this.qbCounterOut, this.rbCounterOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.rbSweepBlockOut));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbDiveOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION", PlayConcept.Misdirection, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.rbSweepBlockOut));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SLIP SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenIn, this.fly, this.fly, this.fly, this.isoBlockIn));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "INSIDE CROSS", PlayConcept.Mid_Pass, this.runPos_screenLeft, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_In, this.corner5, this.post10skinny, this.slantInIn, this.rbIsoOut_Out));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockIn));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA STRETCH", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner5flat, this.post10, this.post5flat, this.rbFlatIn));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA POWER O", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner5flat, this.hitch10in, this.dragInFromSlot, this.rbFlatIn));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA TE LEAK", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.corner5flat, this.fly, this.post5flat, this.rbFlatIn));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA TE OUT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.out5, this.dragIn, this.in10, this.rbFlatIn));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB POWER", PlayConcept.Inside_Run_With_Pulling_OL, this.runPos_actualPosition, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockIn));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SAIL", PlayConcept.Mid_Pass, this.runPos_screenLeft, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_In, this.in5, this.corner10, this.post5skinny, this.isoBlockOut));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLANTS", PlayConcept.Short_Pass, this.runPos_screenLeft, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner5, this.slantIn, this.slantIn, this.rbFlatOut));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "WHEEL OUT", PlayConcept.Mid_Pass, this.runPos_screenLeft, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in5, this.slantInHighPost, this.underOutAndUp, this.isoBlockOut));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "Corner Fly", PlayConcept.Short_Pass, this.runPos_screenLeft, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragInFromSlot, this.upCornerFly, this.dragOutFromSlot, this.isoBlockIn));
    this.iFormPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.iForm_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "TE Corner Fly", PlayConcept.Short_Pass, this.runPos_screenLeft, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.upCornerFly, this.hitch5out, this.corner10, this.isoBlockIn));
  }

  private void SetPlays_IForm_TwinTE()
  {
    this.iFormPlays_TwinTE = new FormationData(FormationType.Offense, this.iForm_TwinTE);
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "COUNTER WEAK", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.runBlockT, this.qbCounterOut, this.rbCounterOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "CURLS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantInHitch, this.hitch10in, this.hitch10in, this.rbFlatIn));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 10, "FB DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbFBDiveIn, this.rbSweepBlockOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.fbDiveIn));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "GOALLINE FADE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.fly, this.fly, this.slantInIn, this.isoBlockIn));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB STRETCH", PlayConcept.Outside_Run, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockTE, this.rbSweepBlockIn));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockTE, this.isoBlockIn));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA FLOW", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.slantInIn, this.hitch10out, this.post10, this.rbFlatOut));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 7, "PA CLEAROUT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_In, this.out5, this.in10, this.fly, this.rbFlatOut));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA POWER O", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner10flat, this.post10, this.in10, this.rbFlatIn));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA SPOT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner10, this.post10, this.hitch10in, this.rbFlatIn));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA TE LEAK", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner5flat, this.post10, this.slantInIn, this.rbFlatIn));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "WR SLUGGO", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.corner10, this.post5Corner, this.hitch10out, this.rbFlatIn));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "ISO WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockTE, this.isoBlockOut));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "POWER O", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockTE, this.rbSweepBlockIn));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "TE POST", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.post10, this.out5, this.dragInFromSlot, this.isoBlockOut));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "WR CORNER", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbOffTackleIn_In, this.underInUpPost, this.corner10, this.hitch5out, this.isoBlockOut));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "WR FADE", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.fly, this.fly, this.dragInFromSlot, this.isoBlockOut));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "WEAK ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockTE, this.isoBlockOut));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X SLANT", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.out5, this.slantIn, this.dragOutFromSlot, this.isoBlockOut));
    this.iFormPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.iForm_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "ZONE WEAK", PlayConcept.Outside_Run, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbOffTackleOut, this.runBlockTE, this.runBlockWR, this.runBlockTE, this.rbSweepBlockOut));
  }

  private void SetPlays_IForm_YTrips()
  {
    this.iFormPlays_YTrips = new FormationData(FormationType.Offense, this.iForm_YTrips);
    this.iFormPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.iForm_YTrips, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 10, "FB DIVE STRONG", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbFBDiveIn, this.isoBlockOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.fbDiveIn));
    this.iFormPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.iForm_YTrips, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "RB BLAST", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.rbSweepBlockOut));
    this.iFormPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.iForm_YTrips, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "RB COUNTER WEAK", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.runBlockT, this.qbCounterOut, this.rbCounterOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.iFormPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.iForm_YTrips, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "RB ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockIn));
    this.iFormPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.iForm_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "RB SLIP SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenIn, this.runBlockTE, this.fly, this.fly, this.rbFlatOut_Fly));
    this.iFormPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.iForm_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "INSIDE CROSS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbDiveIn_In, this.passBlockTE, this.in10, this.post10, this.fbDiveIn_Out));
    this.iFormPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.iForm_YTrips, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA FLOOD STRONG", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.isoBlockOut, this.runBlockTE, this.post5Out, this.slantInUpCorner, this.fbDiveIn_In));
    this.iFormPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.iForm_YTrips, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 8, "PA BOOT RT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.isoBlockOut, this.runBlockTE, this.post5Corner, this.post10, this.fbDiveIn_Out));
    this.iFormPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.iForm_YTrips, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "POWER O", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.rbSweepBlockIn));
    this.iFormPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.iForm_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SAIL", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.passBlockTE, this.slantInUpCorner, this.fly, this.fbDiveIn_In));
    this.iFormPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.iForm_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.passBlockTE, this.slantIn, this.slantIn, this.diveBlockOut));
  }

  private void SetPlays_OffSpecialTeams()
  {
    this.specialOffPlays = new FormationData("SPECIAL TEAMS", FormationType.OffSpecial, this.fieldGoalForm);
    this.kickoffPlays = new FormationData("KICKOFF", FormationType.Kickoff, this.kickoffForm);
    this.clockManagementPlays = new FormationData(Plays.CLOCK_CONTROL_FORMATION_GROUP_NAME, FormationType.Offense, this.singleBack_Big);
    this.clockManagementPlays.AddPlay((PlayData) new PlayDataOff(this.qbKneel_Normal, PlayType.Run, PlayTypeSpecific.QB_Kneel, DropbackType.Kneel, HandoffType.None, 5, "QB KNEEL", PlayConcept.Clock_Control, 0, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbKneel, this.isoBlockIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.clockManagementPlays.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.QB_Spike, DropbackType.Spike, HandoffType.None, 5, "QB SPIKE", PlayConcept.Clock_Control, 0, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbKneel, this.isoBlockIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.spc_kickoffMid = new PlayDataOff(this.kickoffForm, PlayType.Kickoff, PlayTypeSpecific.NormalKick, DropbackType.OneStep, HandoffType.None, 10, "KICKOFF MID", PlayConcept.Kickoff, 0, this.kickoffMid, this.kickoffIn, this.kickoffLeft, this.kickoffIn, this.kickoffLeft, this.kickoffLeft, this.kickoffKicker, this.kickoffMid, this.kickoffLeft, this.kickoffIn, this.kickoffMid);
    this.spc_kickoffRight = new PlayDataOff(this.kickoffForm, PlayType.Kickoff, PlayTypeSpecific.NormalKick, DropbackType.OneStep, HandoffType.None, 10, "KICKOFF RIGHT", PlayConcept.Kickoff, 0, this.kickoffIn, this.kickoffMid, this.kickoffLeft, this.kickoffMid, this.kickoffLeft, this.kickoffMid, this.kickoffKicker, this.kickoffLeft, this.kickoffLeft, this.kickoffIn, this.kickoffMid);
    this.spc_kickoffLeft = new PlayDataOff(this.kickoffForm, PlayType.Kickoff, PlayTypeSpecific.NormalKick, DropbackType.OneStep, HandoffType.None, 10, "KICKOFF LEFT", PlayConcept.Kickoff, 0, this.kickoffMid, this.kickoffLeft, this.kickoffMid, this.kickoffLeft, this.kickoffLeft, this.kickoffMid, this.kickoffKicker, this.kickoffLeft, this.kickoffMid, this.kickoffIn, this.kickoffIn);
    this.spc_onsideKick = new PlayDataOff(this.onsideKickForm, PlayType.Kickoff, PlayTypeSpecific.NormalKick, DropbackType.OneStep, HandoffType.None, 10, "ONSIDE KICK", PlayConcept.Kickoff, 0, this.kickoffMid, this.kickoffMid, this.kickoffMid, this.kickoffMid, this.kickoffMid, this.kickoffMid, this.kickoffKicker, this.kickoffMid, this.kickoffMid, this.kickoffMid, this.kickoffMid);
    this.spc_puntRight = new PlayDataOff(this.puntForm, PlayType.Punt, PlayTypeSpecific.NormalKick, DropbackType.OneStep, HandoffType.None, 10, "PUNT RIGHT", PlayConcept.Special_Teams, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.qbPassPlay, this.passBlockTE, this.puntGunner, this.puntGunner, this.passBlockTE);
    this.spc_puntLeft = new PlayDataOff(this.puntForm, PlayType.Punt, PlayTypeSpecific.NormalKick, DropbackType.OneStep, HandoffType.None, 10, "PUNT LEFT", PlayConcept.Special_Teams, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.qbPassPlay, this.passBlockTE, this.puntGunner, this.puntGunner, this.passBlockTE);
    this.spc_puntProtect = new PlayDataOff(this.puntForm, PlayType.Punt, PlayTypeSpecific.NormalKick, DropbackType.OneStep, HandoffType.None, 10, "PUNT PROTECT", PlayConcept.Special_Teams, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.qbPassPlay, this.passBlockTE, this.puntGunner, this.puntGunner, this.passBlockTE);
    this.spc_fieldGoal = new PlayDataOff(this.fieldGoalForm, PlayType.FG, PlayTypeSpecific.NormalKick, DropbackType.OneStep, HandoffType.None, 10, "FIELD GOAL", PlayConcept.Special_Teams, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, new PlayAssignment(EPlayAssignmentId.KickHolder, this.empty, new float[4]
    {
      1f,
      9f,
      -7f,
      -9f
    }), new PlayAssignment(EPlayAssignmentId.Kicker, this.empty, new float[4]
    {
      1f,
      5f,
      -7f,
      0.0f
    }), this.passBlockTE, this.passBlockTE, this.passBlockTE, this.passBlockTE);
    this.specialOffPlays.SetPlays(new List<PlayData>()
    {
      (PlayData) this.spc_fieldGoal,
      (PlayData) this.spc_puntProtect,
      (PlayData) this.spc_puntRight,
      (PlayData) this.spc_puntLeft
    });
    this.kickoffPlays.SetPlays(new List<PlayData>()
    {
      (PlayData) this.spc_kickoffMid,
      (PlayData) this.spc_kickoffRight,
      (PlayData) this.spc_kickoffLeft,
      (PlayData) this.spc_onsideKick
    });
  }

  private void SetPlays_Pistol_Ace()
  {
    this.pistolPlays_Ace = new FormationData(FormationType.Offense, this.pistol_Ace);
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "ACE POST", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragOut, this.post10, this.fly, this.dragOut));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "ACE TE DRAG", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.fly, this.post10, this.in10));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragOut, this.slantInUpCorner, this.slantInUpCorner, this.dragOut));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "CURL FLATS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragOut, this.hitch10out, this.hitch10out, this.dragOut));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "EMPTY TE CORNER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.corner10flat, this.hitch10out, this.dragInFromSlot, this.corner10));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "EMPTY TE POST", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.post10flat, this.fly, this.dragInFromSlot, this.dragOut));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "HB DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "FLANKER DRIVE", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatIn, this.in10, this.hitch10out, this.dragInFromSlot, this.out10));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole4, 6, "HB STRETCH", PlayConcept.Outside_Run, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole4, 6, "POWER", PlayConcept.Outside_Run, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "INSIDE CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragIn, this.in10, this.post10, this.dragIn));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "LEVELS DIVIDE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.post10skinny, this.dragInFromSlot, this.fly, this.in10));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "MTN HB SWING", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbScreenIn, this.corner10, this.in15, this.dragInFromSlot, this.in10));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 9, "PA GO", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbPistolPAIn, this.rbIsoIn_Fly, this.hitch10out, this.fly, this.fly, this.hitch10in));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 9, "PA CROSSES", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbPistolPAIn, this.rbIsoIn_In, this.dragOut, this.slantInHighPostFlat, this.slantInHighPostFlat, this.dragOut));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 9, "PA FLOOD", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbPistolPAIn, this.rbIsoIn_In, this.dragOut, this.hitch10out, this.slantInUpCorner, this.dragOut));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 7, "PA TE POST", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbPistolPAIn, this.rbIsoIn_In, this.post10flat, this.slantInUpCorner, this.post10skinny, this.out5));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole4, 6, "POWER-0", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatIn, this.dragOut, this.slantIn, this.slantIn, this.dragOut));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 7, "TE OUT", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.out5, this.slantInUpCorner, this.post10skinny, this.in10));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE SEAM", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.slantInFly, this.fly, this.fly, this.hitch10in));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "WR COMEBACKS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.passBlockRB, this.hitch10out, this.hitch10out, this.hitch10out, this.hitch10out));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X DIG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.out10, this.in10, this.slantOutFly, this.dragOut));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "84 X FLY", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.dragOut, this.slantOutFly, this.hitch10in, this.slantInHighPostFlat));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.out10, this.dragOut));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Y SHALLOW CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.in15, this.slantInHighPost, this.post5flat));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Z SPOT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.corner10, this.in10, this.slantInPostFlat, this.dragOut));
    this.pistolPlays_Ace.AddPlay((PlayData) new PlayDataOff(this.pistol_Ace, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole1, 6, "HB DIVE WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
  }

  private void SetPlays_Pistol_Bunch()
  {
    this.pistolPlays_Bunch = new FormationData(FormationType.Offense, this.pistol_Bunch);
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "ACE POSTS", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragInFromSlot, this.post10, this.post10, this.dragOutFromSlot));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "ACE TE DRAG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragInFromSlot, this.fly, this.post10, this.in10));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.post5, this.slantInUpCorner, this.slantInUpCorner, this.dragOut));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURL FLATS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.slantIn, this.hitch10out, this.fly, this.hitch10out));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "HB EMPTY BUBBLE", PlayConcept.Screen_Pass, this.runPos_screenRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPassPlay, this.rbScreenIn, this.runBlockTE, this.fly, this.post10, this.runBlockTE));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "EMPTY TE CORNER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner10flat, this.hitch10out, this.out10, this.post10skinny));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "EMPTY TE POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.post10flat, this.fly, this.dragInFromSlot, this.dragOut));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "HB DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "FLANKER DRIVE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in10, this.hitch10out, this.dragInFromSlot, this.fly));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "HB STRETCH", PlayConcept.Outside_Run, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "HB SLIP SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenIn, this.fly, this.fly, this.fly, this.fly));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "POWER", PlayConcept.Outside_Run, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "INSIDE CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragInFromSlot, this.hitch10out, this.post10, this.slantIn));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "LEVELS DIVIDE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.post10flat, this.dragInFromSlot, this.fly, this.post10skinny));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "MIAMI", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragInFromSlot, this.out10, this.upCornerFly, this.in10));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "MTN HB SWING", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbScreenIn, this.slantInPostSkinny, this.in15, this.dragInFromSlot, this.in10));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 9, "PA ALL GO", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn_Fly, this.fly, this.fly, this.fly, this.fly));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 9, "PA CROSSES", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn, this.dragOutFromSlot, this.slantInHighPostFlat, this.slantInHighPostFlat, this.dragInFromSlot));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 8, "PA DBL POSTS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbIsoIn_In, this.corner10flat, this.post10, this.hitch10out, this.post10flat));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 9, "PA FLOOD", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn_In, this.dragOutFromSlot, this.hitch10out, this.slantInUpCorner, this.post5flat));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 7, "PA TE POST", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn_In, this.post10flat, this.slantInUpCorner, this.post10skinny, this.out5));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 7, "PA WEAK FLOOD", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn_In, this.dragInFromSlot, this.slantInUpCorner, this.slantInHighPostFlat, this.post10));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 7, "PA Y IN", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn_In, this.post5flat, this.hitch10out, this.in15, this.dragOut));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "POWER-0", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragOut, this.slantIn, this.slantIn, this.slantIn));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockIn, this.slantInHighPostFlat, this.fly, this.post10skinny, this.slantInHighPost));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE OUT", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.out5, this.slantInUpCorner, this.post10skinny, this.in10));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE SEAM", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.hitch10in));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "UNDER", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.out10, this.slantInUpHitchOut, this.dragInFromSlot, this.slantOutFly));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "WR COMEBACKS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.slantIn, this.hitch10out, this.slantInUpHitchOut, this.hitch10out));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X DIG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.out10, this.in10, this.slantOutFly, this.dragOut));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X FLY", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragOut, this.slantOutFly, this.hitch10in, this.slantInHighPostFlat));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.out10, this.dragOut));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Y SHALLOW CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragInFromSlot, this.in15, this.slantInHighPost, this.post5flat));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Z SPOT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner10, this.in10, this.slantInPostFlat, this.dragOut));
    this.pistolPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole1, 6, "HB DIVE WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
  }

  private void SetPlays_Pistol_Slot()
  {
    this.pistolPlays_Slot = new FormationData(FormationType.Offense, this.pistol_Slot);
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "ACE POSTS", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragInFromSlot, this.post10, this.out10, this.dragOutFromSlot));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "ACE TE DRAG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragInFromSlot, this.fly, this.post10, this.in10));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.post5, this.slantInUpCorner, this.slantInUpCorner, this.out5));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURL FLATS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragOut, this.hitch10out, this.hitch10out, this.dragOutFromSlot));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "HB EMPTY BUBBLE", PlayConcept.Screen_Pass, this.runPos_screenRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPassPlay, this.rbScreenIn, this.runBlockTE, this.fly, this.post10, this.runBlockTE));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "EMPTY TE CORNER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner10flat, this.hitch10out, this.out10, this.post10skinny));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "EMPTY TE POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.post10flat, this.fly, this.dragInFromSlot, this.dragOutFromSlot));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "HB DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "FLANKER DRIVE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in10, this.hitch10out, this.dragInFromSlot, this.fly));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "HB STRETCH", PlayConcept.Outside_Run, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "HB SLIP SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenIn, this.fly, this.fly, this.fly, this.fly));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "POWER", PlayConcept.Outside_Run, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "INSIDE CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragInFromSlot, this.hitch10out, this.post10, this.slantIn));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "LEVELS DIVIDE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.post10flat, this.dragInFromSlot, this.fly, this.post10skinny));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "MIAMI", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragInFromSlot, this.out10, this.upCornerFly, this.in10));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "MTN HB SWING", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbScreenIn, this.slantInPostSkinny, this.in15, this.dragInFromSlot, this.in10));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 9, "PA ALL GO", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbPistolPAIn, this.rbIsoIn_Fly, this.fly, this.fly, this.fly, this.fly));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 9, "PA CROSSES", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbPistolPAIn, this.rbIsoIn, this.dragOutFromSlot, this.slantInHighPostFlat, this.slantInHighPostFlat, this.dragInFromSlot));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 8, "PA DBL POSTS", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbPistolOffTackleIn, this.rbIsoIn_In, this.corner10flat, this.post10, this.hitch10out, this.post10flat));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 9, "PA FLOOD", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbPistolPAIn, this.rbIsoIn_In, this.dragOutFromSlot, this.hitch10out, this.slantInUpCorner, this.post5flat));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 7, "PA TE POST", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbPistolPAIn, this.rbIsoIn_In, this.post10flat, this.slantInUpCorner, this.post10skinny, this.out5));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 7, "PA WEAK FLOOD", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbPistolPAIn, this.rbIsoIn_In, this.dragInFromSlot, this.slantInUpCorner, this.slantInHighPostFlat, this.post10));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 7, "PA Y IN", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbPistolPAIn, this.rbIsoIn_In, this.post5flat, this.hitch10out, this.in15, this.dragInFromSlot));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "POWER-0", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.passBlockG_PA, this.runBlockT, this.qbPistolOffTackleIn, this.rbIsoIn_In, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragOut, this.slantIn, this.slantIn, this.slantIn));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockIn, this.slantInHighPostFlat, this.out10, this.post10skinny, this.slantInHighPost));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE OUT", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.out5, this.slantInUpCorner, this.post10skinny, this.in10));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE SEAM", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.hitch10in));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "UNDER", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.out10, this.slantInUpHitchOut, this.dragInFromSlot, this.slantIn));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "WR COMEBACKS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.slantIn, this.hitch10out, this.slantInUpHitchOut, this.hitch10out));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X DIG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.out10, this.in10, this.slantOutFly, this.dragOutFromSlot));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X FLY", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragOut, this.slantOutFly, this.hitch10in, this.slantInHighPostFlat));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X POST", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.out10, this.dragOut));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Y SHALLOW CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragInFromSlot, this.in15, this.slantInHighPost, this.post5flat));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Z SPOT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner10, this.in10, this.slantInPostFlat, this.dragOutFromSlot));
    this.pistolPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.pistol_Slot, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole1, 6, "HB DIVE WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
  }

  private void SetPlays_Pistol_SpreadFlex()
  {
    this.pistolPlays_SpreadFlex = new FormationData(FormationType.Offense, this.pistol_SpreadFlex);
    this.pistolPlays_SpreadFlex.AddPlay((PlayData) new PlayDataOff(this.pistol_SpreadFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole1, 6, "HB DIVE WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_SpreadFlex.AddPlay((PlayData) new PlayDataOff(this.pistol_SpreadFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "HB DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_SpreadFlex.AddPlay((PlayData) new PlayDataOff(this.pistol_SpreadFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "ACE POST", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragOut, this.post10, this.post10, this.dragOutFromSlot));
    this.pistolPlays_SpreadFlex.AddPlay((PlayData) new PlayDataOff(this.pistol_SpreadFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "ACE TE DRAG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.fly, this.post10, this.in10));
    this.pistolPlays_SpreadFlex.AddPlay((PlayData) new PlayDataOff(this.pistol_SpreadFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragOut, this.slantInUpCorner, this.slantInUpCorner, this.dragOutFromSlot));
    this.pistolPlays_SpreadFlex.AddPlay((PlayData) new PlayDataOff(this.pistol_SpreadFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "CURL FLATS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragOut, this.hitch10out, this.hitch10out, this.dragOutFromSlot));
  }

  private void SetPlays_Pistol_TreyOpen()
  {
    this.pistolPlays_TreyOpen = new FormationData(FormationType.Offense, this.pistol_TreyOpen);
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "ACE POSTS", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragInFromSlot, this.post10, this.post10, this.dragOutFromSlot));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "ACE TE DRAG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragInFromSlot, this.fly, this.post10, this.in10));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.post5, this.slantInUpCorner, this.slantInUpCorner, this.out5));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURL FLATS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragOut, this.hitch10out, this.hitch10out, this.dragOutFromSlot));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "HB EMPTY BUBBLE", PlayConcept.Screen_Pass, this.runPos_screenRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPassPlay, this.rbScreenIn, this.runBlockTE, this.fly, this.post10, this.runBlockTE));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "EMPTY TE CORNER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner10flat, this.hitch10out, this.out10, this.post10skinny));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "EMPTY TE POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.post10flat, this.fly, this.dragInFromSlot, this.dragOutFromSlot));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "HB DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "FLANKER DRIVE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in10, this.hitch10out, this.dragInFromSlot, this.fly));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "HB STRETCH", PlayConcept.Outside_Run, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "HB SLIP SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenIn, this.fly, this.fly, this.fly, this.fly));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "POWER", PlayConcept.Outside_Run, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "INSIDE CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragInFromSlot, this.hitch10out, this.post10, this.slantIn));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "LEVELS DIVIDE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.post10flat, this.dragInFromSlot, this.fly, this.post10skinny));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "MIAMI", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragInFromSlot, this.out10, this.upCornerFly, this.in10));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "MTN HB SWING", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbScreenIn, this.slantInPostSkinny, this.in15, this.dragInFromSlot, this.in10));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 9, "PA ALL GO", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn_Fly, this.fly, this.fly, this.fly, this.fly));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 9, "PA CROSSES", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn, this.dragOutFromSlot, this.slantInHighPostFlat, this.slantInHighPostFlat, this.dragInFromSlot));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 8, "PA DBL POSTS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbIsoIn_In, this.corner10flat, this.post10, this.hitch10out, this.post10flat));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 9, "PA FLOOD", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn_In, this.dragOutFromSlot, this.hitch10out, this.slantInUpCorner, this.post5flat));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 7, "PA TE POST", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn_In, this.post10flat, this.slantInUpCorner, this.post10skinny, this.out5));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 7, "PA WEAK FLOOD", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn_In, this.dragInFromSlot, this.slantInUpCorner, this.slantInHighPostFlat, this.post10));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 7, "PA Y IN", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn_In, this.post5flat, this.hitch10out, this.in15, this.dragInFromSlot));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "POWER-0", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragOut, this.slantIn, this.slantIn, this.slantIn));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockIn, this.slantInHighPostFlat, this.fly, this.post10skinny, this.slantInHighPost));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE OUT", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.out5, this.slantInUpCorner, this.post10skinny, this.in10));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE SEAM", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.hitch10in));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "UNDER", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.out10, this.slantInUpHitchOut, this.dragInFromSlot, this.slantOutFly));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "WR COMEBACKS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.slantIn, this.hitch10out, this.slantInUpHitchOut, this.hitch10out));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X DIG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.out10, this.in10, this.slantOutFly, this.dragOutFromSlot));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X FLY", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragOut, this.slantOutFly, this.hitch10in, this.slantInHighPostFlat));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.out10, this.dragOut));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Y SHALLOW CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragInFromSlot, this.in15, this.slantInHighPost, this.post5flat));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Z SPOT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner10, this.in10, this.slantInPostFlat, this.dragOutFromSlot));
    this.pistolPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole1, 6, "HB DIVE WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
  }

  private void SetPlays_Pistol_Trio()
  {
    this.pistolPlays_Trio = new FormationData(FormationType.Offense, this.pistol_Trio);
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "ACE POSTS", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragInFromSlot, this.post10, this.post10, this.dragOutFromSlot));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "ACE TE DRAG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragInFromSlot, this.fly, this.post10, this.in10));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.post5, this.slantInUpCorner, this.slantInUpCorner, this.out5));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURL FLATS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragOut, this.hitch10out, this.hitch10out, this.dragOutFromSlot));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "HB EMPTY BUBBLE", PlayConcept.Screen_Pass, this.runPos_screenRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPassPlay, this.rbScreenIn, this.runBlockTE, this.in10, this.post5, this.corner10));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "EMPTY TE CORNER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner10flat, this.hitch10out, this.out10, this.post10skinny));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "EMPTY TE POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.post10flat, this.fly, this.dragInFromSlot, this.dragOutFromSlot));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "HB DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "FLANKER DRIVE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in10, this.hitch10out, this.dragInFromSlot, this.fly));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "HB STRETCH", PlayConcept.Outside_Run, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "HB SLIP SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenIn, this.fly, this.fly, this.fly, this.fly));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "POWER", PlayConcept.Outside_Run, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "INSIDE CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragInFromSlot, this.hitch10out, this.post10, this.slantIn));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "LEVELS DIVIDE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.post10flat, this.dragInFromSlot, this.fly, this.post10skinny));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "MIAMI", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragInFromSlot, this.out10, this.upCornerFly, this.in10));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "MTN HB SWING", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbScreenIn, this.slantInPostSkinny, this.in15, this.dragInFromSlot, this.in10));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 9, "PA ALL GO", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn_Fly, this.fly, this.fly, this.fly, this.fly));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 9, "PA CROSSES", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn, this.dragOutFromSlot, this.slantInHighPostFlat, this.slantInHighPostFlat, this.dragInFromSlot));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 8, "PA DBL POSTS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbIsoIn_In, this.corner10flat, this.post10, this.hitch10out, this.post10flat));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 9, "PA FLOOD", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn_In, this.dragOutFromSlot, this.hitch10out, this.slantInUpCorner, this.post5flat));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 7, "PA TE POST", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn_In, this.post10flat, this.slantInUpCorner, this.post10skinny, this.out5));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 7, "PA WEAK FLOOD", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn_In, this.dragInFromSlot, this.slantInUpCorner, this.slantInHighPostFlat, this.post10));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 7, "PA Y IN", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn_In, this.post5flat, this.hitch10out, this.in15, this.dragInFromSlot));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "POWER-0", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragOut, this.slantIn, this.slantIn, this.slantIn));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockIn, this.slantInHighPostFlat, this.fly, this.post10skinny, this.slantInHighPost));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE OUT", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.out5, this.slantInUpCorner, this.post10skinny, this.in10));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE SEAM", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.hitch10in));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "UNDER", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.out10, this.slantInUpHitchOut, this.dragInFromSlot, this.slantOutFly));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "WR COMEBACKS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.slantIn, this.hitch10out, this.slantInUpHitchOut, this.hitch10out));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X DIG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.out10, this.in10, this.slantOutFly, this.dragOutFromSlot));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X FLY", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragOut, this.slantOutFly, this.hitch10in, this.slantInHighPostFlat));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.out10, this.dragOut));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Y SHALLOW CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragInFromSlot, this.in15, this.slantInHighPost, this.post5flat));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Z SPOT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner10, this.in10, this.slantInPostFlat, this.dragOutFromSlot));
    this.pistolPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.pistol_Trio, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole1, 6, "HB DIVE WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
  }

  private void SetPlays_Pistol_YTrips()
  {
    this.pistolPlays_YTrips = new FormationData(FormationType.Offense, this.pistol_YTrips);
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "ACE TE DRAG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.dragInFromSlot, this.fly, this.post10, this.in10));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbIsoIn_HitchIn, this.post5, this.slantInUpCorner, this.slantInUpCorner, this.out5));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "EMPTY TE CORNER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.corner10flat, this.hitch10out, this.out10, this.post10skinny));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "EMPTY TE POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.post10flat, this.fly, this.dragInFromSlot, this.dragOutFromSlot));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "HB DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "FLANKER DRIVE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.in10, this.hitch10out, this.dragInFromSlot, this.fly));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "HB STRETCH", PlayConcept.Outside_Run, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "POWER", PlayConcept.Outside_Run, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "MIAMI", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.dragInFromSlot, this.out10, this.upCornerFly, this.in10));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "MTN HB SWING", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbScreenIn, this.slantInPostSkinny, this.in15, this.dragInFromSlot, this.in10));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 9, "PA CROSSES", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn, this.dragOutFromSlot, this.slantInHighPostFlat, this.slantInHighPostFlat, this.dragInFromSlot));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 9, "PA FLOOD", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn_In, this.dragOutFromSlot, this.hitch10out, this.slantInUpCorner, this.post5flat));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.PistolHole2, 7, "PA TE POST", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolPAIn, this.rbIsoIn_In, this.post10flat, this.slantInUpCorner, this.post10skinny, this.out5));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 6, "POWER-0", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPistolOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatIn, this.dragOut, this.slantIn, this.slantIn, this.slantIn));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.isoBlockIn, this.slantInHighPostFlat, this.fly, this.post10skinny, this.slantInHighPost));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE OUT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.out5, this.slantInUpCorner, this.post10skinny, this.in10));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "UNDER", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatIn, this.out10, this.slantInUpHitchOut, this.dragInFromSlot, this.slantOutFly));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "WR COMEBACKS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbIsoIn_HitchIn, this.slantIn, this.hitch10out, this.slantInUpHitchOut, this.hitch10out));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X DIG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.out10, this.in10, this.slantOutFly, this.dragOutFromSlot));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X FLY", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.dragOut, this.slantOutFly, this.hitch10in, this.slantInHighPostFlat));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X POST", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.out10, this.dragOut));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Z SPOT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.corner10, this.in10, this.slantInPostFlat, this.dragOutFromSlot));
    this.pistolPlays_YTrips.AddPlay((PlayData) new PlayDataOff(this.pistol_YTrips, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole1, 6, "HB DIVE WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
  }

  private void SetPlays_Shotgun_Bunch5WR()
  {
    this.shotgunPlays_Bunch5WR = new FormationData(FormationType.Offense, this.shotgun_Bunch5WR);
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "ACE POSTS", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragInFromSlot, this.post10, this.post10, this.dragOutFromSlot));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "ACE TE DRAG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.dragInFromSlot, this.slantIn, this.fly, this.fly, this.post10));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.hitch10in, this.dragInFromSlot, this.slantInUpCorner, this.slantInUpCorner, this.out5));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURL FLATS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.hitch10in, this.slantIn, this.hitch10out, this.hitch10out, this.dragOutFromSlot));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "POST CORNER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.corner10, this.post10, this.hitch10out, this.dragInFromSlot, this.in10));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "EMPTY POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.post10, this.post10flat, this.hitch10in, this.dragInFromSlot, this.dragOut));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "FLANKER DRIVE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.dragInFromSlot, this.in10, this.hitch10out, this.dragInFromSlot, this.fly));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "INSIDE CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.dragInFromSlot, this.slantIn, this.fly, this.post5skinny, this.post10flat));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "LEVELS DIVIDE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.dragInFromSlot, this.post10skinny, this.dragInFromSlot, this.fly, this.dragInFromSlot));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "MIAMI", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.dragOutFromSlot, this.dragInFromSlot, this.out10, this.upCornerFly, this.in10));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "WR SWING", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbIsoIn, this.rbScreenOut, this.corner10, this.in15, this.dragInFromSlot, this.in10));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "ALL GO", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.fly, this.fly, this.fly, this.fly, this.fly));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "DBL POSTS", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockT, this.passBlockT, this.qbPassPlay, this.dragInFromSlot, this.corner10flat, this.post10, this.hitch10out, this.post10flat));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Y IN", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.slantIn, this.post5flat, this.hitch10out, this.in15, this.dragOutFromSlot));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantIn, this.slantIn, this.fly, this.slantInHighPostFlat));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "WR CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.corner5, this.slantInHighPostFlat, this.fly, this.post10skinny, this.slantInHighPost));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "WR OUT", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.out10, this.slantIn, this.slantInUpCorner, this.post10skinny, this.in10));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE SEAM", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.slantInFly, this.dragInFromSlot, this.hitch10in, this.hitch10out, this.slantIn));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "UNDER", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.post10, this.hitch10out, this.slantInUpHitchOut, this.dragInFromSlot, this.slantOutFly));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "WR COMEBACKS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.hitch10in, this.hitch10out, this.hitch10out, this.hitch10out));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X DIG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.dragInFromSlot, this.out10, this.in10, this.slantOutFly, this.dragOut));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X Z FLY", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.dragInFromSlot, this.dragOutFromSlot, this.slantOutFly, this.slantOutFly, this.slantInHighPostFlat));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.dragInFromSlot, this.in10, this.post10, this.out5, this.dragOutFromSlot));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Y SHALLOW CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.slantInFly, this.hitch10in, this.in15, this.slantInHighPost, this.dragInFromSlot));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Z SPOT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.dragOutFromSlot, this.corner10, this.in10, this.slantInPostFlat, this.dragOutFromSlot));
    this.shotgunPlays_Bunch5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Bunch5WR, PlayType.Run, PlayTypeSpecific.QB_Keeper, DropbackType.OneStep, HandoffType.None, 5, "QB KEEPER", PlayConcept.QB_Keeper, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbKeeperRight, this.runBlockRB, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
  }

  private void SetPlays_Shotgun_Normal()
  {
    this.shotgunPlays_Normal = new FormationData(FormationType.Offense, this.shotgun_Normal);
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "ACE POST", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.passBlockRB, this.dragOut, this.post10, this.corner10skinny, this.out5));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "ACE TE DRAG", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.corner5, this.out10, this.dragOutFromSlot));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "CURL FLATS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.passBlockRB, this.dragOut, this.hitch5in, this.hitch5in, this.hitch10in));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE SEAM", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.slantInFly, this.fly, this.corner10skinny, this.hitch10in));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "QUICK OUTS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.passBlockRB, this.in10, this.out5, this.out5, this.corner5));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X FLY", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.passBlockTE, this.slantOutFly, this.corner10skinny, this.slantInHighPostFlat));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "Z CORNER POST 80", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.passBlockTE, this.corner10, this.post5Corner, this.dragOutFromSlot));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE OUT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.passBlockRB, this.out10, this.in10, this.hitch5out, this.out5));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "X DIG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.passBlockRB, this.corner5, this.out15, this.out10, this.slantOutFly));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "CORNER STRIKE", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.corner5, this.corner10skinny, this.out5, this.in10));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "DEEP COMEBACKS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.passBlockRB, this.dragIn, this.hitch15out, this.hitch15out, this.in10));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "QUICK SLANTS 86", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.passBlockRB, this.slantOut, this.slantIn, this.slantIn, this.slantInHitch));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Run, PlayTypeSpecific.QB_Keeper, DropbackType.OneStep, HandoffType.None, 5, "QB KEEPER", PlayConcept.QB_Keeper, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbKeeperRight, this.runBlockRB, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Run, PlayTypeSpecific.ReadOption, DropbackType.OneStep, HandoffType.ShotgunRightSideReadOption, 6, "READ OPTION", PlayConcept.Read_Option, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbReadOptionRight, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
  }

  private void SetPlays_Shotgun_NormalDimeDropping()
  {
    this.shotgunPlays_NormalDimeDropping = new FormationData(FormationType.Offense, this.shotgun_NormalDimeDropping);
    this.shotgunPlays_NormalDimeDropping.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalDimeDropping, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "Play2", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.passBlockRB, this.in10, this.hitch10in, this.out15, this.corner10));
    this.shotgunPlays_NormalDimeDropping.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalDimeDropping, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "Play3", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.passBlockRB, this.out10, this.fly, this.slantIn, this.post10));
    this.shotgunPlays_NormalDimeDropping.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalDimeDropping, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "Play4", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.passBlockRB, this.post10, this.out10, this.hitch10in, this.slantIn));
    this.shotgunPlays_NormalDimeDropping.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalDimeDropping, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "Play5", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.passBlockRB, this.out5, this.hitch15out, this.in10, this.post10skinny));
    this.shotgunPlays_NormalDimeDropping.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalDimeDropping, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "Play7", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.passBlockRB, this.in5, this.post10, this.hitch15out, this.in10));
    this.shotgunPlays_NormalDimeDropping.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalDimeDropping, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "Play8", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.passBlockRB, this.in10, this.hitch10in, this.hitch10in, this.dragOutFromSlot));
    this.shotgunPlays_NormalDimeDropping.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalDimeDropping, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "Play10", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.passBlockRB, this.out10, this.fly, this.post10, this.hitch10in));
    this.shotgunPlays_NormalDimeDropping.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalDimeDropping, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "Play11", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.passBlockRB, this.corner10skinny, this.post10, this.hitch10in, this.out10));
    this.shotgunPlays_NormalDimeDropping.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalDimeDropping, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "Play12", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.passBlockRB, this.corner10skinny, this.in10, this.post10, this.out5));
    this.shotgunPlays_NormalDimeDropping.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalDimeDropping, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "Play13", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.passBlockRB, this.out5, this.post10, this.in10, this.out15));
    this.shotgunPlays_NormalDimeDropping.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalDimeDropping, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "Play15", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.passBlockRB, this.in5, this.post10, this.hitch15out, this.post10skinny));
    this.shotgunPlays_NormalDimeDropping.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalDimeDropping, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "Play16", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.passBlockRB, this.post10, this.slantIn, this.in10, this.out10));
  }

  private void SetPlays_Shotgun_NormalYFlex()
  {
    this.shotgunPlays_NormalYFlex = new FormationData(FormationType.Offense, this.shotgun_NormalYFlex);
    this.shotgunPlays_NormalYFlex.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalYFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 7, "TE CROSS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.passBlockRB, this.dragInFromSlot, this.dragIn, this.in10, this.out10));
    this.shotgunPlays_NormalYFlex.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalYFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE SEAM", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.passBlockRB, this.slantInFly, this.corner10, this.fly, this.hitch10in));
    this.shotgunPlays_NormalYFlex.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalYFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 8, "CURL FLATS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.passBlockRB, this.dragOutFromSlot, this.slantInHitch, this.slantInHitch, this.dragOutFromSlot));
    this.shotgunPlays_NormalYFlex.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalYFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE DRAG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.dragInFromSlot, this.out5, this.out5, this.corner10));
    this.shotgunPlays_NormalYFlex.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalYFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "INSIDE CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.passBlockRB, this.dragInFromSlot, this.in10, this.out10, this.out5));
    this.shotgunPlays_NormalYFlex.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalYFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "WR COMEBACKS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.passBlockRB, this.slantIn, this.hitch5in, this.hitch5out, this.hitch15out));
    this.shotgunPlays_NormalYFlex.AddPlay((PlayData) new PlayDataOff(this.shotgun_NormalYFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "84 X FLY", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.passBlockRB, this.dragOut, this.slantOutFly, this.hitch10in, this.out15));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "QUICK SLANTS 86", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.passBlockRB, this.out10, this.slantIn, this.slantIn, this.slantInHitch));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "Z CORNER POST 80", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.passBlockTE, this.corner10, this.post5Corner, this.hitch10in));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "QUICK OUTS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.passBlockRB, this.corner5, this.out5, this.out5, this.in10));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Run, PlayTypeSpecific.QB_Keeper, DropbackType.OneStep, HandoffType.None, 5, "QB KEEPER", PlayConcept.QB_Keeper, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbKeeperRight, this.runBlockRB, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.shotgunPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.shotgun_Normal, PlayType.Run, PlayTypeSpecific.ReadOption, DropbackType.OneStep, HandoffType.ShotgunRightSideReadOption, 6, "READ OPTION", PlayConcept.Read_Option, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbReadOptionRight, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
  }

  private void SetPlays_Shotgun_QuadsTrio()
  {
    this.shotgunPlays_QuadsTrio = new FormationData(FormationType.Offense, this.shotgun_QuadsTrio);
    this.shotgunPlays_QuadsTrio.AddPlay((PlayData) new PlayDataOff(this.shotgun_QuadsTrio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "ACE POSTS", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.passBlockTE, this.dragInFromSlot, this.post10, this.corner10, this.dragOutFromSlot));
    this.shotgunPlays_QuadsTrio.AddPlay((PlayData) new PlayDataOff(this.shotgun_QuadsTrio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "ACE TE DRAG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.dragInFromSlot, this.slantIn, this.fly, this.fly, this.post10));
    this.shotgunPlays_QuadsTrio.AddPlay((PlayData) new PlayDataOff(this.shotgun_QuadsTrio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.hitch10in, this.dragInFromSlot, this.slantInUpCorner, this.slantInUpCorner, this.out5));
  }

  private void SetPlays_Shotgun_SlotOffset()
  {
    this.shotgunPlays_SlotOffset = new FormationData(FormationType.Offense, this.shotgun_SlotOffset);
    this.shotgunPlays_SlotOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SlotOffset, PlayType.Run, PlayTypeSpecific.ReadOption, DropbackType.OneStep, HandoffType.ShotgunLeftSideReadOption, 6, "READ OPTION", PlayConcept.Read_Option, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbReadOptionLeft, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.shotgunPlays_SlotOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SlotOffset, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "84 ACE POSTS", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.passBlockTE, this.post10, this.out15, this.dragOutFromSlot));
    this.shotgunPlays_SlotOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SlotOffset, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "INSIDE CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.passBlockTE, this.in10, this.corner5, this.dragInFromSlot));
    this.shotgunPlays_SlotOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SlotOffset, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.dragOut, this.slantInIn, this.slantIn, this.corner5));
    this.shotgunPlays_SlotOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SlotOffset, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "SLOT FLOOD", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.slantInIn, this.corner5, this.hitch10in, this.slantInHighPost));
    this.shotgunPlays_SlotOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SlotOffset, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "WR COMEBACKS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.passBlockRB, this.hitch10in, this.hitch10out, this.hitch10out, this.hitch10out));
    this.shotgunPlays_SlotOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SlotOffset, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.ShotgunLeftSideHole2, 6, "DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbGunHOAcross, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.shotgunPlays_SlotOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SlotOffset, PlayType.Run, PlayTypeSpecific.QB_Keeper, DropbackType.OneStep, HandoffType.None, 5, "QB KEEPER", PlayConcept.QB_Keeper, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbKeeperRight, this.runBlockRB, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
  }

  private void SetPlays_Shotgun_SplitOffset()
  {
    this.shotgunPlays_SplitOffset = new FormationData(FormationType.Offense, this.shotgun_SplitOffset);
    this.shotgunPlays_SplitOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SplitOffset, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "ACE POSTS", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragOut, this.post10, this.post10, this.dragOutFromSlot));
    this.shotgunPlays_SplitOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SplitOffset, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "ACE SLOT DIG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.rbFlatOut, this.out10, this.post10, this.in10));
    this.shotgunPlays_SplitOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SplitOffset, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.passBlockRB, this.corner10skinny, this.corner5, this.in10));
    this.shotgunPlays_SplitOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SplitOffset, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "EMPTY SLOT CORNER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.rbFlatOut, this.hitch10out, this.dragInFromSlot, this.corner10));
    this.shotgunPlays_SplitOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SplitOffset, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.ShotgunLeftSideHole2, 6, "HB DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbGunHOAcross, this.rbIsoIn, this.isoBlockIn, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.shotgunPlays_SplitOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SplitOffset, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "HB SLIP SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.screenBlockOLOut, this.screenBlockOLOut, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbScreenOut, this.isoBlockIn, this.corner10, this.post10, this.fly));
    this.shotgunPlays_SplitOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SplitOffset, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "INSIDE CROSS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.rbFlatOut, this.in10, this.corner10, this.dragInFromSlot));
    this.shotgunPlays_SplitOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SplitOffset, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "MIAMI", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.rbFlatOut, this.out10, this.upCornerFly, this.in10));
    this.shotgunPlays_SplitOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SplitOffset, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.ShotgunLeftSideHole2, 9, "PA DEEP OUTS", PlayConcept.Play_Action, 7, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbGunHOAcross, this.rbIsoOut_Fly, this.passBlockRB, this.out15, this.out15, this.dragInFromSlot));
    this.shotgunPlays_SplitOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SplitOffset, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 9, "PA FLOOD", PlayConcept.Play_Action, 7, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbGunHOAcross, this.rbFlatOut, this.rbIsoIn_In, this.hitch10out, this.slantInUpCorner, this.dragOutFromSlot));
    this.shotgunPlays_SplitOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SplitOffset, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.dragOut, this.slantIn, this.slantIn, this.underOutUpCorner));
    this.shotgunPlays_SplitOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SplitOffset, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 8, "X Corner", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.rbIsoOut_Out, this.slantInUpCorner, this.post10skinny, this.in10));
    this.shotgunPlays_SplitOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SplitOffset, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "WR COMEBACKS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.passBlockRB, this.hitch10out, this.hitch10out, this.hitch10out));
    this.shotgunPlays_SplitOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SplitOffset, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X DIG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.passBlockRB, this.in10, this.corner5, this.dragOutFromSlot));
    this.shotgunPlays_SplitOffset.AddPlay((PlayData) new PlayDataOff(this.shotgun_SplitOffset, PlayType.Run, PlayTypeSpecific.QB_Keeper, DropbackType.OneStep, HandoffType.None, 5, "QB KEEPER", PlayConcept.QB_Keeper, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbKeeperRight, this.runBlockRB, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
  }

  private void SetPlays_Shotgun_Spread()
  {
    this.shotgunPlays_Spread = new FormationData(FormationType.Offense, this.shotgun_Spread);
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "ACE A DRAG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.fly, this.post10, this.in10));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragOut, this.slantInUpCorner, this.slantInUpCorner, this.dragOutFromSlot));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "28 CURL", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbGunOffTackleIn_In, this.dragOut, this.corner5, this.corner5, this.dragOutFromSlot));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "EMPTY A POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.post10flat, this.fly, this.out10, this.dragOutFromSlot));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 6, "HB DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbGunHOAcross, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "88 FLANKER DRIVE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.passBlockRB, this.in10, this.hitch10out, this.hitch10in, this.fly));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "HB SLIP SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenOut, this.fly, this.fly, this.fly, this.fly));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "INSIDE CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragIn, this.in10, this.post10, this.dragInFromSlot));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "MIAMI", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.out10, this.corner5, this.in10));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "HB SWING", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbIsoIn, this.rbScreenOut, this.corner10, this.in15, this.dragInFromSlot, this.slantOut));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 9, "PA FLOOD", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbGunHOAcross, this.rbIsoIn_In, this.in15, this.hitch10out, this.post5skinny, this.corner10flat));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 7, "PA DEEP OUTS", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbGunHOAcross, this.rbIsoIn_In, this.hitch10in, this.out15, this.out15, this.hitch10in));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.slantIn, this.slantIn, this.slantIn, this.dragOutFromSlot));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 7, "A OUT", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.out5, this.out5, this.post10, this.in10));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "A SEAM", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.passBlockRB, this.slantInFly, this.fly, this.fly, this.hitch10in));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "WR COMEBACKS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.passBlockRB, this.hitch10out, this.hitch5out, this.hitch10in, this.hitch10out));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X FLY", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.dragOut, this.slantOutFly, this.hitch10in, this.slantInHighPostFlat));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Z SPOT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.corner10, this.in10, this.slantInPostFlat, this.dragOutFromSlot));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Run, PlayTypeSpecific.QB_Keeper, DropbackType.OneStep, HandoffType.None, 5, "QB KEEPER", PlayConcept.QB_Keeper, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbKeeperRight, this.runBlockRB, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.shotgunPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread, PlayType.Run, PlayTypeSpecific.ReadOption, DropbackType.OneStep, HandoffType.ShotgunRightSideReadOption, 6, "READ OPTION", PlayConcept.Read_Option, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbReadOptionRight, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
  }

  private void SetPlays_Shotgun_Spread5WR()
  {
    this.shotgunPlays_Spread5WR = new FormationData(FormationType.Offense, this.shotgun_Spread5WR);
    this.shotgunPlays_Spread5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 6, "QUICK OUTS", PlayConcept.Short_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.corner10flat, this.out5, this.out5, this.in5, this.corner10flat));
    this.shotgunPlays_Spread5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 10, "INSIDE OUT", PlayConcept.Short_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.out10, this.fly, this.in5, this.in5, this.out5));
    this.shotgunPlays_Spread5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "FAKE SHUFFLE FLATS", PlayConcept.Mid_Pass, 0, this.passBlockLT_Deep, this.passBlockRG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.post5flat, this.fly, this.post5, this.in10, this.underIn));
    this.shotgunPlays_Spread5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 6, "SWING OUTS", PlayConcept.Short_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.underOut, this.slantIn, this.post5flat, this.post5skinny, this.underOut));
    this.shotgunPlays_Spread5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 10, "FLYBIRD", PlayConcept.Deep_Pass, 0, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.slantInFly, this.hitch10in, this.in10, this.fly, this.slantOutFly));
    this.shotgunPlays_Spread5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 7, "SHORT CROSS", PlayConcept.Short_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.dragOutFromSlot, this.slantInIn, this.in5, this.slantIn, this.dragOut));
    this.shotgunPlays_Spread5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "TRAIL UNDER", PlayConcept.Short_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.underOut, this.corner10, this.hitch10in, this.upPostHitch, this.post5));
    this.shotgunPlays_Spread5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread5WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 10, "HITCHES", PlayConcept.Short_Pass, 0, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.hitch10in, this.hitch5in, this.hitch10in, this.hitch10out, this.hitch10in));
    this.shotgunPlays_Spread5WR.AddPlay((PlayData) new PlayDataOff(this.shotgun_Spread5WR, PlayType.Run, PlayTypeSpecific.QB_Keeper, DropbackType.OneStep, HandoffType.None, 5, "QB KEEPER", PlayConcept.QB_Keeper, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbKeeperRight, this.runBlockRB, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
  }

  private void SetPlays_Shotgun_Tight()
  {
    this.shotgunPlays_Tight = new FormationData(FormationType.Offense, this.shotgun_Tight);
    this.shotgunPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.shotgun_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "84 ACE POSTS", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragOut, this.post10, this.corner10, this.dragOutFromSlot));
    this.shotgunPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.shotgun_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "TE DRAG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.out10, this.corner5, this.in10));
    this.shotgunPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.shotgun_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 6, "HB DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbGunHOAcross, this.rbGunHOAcross, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.shotgunPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.shotgun_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "FLANKER DRIVE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.passBlockRB, this.in10, this.hitch10out, this.hitch10in, this.corner10));
    this.shotgunPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.shotgun_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "HB SLIP SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenOut, this.fly, this.fly, this.fly, this.fly));
    this.shotgunPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.shotgun_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "MIAMI", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.out10, this.upCornerFly, this.in10));
    this.shotgunPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.shotgun_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 7, "PA WEAK FLOOD", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbGunHOAcross, this.rbIsoIn_In, this.dragIn, this.slantOutFly, this.slantInHighPostFlat, this.dragOutFromSlot));
    this.shotgunPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.shotgun_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 7, "PA Y IN", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbGunHOAcross, this.rbIsoIn_In, this.post5flat, this.hitch10out, this.in15, this.corner10));
    this.shotgunPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.shotgun_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "QUICK SLANTS 86", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.dragOut, this.slantInIn, this.slantIn, this.dragOutFromSlot));
    this.shotgunPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.shotgun_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 7, "TE CROSS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.isoBlockIn, this.slantInHighPostFlat, this.hitch10out, this.out10, this.slantInIn));
    this.shotgunPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.shotgun_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 7, "CROSS OUT", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.out5, this.slantInIn, this.in5, this.out10));
    this.shotgunPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.shotgun_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "UNDER", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.out10, this.slantOut, this.dragInFromSlot, this.slantOutFly));
    this.shotgunPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.shotgun_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X FLY", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.dragInFromSlot, this.slantOutFly, this.hitch10out, this.dragInFromSlot));
    this.shotgunPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.shotgun_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Z SPOT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.corner10, this.hitch10in, this.slantInPostFlat, this.dragOutFromSlot));
    this.shotgunPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.shotgun_Tight, PlayType.Run, PlayTypeSpecific.QB_Keeper, DropbackType.OneStep, HandoffType.None, 5, "QB KEEPER", PlayConcept.QB_Keeper, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbKeeperRight, this.runBlockRB, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.shotgunPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.shotgun_Tight, PlayType.Run, PlayTypeSpecific.ReadOption, DropbackType.OneStep, HandoffType.ShotgunRightSideReadOption, 6, "READ OPTION", PlayConcept.Read_Option, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbReadOptionRight, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
  }

  private void SetPlays_Shotgun_TightWideBack()
  {
    this.shotgunPlays_TightWideBack = new FormationData(FormationType.Offense, this.shotgun_TightWideBack);
    this.shotgunPlays_TightWideBack.AddPlay((PlayData) new PlayDataOff(this.shotgun_TightWideBack, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "TE DRAG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.out10, this.dragIn, this.rbFlatOut, this.corner5, this.in10));
    this.shotgunPlays_TightWideBack.AddPlay((PlayData) new PlayDataOff(this.shotgun_TightWideBack, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 8, "19 DIVE Right", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbGunHOAcross, this.runBlockWR, this.runBlockTE, this.rbGunHOAcross, this.runBlockWR, this.runBlockTE));
    this.shotgunPlays_TightWideBack.AddPlay((PlayData) new PlayDataOff(this.shotgun_TightWideBack, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "FLANKER DRIVE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.hitch10out, this.in10, this.rbFlatOut, this.hitch10in, this.corner10));
    this.shotgunPlays_TightWideBack.AddPlay((PlayData) new PlayDataOff(this.shotgun_TightWideBack, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "INSIDE CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.in10, this.dragIn, this.rbIsoIn_HitchIn, this.post10, this.dragInFromSlot));
    this.shotgunPlays_TightWideBack.AddPlay((PlayData) new PlayDataOff(this.shotgun_TightWideBack, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "MIAMI", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.out10, this.dragIn, this.rbFlatOut, this.upCornerFly, this.in10));
    this.shotgunPlays_TightWideBack.AddPlay((PlayData) new PlayDataOff(this.shotgun_TightWideBack, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.OneStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.slantInIn, this.dragOut, this.rbFlatOut, this.slantIn, this.dragOutFromSlot));
    this.shotgunPlays_TightWideBack.AddPlay((PlayData) new PlayDataOff(this.shotgun_TightWideBack, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X FLY", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.slantOutFly, this.dragInFromSlot, this.rbFlatOut, this.hitch10out, this.dragInFromSlot));
  }

  private void SetPlays_Shotgun_Trey()
  {
    this.shotgunPlays_Trey = new FormationData(FormationType.Offense, this.shotgun_Trey);
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "ACE POSTS", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragInFromSlot, this.post10, this.post10, this.dragOutFromSlot));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "ACE TE DRAG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragInFromSlot, this.fly, this.post10, this.in10));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.post5, this.slantInUpCorner, this.slantInUpCorner, this.out5));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURL FLATS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragOut, this.hitch10out, this.hitch10out, this.dragOutFromSlot));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "EMPTY TE CORNER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner10flat, this.hitch10out, this.out10, this.post10skinny));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "EMPTY TE POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.post10flat, this.fly, this.dragInFromSlot, this.dragOutFromSlot));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 6, "HB DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbGunHOAcross, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "FLANKER DRIVE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in10, this.hitch10out, this.dragInFromSlot, this.fly));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "HB SLIP SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenOut, this.fly, this.fly, this.fly, this.fly));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "INSIDE CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragInFromSlot, this.hitch10out, this.post10, this.slantIn));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "LEVELS DIVIDE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.post10flat, this.dragInFromSlot, this.fly, this.post10skinny));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "MIAMI", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragInFromSlot, this.out10, this.upCornerFly, this.in10));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "MTN HB SWING", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbScreenOut, this.slantInPostSkinny, this.in15, this.dragInFromSlot, this.in10));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 9, "PA ALL GO", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbGunHOAcross, this.rbGunOffTackleIn_Fly, this.fly, this.fly, this.fly, this.fly));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 9, "PA CROSSES", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbGunHOAcross, this.rbIsoIn_In, this.dragOutFromSlot, this.slantInHighPostFlat, this.slantInHighPostFlat, this.dragInFromSlot));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 8, "PA DBL POSTS", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbGunHOAcross, this.rbIsoIn_In, this.corner10flat, this.post10, this.hitch10out, this.post10flat));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 9, "PA FLOOD", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbGunHOAcross, this.rbIsoIn_In, this.dragOutFromSlot, this.hitch10out, this.slantInUpCorner, this.post5flat));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 7, "PA TE POST", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbGunHOAcross, this.rbIsoIn_In, this.post10flat, this.slantInUpCorner, this.post10skinny, this.out5));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 7, "PA WEAK FLOOD", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbGunHOAcross, this.rbIsoIn_In, this.dragInFromSlot, this.slantInUpCorner, this.slantInHighPostFlat, this.post10));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 7, "PA Y IN", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbGunHOAcross, this.rbIsoIn_In, this.post5flat, this.hitch10out, this.in15, this.dragInFromSlot));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragOut, this.slantIn, this.slantIn, this.slantIn));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockIn, this.slantInHighPostFlat, this.fly, this.post10skinny, this.slantInHighPost));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE OUT", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.out5, this.slantInUpCorner, this.post10skinny, this.in10));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE SEAM", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.hitch10in));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "UNDER", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.out10, this.slantInUpHitchOut, this.dragInFromSlot, this.slantOutFly));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "WR COMEBACKS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.slantIn, this.hitch10out, this.slantInUpHitchOut, this.hitch10out));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X DIG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.out10, this.in10, this.slantOutFly, this.dragOutFromSlot));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X FLY", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragOut, this.slantOutFly, this.hitch10in, this.slantInHighPostFlat));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.in5, this.post10, this.out10, this.dragOut));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Y SHALLOW CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragInFromSlot, this.in15, this.slantInHighPost, this.post5flat));
    this.shotgunPlays_Trey.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trey, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "Z SPOT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner10, this.in10, this.slantInPostFlat, this.dragOutFromSlot));
  }

  private void SetPlays_Shotgun_Trips()
  {
    this.shotgunPlays_Trips = new FormationData(FormationType.Offense, this.shotgun_Trips);
    this.shotgunPlays_Trips.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.Shotgun, HandoffType.None, 9, "SLANTS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.slantIn, this.post10, this.fly, this.post5flat, this.rbFlatOut));
    this.shotgunPlays_Trips.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.Shotgun, HandoffType.None, 6, "Y UNDER", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.underOutUpCorner, this.post5, this.upPostIn, this.post10skinny, this.passBlockRB));
    this.shotgunPlays_Trips.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.Shotgun, HandoffType.None, 8, "FAKE REVERSE", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.underIn, this.post5, this.post5flat, this.post10skinny, this.slantOut));
    this.shotgunPlays_Trips.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.Shotgun, HandoffType.None, 7, "DEEP CROSS", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.corner10, this.in5, this.post5, this.post10flat, this.passBlockRB));
    this.shotgunPlays_Trips.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.Shotgun, HandoffType.None, 6, "FIREBIRD", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.corner10, this.in5, this.slantIn, this.post5, this.passBlockRB));
    this.shotgunPlays_Trips.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trips, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 10, "DRAW ACROSS", PlayConcept.Outside_Run, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbGunHOAcross, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.rbGunHOAcross));
    this.shotgunPlays_Trips.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.Shotgun, HandoffType.None, 10, "SWING SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.slantIn, this.runBlockWR, this.fly, this.runBlockWR, this.rbScreenOut));
    this.shotgunPlays_Trips.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.Shotgun, HandoffType.None, 6, "CLEAR AWAY", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.upOutFly, this.slantIn, this.hitch10in, this.post5, this.rbFlatOut));
    this.shotgunPlays_Trips.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.Shotgun, HandoffType.None, 7, "SEAM PROTECT", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.slantInFly, this.hitch10in, this.in10, this.slantOutFly, this.passBlockRB));
    this.shotgunPlays_Trips.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.Shotgun, HandoffType.None, 7, "FOLLOW UNDER", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.post10, this.post5Corner, this.slantInIn, this.underIn, this.rbFlatIn));
    this.shotgunPlays_Trips.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trips, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 6, "PA HITCH", PlayConcept.Play_Action, 10, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbGunHOAcross, this.corner10skinny, this.hitch5in, this.post5, this.hitch10out, this.rbGunOffTackleIn_Fly));
    this.shotgunPlays_Trips.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trips, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.Shotgun, HandoffType.None, 7, "DRAG PROTECT", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC_Shotgun, this.passBlockG, this.passBlockT, this.qbPassPlay, this.corner10, this.slantIn, this.slantInIn, this.in10, this.rbIsoIn_In));
    this.shotgunPlays_Trips.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trips, PlayType.Run, PlayTypeSpecific.QB_Keeper, DropbackType.OneStep, HandoffType.None, 5, "QB KEEPER", PlayConcept.QB_Keeper, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbKeeperRight, this.runBlockRB, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.shotgunPlays_Trips.AddPlay((PlayData) new PlayDataOff(this.shotgun_Trips, PlayType.Run, PlayTypeSpecific.ReadOption, DropbackType.OneStep, HandoffType.ShotgunRightSideReadOption, 6, "READ OPTION", PlayConcept.Read_Option, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbReadOptionRight, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
  }

  private void SetPlays_SingleBack_Big()
  {
    this.singleBackPlays_Big = new FormationData(FormationType.Offense, this.singleBack_Big);
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "ACE TE DRAG", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.corner5, this.out10, this.in10));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragOut, this.slantInUpCorner, this.slantInUpCorner, this.dragOut));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "COMEBACK FLATS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.passBlockRB, this.dragOut, this.hitch10out, this.hitch10out, this.dragOut));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "EMPTY TE CORNER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatIn, this.corner10flat, this.hitch10out, this.dragInFromSlot, this.corner10));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.SevenStep, HandoffType.None, 7, "EMPTY TE POST", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn, this.post10flat, this.fly, this.fly, this.dragOut));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "22 DIVE RIGHT", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbDiveIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "COUNTER 20", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.pullBlockIn, this.qbCounterOut, this.rbCounterOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "MIAMI", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatIn, this.in10, this.out10, this.upCornerFly, this.passBlockTE));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA DEEP OUTS", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_Fly, this.post10, this.out15, this.out10, this.corner10));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA FLOOD", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.dragOut, this.hitch10out, this.slantInUpCorner, this.dragOut));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA TE POST", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.post10flat, this.slantInUpCorner, this.post10skinny, this.out5));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole4, 6, "POWER-0", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatIn, this.passBlockTE, this.slantIn, this.slantIn, this.passBlockTE));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "TE OUTS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.passBlockRB, this.out10, this.in5, this.corner5skinny, this.out10));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "COMEBACKS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.passBlockRB, this.hitch10in, this.hitch10out, this.hitch10out, this.hitch10in));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "X DIG", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.passBlockRB, this.out10, this.in10, this.slantOutFly, this.dragOut));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.SevenStep, HandoffType.None, 8, "X FLY", PlayConcept.Deep_Pass, 0, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.passBlockTE, this.slantOutFly, this.out10, this.slantInHighPostFlat));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.out10, this.passBlockTE));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "SHALLOW CROSS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.out15, this.slantInHighPost, this.post5flat));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "Z SPOT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatIn, this.corner10, this.in10, this.slantInPostFlat, this.dragOut));
    this.singleBackPlays_Big.AddPlay((PlayData) new PlayDataOff(this.singleBack_Big, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB DIVE WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbDiveOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
  }

  private void SetPlays_SingleBack_BigTwins()
  {
    this.singleBackPlays_BigTwins = new FormationData(FormationType.Offense, this.singleBack_BigTwins);
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "CURL FLAT", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.post10flat, this.hitch10in, this.dragOutFromSlot, this.slantInHitch));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CURL FLAT CORNER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.post10flat, this.hitch10in, this.corner10, this.slantInHitch));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "DBL POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbIsoIn_HitchIn, this.dragOut, this.post10, this.hitch10in, this.slantIn));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "FLOOD", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.post10flat, this.out10, this.fly, this.dragOut));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB DIVE WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRHBDive, this.rbDiveIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB COUNTER", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockOut, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB DIVE STRONG", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbDiveOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockOut, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB POWER", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleLeft, this.runBlockT, this.pullBlockOut, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleOut, this.rbOffTackleOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB POWER WEAK", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockOut, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN WEAK", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.pullBlockOut, this.pullBlockOut, this.qbPassPlay, this.rbScreenIn, this.fly, this.fly, this.corner10skinny, this.fly));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA DIG", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.runBlockC, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.dragIn, this.in10, this.fly, this.dragIn));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.dragOut, this.fly, this.post10flat, this.slantInHighPostFlat));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA VERTICAL", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.dragOut, this.slantOutFly, this.post10, this.post5flat));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA SMASH", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.post10, this.slantInHitch, this.corner10, this.dragIn));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SLANT", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragOut, this.slantIn, this.slantIn, this.dragOut));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLOT UNDER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.fly, this.hitch10in, this.slantInPostFlat, this.fly));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SMASH", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner10, this.hitch10in, this.corner10, this.fly));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 10, "OUTS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.out5, this.fly, this.out10, this.out5));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "WEAK FLOOD", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.post10skinny, this.out10, this.slantInUpCorner));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "X SPOT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.post10, this.slantInHitch, this.corner10, this.dragOut));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "TE SPECIAL", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner10flat, this.dragInFromSlot, this.out10, this.post10flat));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "TE SCREEN STRONG", PlayConcept.Screen_Pass, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPassPlay, this.rbFlatIn, this.teAround, this.runBlockWR, this.runBlockWR, this.runBlockTE));
    this.singleBackPlays_BigTwins.AddPlay((PlayData) new PlayDataOff(this.singleBack_BigTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB FLATS", PlayConcept.Screen_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantIn, this.fly, this.fly, this.fly));
  }

  private void SetPlays_SingleBack_Bunch()
  {
    this.singleBackPlays_Bunch = new FormationData(FormationType.Offense, this.singleBack_Bunch);
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "CURL FLAT", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatIn_Fly, this.dragOutFromSlot, this.hitch10in, this.hitch10in, this.slantIn));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CURL FLAT CORNER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatIn_Fly, this.dragOutFromSlot, this.hitch10in, this.corner10, this.slantIn));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "FLOOD", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut_Fly, this.dragInFromSlot, this.out10, this.post10, this.slantIn));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB DIVE STRONG", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbDiveIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB DIVE WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbDiveOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION STRONG", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockOut, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB POWER WEAK", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleLeft, this.runBlockT, this.pullBlockOut, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleOut, this.rbOffTackleOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB POWER STRONG", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockOut, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN STRONG", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.pullBlockOut, this.pullBlockOut, this.qbPassPlay, this.rbScreenIn, this.fly, this.fly, this.fly, this.fly));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN WEAK", PlayConcept.Screen_Pass, this.runPos_screenLeft, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbScreenOut, this.fly, this.fly, this.fly, this.fly));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA DIG", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.dragInFromSlot, this.post10skinny, this.slantIn, this.post10flat));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.dragInFromSlot, this.fly, this.post5skinny, this.slantIn));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA HAMMER", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_Out, this.out10, this.slantInUpCorner, this.fly, this.post10flat));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA VERTICAL", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.slantInHighPostFlat, this.slantOutFly, this.slantOutFly, this.fly));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA SMASH", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.fly, this.hitch10in, this.hitch10in, this.slantInPostFlat));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SLANT", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatIn, this.dragInFromSlot, this.slantIn, this.slantIn, this.slantIn));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "SLOT UNDER", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatIn_Fly, this.dragOutFromSlot, this.in5, this.corner10, this.out10));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SMASH", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.out10, this.corner10, this.corner10, this.post10flat));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "OUTS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.dragOutFromSlot, this.out15, this.out15, this.in10));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "WEAK FLOOD", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.dragInFromSlot, this.fly, this.post10, this.slantIn));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.SevenStep, HandoffType.None, 8, "X SPOT", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.dragInFromSlot, this.slantInUpCorner, this.fly, this.post10flat));
    this.singleBackPlays_Bunch.AddPlay((PlayData) new PlayDataOff(this.singleBack_Bunch, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.SevenStep, HandoffType.None, 7, "TE SPECIAL", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut_Fly, this.corner10, this.in10, this.post10, this.dragIn));
  }

  private void SetPlays_SingleBack_Slot()
  {
    this.singleBackPlays_Slot = new FormationData(FormationType.Offense, this.singleBack_Slot);
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "TAIL ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURL FLAT CORNER", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.passBlockRB, this.dragOut, this.hitch10in, this.corner10, this.slantIn));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "FLOOD", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut_Fly, this.hitch5in, this.out10, this.out15, this.hitch5out));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB DIVE STRONG", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbDiveIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole3, 6, "HB COUNTER 20", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB DIVE WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbDiveOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole3, 6, "HB POWER WEAK", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.runBlockT, this.qbOffTackleOut, this.rbOffTackleOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole4, 6, "34 BLAST", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA DIG", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.dragIn, this.post10skinny, this.slantIn, this.post10flat));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.isoBlockIn, this.dragIn, this.fly, this.post5skinny, this.slantIn));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA HAMMER", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_Out, this.out10, this.out15, this.corner10flat, this.post10flat));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA SMASH", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.fly, this.hitch10in, this.hitch10in, this.slantInPostFlat));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SLANTS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.passBlockRB, this.dragOut, this.slantIn, this.out15, this.slantIn));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLOT UNDER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.passBlockRB, this.in5, this.out5, this.out15, this.in15));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.SevenStep, HandoffType.None, 8, "SMASH", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.out10, this.corner10, this.corner10, this.post10flat));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "DEEP OUTS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.passBlockRB, this.dragOut, this.out15, this.out15, this.in10));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "QUICK OUTS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.passBlockRB, this.dragOut, this.out5, this.out5, this.corner10));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.SevenStep, HandoffType.None, 8, "X SPOT", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.slantInUpCorner, this.fly, this.post10flat));
    this.singleBackPlays_Slot.AddPlay((PlayData) new PlayDataOff(this.singleBack_Slot, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB FLATS", PlayConcept.Screen_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.post5, this.out10, this.post10, this.corner10));
  }

  private void SetPlays_SingleBack_Spread()
  {
    this.singleBackPlays_Spread = new FormationData(FormationType.Offense, this.singleBack_Spread);
    this.singleBackPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.singleBack_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "FLOOD", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut_Fly, this.dragIn, this.out5, this.dragIn, this.corner10));
    this.singleBackPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.singleBack_Spread, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB ISO 20", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbDiveOut, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.singleBack_Spread, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB BLAST", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbDiveIn, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.singleBack_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "SLANTS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatOut_Fly, this.slantIn, this.slantIn, this.slantIn, this.slantOut));
    this.singleBackPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.singleBack_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLOT UNDER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.passBlockRB, this.post10skinny, this.corner5flat, this.corner5flat, this.slantInIn));
    this.singleBackPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.singleBack_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURLS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatOut_Fly, this.slantIn, this.hitch10in, this.hitch10out, this.hitch5in));
    this.singleBackPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.singleBack_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK OUTS", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatOut_Fly, this.corner10, this.out5, this.out10, this.slantIn));
    this.singleBackPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.singleBack_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "DEEP OUTS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut_Fly, this.in15, this.out15, this.out15, this.slantIn));
    this.singleBackPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.singleBack_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.SevenStep, HandoffType.None, 9, "POST CORNER 80", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatIn_Fly, this.dragIn, this.corner10flat, this.post5Corner, this.post5));
    this.singleBackPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.singleBack_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "86 QUICK HITCHES", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.passBlockRB, this.hitch10out, this.hitch5in, this.hitch10in, this.hitch10out));
    this.singleBackPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.singleBack_Spread, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA HAMMER", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.in15, this.hitch10out, this.post5skinny, this.corner10flat));
    this.singleBackPlays_Spread.AddPlay((PlayData) new PlayDataOff(this.singleBack_Spread, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.SevenStep, HandoffType.None, 8, "X SPOT", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.passBlockRB, this.dragIn, this.slantInUpCorner, this.fly, this.post10flat));
  }

  private void SetPlays_SingleBack_TreyOpen()
  {
    this.singleBackPlays_TreyOpen = new FormationData(FormationType.Offense, this.singleBack_TreyOpen);
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole4, 6, "TAIL ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "CURL FLAT", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatIn_Fly, this.dragOut, this.hitch10in, this.hitch10in, this.slantIn));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CURL FLAT CORNER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatIn_Fly, this.dragOut, this.hitch10in, this.corner10, this.slantIn));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "FLOOD", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatOut_Fly, this.dragIn, this.out10, this.post10, this.slantIn));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB DIVE", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbDiveIn, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB DIVE WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbDiveOut, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole3, 6, "HB POWER WEAK", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleLeft, this.runBlockT, this.pullBlockOut, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleOut, this.rbOffTackleOut, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole4, 6, "HB POWER STRONG", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockOut, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA DIG", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.dragIn, this.post10skinny, this.slantIn, this.post10flat));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.dragIn, this.fly, this.post10skinny, this.slantIn));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA HAMMER", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_Out, this.out10, this.slantInUpCorner, this.post10flat, this.post10flat));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA VERTICAL", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.slantInHighPostFlat, this.slantOutFly, this.slantOutFly, this.fly));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SLANT", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.slantIn, this.slantIn, this.slantIn));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "SLOT UNDER", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockLT_Short, this.passBlockLG_Short, this.passBlockC_Short, this.passBlockRG_Short, this.passBlockRT_Short, this.qbPassPlay, this.rbFlatIn_Fly, this.dragOut, this.in5, this.corner10, this.out5));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.SevenStep, HandoffType.None, 8, "SMASH", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.out5, this.corner10, this.corner10, this.post10flat));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "DEEP OUTS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.dragOut, this.out15, this.out15, this.in10));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "WEAK FLOOD", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.fly, this.post10, this.slantIn));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.SevenStep, HandoffType.None, 8, "X SPOT", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockLT_Deep, this.passBlockLG_Deep, this.passBlockC_Deep, this.passBlockRG_Deep, this.passBlockRT_Deep, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.slantInUpCorner, this.fly, this.post10flat));
    this.singleBackPlays_TreyOpen.AddPlay((PlayData) new PlayDataOff(this.singleBack_TreyOpen, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB FLATS", PlayConcept.Screen_Pass, this.runPos_actualPosition, this.passBlockLT_Mid, this.passBlockLG_Mid, this.passBlockC_Mid, this.passBlockRG_Mid, this.passBlockRT_Mid, this.qbPassPlay, this.rbFlatIn, this.fly, this.fly, this.fly, this.fly));
  }

  private void SetPlays_SingleBack_Trio()
  {
    this.singleBackPlays_Trio = new FormationData(FormationType.Offense, this.singleBack_Trio);
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "TAIL ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "CURL FLAT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.dragOut, this.hitch10in, this.hitch10in, this.slantIn));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CURL FLAT CORNER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.dragOut, this.hitch10in, this.corner10, this.slantIn));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "DBL POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.in5, this.post10, this.post10, this.out5));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "FLOOD", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.dragIn, this.out10, this.post10, this.slantIn));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB DIVE STRONG", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbDiveIn, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB COUNTER STRONG", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockOut, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB COUNTER WEAK", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleLeft, this.runBlockT, this.pullBlockOut, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterOut, this.rbCounterOut, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB DIVE WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbDiveOut, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION STRONG", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockOut, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB POWER WEAK", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleLeft, this.runBlockT, this.pullBlockOut, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleOut, this.rbOffTackleOut, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB POWER STRONG", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockOut, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN STRONG", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.pullBlockOut, this.pullBlockOut, this.qbPassPlay, this.rbScreenIn, this.fly, this.fly, this.fly, this.fly));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN WEAK", PlayConcept.Screen_Pass, this.runPos_screenLeft, this.pullBlockOut, this.pullBlockOut, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbScreenOut, this.fly, this.fly, this.fly, this.fly));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA DIG", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.dragIn, this.post10skinny, this.slantIn, this.post10flat));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.dragIn, this.fly, this.post10skinny, this.slantIn));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA HAMMER", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_Out, this.out10, this.slantInUpCorner, this.fly, this.post10flat));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA VERTICAL", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.slantInHighPostFlat, this.slantOutFly, this.slantOutFly, this.fly));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA SMASH", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.fly, this.hitch10in, this.hitch10in, this.slantInPostFlat));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SLANT", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.slantIn, this.slantIn, this.slantIn));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "Inside Slot Pick", PlayConcept.Short_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.slantIn, this.slantIn, this.dragOutFromSlot));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLOT UNDER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.dragOut, this.in5, this.corner10, this.out5));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SMASH", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.out5, this.corner10, this.corner10, this.post10flat));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "OUTS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragOut, this.out15, this.out15, this.in10));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "WEAK FLOOD", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.fly, this.post10, this.slantIn));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.SevenStep, HandoffType.None, 8, "X SPOT", PlayConcept.Deep_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.slantInUpCorner, this.fly, this.post10flat));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "WRA SPECIAL", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.corner10, this.in10, this.post5, this.dragInFromSlot));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "WRA SCREEN STRONG", PlayConcept.Screen_Pass, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbPassPlay, this.rbFlatIn, this.teAround, this.fly, this.fly, this.fly));
    this.singleBackPlays_Trio.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB FLATS", PlayConcept.Screen_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.fly, this.fly, this.fly, this.fly));
  }

  private void SetPlays_SingleBack_Trio4WR()
  {
    this.singleBackPlays_Trio4WR = new FormationData(FormationType.Offense, this.singleBack_Trio4WR);
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "TAIL ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "CURL FLAT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.dragOut, this.hitch10in, this.hitch10in, this.slantIn));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CURL FLAT CORNER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.dragOut, this.hitch10in, this.corner10, this.slantIn));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "DBL POST", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.in5, this.post10, this.post10, this.out5));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "FLOOD", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.dragIn, this.out10, this.post10, this.slantIn));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB DIVE STRONG", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveIn, this.rbDiveIn, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB COUNTER STRONG", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockOut, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB COUNTER WEAK", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleLeft, this.runBlockT, this.pullBlockOut, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterOut, this.rbCounterOut, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB DIVE WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRBDiveOut, this.rbDiveOut, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION STRONG", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockOut, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB POWER WEAK", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleLeft, this.runBlockT, this.pullBlockOut, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleOut, this.rbOffTackleOut, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB POWER STRONG", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockOut, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockWR, this.runBlockWR, this.runBlockWR, this.runBlockWR));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN STRONG", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.pullBlockOut, this.pullBlockOut, this.qbPassPlay, this.rbScreenIn, this.fly, this.fly, this.fly, this.fly));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN WEAK", PlayConcept.Screen_Pass, this.runPos_screenLeft, this.pullBlockOut, this.pullBlockOut, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbScreenOut, this.fly, this.fly, this.fly, this.fly));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA DIG", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.runBlockC, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.isoBlockIn, this.dragIn, this.post10skinny, this.slantIn, this.post10flat));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.isoBlockIn, this.dragIn, this.fly, this.post10skinny, this.slantIn));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA HAMMER", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_Out, this.out10, this.slantInUpCorner, this.fly, this.post10flat));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA VERTICAL", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.rbIsoIn_In, this.slantInHighPostFlat, this.slantOutFly, this.slantOutFly, this.fly));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA SMASH", PlayConcept.Play_Action, 6, this.passBlockT_PA, this.passBlockG_PA, this.passBlockC_PA, this.passBlockG_PA, this.passBlockT_PA, this.qbIsoIn, this.isoBlockIn, this.fly, this.hitch10in, this.hitch10in, this.slantInPostFlat));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SLANT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.slantIn, this.slantIn, this.slantIn));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "Inside Slot Pick", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.slantIn, this.slantIn, this.dragOutFromSlot));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLOT UNDER", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.dragOut, this.in5, this.corner10, this.out5));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SMASH", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.out5, this.corner10, this.corner10, this.post10flat));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "OUTS", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragOut, this.out15, this.out15, this.in10));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "WEAK FLOOD", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.fly, this.post10, this.slantIn));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "X SPOT", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.slantInUpCorner, this.fly, this.post10flat));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "WRA SPECIAL", PlayConcept.Mid_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.corner10, this.in10, this.post5, this.dragInFromSlot));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "WRA SCREEN STRONG", PlayConcept.Screen_Pass, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.passBlockC_PA, this.runBlockG, this.runBlockT, this.qbPassPlay, this.rbFlatIn, this.teAround, this.fly, this.fly, this.fly));
    this.singleBackPlays_Trio4WR.AddPlay((PlayData) new PlayDataOff(this.singleBack_Trio4WR, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB FLATS", PlayConcept.Screen_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.fly, this.fly, this.fly, this.fly));
  }

  private void SetPlays_SplitBack_Normal()
  {
    this.splitBackPlays_Normal = new FormationData(FormationType.Offense, this.splitBack_Normal);
    this.splitBackPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.splitBack_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "OFF TACKLE LEAD", PlayConcept.Outside_Run, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRHBAcrossIso, this.rhbSplitAcrossIso, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.splitBackPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.splitBack_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "RB LEAD", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRHBDive, this.rbSplitDive, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockIn));
    this.splitBackPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.splitBack_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "OUTSIDE TRAP", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.pullBlockOut, this.runBlockG, this.runBlockT, this.qbRHBAcrossIso, this.rhbSplitAcrossIso, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.splitBackPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.splitBack_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA SWITCH", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRHBDive, this.splitBackIso_In, this.hitch10out, this.in5, this.post5flat, this.rbFlatOut_Fly));
    this.splitBackPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.splitBack_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "TE SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.screenBlockRBOut, this.teScreenRoute, this.post5, this.runBlockWR, this.screenBlockRBIn));
    this.splitBackPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.splitBack_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 10, "RB CROSS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbDiveIn_In, this.corner5flat, this.post5flat, this.post5flat, this.rbOffTackleOut_In));
    this.splitBackPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.splitBack_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "CLEAR FLATS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.hitch10in, this.post5, this.slantIn, this.passBlockRB));
    this.splitBackPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.splitBack_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "OUTSIDE LEAD", PlayConcept.Outside_Run, this.runPos_reverseToLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbRHBAcrossIso, this.rhbSplitAcrossIso, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.splitBackPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.splitBack_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "WEAK SCREEN", PlayConcept.Screen_Pass, this.runPos_screenLeft, this.screenBlockOLOut, this.screenBlockOLOut, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbScreenIn, this.slantIn, this.fly, this.fly, this.diveBlockOut));
    this.splitBackPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.splitBack_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "QUICK SLANTS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.slantOut, this.corner5, this.slantIn, this.slantIn, this.slantOut));
    this.splitBackPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.splitBack_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "INSIDE SCREEN", PlayConcept.Screen_Pass, this.runPos_actualPosition, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.screenBlockOLOut, this.qbPassPlay, this.diveBlockOut, this.screenBlockOLOut, this.fly, this.wrScreenRoute, this.slantOut));
    this.splitBackPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.splitBack_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 7, "PA SAFETY RUSH", PlayConcept.Play_Action, 10, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbLHBDive, this.slantOut, this.hitch10in, this.in10, this.post5skinny, this.splitBackIso_Out));
  }

  private void SetPlays_StrongI_Close()
  {
    this.strongIPlays_Close = new FormationData(FormationType.Offense, this.strongI_Close);
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "RB LEAD", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenIn, this.runBlockTE, this.post5, this.fly, this.rbFlatOut_Fly));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post10, this.in15, this.rbScreenOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "TE DRAG", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.post10flat, this.fly, this.fbDiveIn_In));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "DEUCE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.out5, this.in10, this.slantIn, this.passBlockRB));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "WEAK TRAP", PlayConcept.Inside_Run_With_Pulling_OL, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 10, "FB DIVE STRONG", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbFBDiveIn, this.isoBlockOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.fbDive_OffsetRight));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 10, "FB FLARE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.corner5, this.hitch10in, this.in10, this.rbFlatOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "SWING BACKS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner10, this.post5, this.in5, this.rbFlatOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SHUTTER", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.hitch10in, this.slantIn, this.post5skinny, this.fbDiveIn_Out));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA X COMEBACK", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.upOutFly, this.upPostHitch, this.slantIn, this.rbFlatOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbOffTackleOut_In, this.out5, this.in10, this.slantInUpCorner, this.isoBlockIn));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "CROSS IN", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInIn, this.in10, this.slantInUpPost, this.isoBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURLS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantInHitch, this.hitch10in, this.hitch10in, this.rbFlatOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "GOALLINE FADE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "RB BLAST", PlayConcept.Outside_Run, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleOut, this.rbOffTackleOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB COUNTER", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION", PlayConcept.Misdirection, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "MID ATTACK", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.post10, this.out10, this.hitch10in, this.rbOffTackleOut_In));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA BOOT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.isoBlockOut, this.dragOut, this.slantInIn, this.hitch10out, this.isoBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA CANE", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.post5, this.in5, this.post10, this.rbFlatOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA CLEAROUT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.fly, this.fly, this.fly, this.rbFlatOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA Z UNDER", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner10, this.post5, this.dragInFromSlot, this.rbFlatIn));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA FB FLAT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post5, this.fly, this.rbFlatIn));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA POST WHEEL", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.in5, this.post10, this.dragInFromSlot, this.rbFlatOut_Fly));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA POWER O", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner5, this.post5flat, this.fly, this.rbFlatOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA SCISSORS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.corner5, this.post5flat, this.post10, this.rbFlatIn));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA STREAKS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.post5, this.fly, this.fly, this.fbDiveIn_Out));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA WR IN", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.slantInIn, this.in10, this.post10skinny, this.rbFlatOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "POST FLAGS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.post10, this.post5Corner, this.post5Corner, this.rbFlatOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "POWER O", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.slantIn, this.slantIn, this.slantIn, this.isoBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SEAM 678", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner5, this.in5, this.post10, this.rbFlatOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SHORT FLYS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantInHitch, this.post5skinny, this.post5skinny, this.isoBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLANT AND GO", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.in5, this.slantInFly, this.slantInFly, this.rbFlatOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantIn, this.post5flat, this.post5flat, this.isoBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLUGGOS", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.fly, this.slantInFly, this.post5Corner, this.isoBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.in10, this.isoBlockIn));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB SLAM", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB ISO WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA DBL GO'S", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.slantInIn, this.fly, this.upCornerFly, this.diveBlockOut));
    this.strongIPlays_Close.AddPlay((PlayData) new PlayDataOff(this.strongI_Close, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS TE DRAG", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.post10, this.post5skinny, this.isoBlockOut));
  }

  private void SetPlays_StrongI_Normal()
  {
    this.strongIPlays_Normal = new FormationData(FormationType.Offense, this.strongI_Normal);
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "RB LEAD", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenIn, this.runBlockTE, this.post5, this.fly, this.rbFlatOut_Fly));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post10, this.in15, this.rbScreenOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "TE DRAG", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.post10flat, this.fly, this.fbDiveIn_In));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "DEUCE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.out5, this.in10, this.slantIn, this.passBlockRB));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "WEAK TRAP", PlayConcept.Inside_Run_With_Pulling_OL, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 10, "FB DIVE STRONG", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbFBDiveIn, this.isoBlockOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.fbDive_OffsetRight));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 10, "FB FLARE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.corner5, this.hitch10in, this.in10, this.rbFlatOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "SWING BACKS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner10, this.post5, this.in5, this.rbFlatOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SHUTTER", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.hitch10in, this.slantIn, this.post5skinny, this.fbDiveIn_Out));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA X COMEBACK", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.upOutFly, this.upPostHitch, this.slantIn, this.rbFlatOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbOffTackleOut_In, this.out5, this.in10, this.slantInUpCorner, this.isoBlockIn));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "CROSS IN", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInIn, this.in10, this.slantInUpPost, this.isoBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURLS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantInHitch, this.hitch10in, this.hitch10in, this.rbFlatOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "GOALLINE FADE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "RB BLAST", PlayConcept.Outside_Run, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleOut, this.rbOffTackleOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB COUNTER", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION", PlayConcept.Misdirection, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "MID ATTACK", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.post10, this.out10, this.hitch10in, this.rbOffTackleOut_In));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA BOOT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.isoBlockOut, this.dragOut, this.slantInIn, this.hitch10out, this.isoBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA CANE", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.post5, this.in5, this.post10, this.rbFlatOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA CLEAROUT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.fly, this.fly, this.fly, this.rbFlatOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA Z UNDER", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner10, this.post5, this.dragInFromSlot, this.rbFlatIn));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA FB FLAT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post5, this.fly, this.rbFlatIn));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA POST WHEEL", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.in5, this.post10, this.dragInFromSlot, this.rbFlatOut_Fly));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA POWER O", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner5, this.post5flat, this.fly, this.rbFlatOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA SCISSORS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.corner5, this.post5flat, this.post10, this.rbFlatIn));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA STREAKS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.post5, this.fly, this.fly, this.fbDiveIn_Out));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA WR IN", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.slantInIn, this.in10, this.post10skinny, this.rbFlatOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "POST FLAGS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.post10, this.post5Corner, this.post5Corner, this.rbFlatOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "POWER O", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.slantIn, this.slantIn, this.slantIn, this.isoBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SEAM 678", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner5, this.in5, this.post10, this.rbFlatOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SHORT FLYS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantInHitch, this.post5skinny, this.post5skinny, this.isoBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLANT AND GO", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.in5, this.slantInFly, this.slantInFly, this.rbFlatOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantIn, this.post5flat, this.post5flat, this.isoBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLUGGOS", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.fly, this.slantInFly, this.post5Corner, this.isoBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.in10, this.isoBlockIn));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB SLAM", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB ISO WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA DBL GO'S", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.slantInIn, this.fly, this.upCornerFly, this.diveBlockOut));
    this.strongIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.strongI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS TE DRAG", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.post5skinny, this.post5skinny, this.isoBlockOut));
  }

  private void SetPlays_StrongI_Tight()
  {
    this.strongIPlays_Tight = new FormationData(FormationType.Offense, this.strongI_Tight);
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "RB LEAD", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenIn, this.runBlockTE, this.post5, this.fly, this.rbFlatOut_Fly));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post10, this.in15, this.rbScreenOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "TE DRAG", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.post10flat, this.fly, this.fbDiveIn_In));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "DEUCE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.out5, this.in10, this.slantIn, this.passBlockRB));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "WEAK TRAP", PlayConcept.Inside_Run_With_Pulling_OL, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 10, "FB DIVE STRONG", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbFBDiveIn, this.isoBlockOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.fbDive_OffsetRight));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 10, "FB FLARE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.corner5, this.hitch10in, this.in10, this.rbFlatOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "SWING BACKS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner10, this.post5, this.in5, this.rbFlatOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SHUTTER", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.hitch10in, this.slantIn, this.post5skinny, this.fbDiveIn_Out));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA X COMEBACK", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.upOutFly, this.upPostHitch, this.slantIn, this.rbFlatOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbOffTackleOut_In, this.out5, this.in10, this.slantInUpCorner, this.isoBlockIn));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "CROSS IN", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInIn, this.in10, this.slantInUpPost, this.isoBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURLS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantInHitch, this.hitch10in, this.hitch10in, this.rbFlatOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "GOALLINE FADE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "RB BLAST", PlayConcept.Outside_Run, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleOut, this.rbOffTackleOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB COUNTER", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION", PlayConcept.Misdirection, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "MID ATTACK", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.post10, this.out10, this.hitch10in, this.rbOffTackleOut_In));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA BOOT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.isoBlockOut, this.dragOut, this.slantInIn, this.hitch10out, this.isoBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA CANE", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.post5, this.in5, this.post10, this.rbFlatOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA CLEAROUT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.fly, this.fly, this.fly, this.rbFlatOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA Z UNDER", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner10, this.post5, this.dragInFromSlot, this.rbFlatIn));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA FB FLAT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post5, this.fly, this.rbFlatIn));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA POST WHEEL", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.in5, this.post10, this.dragInFromSlot, this.rbFlatOut_Fly));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA POWER O", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner5, this.post5flat, this.fly, this.rbFlatOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA SCISSORS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.corner5, this.post5flat, this.post10, this.rbFlatIn));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA STREAKS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.post5, this.fly, this.fly, this.fbDiveIn_Out));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA WR IN", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.slantInIn, this.in10, this.post10skinny, this.rbFlatOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "POST FLAGS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.post10, this.post5Corner, this.post5Corner, this.rbFlatOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "POWER O", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.slantIn, this.slantIn, this.slantIn, this.isoBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SEAM 678", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner5, this.in5, this.post10, this.rbFlatOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SHORT FLYS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantInHitch, this.post5skinny, this.post5skinny, this.isoBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLANT AND GO", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.in5, this.slantInFly, this.slantInFly, this.rbFlatOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantIn, this.post5flat, this.post5flat, this.isoBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLUGGOS", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.fly, this.slantInFly, this.post5Corner, this.isoBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.in10, this.isoBlockIn));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB SLAM", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB ISO WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA DBL GO'S", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.slantInIn, this.fly, this.upCornerFly, this.diveBlockOut));
    this.strongIPlays_Tight.AddPlay((PlayData) new PlayDataOff(this.strongI_Tight, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS TE DRAG", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.post5skinny, this.post5skinny, this.isoBlockOut));
  }

  private void SetPlays_StrongI_Twins()
  {
    this.strongIPlays_Twins = new FormationData(FormationType.Offense, this.strongI_Twins);
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "RB LEAD", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenIn, this.runBlockTE, this.post5, this.fly, this.rbFlatOut_Fly));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post10, this.in15, this.rbScreenOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "TE DRAG", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.post10flat, this.fly, this.fbDiveIn_In));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "DEUCE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.out5, this.in10, this.slantIn, this.passBlockRB));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "WEAK TRAP", PlayConcept.Inside_Run_With_Pulling_OL, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 10, "FB DIVE STRONG", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbFBDiveIn, this.isoBlockOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.fbDive_OffsetRight));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 10, "FB FLARE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.corner5, this.hitch10in, this.in10, this.rbFlatOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "SWING BACKS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner10, this.post5, this.in5, this.rbFlatOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SHUTTER", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.hitch10in, this.slantIn, this.post5skinny, this.fbDiveIn_Out));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA X COMEBACK", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.upOutFly, this.upPostHitch, this.slantIn, this.rbFlatOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbOffTackleOut_In, this.out5, this.in10, this.slantInUpCorner, this.isoBlockIn));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "CROSS IN", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInIn, this.in10, this.slantInUpPost, this.isoBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURLS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantInHitch, this.hitch10in, this.hitch10in, this.rbFlatOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "GOALLINE FADE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "RB BLAST", PlayConcept.Outside_Run, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleOut, this.rbOffTackleOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB COUNTER", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION", PlayConcept.Misdirection, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "MID ATTACK", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.post10, this.out10, this.hitch10in, this.rbOffTackleOut_In));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA BOOT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.isoBlockOut, this.dragOut, this.slantInIn, this.hitch10out, this.isoBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA CANE", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.post5, this.in5, this.post10, this.rbFlatOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA CLEAROUT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.fly, this.fly, this.fly, this.rbFlatOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA Z UNDER", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner10, this.post5, this.dragInFromSlot, this.rbFlatIn));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA FB FLAT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post5, this.fly, this.rbFlatIn));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA POST WHEEL", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.in5, this.post10, this.dragInFromSlot, this.rbFlatOut_Fly));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA POWER O", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner5, this.post5flat, this.fly, this.rbFlatOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA SCISSORS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.corner5, this.post5flat, this.post10, this.rbFlatIn));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA STREAKS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.post5, this.fly, this.fly, this.fbDiveIn_Out));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA WR IN", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.slantInIn, this.in10, this.post10skinny, this.rbFlatOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "POST FLAGS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.post10, this.post5Corner, this.post5Corner, this.rbFlatOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "POWER O", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.slantIn, this.slantIn, this.slantIn, this.isoBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SEAM 678", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner5, this.in5, this.post10, this.rbFlatOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SHORT FLYS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantInHitch, this.post5skinny, this.post5skinny, this.isoBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLANT AND GO", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.in5, this.slantInFly, this.slantInFly, this.rbFlatOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantIn, this.post5flat, this.post5flat, this.isoBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLUGGOS", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.fly, this.slantInFly, this.post5Corner, this.isoBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.in10, this.isoBlockIn));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB SLAM", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB ISO WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA DBL GO'S", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.slantInIn, this.fly, this.upCornerFly, this.diveBlockOut));
    this.strongIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.strongI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS TE DRAG", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.post5skinny, this.post5skinny, this.isoBlockOut));
  }

  private void SetPlays_StrongI_TwinsFlex()
  {
    this.strongIPlays_TwinsFlex = new FormationData(FormationType.Offense, this.strongI_TwinsFlex);
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "RB LEAD", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenIn, this.runBlockTE, this.post5, this.fly, this.rbFlatOut_Fly));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post10, this.in15, this.rbScreenOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "TE DRAG", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.post10flat, this.fly, this.fbDiveIn_In));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "DEUCE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.out5, this.in10, this.slantIn, this.passBlockRB));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "WEAK TRAP", PlayConcept.Inside_Run_With_Pulling_OL, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.PistolHole2, 10, "FB DIVE STRONG", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbFBDiveIn, this.isoBlockOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.fbDive_OffsetRight));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 10, "FB FLARE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.corner5, this.hitch10in, this.in10, this.rbFlatOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "SWING BACKS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner10, this.post5, this.in5, this.rbFlatOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SHUTTER", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.hitch10in, this.slantIn, this.post5skinny, this.fbDiveIn_Out));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA X COMEBACK", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.upOutFly, this.upPostHitch, this.slantIn, this.rbFlatOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbOffTackleOut_In, this.out5, this.in10, this.slantInUpCorner, this.isoBlockIn));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "CROSS IN", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInIn, this.in10, this.slantInUpPost, this.isoBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURLS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantInHitch, this.hitch10in, this.hitch10in, this.rbFlatOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "GOALLINE FADE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "RB BLAST", PlayConcept.Outside_Run, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleOut, this.rbOffTackleOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB COUNTER", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION", PlayConcept.Misdirection, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "MID ATTACK", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.post10, this.out10, this.hitch10in, this.rbOffTackleOut_In));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA BOOT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.isoBlockOut, this.dragOut, this.slantInIn, this.hitch10out, this.isoBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA CANE", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.post5, this.in5, this.post10, this.rbFlatOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA CLEAROUT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.fly, this.fly, this.fly, this.rbFlatOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA Z UNDER", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner10, this.post5, this.dragInFromSlot, this.rbFlatIn));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA FB FLAT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post5, this.fly, this.rbFlatIn));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA POST WHEEL", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.in5, this.post10, this.dragInFromSlot, this.rbFlatOut_Fly));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA POWER O", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner5, this.post5flat, this.fly, this.rbFlatOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA SCISSORS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.corner5, this.post5flat, this.post10, this.rbFlatIn));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA STREAKS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.post5, this.fly, this.fly, this.fbDiveIn_Out));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA WR IN", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.slantInIn, this.in10, this.post10skinny, this.rbFlatOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "POST FLAGS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.post10, this.post5Corner, this.post5Corner, this.rbFlatOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "POWER O", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.slantIn, this.slantIn, this.slantIn, this.isoBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SEAM 678", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner5, this.in5, this.post10, this.rbFlatOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SHORT FLYS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantInHitch, this.post5skinny, this.post5skinny, this.isoBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLANT AND GO", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.in5, this.slantInFly, this.slantInFly, this.rbFlatOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantIn, this.post5flat, this.post5flat, this.isoBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLUGGOS", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.fly, this.slantInFly, this.post5Corner, this.isoBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.in10, this.isoBlockIn));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB SLAM", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB ISO WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA DBL GO'S", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.slantInIn, this.fly, this.upCornerFly, this.diveBlockOut));
    this.strongIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS TE DRAG", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.post5skinny, this.post5skinny, this.isoBlockOut));
  }

  private void SetPlays_StrongI_TwinTE()
  {
    this.strongIPlays_TwinTE = new FormationData(FormationType.Offense, this.strongI_TwinTE);
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "RB LEAD", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenIn, this.runBlockTE, this.post5, this.fly, this.rbFlatOut_Fly));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post10, this.out15, this.rbScreenOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "TE DRAG", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.post10flat, this.fly, this.fbDiveIn_In));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "DEUCE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.out5, this.in10, this.slantIn, this.passBlockRB));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "WEAK TRAP", PlayConcept.Inside_Run_With_Pulling_OL, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.ShotgunRightSideHole1, 10, "FB DIVE STRONG", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbFBDiveIn, this.isoBlockOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.fbDive_OffsetRight));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 10, "FB FLARE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.corner5, this.hitch10in, this.in10, this.rbFlatOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "SWING BACKS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner10, this.post5, this.in10, this.rbFlatOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SHUTTER", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.hitch10in, this.slantIn, this.post5skinny, this.fbDiveIn_Out));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA X COMEBACK", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.upOutFly, this.upPostHitch, this.slantIn, this.rbFlatOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbOffTackleOut_In, this.out5, this.in10, this.slantInUpCorner, this.isoBlockIn));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "CROSS IN", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInIn, this.in10, this.slantInUpPost, this.isoBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURLS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantInHitch, this.hitch10in, this.hitch10in, this.rbFlatOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "GOALLINE FADE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "RB BLAST", PlayConcept.Outside_Run, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleOut, this.rbOffTackleOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB COUNTER", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION", PlayConcept.Misdirection, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "MID ATTACK", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.post10, this.out10, this.hitch10in, this.rbOffTackleOut_In));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA BOOT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.isoBlockOut, this.dragOut, this.slantInIn, this.hitch10out, this.isoBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA CANE", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.post5, this.in5, this.post10, this.rbFlatOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA CLEAROUT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.fly, this.fly, this.fly, this.rbFlatOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA Z UNDER", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner10, this.post5, this.dragInFromSlot, this.rbFlatIn));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA FB FLAT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post5, this.fly, this.rbFlatIn));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA POST WHEEL", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.in5, this.post10, this.dragInFromSlot, this.rbFlatOut_Fly));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA POWER O", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner5, this.post5flat, this.fly, this.rbFlatOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA SCISSORS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.corner5, this.post5flat, this.post10, this.rbFlatIn));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA STREAKS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.post5, this.fly, this.fly, this.fbDiveIn_Out));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA WR IN", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.slantInIn, this.in10, this.post10skinny, this.rbFlatOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "POST FLAGS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.post10, this.post5Corner, this.post5Corner, this.rbFlatOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "POWER O", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.slantIn, this.slantIn, this.slantIn, this.isoBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SEAM 678", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner5, this.in5, this.post10, this.rbFlatOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SHORT FLYS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantInHitch, this.post5skinny, this.post5skinny, this.isoBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLANT AND GO", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.in5, this.slantInFly, this.slantInFly, this.rbFlatOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantIn, this.post5flat, this.post5flat, this.isoBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLUGGOS", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.fly, this.slantInFly, this.post5Corner, this.isoBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.in10, this.isoBlockIn));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB SLAM", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB ISO WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA DBL GO'S", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.slantInIn, this.fly, this.upCornerFly, this.diveBlockOut));
    this.strongIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.strongI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS TE DRAG", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.post5skinny, this.post5skinny, this.isoBlockOut));
  }

  private void SetPlays_WeakI_CloseTwins()
  {
    this.weakIPlays_CloseTwins = new FormationData(FormationType.Offense, this.weakI_CloseTwins);
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "RB LEAD", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenIn, this.runBlockTE, this.post5, this.fly, this.rbFlatOut_Fly));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post10, this.in15, this.rbScreenOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "TE DRAG", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.post10flat, this.fly, this.fbDiveIn_In));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "DEUCE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.out5, this.in10, this.slantIn, this.passBlockRB));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "WEAK TRAP", PlayConcept.Inside_Run_With_Pulling_OL, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 10, "FB FLARE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.corner5, this.hitch10in, this.in10, this.rbFlatOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "SWING BACKS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner10, this.post5, this.in5, this.rbFlatOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SHUTTER", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.hitch10in, this.slantIn, this.post5skinny, this.fbDiveIn_Out));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA X COMEBACK", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.upOutFly, this.upPostHitch, this.slantIn, this.rbFlatOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbOffTackleOut_In, this.out5, this.in10, this.slantInUpCorner, this.isoBlockIn));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "CROSS IN", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInIn, this.in10, this.slantInUpPost, this.isoBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURLS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantInHitch, this.hitch10in, this.hitch10in, this.rbFlatOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "GOALLINE FADE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "RB BLAST", PlayConcept.Outside_Run, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleOut, this.rbOffTackleOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB COUNTER", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION", PlayConcept.Misdirection, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "MID ATTACK", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.post10, this.out10, this.hitch10in, this.rbOffTackleOut_In));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA BOOT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.isoBlockOut, this.dragOut, this.slantInIn, this.hitch10out, this.isoBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA CANE", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.post5, this.in5, this.post10, this.rbFlatOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA CLEAROUT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.fly, this.fly, this.fly, this.rbFlatOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA Z UNDER", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner10, this.post5, this.dragInFromSlot, this.rbFlatIn));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA FB FLAT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post5, this.fly, this.rbFlatIn));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA POST WHEEL", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.in5, this.post10, this.dragInFromSlot, this.rbFlatOut_Fly));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA POWER O", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner5, this.post5flat, this.fly, this.rbFlatOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA SCISSORS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.corner5, this.post5flat, this.post10, this.rbFlatIn));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA STREAKS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.post5, this.fly, this.fly, this.fbDiveIn_Out));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA WR IN", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.slantInIn, this.in10, this.post10skinny, this.rbFlatOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "POST FLAGS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.post10, this.post5Corner, this.post5Corner, this.rbFlatOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "POWER O", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.slantIn, this.slantIn, this.slantIn, this.isoBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SEAM 678", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.corner5, this.in5, this.post10, this.rbFlatOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SHORT FLYS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantInHitch, this.post5skinny, this.post5skinny, this.isoBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLANT AND GO", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.in5, this.slantInFly, this.slantInFly, this.rbFlatOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantIn, this.post5flat, this.post5flat, this.isoBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLUGGOS", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.fly, this.slantInFly, this.post5Corner, this.isoBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.in10, this.isoBlockIn));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB SLAM", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB ISO WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA DBL GO'S", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.slantInIn, this.fly, this.upCornerFly, this.diveBlockOut));
    this.weakIPlays_CloseTwins.AddPlay((PlayData) new PlayDataOff(this.weakI_CloseTwins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS TE DRAG", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.post5skinny, this.post5skinny, this.isoBlockOut));
  }

  private void SetPlays_WeakI_Normal()
  {
    this.weakIPlays_Normal = new FormationData(FormationType.Offense, this.weakI_Normal);
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "RB LEAD", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenIn, this.runBlockTE, this.post5, this.fly, this.rbFlatOut_Fly));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post10, this.in15, this.rbScreenOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "TE DRAG", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.post10flat, this.fly, this.fbDiveIn_In));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "DEUCE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.out5, this.in10, this.slantIn, this.passBlockRB));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "WEAK TRAP", PlayConcept.Inside_Run_With_Pulling_OL, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 10, "FB FLARE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockIn, this.corner5, this.hitch10in, this.in10, this.rbFlatOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "SWING BACKS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner10, this.post5, this.in5, this.rbFlatOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SHUTTER", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.hitch10in, this.slantIn, this.post5skinny, this.fbDiveIn_Out));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA X COMEBACK", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.upOutFly, this.upPostHitch, this.slantIn, this.rbFlatOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbOffTackleOut_In, this.out5, this.in10, this.slantInUpCorner, this.isoBlockIn));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "CROSS IN", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInIn, this.in10, this.slantInUpPost, this.isoBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURLS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInHitch, this.hitch10in, this.hitch10in, this.rbFlatOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "GOALLINE FADE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "RB BLAST", PlayConcept.Outside_Run, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleOut, this.rbOffTackleOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB COUNTER", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION", PlayConcept.Misdirection, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "MID ATTACK", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.post10, this.out10, this.hitch10in, this.rbOffTackleOut_In));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA BOOT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.isoBlockIn, this.dragOut, this.slantInIn, this.hitch10out, this.isoBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA CANE", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.post5, this.in5, this.post10, this.rbFlatOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA CLEAROUT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.fly, this.fly, this.fly, this.rbFlatOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA Z UNDER", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner10, this.post5, this.dragInFromSlot, this.rbFlatIn));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA FB FLAT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post5, this.fly, this.rbFlatOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA POST WHEEL", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.in5, this.post10, this.dragInFromSlot, this.rbFlatOut_Fly));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA POWER O", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner5, this.post5flat, this.fly, this.rbFlatOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA SCISSORS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.corner5, this.post5flat, this.post10, this.rbFlatOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA STREAKS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_In, this.post5, this.fly, this.fly, this.fbDiveIn_Out));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA WR IN", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.slantInIn, this.in10, this.post10skinny, this.rbFlatOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "POST FLAGS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.post10, this.post5Corner, this.post5Corner, this.rbFlatOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "POWER O", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut_Fly, this.slantIn, this.slantIn, this.slantIn, this.isoBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SEAM 678", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner5, this.in5, this.post10, this.rbFlatOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SHORT FLYS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.slantInHitch, this.post5skinny, this.post5skinny, this.isoBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLANT AND GO", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.in5, this.slantInFly, this.slantInFly, this.rbFlatOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantIn, this.post5flat, this.post5flat, this.isoBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLUGGOS", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.fly, this.slantInFly, this.post5Corner, this.isoBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.in10, this.isoBlockIn));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB SLAM", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB ISO WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA DBL GO'S", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.slantInIn, this.fly, this.upCornerFly, this.diveBlockOut));
    this.weakIPlays_Normal.AddPlay((PlayData) new PlayDataOff(this.weakI_Normal, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS TE DRAG", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.post5skinny, this.post5skinny, this.isoBlockOut));
  }

  private void SetPlays_WeakI_Twins()
  {
    this.weakIPlays_Twins = new FormationData(FormationType.Offense, this.weakI_Twins);
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "RB LEAD", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenIn, this.runBlockTE, this.post5, this.fly, this.rbFlatOut_Fly));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post10, this.in15, this.rbScreenOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "TE DRAG", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.post10flat, this.fly, this.fbDiveIn_In));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "DEUCE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.out5, this.in10, this.slantIn, this.passBlockRB));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "WEAK TRAP", PlayConcept.Inside_Run_With_Pulling_OL, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 10, "FB FLARE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockIn, this.corner5, this.hitch10in, this.in10, this.rbFlatOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "SWING BACKS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner10, this.post5, this.in5, this.rbFlatOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SHUTTER", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.hitch10in, this.slantIn, this.post5skinny, this.fbDiveIn_Out));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA X COMEBACK", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.upOutFly, this.upPostHitch, this.slantIn, this.rbFlatOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbOffTackleOut_In, this.out5, this.in10, this.slantInUpCorner, this.isoBlockIn));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "CROSS IN", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInIn, this.in10, this.slantInUpPost, this.isoBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURLS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInHitch, this.hitch10in, this.hitch10in, this.rbFlatOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "GOALLINE FADE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "RB BLAST", PlayConcept.Outside_Run, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleOut, this.rbOffTackleOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB COUNTER", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION", PlayConcept.Misdirection, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "MID ATTACK", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.post10, this.out10, this.hitch10in, this.rbOffTackleOut_In));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA BOOT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.isoBlockIn, this.dragOut, this.slantInIn, this.hitch10out, this.isoBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA CANE", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.post5, this.in5, this.post10, this.rbFlatOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA CLEAROUT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.fly, this.fly, this.fly, this.rbFlatOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA Z UNDER", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner10, this.post5, this.dragInFromSlot, this.rbFlatOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA FB FLAT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post5, this.fly, this.rbFlatOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA POST WHEEL", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.in5, this.post10, this.dragInFromSlot, this.rbFlatOut_Fly));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA POWER O", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner5, this.post5flat, this.fly, this.rbFlatOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA SCISSORS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.corner5, this.post5flat, this.post10, this.rbFlatOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA STREAKS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.post5, this.fly, this.fly, this.fbDiveIn_Out));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA WR IN", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.slantInIn, this.in10, this.post10skinny, this.rbFlatOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "POST FLAGS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.post10, this.post5Corner, this.post5Corner, this.rbFlatOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "POWER O", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.slantIn, this.slantIn, this.slantIn, this.isoBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SEAM 678", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner5, this.in5, this.post10, this.rbFlatOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SHORT FLYS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInHitch, this.post5skinny, this.post5skinny, this.isoBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLANT AND GO", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.in5, this.slantInFly, this.slantInFly, this.rbFlatOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantIn, this.post5flat, this.post5flat, this.isoBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLUGGOS", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.fly, this.slantInFly, this.post5Corner, this.isoBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.in10, this.isoBlockIn));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB SLAM", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB ISO WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA DBL GO'S", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoOut_Out, this.slantInIn, this.fly, this.upCornerFly, this.diveBlockOut));
    this.weakIPlays_Twins.AddPlay((PlayData) new PlayDataOff(this.weakI_Twins, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS TE DRAG", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.post5skinny, this.post5skinny, this.isoBlockOut));
  }

  private void SetPlays_WeakI_TwinsFlex()
  {
    this.weakIPlays_TwinsFlex = new FormationData(FormationType.Offense, this.weakI_TwinsFlex);
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "RB LEAD", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenIn, this.runBlockTE, this.post5, this.fly, this.rbFlatOut_Fly));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post10, this.in15, this.rbScreenOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "TE DRAG", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.post10flat, this.fly, this.fbDiveIn_In));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "DEUCE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.out5, this.in10, this.slantIn, this.passBlockRB));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "WEAK TRAP", PlayConcept.Inside_Run_With_Pulling_OL, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 10, "FB FLARE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockIn, this.corner5, this.hitch10in, this.in10, this.rbFlatOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "SWING BACKS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner10, this.post5, this.in5, this.rbFlatOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SHUTTER", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.hitch10in, this.slantIn, this.post5skinny, this.fbDiveIn_Out));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA X COMEBACK", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.upOutFly, this.upPostHitch, this.slantIn, this.rbFlatOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbOffTackleOut_In, this.out5, this.in10, this.slantInUpCorner, this.isoBlockIn));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "CROSS IN", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInIn, this.in10, this.slantInUpPost, this.isoBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURLS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInHitch, this.hitch10in, this.hitch10in, this.rbFlatOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "GOALLINE FADE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "RB BLAST", PlayConcept.Outside_Run, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleOut, this.rbOffTackleOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB COUNTER", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION", PlayConcept.Misdirection, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "MID ATTACK", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.post10, this.out10, this.hitch10in, this.rbOffTackleOut_In));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA BOOT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.isoBlockIn, this.dragOut, this.slantInIn, this.hitch10out, this.isoBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA CANE", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.post5, this.in5, this.post10, this.rbFlatOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA CLEAROUT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.fly, this.fly, this.fly, this.rbFlatOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA Z UNDER", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner10, this.post5, this.dragInFromSlot, this.rbFlatOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA FB FLAT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post5, this.fly, this.rbFlatOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA POST WHEEL", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.in5, this.post10, this.dragInFromSlot, this.rbFlatOut_Fly));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA POWER O", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner5, this.post5flat, this.fly, this.rbFlatOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA SCISSORS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.corner5, this.post5flat, this.post10, this.rbFlatOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA STREAKS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.post5, this.fly, this.fly, this.fbDiveIn_Out));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA WR IN", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.slantInIn, this.in10, this.post10skinny, this.rbFlatOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "POST FLAGS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.post10, this.post5Corner, this.post5Corner, this.rbFlatOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "POWER O", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.slantIn, this.slantIn, this.slantIn, this.isoBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SEAM 678", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner5, this.in5, this.post10, this.rbFlatOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SHORT FLYS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInHitch, this.post5skinny, this.post5skinny, this.isoBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLANT AND GO", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.in5, this.slantInFly, this.slantInFly, this.rbFlatOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantIn, this.post5flat, this.post5flat, this.isoBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLUGGOS", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.fly, this.slantInFly, this.post5Corner, this.isoBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.in10, this.isoBlockIn));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB SLAM", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB ISO WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA DBL GO'S", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoOut_Out, this.slantInIn, this.fly, this.upCornerFly, this.diveBlockOut));
    this.weakIPlays_TwinsFlex.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS TE DRAG", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.post5skinny, this.post5skinny, this.isoBlockOut));
  }

  private void SetPlays_WeakI_TwinTE()
  {
    this.weakIPlays_TwinTE = new FormationData(FormationType.Offense, this.weakI_TwinTE);
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "RB LEAD", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 6, "HB SCREEN", PlayConcept.Screen_Pass, this.runPos_screenRight, this.passBlockT, this.passBlockG, this.passBlockC, this.screenBlockOLOut, this.screenBlockOLOut, this.qbPassPlay, this.rbScreenIn, this.runBlockTE, this.post5, this.fly, this.rbFlatOut_Fly));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA STRETCH", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post10, this.in15, this.rbScreenOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "TE DRAG", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatOut, this.dragIn, this.post10flat, this.fly, this.fbDiveIn_In));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 7, "DEUCE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.out5, this.in10, this.slantIn, this.passBlockRB));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "WEAK TRAP", PlayConcept.Inside_Run_With_Pulling_OL, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.pullBlockIn, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 10, "FB FLARE", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockIn, this.corner5, this.hitch10in, this.in10, this.rbFlatOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 6, "SWING BACKS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner10, this.post5, this.in5, this.rbFlatOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 8, "SHUTTER", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.hitch10in, this.slantIn, this.post5skinny, this.fbDiveIn_Out));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA X COMEBACK", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.upOutFly, this.upPostHitch, this.slantIn, this.rbFlatOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "CORNER STRIKE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbOffTackleOut_In, this.out5, this.in10, this.slantInUpCorner, this.isoBlockIn));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinsFlex, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "CROSS IN", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInIn, this.in10, this.slantInUpPost, this.isoBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "CURLS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInHitch, this.hitch10in, this.hitch10in, this.rbFlatOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "GOALLINE FADE", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "RB BLAST", PlayConcept.Outside_Run, this.runPos_offTackleLeft, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleOut, this.rbOffTackleOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB COUNTER", PlayConcept.Misdirection_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB MISDIRECTION", PlayConcept.Misdirection, this.runPos_offTackleRight, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbCounterIn, this.rbCounterIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 7, "MID ATTACK", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.isoBlockOut, this.post10, this.out10, this.hitch10in, this.rbOffTackleOut_In));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA BOOT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.isoBlockIn, this.dragOut, this.slantInIn, this.hitch10out, this.isoBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA CANE", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_Out, this.post5, this.in5, this.post10, this.rbFlatOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA CLEAROUT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.fly, this.fly, this.fly, this.rbFlatOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA Z UNDER", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner10, this.post5, this.dragInFromSlot, this.rbFlatOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA FB FLAT", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_In, this.in10, this.post5, this.fly, this.rbFlatOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 10, "PA POST WHEEL", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.in5, this.post10, this.dragInFromSlot, this.rbFlatOut_Fly));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 9, "PA POWER O", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.isoBlockIn, this.corner5, this.post5flat, this.fly, this.rbFlatOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 7, "PA SCISSORS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.corner5, this.post5flat, this.post10, this.rbFlatOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole1, 9, "PA STREAKS", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut_Out, this.post5, this.fly, this.fly, this.fbDiveIn_Out));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA WR IN", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn_HitchIn, this.slantInIn, this.in10, this.post10skinny, this.rbFlatOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "POST FLAGS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.post10, this.post5Corner, this.post5Corner, this.rbFlatOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "POWER O", PlayConcept.Outside_Run_With_Pulling_OL, this.runPos_offTackleRight, this.runBlockT, this.pullBlockIn, this.runBlockC, this.runBlockG, this.runBlockT, this.qbOffTackleIn, this.rbOffTackleIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.ThreeStep, HandoffType.None, 9, "QUICK SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.slantIn, this.slantIn, this.slantIn, this.isoBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SEAM 678", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.corner5, this.in5, this.post10, this.rbFlatOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SHORT FLYS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.fly, this.fly, this.fly, this.isoBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantInHitch, this.post5skinny, this.post5skinny, this.isoBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLANT AND GO", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.in5, this.slantInFly, this.slantInFly, this.rbFlatOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SLANTS", PlayConcept.Short_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.slantIn, this.post5flat, this.post5flat, this.isoBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 9, "SLUGGOS", PlayConcept.Deep_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn_Fly, this.fly, this.slantInFly, this.post5Corner, this.isoBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "X POST", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.in5, this.post10, this.in10, this.isoBlockIn));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB SLAM", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.isoBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole2, 6, "HB ISO", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoIn, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Run, PlayTypeSpecific.NormalRun, DropbackType.OneStep, HandoffType.SinglebackHole1, 6, "HB ISO WEAK", PlayConcept.Inside_Run, this.runPos_actualPosition, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoOut, this.rbIsoOut, this.runBlockTE, this.runBlockWR, this.runBlockWR, this.diveBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.PlayAction, DropbackType.OneStep, HandoffType.SinglebackHole2, 8, "PA DBL GO'S", PlayConcept.Play_Action, 6, this.runBlockT, this.runBlockG, this.runBlockC, this.runBlockG, this.runBlockT, this.qbIsoIn, this.rbIsoOut_Out, this.slantInIn, this.fly, this.upCornerFly, this.diveBlockOut));
    this.weakIPlays_TwinTE.AddPlay((PlayData) new PlayDataOff(this.weakI_TwinTE, PlayType.Pass, PlayTypeSpecific.NormalPass, DropbackType.FiveStep, HandoffType.None, 8, "SKINNY POSTS TE DRAG", PlayConcept.Mid_Pass, 0, this.passBlockT, this.passBlockG, this.passBlockC, this.passBlockG, this.passBlockT, this.qbPassPlay, this.rbFlatIn, this.dragIn, this.post5skinny, this.post5skinny, this.isoBlockOut));
  }

  private void Awake()
  {
    if ((UnityEngine.Object) Plays.self == (UnityEngine.Object) null)
    {
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
      Plays.self = this;
      this.InitializePlays();
    }
    else
      UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
  }

  public void InitializePlays()
  {
    this.SetRouteGraphicData_Offense();
    this.SetRouteGraphicData_Defense();
    this.SetRouteDataVariables();
    this.SetRouteData_Offense();
    this.SetRouteData_Defense();
    this.SetFormationPositions_Offense();
    this.SetFormationPositions_Defense();
    this.SetPlays_Goalline_Heavy();
    this.SetPlays_IForm_Normal();
    this.SetPlays_IForm_Tight();
    this.SetPlays_iForm_SlotFlex();
    this.SetPlays_IForm_TwinTE();
    this.SetPlays_IForm_Twins();
    this.SetPlays_IForm_YTrips();
    this.SetPlays_StrongI_Close();
    this.SetPlays_StrongI_Normal();
    this.SetPlays_StrongI_Tight();
    this.SetPlays_StrongI_TwinTE();
    this.SetPlays_StrongI_Twins();
    this.SetPlays_StrongI_TwinsFlex();
    this.SetPlays_WeakI_CloseTwins();
    this.SetPlays_WeakI_Normal();
    this.SetPlays_WeakI_Twins();
    this.SetPlays_WeakI_TwinsFlex();
    this.SetPlays_WeakI_TwinTE();
    this.SetPlays_SplitBack_Normal();
    this.SetPlays_SingleBack_Big();
    this.SetPlays_SingleBack_BigTwins();
    this.SetPlays_SingleBack_Bunch();
    this.SetPlays_SingleBack_Slot();
    this.SetPlays_SingleBack_Spread();
    this.SetPlays_SingleBack_TreyOpen();
    this.SetPlays_SingleBack_Trio();
    this.SetPlays_SingleBack_Trio4WR();
    this.SetPlays_Empty_TreyOpen();
    this.SetPlays_Empty_FlexTrips();
    this.SetPlays_Pistol_Ace();
    this.SetPlays_Pistol_Bunch();
    this.SetPlays_Pistol_Slot();
    this.SetPlays_Pistol_SpreadFlex();
    this.SetPlays_Pistol_TreyOpen();
    this.SetPlays_Pistol_Trio();
    this.SetPlays_Pistol_YTrips();
    this.SetPlays_Shotgun_Normal();
    this.SetPlays_Shotgun_NormalYFlex();
    this.SetPlays_Shotgun_QuadsTrio();
    this.SetPlays_Shotgun_SlotOffset();
    this.SetPlays_Shotgun_SplitOffset();
    this.SetPlays_Shotgun_Spread();
    this.SetPlays_Shotgun_Tight();
    this.SetPlays_Shotgun_TightWideBack();
    this.SetPlays_Shotgun_Trey();
    this.SetPlays_Shotgun_Trips();
    this.SetPlays_Shotgun_Spread5WR();
    this.SetPlays_Shotgun_Bunch5WR();
    this.SetPlays_Shotgun_NormalDimeDropping();
    this.SetPlays_HailMary_Normal();
    this.SetPlays_OffSpecialTeams();
    this.SetPlays_SixTwo();
    this.SetPlays_FiveThree();
    this.SetPlays_FourFour();
    this.SetPlays_FourThree();
    this.SetPlays_ThreeFour();
    this.SetPlays_Nickel();
    this.SetPlays_TwoFourNickel();
    this.SetPlays_Dime();
    this.SetPlays_DefSpecialTeams();
    this.SetPlaybooks();
  }

  private void SetFormationPositions_Offense()
  {
    float num1 = 0.0f;
    float num2 = num1;
    float num3 = -0.364f;
    float num4 = -0.728f;
    float num5 = -0.736f;
    float num6 = -2.76f;
    float num7 = -1.38f;
    float num8 = 0.0f;
    float num9 = 1.38f;
    float num10 = 2.76f;
    float num11 = 0.0f;
    float num12 = 0.0f;
    float num13 = 2.8f;
    float num14 = 2.8f;
    float num15 = 2.73f;
    float num16 = 4.14f;
    float num17 = 5.52f;
    float num18 = 9f;
    float num19 = 15.5f;
    float num20 = 7.5f;
    float num21 = 11.5f;
    float num22 = 15.5f;
    float num23 = 9f;
    float num24 = 11f;
    float num25 = 13f;
    float num26 = 6.5f;
    float num27 = 10.5f;
    float num28 = 15.5f;
    float num29 = -1.665f;
    float num30 = -3.64f;
    float num31 = -6.76f;
    float num32 = -6.67f;
    float num33 = -7.26f;
    float num34 = -5.05f;
    float num35 = -1.82f;
    float num36 = 0.91f;
    float num37 = -1.99f;
    float num38 = -6.37f;
    float num39 = -8.48f;
    float num40 = 4.55f;
    float num41 = -14.5f;
    this.goalline_Heavy = new FormationPositions("1 WR | 2 RB | 2 TE", BaseFormation.Goalline, SubFormation.Heavy, 2);
    this.goalline_Heavy.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      -num16,
      num28,
      num16,
      num12
    });
    this.goalline_Heavy.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num4,
      num5,
      num34
    });
    this.goalline_Heavy.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      0,
      0
    });
    this.goalline_Heavy.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.TE,
      Position.FB
    });
    this.iForm_Normal = new FormationPositions("2 WR | 2 RB | 1 TE", BaseFormation.I_Form, SubFormation.Normal, 2);
    this.iForm_Normal.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num19,
      num19,
      num12
    });
    this.iForm_Normal.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num1,
      num35,
      num34
    });
    this.iForm_Normal.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      2,
      0
    });
    this.iForm_Normal.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.FB
    });
    this.iForm_Tight = new FormationPositions("1 WR | 2 RB | 2 TE", BaseFormation.I_Form, SubFormation.Tight, 2);
    this.iForm_Tight.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num16,
      num18,
      num12
    });
    this.iForm_Tight.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num5,
      num35,
      num34
    });
    this.iForm_Tight.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      0,
      2,
      8
    });
    this.iForm_Tight.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.TE,
      Position.WR,
      Position.FB
    });
    this.iForm_SlotFlex = new FormationPositions("3 WR | 2 RB", BaseFormation.I_Form, SubFormation.Slot_Flex, 3);
    this.iForm_SlotFlex.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num18,
      -num19,
      num19,
      num12
    });
    this.iForm_SlotFlex.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num1,
      num1,
      num35,
      num34
    });
    this.iForm_SlotFlex.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      2,
      2,
      2,
      0
    });
    this.iForm_SlotFlex.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.WR,
      Position.WR,
      Position.WR,
      Position.FB
    });
    this.iForm_TwinTE = new FormationPositions("1 WR | 2 RB | 2 TE", BaseFormation.I_Form, SubFormation.Twin_TE, 2);
    this.iForm_TwinTE.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num19,
      num17,
      num12
    });
    this.iForm_TwinTE.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num1,
      num5,
      num34
    });
    this.iForm_TwinTE.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      0,
      0
    });
    this.iForm_TwinTE.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.TE,
      Position.FB
    });
    this.iForm_Twins = new FormationPositions("2 WR | 2 RB | 1 TE", BaseFormation.I_Form, SubFormation.Twins, 4);
    this.iForm_Twins.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num19,
      -num18,
      num12
    });
    this.iForm_Twins.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num1,
      num35,
      num34
    });
    this.iForm_Twins.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      2,
      0
    });
    this.iForm_Twins.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.FB
    });
    this.iForm_YTrips = new FormationPositions("2 WR | 2 RB | 1 TE", BaseFormation.I_Form, SubFormation.Y_Trips, 5);
    this.iForm_YTrips.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      num20,
      num21,
      num12
    });
    this.iForm_YTrips.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num35,
      num1,
      num34
    });
    this.iForm_YTrips.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      2,
      0
    });
    this.iForm_YTrips.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.FB
    });
    this.strongI_Close = new FormationPositions("2 WR | 2 RB | 1 TE", BaseFormation.Strong_I, SubFormation.Close, 2);
    this.strongI_Close.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num20,
      num20,
      num15
    });
    this.strongI_Close.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num1,
      num35,
      num34
    });
    this.strongI_Close.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      2,
      0
    });
    this.strongI_Close.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.FB
    });
    this.strongI_Normal = new FormationPositions("2 WR | 2 RB | 1 TE", BaseFormation.Strong_I, SubFormation.Normal, 2);
    this.strongI_Normal.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num19,
      num19,
      num15
    });
    this.strongI_Normal.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num1,
      num35,
      num34
    });
    this.strongI_Normal.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      2,
      0
    });
    this.strongI_Normal.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.FB
    });
    this.strongI_Tight = new FormationPositions("1 WR | 2 RB | 2 TE", BaseFormation.Strong_I, SubFormation.Tight, 2);
    this.strongI_Tight.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num16,
      num19,
      num15
    });
    this.strongI_Tight.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num5,
      num35,
      num34
    });
    this.strongI_Tight.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      0,
      2,
      0
    });
    this.strongI_Tight.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.TE,
      Position.WR,
      Position.FB
    });
    this.strongI_TwinTE = new FormationPositions("2 WR | 2 RB | 1 TE", BaseFormation.Strong_I, SubFormation.Twin_TE, 2);
    this.strongI_TwinTE.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num19,
      num17,
      num15
    });
    this.strongI_TwinTE.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num1,
      num5,
      num34
    });
    this.strongI_TwinTE.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      0,
      0
    });
    this.strongI_TwinTE.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.TE,
      Position.FB
    });
    this.strongI_Twins = new FormationPositions("2 WR | 2 RB | 1 TE", BaseFormation.Strong_I, SubFormation.Twins, 4);
    this.strongI_Twins.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num19,
      -num18,
      num15
    });
    this.strongI_Twins.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num1,
      num35,
      num34
    });
    this.strongI_Twins.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      2,
      0
    });
    this.strongI_Twins.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.FB
    });
    this.strongI_TwinsFlex = new FormationPositions("2 WR | 2 RB | 1 TE", BaseFormation.Strong_I, SubFormation.Twins_Flex, 4);
    this.strongI_TwinsFlex.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num19,
      -num18,
      num15
    });
    this.strongI_TwinsFlex.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num35,
      num1,
      num34
    });
    this.strongI_TwinsFlex.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      2,
      0
    });
    this.strongI_TwinsFlex.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.FB
    });
    this.weakI_CloseTwins = new FormationPositions("2 WR | 2 RB | 1 TE", BaseFormation.Weak_I, SubFormation.Close_Twins, 4);
    this.weakI_CloseTwins.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num18,
      -num26,
      -num15
    });
    this.weakI_CloseTwins.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num35,
      num1,
      num34
    });
    this.weakI_CloseTwins.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      2,
      0
    });
    this.weakI_CloseTwins.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.FB
    });
    this.weakI_TwinsFlex = new FormationPositions("2 WR | 2 RB | 1 TE", BaseFormation.Weak_I, SubFormation.Twins_Flex, 4);
    this.weakI_TwinsFlex.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num19,
      -num18,
      -num15
    });
    this.weakI_TwinsFlex.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num35,
      num1,
      num34
    });
    this.weakI_TwinsFlex.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      2,
      0
    });
    this.weakI_TwinsFlex.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.FB
    });
    this.weakI_Normal = new FormationPositions("2 WR | 2 RB | 1 TE", BaseFormation.Weak_I, SubFormation.Normal, 2);
    this.weakI_Normal.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num19,
      num19,
      -num15
    });
    this.weakI_Normal.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num1,
      num35,
      num34
    });
    this.weakI_Normal.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      2,
      0
    });
    this.weakI_Normal.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.FB
    });
    this.weakI_TwinTE = new FormationPositions("2 WR | 2 RB | 1 TE", BaseFormation.Weak_I, SubFormation.Twin_TE, 2);
    this.weakI_TwinTE.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num19,
      num17,
      -num15
    });
    this.weakI_TwinTE.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num1,
      num5,
      num34
    });
    this.weakI_TwinTE.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      0,
      0
    });
    this.weakI_TwinTE.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.FB
    });
    this.weakI_Twins = new FormationPositions("2 WR | 2 RB | 1 TE", BaseFormation.Weak_I, SubFormation.Twins, 4);
    this.weakI_Twins.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num19,
      -num18,
      -num15
    });
    this.weakI_Twins.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num1,
      num35,
      num34
    });
    this.weakI_Twins.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      2,
      0
    });
    this.weakI_Twins.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.FB
    });
    this.splitBack_Normal = new FormationPositions("2 WR | 2 RB | 1 TE", BaseFormation.Split_Back, SubFormation.Normal, 2);
    this.splitBack_Normal.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num14,
      num16,
      -num19,
      num19,
      -num14
    });
    this.splitBack_Normal.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num33,
      num5,
      num1,
      num35,
      num33
    });
    this.splitBack_Normal.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      2,
      8
    });
    this.splitBack_Normal.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.FB
    });
    this.singleBack_Big = new FormationPositions("2 WR | 1 RB |\t2 TE", BaseFormation.Single_Back, SubFormation.Big, 2);
    this.singleBack_Big.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num19,
      num19,
      -num16
    });
    this.singleBack_Big.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num35,
      num35,
      num5
    });
    this.singleBack_Big.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      2,
      0
    });
    this.singleBack_Big.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.TE
    });
    this.singleBack_BigTwins = new FormationPositions("2 WR | 1 RB | 2 TE", BaseFormation.Single_Back, SubFormation.Big_Twins, 4);
    this.singleBack_BigTwins.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num19,
      -num18,
      -num16
    });
    this.singleBack_BigTwins.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num35,
      num35,
      num5
    });
    this.singleBack_BigTwins.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      2,
      0
    });
    this.singleBack_BigTwins.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.TE
    });
    this.singleBack_Bunch = new FormationPositions("3 WR | 1 RB | 1 TE", BaseFormation.Single_Back, SubFormation.Bunch, 3);
    this.singleBack_Bunch.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num23,
      -num19,
      num25,
      num24
    });
    this.singleBack_Bunch.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num35,
      num1,
      num35,
      num1
    });
    this.singleBack_Bunch.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      2,
      2,
      2,
      2
    });
    this.singleBack_Bunch.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.singleBack_Slot = new FormationPositions("3 WR | 1 RB | 1 TE", BaseFormation.Single_Back, SubFormation.Slot, 0);
    this.singleBack_Slot.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num19,
      num19,
      -num18
    });
    this.singleBack_Slot.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num1,
      num35,
      num35
    });
    this.singleBack_Slot.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      2,
      2
    });
    this.singleBack_Slot.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.singleBack_Spread = new FormationPositions("4 WR | 1 RB", BaseFormation.Single_Back, SubFormation.Spread, 1);
    this.singleBack_Spread.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num18,
      -num19,
      num19,
      -num18
    });
    this.singleBack_Spread.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num1,
      num1,
      num35,
      num35
    });
    this.singleBack_Spread.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      2,
      2,
      2,
      2
    });
    this.singleBack_Spread.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.SLT,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.singleBack_TreyOpen = new FormationPositions("3 WR | 1 RB | 1 TE", BaseFormation.Single_Back, SubFormation.Trey_Open, 3);
    this.singleBack_TreyOpen.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num26,
      -num19,
      num28,
      num27
    });
    this.singleBack_TreyOpen.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num1,
      num35,
      num35
    });
    this.singleBack_TreyOpen.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      2,
      2,
      2,
      2
    });
    this.singleBack_TreyOpen.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.singleBack_Trio = new FormationPositions("3 WR | 1 RB | 1 TE", BaseFormation.Single_Back, SubFormation.Trio, 4);
    this.singleBack_Trio.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num22,
      -num20,
      -num21
    });
    this.singleBack_Trio.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num1,
      num35,
      num35
    });
    this.singleBack_Trio.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      2,
      2,
      2
    });
    this.singleBack_Trio.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.singleBack_Trio4WR = new FormationPositions("4 WR | 1 RB", BaseFormation.Single_Back, SubFormation.Trio_4WR, 3);
    this.singleBack_Trio4WR.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num20,
      -num19,
      num22,
      num21
    });
    this.singleBack_Trio4WR.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num35,
      num1,
      num35,
      num1
    });
    this.singleBack_Trio4WR.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      2,
      2,
      2,
      2
    });
    this.singleBack_Trio4WR.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.SLT,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.empty_TreyOpen = new FormationPositions("4 WR | 1 TE", BaseFormation.Empty, SubFormation.Trey_Open, 1);
    this.empty_TreyOpen.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num26,
      num27,
      -num19,
      num28,
      -num18
    });
    this.empty_TreyOpen.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num1,
      num35,
      num1,
      num35,
      num35
    });
    this.empty_TreyOpen.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      2,
      2,
      2,
      2,
      2
    });
    this.empty_TreyOpen.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.TE,
      Position.SLT,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.empty_FlexTrips = new FormationPositions("5 WR", BaseFormation.Empty, SubFormation.Flex_Trips, 1);
    this.empty_FlexTrips.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num20,
      num21,
      -num19,
      num22,
      -num18
    });
    this.empty_FlexTrips.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num35,
      num35,
      num1,
      num1,
      num35
    });
    this.empty_FlexTrips.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      2,
      2,
      2,
      2,
      2
    });
    this.empty_FlexTrips.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.SLT,
      Position.SLT,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.pistol_Ace = new FormationPositions("2 WR | 1 RB | 2 TE", BaseFormation.Pistol, SubFormation.Ace, 2);
    this.pistol_Ace.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num19,
      num19,
      -num16
    });
    this.pistol_Ace.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num30,
      num32,
      num5,
      num35,
      num35,
      num5
    });
    this.pistol_Ace.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      7,
      8,
      0,
      2,
      2,
      0
    });
    this.pistol_Ace.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.TE
    });
    this.pistol_Bunch = new FormationPositions("3 WR | 1 RB | 1 TE", BaseFormation.Pistol, SubFormation.Bunch, 3);
    this.pistol_Bunch.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num23,
      -num19,
      num25,
      num24
    });
    this.pistol_Bunch.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num30,
      num32,
      num35,
      num1,
      num35,
      num1
    });
    this.pistol_Bunch.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      7,
      8,
      2,
      2,
      2,
      2
    });
    this.pistol_Bunch.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.WR
    });
    this.pistol_Slot = new FormationPositions("3 WR | 1 RB | 1 TE", BaseFormation.Pistol, SubFormation.Slot, 0);
    this.pistol_Slot.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num19,
      num19,
      -num18
    });
    this.pistol_Slot.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num30,
      num32,
      num5,
      num1,
      num35,
      num35
    });
    this.pistol_Slot.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      7,
      8,
      0,
      2,
      2,
      2
    });
    this.pistol_Slot.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.pistol_SpreadFlex = new FormationPositions("4 WR | 1 RB", BaseFormation.Pistol, SubFormation.Spread_Flex, 1);
    this.pistol_SpreadFlex.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num18,
      -num19,
      num19,
      -num18
    });
    this.pistol_SpreadFlex.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num30,
      num32,
      num1,
      num1,
      num35,
      num35
    });
    this.pistol_SpreadFlex.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      7,
      8,
      2,
      2,
      2,
      2
    });
    this.pistol_SpreadFlex.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.SLT,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.pistol_TreyOpen = new FormationPositions("3 WR | 1 RB | 1 TE", BaseFormation.Pistol, SubFormation.Trey_Open, 3);
    this.pistol_TreyOpen.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num26,
      -num19,
      num28,
      num27
    });
    this.pistol_TreyOpen.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num30,
      num32,
      num5,
      num1,
      num35,
      num35
    });
    this.pistol_TreyOpen.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      7,
      8,
      2,
      2,
      2,
      2
    });
    this.pistol_TreyOpen.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.pistol_Trio = new FormationPositions("3 WR | 1 RB | 1 TE", BaseFormation.Pistol, SubFormation.Trio, 4);
    this.pistol_Trio.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      -num20,
      -num22,
      -num21
    });
    this.pistol_Trio.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num30,
      num32,
      num5,
      num35,
      num1,
      num35
    });
    this.pistol_Trio.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      7,
      8,
      0,
      2,
      2,
      2
    });
    this.pistol_Trio.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.pistol_YTrips = new FormationPositions("3 WR | 1 RB | 1 TE", BaseFormation.Pistol, SubFormation.Y_Trips, 3);
    this.pistol_YTrips.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num20,
      -num19,
      num22,
      num21
    });
    this.pistol_YTrips.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num30,
      num32,
      num5,
      num1,
      num35,
      num35
    });
    this.pistol_YTrips.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      7,
      8,
      2,
      2,
      2,
      2
    });
    this.pistol_YTrips.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.shotgun_Normal = new FormationPositions("3 WR | 1 RB | 1 TE", BaseFormation.Shotgun, SubFormation.Normal, 0);
    this.shotgun_Normal.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      0.0f,
      num13,
      num16,
      -num19,
      num19,
      -num18
    });
    this.shotgun_Normal.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num31,
      num33,
      num5,
      num1,
      num35,
      num35
    });
    this.shotgun_Normal.SetStances(new int[11]
    {
      14,
      0,
      0,
      0,
      14,
      7,
      8,
      0,
      2,
      2,
      2
    });
    this.shotgun_Normal.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.shotgun_NormalYFlex = new FormationPositions("3 WR | 1 RB | 1 TE", BaseFormation.Shotgun, SubFormation.Normal_Y_Flex, 1);
    this.shotgun_NormalYFlex.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      0.0f,
      num13,
      num18,
      -num19,
      num19,
      -num18
    });
    this.shotgun_NormalYFlex.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num31,
      num33,
      num1,
      num1,
      num35,
      num35
    });
    this.shotgun_NormalYFlex.SetStances(new int[11]
    {
      14,
      0,
      0,
      0,
      14,
      7,
      8,
      2,
      2,
      2,
      2
    });
    this.shotgun_NormalYFlex.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.shotgun_QuadsTrio = new FormationPositions("4 WR | 1 TE", BaseFormation.Shotgun, SubFormation.Quads_Trio, 3);
    this.shotgun_QuadsTrio.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      0.0f,
      num16,
      num20,
      -num19,
      num22,
      num21
    });
    this.shotgun_QuadsTrio.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num31,
      num35,
      num35,
      num1,
      num35,
      num1
    });
    this.shotgun_QuadsTrio.SetStances(new int[11]
    {
      14,
      0,
      0,
      0,
      14,
      7,
      0,
      2,
      2,
      2,
      2
    });
    this.shotgun_QuadsTrio.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.TE,
      Position.SLT,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.shotgun_SlotOffset = new FormationPositions("3 WR | 1 RB | 1 TE", BaseFormation.Shotgun, SubFormation.Slot_Offset, 1);
    this.shotgun_SlotOffset.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      0.0f,
      -num13,
      num16,
      -num19,
      num19,
      num18
    });
    this.shotgun_SlotOffset.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num31,
      num33,
      num5,
      num1,
      num35,
      num35
    });
    this.shotgun_SlotOffset.SetStances(new int[11]
    {
      14,
      0,
      0,
      0,
      14,
      7,
      8,
      0,
      2,
      2,
      2
    });
    this.shotgun_SlotOffset.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.shotgun_SplitOffset = new FormationPositions("3 WR | 2 RB", BaseFormation.Shotgun, SubFormation.Split_Offset, 3);
    this.shotgun_SplitOffset.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      0.0f,
      -num13,
      num13,
      -num19,
      num19,
      num18
    });
    this.shotgun_SplitOffset.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num31,
      num33,
      num33,
      num1,
      num35,
      num1
    });
    this.shotgun_SplitOffset.SetStances(new int[11]
    {
      14,
      0,
      0,
      0,
      14,
      7,
      8,
      8,
      2,
      2,
      2
    });
    this.shotgun_SplitOffset.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.FB,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.shotgun_Spread = new FormationPositions("4 WR | 1 RB", BaseFormation.Shotgun, SubFormation.Spread, 1);
    this.shotgun_Spread.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      0.0f,
      num13,
      num18,
      -num19,
      num19,
      -num18
    });
    this.shotgun_Spread.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num31,
      num33,
      num35,
      num1,
      num1,
      num35
    });
    this.shotgun_Spread.SetStances(new int[11]
    {
      14,
      0,
      0,
      0,
      14,
      7,
      8,
      2,
      2,
      2,
      2
    });
    this.shotgun_Spread.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.SLT,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.shotgun_Tight = new FormationPositions("3 WR | 1 RB | 1 TE", BaseFormation.Shotgun, SubFormation.Tight, 1);
    this.shotgun_Tight.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      0.0f,
      num13,
      num26,
      -num27,
      num27,
      -num26
    });
    this.shotgun_Tight.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num31,
      num33,
      num1,
      num35,
      num35,
      num1
    });
    this.shotgun_Tight.SetStances(new int[11]
    {
      14,
      0,
      0,
      0,
      14,
      7,
      8,
      2,
      2,
      2,
      2
    });
    this.shotgun_Tight.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.shotgun_Tight.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.shotgun_TightWideBack = new FormationPositions("3 WR | 1 RB | 1 TE", BaseFormation.Shotgun, SubFormation.TightWideBack, 1);
    this.shotgun_TightWideBack.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      0.0f,
      -num27,
      num26,
      num13,
      num27,
      -num26
    });
    this.shotgun_TightWideBack.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num31,
      num35,
      num1,
      num33,
      num35,
      num1
    });
    this.shotgun_TightWideBack.SetStances(new int[11]
    {
      14,
      0,
      0,
      0,
      14,
      7,
      2,
      2,
      8,
      2,
      2
    });
    this.shotgun_TightWideBack.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.shotgun_Trey = new FormationPositions("3 WR | 1 RB | 1 TE", BaseFormation.Shotgun, SubFormation.Trey, 4);
    this.shotgun_Trey.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      0.0f,
      num13,
      num16,
      -num19,
      num19,
      num21
    });
    this.shotgun_Trey.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num31,
      num33,
      num5,
      num1,
      num35,
      num35
    });
    this.shotgun_Trey.SetStances(new int[11]
    {
      14,
      0,
      0,
      0,
      14,
      7,
      8,
      0,
      2,
      2,
      2
    });
    this.shotgun_Trey.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.shotgun_Trips = new FormationPositions("4 WR | 1 RB", BaseFormation.Shotgun, SubFormation.Trips, 3);
    this.shotgun_Trips.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      0.0f,
      num20,
      num21,
      -num19,
      num22,
      num13
    });
    this.shotgun_Trips.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num31,
      num35,
      num1,
      num1,
      num35,
      num33
    });
    this.shotgun_Trips.SetStances(new int[11]
    {
      14,
      0,
      0,
      0,
      14,
      7,
      2,
      2,
      2,
      2,
      8
    });
    this.shotgun_Trips.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.SLT,
      Position.WR,
      Position.WR,
      Position.SLT,
      Position.RB
    });
    this.shotgun_Spread5WR = new FormationPositions("5 WR", BaseFormation.Shotgun, SubFormation.Spread_5WR, 1);
    this.shotgun_Spread5WR.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      0.0f,
      num20,
      num21,
      -num19,
      num19,
      -num18
    });
    this.shotgun_Spread5WR.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num31,
      num35,
      num1,
      num1,
      num35,
      num35
    });
    this.shotgun_Spread5WR.SetStances(new int[11]
    {
      14,
      0,
      0,
      0,
      14,
      7,
      2,
      2,
      2,
      2,
      2
    });
    this.shotgun_Spread5WR.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.SLT,
      Position.WR,
      Position.WR,
      Position.SLT,
      Position.SLT
    });
    this.shotgun_Bunch5WR = new FormationPositions("5 WR", BaseFormation.Shotgun, SubFormation.Bunch_5WR, 1);
    this.shotgun_Bunch5WR.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      0.0f,
      num23,
      num24,
      -num28,
      num25,
      -num20
    });
    this.shotgun_Bunch5WR.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num31,
      num35,
      num1,
      num1,
      num35,
      num35
    });
    this.shotgun_Bunch5WR.SetStances(new int[11]
    {
      14,
      0,
      0,
      0,
      14,
      7,
      2,
      2,
      2,
      2,
      2
    });
    this.shotgun_Bunch5WR.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.SLT,
      Position.WR,
      Position.WR,
      Position.SLT,
      Position.SLT
    });
    this.shotgun_NormalDimeDropping = new FormationPositions("3 WR | 1 RB | 1 TE", BaseFormation.Shotgun, SubFormation.NormalDimeDropping, 0);
    this.shotgun_NormalDimeDropping.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num13,
      num16,
      -num19,
      num19,
      -num18
    });
    this.shotgun_NormalDimeDropping.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num31,
      num33,
      num5,
      num1,
      num35,
      num35
    });
    this.shotgun_NormalDimeDropping.SetStances(new int[11]
    {
      14,
      0,
      0,
      0,
      14,
      7,
      8,
      0,
      2,
      2,
      2
    });
    this.shotgun_NormalDimeDropping.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.hailMary_Normal = new FormationPositions("3 WR | 1 RB | 1 TE", BaseFormation.Hail_Mary, SubFormation.Normal, 4);
    this.hailMary_Normal.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num13,
      num16,
      -num19,
      -num21,
      -num20
    });
    this.hailMary_Normal.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num31,
      num33,
      num5,
      num1,
      num35,
      num35
    });
    this.hailMary_Normal.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      7,
      8,
      0,
      2,
      2,
      2
    });
    this.hailMary_Normal.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.qbKneel_Normal = new FormationPositions("2 WR | 1 RB |\t2 TE", BaseFormation.QB_Kneel, SubFormation.Normal, 2);
    this.qbKneel_Normal.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num11,
      num12,
      num16,
      num7,
      num9,
      -num16
    });
    this.qbKneel_Normal.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num29,
      num32,
      num5,
      num34,
      num34,
      num5
    });
    this.qbKneel_Normal.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      1,
      8,
      0,
      8,
      8,
      0
    });
    this.qbKneel_Normal.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.QB,
      Position.RB,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.TE
    });
    this.fieldGoalForm = new FormationPositions("SPECIAL TEAMS", BaseFormation.Special_Teams, SubFormation.Field_Goal, 3);
    this.fieldGoalForm.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num36,
      num37,
      num16,
      -num16,
      -num17,
      num17
    });
    this.fieldGoalForm.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num38,
      num39,
      num5,
      num5,
      num35,
      num35
    });
    this.fieldGoalForm.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      6,
      4,
      0,
      0,
      0,
      3
    });
    this.fieldGoalForm.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.P,
      Position.K,
      Position.TE,
      Position.WR,
      Position.WR,
      Position.SLT
    });
    this.puntForm = new FormationPositions("SPECIAL TEAMS", BaseFormation.Special_Teams, SubFormation.Punt, 3);
    this.puntForm.SetXLocations(new float[11]
    {
      num6,
      num7,
      num8,
      num9,
      num10,
      num13,
      1f,
      num40,
      -num19,
      num19,
      -num40
    });
    this.puntForm.SetZLocations(new float[11]
    {
      num4,
      num3,
      num2,
      num3,
      num4,
      num31,
      num41,
      num35,
      num1,
      num1,
      num35
    });
    this.puntForm.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      3,
      13,
      3,
      2,
      2,
      3
    });
    this.puntForm.SetPositions(new Position[11]
    {
      Position.LT,
      Position.LG,
      Position.C,
      Position.RG,
      Position.RT,
      Position.FB,
      Position.P,
      Position.TE,
      Position.GUN,
      Position.GUN,
      Position.TE
    });
    this.kickoffForm = new FormationPositions("SPECIAL TEAMS", BaseFormation.Special_Teams, SubFormation.Kickoff, 0);
    this.kickoffForm.SetXLocations(new float[11]
    {
      -22f,
      -18f,
      -14f,
      -10f,
      -6f,
      4f,
      -1.83f,
      8f,
      12f,
      17f,
      21f
    });
    this.kickoffForm.SetZLocations(new float[11]
    {
      -0.8f,
      -1.03f,
      -0.72f,
      -0.63f,
      -0.85f,
      -0.74f,
      -6.85f,
      -0.68f,
      -0.7f,
      -0.75f,
      -0.91f
    });
    this.kickoffForm.SetStances(new int[11]
    {
      2,
      5,
      5,
      2,
      2,
      5,
      4,
      2,
      5,
      2,
      5
    });
    this.kickoffForm.SetPositions(new Position[11]
    {
      Position.GUN,
      Position.GUN,
      Position.GUN,
      Position.GUN,
      Position.GUN,
      Position.GUN,
      Position.K,
      Position.GUN,
      Position.GUN,
      Position.GUN,
      Position.GUN
    });
    this.onsideKickForm = new FormationPositions("SPECIAL TEAMS", BaseFormation.Special_Teams, SubFormation.Onside_Kick, 0);
    this.onsideKickForm.SetXLocations(new float[11]
    {
      -22f,
      -18f,
      -14f,
      -10f,
      -6f,
      6f,
      -2.73f,
      10f,
      14f,
      18f,
      22f
    });
    this.onsideKickForm.SetZLocations(new float[11]
    {
      -0.8f,
      -0.93f,
      -0.91f,
      -0.65f,
      -0.75f,
      -0.82f,
      -4.1f,
      -0.95f,
      -0.78f,
      -0.74f,
      -0.77f
    });
    this.onsideKickForm.SetStances(new int[11]
    {
      3,
      3,
      3,
      3,
      3,
      3,
      4,
      3,
      3,
      3,
      3
    });
    this.onsideKickForm.SetPositions(new Position[11]
    {
      Position.GUN,
      Position.WR,
      Position.GUN,
      Position.WR,
      Position.GUN,
      Position.WR,
      Position.K,
      Position.WR,
      Position.WR,
      Position.WR,
      Position.WR
    });
  }

  private void SetFormationPositions_Defense()
  {
    float num1 = -4.55f;
    float num2 = -2.73f;
    float num3 = -0.91f;
    float num4 = 1.82f;
    float num5 = 3.64f;
    float num6 = 5.46f;
    float num7 = 5.46f;
    float num8 = 2.73f;
    float num9 = 4.55f;
    float num10 = 1.82f;
    float num11 = 2.73f;
    float num12 = 0.0f;
    float num13 = 5.46f;
    float num14 = 1.82f;
    float num15 = 3.64f;
    float num16 = 4.55f;
    float num17 = 5.46f;
    float num18 = 6.37f;
    float num19 = 7.28f;
    float num20 = 9.1f;
    float num21 = 10.92f;
    float num22 = 2.73f;
    float num23 = 4.55f;
    float num24 = 0.0f;
    float num25 = 15f;
    float num26 = 0.0f;
    float num27 = 5.46f;
    float num28 = -9.5f;
    float num29 = 9.5f;
    float num30 = 0.53f;
    float num31 = 4.65f;
    float num32 = 2.82f;
    float num33 = 4.64f;
    float num34 = 4.64f;
    float num35 = 3.64f;
    float num36 = 2.73f;
    float num37 = 6.1f;
    float num38 = 9.144f;
    float num39 = 7.28f;
    float num40 = 6.37f;
    float num41 = 13.76f;
    this.sixTwoForm = new FormationPositionsDef("6 DL | 2 LB | 3 DB", BaseFormation.Six_Two, SubFormation.None);
    this.sixTwoForm.SetXLocations(new float[11]
    {
      num1,
      num2,
      num3,
      num4,
      num5,
      num6,
      -num22,
      num22,
      -num25,
      num26,
      num25
    });
    this.sixTwoForm.SetZLocations(new float[11]
    {
      num30,
      0.58f,
      0.55f,
      0.62f,
      0.54f,
      0.62f,
      num31,
      num31,
      num36,
      num38,
      num36
    });
    this.sixTwoForm.SetXLocationsShift1(new float[11]
    {
      num1,
      num2,
      num3,
      num4,
      num5,
      num6,
      -0.91f,
      num20,
      -num25,
      -num20,
      num25
    });
    this.sixTwoForm.SetZLocationsShift1(new float[11]
    {
      num30,
      0.58f,
      0.55f,
      0.62f,
      0.54f,
      0.62f,
      num31,
      num31,
      num36,
      num40,
      num36
    });
    this.sixTwoForm.SetXLocationsShift2(new float[11]
    {
      num1,
      num2,
      num3,
      num4,
      num5,
      num6,
      -num22,
      num22,
      -num25,
      num26,
      num25
    });
    this.sixTwoForm.SetZLocationsShift2(new float[11]
    {
      num30,
      0.58f,
      0.55f,
      0.62f,
      0.54f,
      0.62f,
      num31,
      num31,
      num36,
      num40,
      num36
    });
    this.sixTwoForm.SetXLocationsShift3(new float[11]
    {
      num1,
      num2,
      num3,
      num4,
      num5,
      num6,
      -num22,
      num16,
      -num25,
      num20,
      num25
    });
    this.sixTwoForm.SetZLocationsShift3(new float[11]
    {
      num30,
      0.58f,
      0.55f,
      0.62f,
      0.54f,
      0.62f,
      num31,
      num31,
      num36,
      num40,
      num36
    });
    this.sixTwoForm.SetXLocationsShift4(new float[11]
    {
      num1,
      num2,
      num3,
      num4,
      num5,
      num6,
      -num22,
      num22,
      -num25,
      num26,
      -num19
    });
    this.sixTwoForm.SetZLocationsShift4(new float[11]
    {
      num30,
      0.58f,
      0.55f,
      0.62f,
      0.54f,
      0.62f,
      num31,
      num31,
      num36,
      num40,
      num36
    });
    this.sixTwoForm.SetXLocationsShift5(new float[11]
    {
      num1,
      num2,
      num3,
      num4,
      num5,
      num6,
      -num22,
      num22,
      num19,
      num26,
      num25
    });
    this.sixTwoForm.SetZLocationsShift5(new float[11]
    {
      num30,
      0.58f,
      0.55f,
      0.62f,
      0.54f,
      0.62f,
      num31,
      num31,
      num31,
      num40,
      num36
    });
    this.sixTwoForm.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      0,
      3,
      3,
      5,
      5,
      5
    });
    this.sixTwoForm.SetPositions(new Position[11]
    {
      Position.DE,
      Position.DT,
      Position.DT,
      Position.DT,
      Position.DT,
      Position.DE,
      Position.OLB,
      Position.OLB,
      Position.CB,
      Position.SS,
      Position.CB
    });
    this.fiveThreeForm = new FormationPositionsDef("5 DL | 3 LB | 3 DB", BaseFormation.Five_Three, SubFormation.None);
    this.fiveThreeForm.SetXLocations(new float[11]
    {
      -num7,
      -num8,
      num12,
      num8,
      num7,
      -num23,
      num24,
      num23,
      -num25,
      num26,
      num25
    });
    this.fiveThreeForm.SetZLocations(new float[11]
    {
      0.58f,
      0.55f,
      0.62f,
      0.54f,
      0.6f,
      num31,
      num31,
      num31,
      num36,
      num38,
      num36
    });
    this.fiveThreeForm.SetXLocationsShift1(new float[11]
    {
      -num7,
      -num8,
      num12,
      num8,
      num7,
      -num20,
      num24,
      num20,
      -num25,
      1.82f,
      num25
    });
    this.fiveThreeForm.SetZLocationsShift1(new float[11]
    {
      0.58f,
      0.55f,
      0.62f,
      0.54f,
      0.6f,
      num31,
      num31,
      num31,
      num36,
      num40,
      num36
    });
    this.fiveThreeForm.SetXLocationsShift2(new float[11]
    {
      -num7,
      -num8,
      num12,
      num8,
      num7,
      -num23,
      num24,
      num23,
      -num25,
      num26,
      num25
    });
    this.fiveThreeForm.SetZLocationsShift2(new float[11]
    {
      0.58f,
      0.55f,
      0.62f,
      0.54f,
      0.6f,
      num31,
      num31,
      num31,
      num36,
      num38,
      num36
    });
    this.fiveThreeForm.SetXLocationsShift3(new float[11]
    {
      -num7,
      -num8,
      num12,
      num8,
      num7,
      -num17,
      num24,
      num20,
      -num25,
      num17,
      num25
    });
    this.fiveThreeForm.SetZLocationsShift3(new float[11]
    {
      0.58f,
      0.55f,
      0.62f,
      0.54f,
      0.6f,
      num31,
      num31,
      num31,
      num36,
      num40,
      num36
    });
    this.fiveThreeForm.SetXLocationsShift4(new float[11]
    {
      -num7,
      -num8,
      num12,
      num8,
      num7,
      -num23,
      num24,
      num23,
      -num25,
      num26,
      -num19
    });
    this.fiveThreeForm.SetZLocationsShift4(new float[11]
    {
      0.58f,
      0.55f,
      0.62f,
      0.54f,
      0.6f,
      num31,
      num31,
      num31,
      num36,
      num38,
      num35
    });
    this.fiveThreeForm.SetXLocationsShift5(new float[11]
    {
      -num7,
      -num8,
      num12,
      num8,
      num7,
      -num23,
      num24,
      num23,
      num19,
      num26,
      num25
    });
    this.fiveThreeForm.SetZLocationsShift5(new float[11]
    {
      0.58f,
      0.55f,
      0.62f,
      0.54f,
      0.6f,
      num31,
      num31,
      num31,
      num31,
      num38,
      num36
    });
    this.fiveThreeForm.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      3,
      3,
      3,
      5,
      5,
      5
    });
    this.fiveThreeForm.SetPositions(new Position[11]
    {
      Position.DE,
      Position.DT,
      Position.NT,
      Position.DT,
      Position.DE,
      Position.OLB,
      Position.MLB,
      Position.OLB,
      Position.CB,
      Position.SS,
      Position.CB
    });
    this.fourFourForm = new FormationPositionsDef("4 DL | 4 LB | 3 DB", BaseFormation.Four_Four, SubFormation.None);
    this.fourFourForm.SetXLocations(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num13,
      -num14,
      num14,
      num13,
      -num25,
      num26,
      num25
    });
    this.fourFourForm.SetZLocations(new float[11]
    {
      0.53f,
      0.6f,
      0.56f,
      0.63f,
      num31,
      num31,
      num31,
      num31,
      num36,
      num38,
      num36
    });
    this.fourFourForm.SetXLocationsShift1(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num20,
      -num14,
      num14,
      num20,
      -num25,
      num26,
      num25
    });
    this.fourFourForm.SetZLocationsShift1(new float[11]
    {
      0.53f,
      0.6f,
      0.56f,
      0.63f,
      num31,
      num31,
      num31,
      num31,
      num36,
      num38,
      num36
    });
    this.fourFourForm.SetXLocationsShift2(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num13,
      -num14,
      num14,
      num13,
      -num25,
      num26,
      num25
    });
    this.fourFourForm.SetZLocationsShift2(new float[11]
    {
      0.53f,
      0.6f,
      0.56f,
      0.63f,
      num31,
      num31,
      num31,
      num31,
      num36,
      num38,
      num36
    });
    this.fourFourForm.SetXLocationsShift3(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num15,
      num24,
      num15,
      num20,
      -num25,
      num26,
      num25
    });
    this.fourFourForm.SetZLocationsShift3(new float[11]
    {
      0.53f,
      0.6f,
      0.56f,
      0.63f,
      num31,
      num31,
      num31,
      num31,
      num36,
      num38,
      num36
    });
    this.fourFourForm.SetXLocationsShift4(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num13,
      -num14,
      num14,
      num13,
      -num25,
      num26,
      -num19
    });
    this.fourFourForm.SetZLocationsShift4(new float[11]
    {
      0.53f,
      0.6f,
      0.56f,
      0.63f,
      num31,
      num31,
      num31,
      num31,
      num36,
      num38,
      num35
    });
    this.fourFourForm.SetXLocationsShift5(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num13,
      -num14,
      num14,
      num13,
      num19,
      num26,
      num25
    });
    this.fourFourForm.SetZLocationsShift5(new float[11]
    {
      0.53f,
      0.6f,
      0.56f,
      0.63f,
      num31,
      num31,
      num31,
      num31,
      num31,
      num39,
      num36
    });
    this.fourFourForm.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      3,
      3,
      3,
      3,
      5,
      5,
      5
    });
    this.fourFourForm.SetPositions(new Position[11]
    {
      Position.DE,
      Position.DT,
      Position.DT,
      Position.DE,
      Position.OLB,
      Position.ILB,
      Position.ILB,
      Position.OLB,
      Position.CB,
      Position.SS,
      Position.CB
    });
    this.fourThreeForm = new FormationPositionsDef("4 DL | 3 LB | 4 DB", BaseFormation.Four_Three, SubFormation.None);
    this.fourThreeForm.SetXLocations(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num15,
      num24,
      num15,
      -num25,
      num27,
      -num27,
      num25
    });
    this.fourThreeForm.SetZLocations(new float[11]
    {
      0.53f,
      0.6f,
      0.56f,
      0.63f,
      4.45f,
      num31,
      4.55f,
      num35,
      num38,
      num41,
      num35
    });
    this.fourThreeForm.SetXLocationsShift1(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num15,
      num24,
      num19,
      -num25,
      num21,
      -num20,
      num25
    });
    this.fourThreeForm.SetZLocationsShift1(new float[11]
    {
      0.53f,
      0.6f,
      0.56f,
      0.63f,
      num34,
      num34,
      num34,
      num35,
      num39,
      num41,
      num35
    });
    this.fourThreeForm.SetXLocationsShift2(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num15,
      num24,
      num15,
      -num25,
      num27,
      -num27,
      num25
    });
    this.fourThreeForm.SetZLocationsShift2(new float[11]
    {
      0.53f,
      0.6f,
      0.56f,
      0.63f,
      num34,
      num34,
      num34,
      num35,
      num39,
      num41,
      num35
    });
    this.fourThreeForm.SetXLocationsShift3(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num15,
      num24,
      num19,
      -num25,
      num21,
      num26,
      num25
    });
    this.fourThreeForm.SetZLocationsShift3(new float[11]
    {
      0.53f,
      0.6f,
      0.56f,
      0.63f,
      num34,
      num34,
      num34,
      num35,
      num39,
      num41,
      num35
    });
    this.fourThreeForm.SetXLocationsShift4(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num15,
      num24,
      num15,
      -num25,
      num27,
      -num27,
      -num19
    });
    this.fourThreeForm.SetZLocationsShift4(new float[11]
    {
      0.53f,
      0.6f,
      0.56f,
      0.63f,
      num34,
      num34,
      num34,
      num35,
      num39,
      num41,
      num35
    });
    this.fourThreeForm.SetXLocationsShift5(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num15,
      num24,
      num15,
      num19,
      num27,
      -num27,
      num25
    });
    this.fourThreeForm.SetZLocationsShift5(new float[11]
    {
      0.53f,
      0.6f,
      0.56f,
      0.63f,
      num34,
      num34,
      num34,
      num35,
      num39,
      num41,
      num35
    });
    this.fourThreeForm.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      3,
      3,
      3,
      5,
      5,
      5,
      5
    });
    this.fourThreeForm.SetPositions(new Position[11]
    {
      Position.DE,
      Position.DT,
      Position.DT,
      Position.DE,
      Position.OLB,
      Position.MLB,
      Position.OLB,
      Position.CB,
      Position.SS,
      Position.FS,
      Position.CB
    });
    this.threeFourForm = new FormationPositionsDef("3 DL | 4 LB | 4 DB", BaseFormation.Three_Four, SubFormation.None);
    this.threeFourForm.SetXLocations(new float[11]
    {
      -num11,
      num12,
      num11,
      -num13,
      -num14,
      num14,
      num13,
      -num25,
      num27,
      -num27,
      num25
    });
    this.threeFourForm.SetZLocations(new float[11]
    {
      0.6f,
      num30,
      0.63f,
      num32,
      4.78f,
      num33,
      num32,
      num35,
      num38,
      num41,
      num35
    });
    this.threeFourForm.SetXLocationsShift1(new float[11]
    {
      -num11,
      num12,
      num11,
      -num13,
      -num14,
      num14,
      num19,
      -num25,
      num21,
      -num20,
      num25
    });
    this.threeFourForm.SetZLocationsShift1(new float[11]
    {
      0.6f,
      num30,
      0.63f,
      num32,
      num33,
      num33,
      num32,
      num35,
      num39,
      num41,
      num35
    });
    this.threeFourForm.SetXLocationsShift2(new float[11]
    {
      -num11,
      num12,
      num11,
      -num13,
      -num14,
      num14,
      num13,
      -num25,
      num27,
      -num27,
      num25
    });
    this.threeFourForm.SetZLocationsShift2(new float[11]
    {
      0.6f,
      num30,
      0.63f,
      num32,
      num33,
      num33,
      num32,
      num35,
      num39,
      num41,
      num35
    });
    this.threeFourForm.SetXLocationsShift3(new float[11]
    {
      -num11,
      num12,
      num11,
      -num13,
      -num14,
      num14,
      num19,
      -num25,
      num21,
      num26,
      num25
    });
    this.threeFourForm.SetZLocationsShift3(new float[11]
    {
      0.6f,
      num30,
      0.63f,
      num32,
      num33,
      num33,
      num32,
      num35,
      num39,
      num41,
      num35
    });
    this.threeFourForm.SetXLocationsShift4(new float[11]
    {
      -num11,
      num12,
      num11,
      -num13,
      -num14,
      num14,
      num13,
      -num25,
      num27,
      -num27,
      -num19
    });
    this.threeFourForm.SetZLocationsShift4(new float[11]
    {
      0.6f,
      num30,
      0.63f,
      num32,
      num33,
      num33,
      num32,
      num35,
      num39,
      num41,
      num35
    });
    this.threeFourForm.SetXLocationsShift5(new float[11]
    {
      -num11,
      num12,
      num11,
      -num13,
      -num14,
      num14,
      num13,
      num19,
      num27,
      -num27,
      num25
    });
    this.threeFourForm.SetZLocationsShift5(new float[11]
    {
      0.6f,
      num30,
      0.63f,
      num32,
      num33,
      num33,
      num32,
      num35,
      num39,
      num41,
      num35
    });
    this.threeFourForm.SetStances(new int[11]
    {
      0,
      0,
      0,
      3,
      3,
      3,
      3,
      5,
      5,
      5,
      5
    });
    this.threeFourForm.SetPositions(new Position[11]
    {
      Position.DT,
      Position.NT,
      Position.DT,
      Position.OLB,
      Position.ILB,
      Position.ILB,
      Position.OLB,
      Position.CB,
      Position.SS,
      Position.FS,
      Position.CB
    });
    this.nickelForm = new FormationPositionsDef("4 DL | 2 LB | 5 DB", BaseFormation.Nickel, SubFormation.Normal);
    this.nickelForm.SetXLocations(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num22,
      num22,
      num28,
      -num25,
      num27,
      -num27,
      num25
    });
    this.nickelForm.SetZLocations(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      4.6f,
      4.76f,
      num35,
      num35,
      num38,
      num41,
      num35
    });
    this.nickelForm.SetXLocationsShift1(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num14,
      num17,
      num28,
      -num25,
      num20,
      num26,
      num25
    });
    this.nickelForm.SetZLocationsShift1(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num34,
      num34,
      num35,
      num35,
      num39,
      num41,
      num35
    });
    this.nickelForm.SetXLocationsShift2(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num22,
      num22,
      num28,
      -num25,
      num27,
      -num27,
      num25
    });
    this.nickelForm.SetZLocationsShift2(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num34,
      num34,
      num35,
      num35,
      num39,
      num41,
      num35
    });
    this.nickelForm.SetXLocationsShift3(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      num24,
      num19,
      -num19,
      -num25,
      num21,
      num26,
      num25
    });
    this.nickelForm.SetZLocationsShift3(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num34,
      num34,
      num35,
      num35,
      num39,
      num41,
      num35
    });
    this.nickelForm.SetXLocationsShift4(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num22,
      num22,
      num28,
      -num25,
      num27,
      -num27,
      -num18
    });
    this.nickelForm.SetZLocationsShift4(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num34,
      num34,
      num35,
      num35,
      num39,
      num41,
      num35
    });
    this.nickelForm.SetXLocationsShift5(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num22,
      num22,
      num28,
      num18,
      num27,
      -num27,
      num25
    });
    this.nickelForm.SetZLocationsShift5(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num34,
      num34,
      num35,
      num35,
      num39,
      num41,
      num35
    });
    this.nickelForm.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      3,
      3,
      5,
      5,
      5,
      5,
      5
    });
    this.nickelForm.SetPositions(new Position[11]
    {
      Position.DE,
      Position.DT,
      Position.DT,
      Position.DE,
      Position.OLB,
      Position.OLB,
      Position.CB,
      Position.CB,
      Position.SS,
      Position.FS,
      Position.CB
    });
    this.twoFourNickelForm = new FormationPositionsDef("2 DL | 4 LB | 5 DB", BaseFormation.Nickel, SubFormation.Two_Four);
    this.twoFourNickelForm.SetXLocations(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num22,
      num22,
      num28,
      -num25,
      num27,
      -num27,
      num25
    });
    this.twoFourNickelForm.SetZLocations(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      4.6f,
      4.76f,
      num35,
      num35,
      num38,
      num41,
      num35
    });
    this.twoFourNickelForm.SetXLocationsShift1(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num14,
      num17,
      num28,
      -num25,
      num20,
      num26,
      num25
    });
    this.twoFourNickelForm.SetZLocationsShift1(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num34,
      num34,
      num35,
      num35,
      num39,
      num41,
      num35
    });
    this.twoFourNickelForm.SetXLocationsShift2(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num22,
      num22,
      num28,
      -num25,
      num27,
      -num27,
      num25
    });
    this.twoFourNickelForm.SetZLocationsShift2(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num34,
      num34,
      num35,
      num35,
      num39,
      num41,
      num35
    });
    this.twoFourNickelForm.SetXLocationsShift3(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      num24,
      num19,
      -num19,
      -num25,
      num21,
      num26,
      num25
    });
    this.twoFourNickelForm.SetZLocationsShift3(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num34,
      num34,
      num35,
      num35,
      num39,
      num41,
      num35
    });
    this.twoFourNickelForm.SetXLocationsShift4(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num22,
      num22,
      num28,
      -num25,
      num27,
      -num27,
      -num18
    });
    this.twoFourNickelForm.SetZLocationsShift4(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num34,
      num34,
      num35,
      num35,
      num39,
      num41,
      num35
    });
    this.twoFourNickelForm.SetXLocationsShift5(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      -num22,
      num22,
      num28,
      num18,
      num27,
      -num27,
      num25
    });
    this.twoFourNickelForm.SetZLocationsShift5(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num34,
      num34,
      num35,
      num35,
      num39,
      num41,
      num35
    });
    this.twoFourNickelForm.SetStances(new int[11]
    {
      2,
      0,
      0,
      2,
      3,
      3,
      5,
      5,
      5,
      5,
      5
    });
    this.twoFourNickelForm.SetPositions(new Position[11]
    {
      Position.OLB,
      Position.DE,
      Position.DT,
      Position.OLB,
      Position.ILB,
      Position.ILB,
      Position.CB,
      Position.CB,
      Position.SS,
      Position.FS,
      Position.CB
    });
    this.dimeForm = new FormationPositionsDef("4 DL | 1 LB | 6 DB", BaseFormation.Dime, SubFormation.None);
    this.dimeForm.SetXLocations(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      num24,
      num29,
      num28,
      -num25,
      num27,
      -num27,
      num25
    });
    this.dimeForm.SetZLocations(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num34,
      num35,
      num35,
      num37,
      num38,
      num41,
      num37
    });
    this.dimeForm.SetXLocationsShift1(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      num24,
      num29,
      num28,
      -num25,
      num21,
      num26,
      num25
    });
    this.dimeForm.SetZLocationsShift1(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num34,
      num35,
      num35,
      num37,
      num39,
      num41,
      num37
    });
    this.dimeForm.SetXLocationsShift2(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      num24,
      num29,
      num28,
      -num25,
      num27,
      -num27,
      num25
    });
    this.dimeForm.SetZLocationsShift2(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num34,
      num35,
      num35,
      num37,
      num39,
      num41,
      num37
    });
    this.dimeForm.SetXLocationsShift3(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      num24,
      num29,
      -num19,
      -num25,
      num21,
      num26,
      num25
    });
    this.dimeForm.SetZLocationsShift3(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num34,
      num35,
      num35,
      num37,
      num39,
      num41,
      num37
    });
    this.dimeForm.SetXLocationsShift4(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      num24,
      num29,
      num28,
      -num25,
      num27,
      -num27,
      -num18
    });
    this.dimeForm.SetZLocationsShift4(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num34,
      num35,
      num35,
      num37,
      num39,
      num41,
      num37
    });
    this.dimeForm.SetXLocationsShift5(new float[11]
    {
      -num9,
      -num10,
      num10,
      num9,
      num24,
      num29,
      num28,
      num18,
      num27,
      -num27,
      num25
    });
    this.dimeForm.SetZLocationsShift5(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num34,
      num35,
      num35,
      num37,
      num39,
      num41,
      num37
    });
    this.dimeForm.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      3,
      5,
      5,
      5,
      5,
      5,
      5
    });
    this.dimeForm.SetPositions(new Position[11]
    {
      Position.DE,
      Position.DT,
      Position.DT,
      Position.DE,
      Position.MLB,
      Position.CB,
      Position.CB,
      Position.CB,
      Position.SS,
      Position.FS,
      Position.CB
    });
    this.fgBlockForm = new FormationPositionsDef("SPECIAL TEAMS", BaseFormation.Special_Teams, SubFormation.Field_Goal_Block);
    this.fgBlockForm.SetXLocations(new float[11]
    {
      num1,
      num3,
      -3.6f,
      num12,
      num4,
      num5,
      -num22,
      num22,
      -8.2f,
      num6,
      8.2f
    });
    this.fgBlockForm.SetZLocations(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num30,
      0.6f,
      num34,
      num34,
      num30,
      0.58f,
      0.64f
    });
    this.fgBlockForm.SetXLocationsShift1(this.fgBlockForm.GetXLocations());
    this.fgBlockForm.SetZLocationsShift1(this.fgBlockForm.GetZLocations());
    this.fgBlockForm.SetXLocationsShift2(this.fgBlockForm.GetXLocations());
    this.fgBlockForm.SetZLocationsShift2(this.fgBlockForm.GetZLocations());
    this.fgBlockForm.SetXLocationsShift3(this.fgBlockForm.GetXLocations());
    this.fgBlockForm.SetZLocationsShift3(this.fgBlockForm.GetZLocations());
    this.fgBlockForm.SetXLocationsShift4(this.fgBlockForm.GetXLocations());
    this.fgBlockForm.SetZLocationsShift4(this.fgBlockForm.GetZLocations());
    this.fgBlockForm.SetXLocationsShift5(this.fgBlockForm.GetXLocations());
    this.fgBlockForm.SetZLocationsShift5(this.fgBlockForm.GetZLocations());
    FormationPositionsDef fgBlockForm = this.fgBlockForm;
    int[] s = new int[11];
    s[6] = 3;
    s[7] = 3;
    fgBlockForm.SetStances(s);
    this.fgBlockForm.SetPositions(new Position[11]
    {
      Position.DE,
      Position.DT,
      Position.DT,
      Position.NT,
      Position.DT,
      Position.DT,
      Position.OLB,
      Position.OLB,
      Position.CB,
      Position.DE,
      Position.CB
    });
    this.puntRetForm = new FormationPositionsDef("SPECIAL TEAMS", BaseFormation.Special_Teams, SubFormation.Punt_Return);
    this.puntRetForm.SetXLocations(new float[11]
    {
      -6.37f,
      -3.64f,
      -0.91f,
      1.82f,
      4.55f,
      7.28f,
      -num22,
      num22,
      -num25,
      num26,
      num25
    });
    this.puntRetForm.SetZLocations(new float[11]
    {
      0.58f,
      0.6f,
      0.53f,
      0.62f,
      num30,
      0.6f,
      num31,
      num31,
      num31,
      50f,
      num31
    });
    this.puntRetForm.SetXLocationsShift1(this.puntRetForm.GetXLocations());
    this.puntRetForm.SetZLocationsShift1(this.puntRetForm.GetZLocations());
    this.puntRetForm.SetXLocationsShift2(this.puntRetForm.GetXLocations());
    this.puntRetForm.SetZLocationsShift2(this.puntRetForm.GetZLocations());
    this.puntRetForm.SetXLocationsShift3(this.puntRetForm.GetXLocations());
    this.puntRetForm.SetZLocationsShift3(this.puntRetForm.GetZLocations());
    this.puntRetForm.SetXLocationsShift4(this.puntRetForm.GetXLocations());
    this.puntRetForm.SetZLocationsShift4(this.puntRetForm.GetZLocations());
    this.puntRetForm.SetXLocationsShift5(this.puntRetForm.GetXLocations());
    this.puntRetForm.SetZLocationsShift5(this.puntRetForm.GetZLocations());
    this.puntRetForm.SetStances(new int[11]
    {
      0,
      0,
      0,
      0,
      0,
      0,
      3,
      3,
      3,
      3,
      3
    });
    this.puntRetForm.SetPositions(new Position[11]
    {
      Position.DE,
      Position.DT,
      Position.DT,
      Position.DT,
      Position.DT,
      Position.DE,
      Position.OLB,
      Position.OLB,
      Position.CB,
      Position.PR,
      Position.CB
    });
    this.kickRetForm = new FormationPositionsDef("SPECIAL TEAMS", BaseFormation.Special_Teams, SubFormation.Kick_Return);
    this.kickRetForm.SetXLocations(new float[11]
    {
      -18.38f,
      -8.92f,
      -0.1f,
      0.64f,
      8.42f,
      18.18f,
      -6.1f,
      -14.73f,
      6.56f,
      0.0f,
      14.3f
    });
    this.kickRetForm.SetZLocations(new float[11]
    {
      16.48f,
      15.29f,
      15.38f,
      24.67f,
      14.77f,
      16.92f,
      40f,
      28.9f,
      40.3f,
      65.5f,
      28.8f
    });
    this.kickRetForm.SetStances(new int[11]
    {
      3,
      11,
      3,
      3,
      12,
      12,
      3,
      3,
      11,
      3,
      12
    });
    this.kickRetForm.SetPositions(new Position[11]
    {
      Position.BLK,
      Position.BLK,
      Position.BLK,
      Position.BLK,
      Position.BLK,
      Position.BLK,
      Position.BLK,
      Position.BLK,
      Position.BLK,
      Position.KR,
      Position.KR
    });
    this.kickRetOnsideForm = new FormationPositionsDef("SPECIAL TEAMS", BaseFormation.Special_Teams, SubFormation.Onside_Kick_Return);
    this.kickRetOnsideForm.SetXLocations(new float[11]
    {
      -20f,
      -13.3f,
      -6.7f,
      0.0f,
      6.7f,
      13.3f,
      20f,
      -15f,
      15f,
      -10f,
      10f
    });
    this.kickRetOnsideForm.SetZLocations(new float[11]
    {
      15.47f,
      15.47f,
      15.47f,
      15.47f,
      15.47f,
      15.47f,
      15.47f,
      18.2f,
      18.2f,
      25.5f,
      25.5f
    });
    this.kickRetOnsideForm.SetStances(new int[11]
    {
      0,
      3,
      3,
      3,
      3,
      3,
      3,
      3,
      3,
      3,
      3
    });
    this.kickRetOnsideForm.SetPositions(new Position[11]
    {
      Position.KR,
      Position.KR,
      Position.KR,
      Position.KR,
      Position.KR,
      Position.KR,
      Position.KR,
      Position.KR,
      Position.KR,
      Position.KR,
      Position.KR
    });
  }

  public string GetCurOffPlaybookP1() => this.CurOffPlaybookP1;

  public string GetCurDefPlaybookP1() => this.CurDefPlaybookP1;

  public string GetCurOffPlaybookP2() => this.CurOffPlaybookP2;

  public string GetCurDefPlaybookP2() => this.CurDefPlaybookP2;

  public SpriteAtlas OffSpriteAtlasP1 { get; private set; }

  public SpriteAtlas DefSpriteAtlasP1 { get; private set; }

  public void SetPlaybooks()
  {
    this.playbook_AirRaid = new List<FormationData>()
    {
      this.shotgunPlays_Normal,
      this.shotgunPlays_NormalYFlex,
      this.shotgunPlays_SlotOffset,
      this.shotgunPlays_Spread,
      this.shotgunPlays_Tight,
      this.shotgunPlays_Spread5WR,
      this.singleBackPlays_Big,
      this.singleBackPlays_Bunch,
      this.singleBackPlays_Slot,
      this.singleBackPlays_Spread,
      this.singleBackPlays_TreyOpen,
      this.goallinePlays_Heavy,
      this.hailMaryPlays_Normal,
      this.clockManagementPlays,
      this.specialOffPlays
    };
    this.playbook_Tampa = new List<FormationData>()
    {
      this.shotgunPlays_Normal,
      this.shotgunPlays_NormalYFlex,
      this.shotgunPlays_SlotOffset,
      this.shotgunPlays_SplitOffset,
      this.shotgunPlays_Spread,
      this.shotgunPlays_Tight,
      this.shotgunPlays_Spread5WR,
      this.singleBackPlays_Big,
      this.singleBackPlays_Bunch,
      this.singleBackPlays_Slot,
      this.singleBackPlays_Spread,
      this.singleBackPlays_TreyOpen,
      this.goallinePlays_Heavy,
      this.hailMaryPlays_Normal,
      this.clockManagementPlays,
      this.specialOffPlays
    };
    this.playbook_PowerRun = new List<FormationData>()
    {
      this.goallinePlays_Heavy,
      this.iFormPlays_Normal,
      this.iFormPlays_Tight,
      this.iFormPlays_SlotFlex,
      this.iFormPlays_TwinTE,
      this.iFormPlays_Twins,
      this.iFormPlays_YTrips,
      this.strongIPlays_Close,
      this.strongIPlays_Normal,
      this.strongIPlays_Tight,
      this.strongIPlays_TwinTE,
      this.strongIPlays_Twins,
      this.strongIPlays_TwinsFlex,
      this.weakIPlays_CloseTwins,
      this.weakIPlays_Normal,
      this.weakIPlays_Twins,
      this.weakIPlays_TwinsFlex,
      this.weakIPlays_TwinTE,
      this.singleBackPlays_Big,
      this.singleBackPlays_Slot,
      this.singleBackPlays_Spread,
      this.shotgunPlays_Normal,
      this.hailMaryPlays_Normal,
      this.clockManagementPlays,
      this.specialOffPlays
    };
    this.playbook_WestCoast = new List<FormationData>()
    {
      this.shotgunPlays_Normal,
      this.shotgunPlays_NormalYFlex,
      this.shotgunPlays_SlotOffset,
      this.shotgunPlays_SplitOffset,
      this.shotgunPlays_Spread,
      this.shotgunPlays_Tight,
      this.shotgunPlays_Spread5WR,
      this.singleBackPlays_Big,
      this.singleBackPlays_Bunch,
      this.singleBackPlays_Slot,
      this.singleBackPlays_Spread,
      this.singleBackPlays_TreyOpen,
      this.iFormPlays_Normal,
      this.goallinePlays_Heavy,
      this.hailMaryPlays_Normal,
      this.clockManagementPlays,
      this.specialOffPlays
    };
    this.playbook_Pistol = new List<FormationData>()
    {
      this.pistolPlays_Ace,
      this.pistolPlays_YTrips,
      this.pistolPlays_SpreadFlex,
      this.shotgunPlays_Normal,
      this.shotgunPlays_NormalYFlex,
      this.shotgunPlays_SlotOffset,
      this.shotgunPlays_SplitOffset,
      this.shotgunPlays_Spread,
      this.shotgunPlays_Tight,
      this.shotgunPlays_Spread5WR,
      this.singleBackPlays_Big,
      this.singleBackPlays_Slot,
      this.singleBackPlays_Spread,
      this.goallinePlays_Heavy,
      this.hailMaryPlays_Normal,
      this.clockManagementPlays,
      this.specialOffPlays
    };
    this.playbook_PistolLite = new List<FormationData>()
    {
      this.shotgunPlays_Normal,
      this.shotgunPlays_NormalYFlex,
      this.shotgunPlays_SlotOffset,
      this.shotgunPlays_SplitOffset,
      this.shotgunPlays_Spread,
      this.shotgunPlays_Tight,
      this.shotgunPlays_Spread5WR,
      this.singleBackPlays_Big,
      this.singleBackPlays_Slot,
      this.singleBackPlays_Spread,
      this.pistolPlays_Ace,
      this.goallinePlays_Heavy,
      this.hailMaryPlays_Normal,
      this.clockManagementPlays,
      this.specialOffPlays
    };
    this.playbook_SanFrancisco = new List<FormationData>()
    {
      this.shotgunPlays_Normal,
      this.shotgunPlays_NormalYFlex,
      this.shotgunPlays_SlotOffset,
      this.shotgunPlays_SplitOffset,
      this.shotgunPlays_Spread,
      this.shotgunPlays_Tight,
      this.shotgunPlays_TightWideBack,
      this.singleBackPlays_Big,
      this.singleBackPlays_Slot,
      this.singleBackPlays_Spread,
      this.iFormPlays_Normal,
      this.pistolPlays_Ace,
      this.goallinePlays_Heavy,
      this.hailMaryPlays_Normal,
      this.clockManagementPlays,
      this.specialOffPlays
    };
    this.playbook_Singleback = new List<FormationData>()
    {
      this.singleBackPlays_Big,
      this.singleBackPlays_Bunch,
      this.singleBackPlays_Slot,
      this.singleBackPlays_Spread,
      this.singleBackPlays_TreyOpen,
      this.shotgunPlays_Normal,
      this.shotgunPlays_NormalYFlex,
      this.shotgunPlays_SlotOffset,
      this.goallinePlays_Heavy,
      this.hailMaryPlays_Normal,
      this.clockManagementPlays,
      this.specialOffPlays
    };
    this.playbook_AllOffPlays = new List<FormationData>()
    {
      this.shotgunPlays_Normal,
      this.shotgunPlays_NormalYFlex,
      this.shotgunPlays_SlotOffset,
      this.shotgunPlays_SplitOffset,
      this.shotgunPlays_Spread,
      this.shotgunPlays_Tight,
      this.shotgunPlays_Spread5WR,
      this.pistolPlays_Ace,
      this.pistolPlays_SpreadFlex,
      this.pistolPlays_YTrips,
      this.singleBackPlays_Big,
      this.singleBackPlays_Bunch,
      this.singleBackPlays_Slot,
      this.singleBackPlays_Spread,
      this.singleBackPlays_TreyOpen,
      this.shotgunPlays_TightWideBack,
      this.emptyPlays_TreyOpen,
      this.emptyPlays_FlexTrips,
      this.iFormPlays_Normal,
      this.goallinePlays_Heavy,
      this.hailMaryPlays_Normal,
      this.clockManagementPlays,
      this.specialOffPlays
    };
    this.playbook_DimeDropping = new List<FormationData>()
    {
      this.shotgunPlays_NormalDimeDropping,
      this.goallinePlays_Heavy,
      this.hailMaryPlays_Normal,
      this.clockManagementPlays,
      this.specialOffPlays
    };
    this.playbook_DemoOffPlays = new List<FormationData>()
    {
      this.shotgunPlays_Normal,
      this.goallinePlays_Heavy,
      this.hailMaryPlays_Normal,
      this.clockManagementPlays,
      this.specialOffPlays
    };
    this.playbook_FourThree = new List<FormationData>()
    {
      this.fourThreePlays,
      this.nickelPlays,
      this.dimePlays,
      this.fiveThreePlays,
      this.specialDefPlays
    };
    this.playbook_ThreeFour = new List<FormationData>()
    {
      this.threeFourPlays,
      this.twoFourNickelPlays,
      this.dimePlays,
      this.fiveThreePlays,
      this.specialDefPlays
    };
    this.playbook_AllDefPlays = new List<FormationData>()
    {
      this.threeFourPlays,
      this.fourThreePlays,
      this.nickelPlays,
      this.dimePlays,
      this.twoFourNickelPlays,
      this.fiveThreePlays,
      this.specialDefPlays
    };
    this.SetOffensivePlaybookP1("ALL");
  }

  public void SetOffensivePlaybookP1(string _playbook)
  {
    this.CurOffPlaybookP1 = _playbook;
    switch (_playbook)
    {
      case "AIR RAID":
        this.offPlaybookP1 = this.playbook_AirRaid;
        break;
      case "POWER RUN":
        this.offPlaybookP1 = this.playbook_PowerRun;
        break;
      case "WEST COAST":
        this.offPlaybookP1 = this.playbook_WestCoast;
        break;
      case "PISTOL":
        this.offPlaybookP1 = this.playbook_Pistol;
        break;
      case "SINGLEBACK":
        this.offPlaybookP1 = this.playbook_Singleback;
        break;
      case "SAN FRANCISCO":
        this.offPlaybookP1 = this.playbook_SanFrancisco;
        break;
      case "TAMPA":
        this.offPlaybookP1 = this.playbook_Tampa;
        break;
      case "PISTOL LITE":
        this.offPlaybookP1 = this.playbook_PistolLite;
        break;
      case "ALL":
        this.offPlaybookP1 = this.playbook_AllOffPlays;
        break;
      case "DEMO":
        this.offPlaybookP1 = this.playbook_DemoOffPlays;
        break;
      case "DIME DROPPING":
        this.offPlaybookP1 = this.playbook_DimeDropping;
        break;
      default:
        bool flag = false;
        foreach (CustomPlaybook offensivePlaybook in this.customOffensivePlaybooks)
        {
          if (offensivePlaybook.Name.Equals(_playbook, StringComparison.OrdinalIgnoreCase))
          {
            this.offPlaybookP1 = offensivePlaybook.Playbook;
            flag = true;
          }
        }
        if (!flag)
        {
          Debug.Log((object) ("No Offensive Playbook Found For Player 1 With Name: " + _playbook + ". Setting default playbook."));
          this.offPlaybookP1 = this.playbook_AllOffPlays;
          break;
        }
        break;
    }
    this._offAtlasNameP1 = "ALL ATLAS";
    this.OffSpriteAtlasP1 = Resources.Load<SpriteAtlas>(this._offAtlasNameP1);
    this.SetPlayCategories_Offense(true);
  }

  public void SetDefensivePlaybookP1(string _playbook)
  {
    this.CurDefPlaybookP1 = _playbook;
    switch (_playbook)
    {
      case "THREE FOUR":
        this.defPlaybookP1 = this.playbook_ThreeFour;
        break;
      case "FOUR THREE":
        this.defPlaybookP1 = this.playbook_FourThree;
        break;
      case "ALL":
        this.defPlaybookP1 = this.playbook_AllDefPlays;
        break;
      default:
        bool flag = false;
        foreach (CustomPlaybook defensivePlaybook in this.customDefensivePlaybooks)
        {
          if (defensivePlaybook.Name.Equals(_playbook, StringComparison.OrdinalIgnoreCase))
          {
            this.defPlaybookP1 = defensivePlaybook.Playbook;
            flag = true;
          }
        }
        if (!flag)
        {
          Debug.Log((object) ("No Defensive Playbook Found For Player 1 With Name: " + _playbook + ". Setting default playbook."));
          this.defPlaybookP1 = this.playbook_AllDefPlays;
          break;
        }
        break;
    }
    this._defAtlasNameP1 = "ALL ATLAS";
    this.DefSpriteAtlasP1 = Resources.Load<SpriteAtlas>(this._defAtlasNameP1);
    this.SetPlayCategories_Defense(true);
  }

  public void SetOffensivePlaybookP2(string _playbook)
  {
    this.CurOffPlaybookP2 = _playbook;
    switch (_playbook)
    {
      case "AIR RAID":
        this.offPlaybookP2 = this.playbook_AirRaid;
        break;
      case "POWER RUN":
        this.offPlaybookP2 = this.playbook_PowerRun;
        break;
      case "WEST COAST":
        this.offPlaybookP2 = this.playbook_WestCoast;
        break;
      case "PISTOL":
        this.offPlaybookP2 = this.playbook_Pistol;
        break;
      case "SINGLEBACK":
        this.offPlaybookP2 = this.playbook_Singleback;
        break;
      case "SAN FRANCISCO":
        this.offPlaybookP2 = this.playbook_SanFrancisco;
        break;
      case "TAMPA":
        this.offPlaybookP2 = this.playbook_Tampa;
        break;
      case "PISTOL LITE":
        this.offPlaybookP2 = this.playbook_PistolLite;
        break;
      case "ALL":
        this.offPlaybookP2 = this.playbook_AllOffPlays;
        break;
      default:
        bool flag = false;
        foreach (CustomPlaybook offensivePlaybook in this.customOffensivePlaybooks)
        {
          if (offensivePlaybook.Name.Equals(_playbook, StringComparison.OrdinalIgnoreCase))
          {
            this.offPlaybookP2 = offensivePlaybook.Playbook;
            flag = true;
          }
        }
        if (!flag)
        {
          Debug.Log((object) ("No Offensive Playbook Found For Player 2 With Name: " + _playbook + ". Setting default playbook."));
          this.offPlaybookP2 = this.playbook_AllOffPlays;
          break;
        }
        break;
    }
    this.SetPlayCategories_Offense(false);
  }

  public void SetDefensivePlaybookP2(string _playbook)
  {
    this.CurDefPlaybookP2 = _playbook;
    switch (_playbook)
    {
      case "THREE FOUR":
        this.defPlaybookP2 = this.playbook_ThreeFour;
        break;
      case "FOUR THREE":
        this.defPlaybookP2 = this.playbook_FourThree;
        break;
      case "ALL":
        this.defPlaybookP2 = this.playbook_AllDefPlays;
        break;
      default:
        bool flag = false;
        foreach (CustomPlaybook defensivePlaybook in this.customDefensivePlaybooks)
        {
          if (defensivePlaybook.Name.Equals(_playbook, StringComparison.OrdinalIgnoreCase))
          {
            this.defPlaybookP2 = defensivePlaybook.Playbook;
            flag = true;
          }
        }
        if (!flag)
        {
          Debug.Log((object) ("No Defensive Playbook Found For Player 2 With Name: " + _playbook + ". Setting default playbook."));
          this.defPlaybookP2 = this.playbook_AllDefPlays;
          break;
        }
        break;
    }
    this.SetPlayCategories_Defense(false);
  }

  public int GetOffensivePlaybookIndex(string _playbook)
  {
    for (int index = 0; index < this.offensivePlaybookNames.Count; ++index)
    {
      if (_playbook.Equals(this.offensivePlaybookNames[index], StringComparison.OrdinalIgnoreCase))
        return index;
    }
    Debug.Log((object) ("Offensive playbook: " + _playbook + " not found in master list. Returning 0"));
    return 0;
  }

  public int GetDefensivePlaybookIndex(string _playbook)
  {
    for (int index = 0; index < this.defensivePlaybookNames.Count; ++index)
    {
      if (_playbook.Equals(this.defensivePlaybookNames[index], StringComparison.OrdinalIgnoreCase))
        return index;
    }
    Debug.Log((object) ("Defensive playbook: " + _playbook + " not found in master list. Returning 0"));
    return 0;
  }

  private void SetPlayCategories_Offense(bool isPlayerOne)
  {
    List<PlayDataOff> playDataOffList1 = new List<PlayDataOff>();
    List<PlayDataOff> playDataOffList2 = new List<PlayDataOff>();
    List<PlayDataOff> playDataOffList3 = new List<PlayDataOff>();
    List<PlayDataOff> playDataOffList4 = new List<PlayDataOff>();
    List<PlayDataOff> playDataOffList5 = new List<PlayDataOff>();
    List<PlayDataOff> playDataOffList6 = new List<PlayDataOff>();
    List<PlayDataOff> playDataOffList7 = new List<PlayDataOff>();
    List<PlayDataOff> playDataOffList8 = new List<PlayDataOff>();
    List<PlayDataOff> playDataOffList9 = new List<PlayDataOff>();
    List<PlayDataOff> playDataOffList10 = new List<PlayDataOff>();
    List<PlayDataOff> playDataOffList11 = new List<PlayDataOff>();
    List<FormationData> formationDataList = isPlayerOne ? this.offPlaybookP1 : this.offPlaybookP2;
    for (int index = 0; index < formationDataList.Count; ++index)
    {
      FormationPositions formationPositions = formationDataList[index].GetFormationPositions();
      int playsInFormation = formationDataList[index].GetNumberOfPlaysInFormation();
      int receiversInFormation = formationPositions.GetReceiversInFormation();
      int tesInFormation = formationPositions.GetTEsInFormation();
      int backsInFormation = formationPositions.GetBacksInFormation();
      for (int i = 0; i < playsInFormation; ++i)
      {
        PlayDataOff play = (PlayDataOff) formationDataList[index].GetPlay(i);
        PlayConcept playConcept = play.GetPlayConcept();
        switch (playConcept)
        {
          case PlayConcept.Inside_Run:
          case PlayConcept.Misdirection:
            playDataOffList1.Add(play);
            break;
          case PlayConcept.Outside_Run:
          case PlayConcept.Reverse:
          case PlayConcept.Inside_Run_With_Pulling_OL:
          case PlayConcept.Outside_Run_With_Pulling_OL:
          case PlayConcept.Misdirection_With_Pulling_OL:
            playDataOffList2.Add(play);
            break;
          case PlayConcept.Short_Pass:
            playDataOffList5.Add(play);
            break;
          case PlayConcept.Mid_Pass:
            playDataOffList6.Add(play);
            break;
          case PlayConcept.Deep_Pass:
            playDataOffList7.Add(play);
            break;
          case PlayConcept.Screen_Pass:
            playDataOffList8.Add(play);
            break;
          case PlayConcept.Play_Action:
            playDataOffList9.Add(play);
            break;
          case PlayConcept.GL_Run:
            playDataOffList10.Add(play);
            break;
          case PlayConcept.GL_Pass:
            playDataOffList11.Add(play);
            break;
          case PlayConcept.QB_Keeper:
            playDataOffList3.Add(play);
            break;
          case PlayConcept.Read_Option:
            playDataOffList4.Add(play);
            break;
          default:
            if (tesInFormation + backsInFormation < 4 && playConcept != PlayConcept.Special_Teams && playConcept != PlayConcept.Clock_Control)
            {
              Debug.Log((object) string.Format("Did not find a category for play: {0} - {1} => Concept = {2}, TE = {3}, WR = {4}, RB = {5}", (object) formationDataList[index].GetName(), (object) play.GetPlayName(), (object) playConcept, (object) tesInFormation, (object) receiversInFormation, (object) backsInFormation));
              break;
            }
            break;
        }
      }
    }
    if (playDataOffList1.Count == 0)
      Debug.Log((object) ("No Inside Run plays for this playbook. Player 1: " + isPlayerOne.ToString()));
    if (playDataOffList2.Count == 0)
      Debug.Log((object) ("No Outside Run plays for this playbook. Player 1: " + isPlayerOne.ToString()));
    if (playDataOffList5.Count == 0)
      Debug.Log((object) ("No Short Pass plays for this playbook. Player 1: " + isPlayerOne.ToString()));
    if (playDataOffList6.Count == 0)
      Debug.Log((object) ("No Mid Pass plays for this playbook. Player 1: " + isPlayerOne.ToString()));
    if (playDataOffList7.Count == 0)
      Debug.Log((object) ("No Deep Pass plays for this playbook. Player 1: " + isPlayerOne.ToString()));
    if (playDataOffList8.Count == 0)
      Debug.Log((object) ("No Screen Pass plays for this playbook. Player 1: " + isPlayerOne.ToString()));
    if (playDataOffList9.Count == 0)
      Debug.Log((object) ("No Play Action Pass plays for this playbook. Player 1: " + isPlayerOne.ToString()));
    if (playDataOffList11.Count == 0)
      Debug.Log((object) ("No Goal Line Pass plays for this playbook. Player 1: " + isPlayerOne.ToString()));
    if (playDataOffList10.Count == 0)
      Debug.Log((object) ("No Goal Line Run plays for this playbook. Player 1: " + isPlayerOne.ToString()));
    if (playDataOffList4.Count == 0)
      Debug.Log((object) ("No Read Option plays for this playbook. Player 1: " + isPlayerOne.ToString()));
    if (isPlayerOne)
    {
      this.insideRunPlays_P1 = playDataOffList1;
      this.outsideRunPlays_P1 = playDataOffList2;
      this.qbKeeperPlays_P1 = playDataOffList3;
      this.readOptionPlays_P1 = playDataOffList4;
      this.shortPassPlays_P1 = playDataOffList5;
      this.midPassPlays_P1 = playDataOffList6;
      this.deepPassPlays_P1 = playDataOffList7;
      this.screenPassPlays_P1 = playDataOffList8;
      this.playActionPassPlays_P1 = playDataOffList9;
      this.goalLinePassPlays_P1 = playDataOffList11;
      this.goalLineRunPlays_P1 = playDataOffList10;
    }
    else
    {
      this.insideRunPlays_P2 = playDataOffList1;
      this.outsideRunPlays_P2 = playDataOffList2;
      this.qbKeeperPlays_P2 = playDataOffList3;
      this.readOptionPlays_P2 = playDataOffList4;
      this.shortPassPlays_P2 = playDataOffList5;
      this.midPassPlays_P2 = playDataOffList6;
      this.deepPassPlays_P2 = playDataOffList7;
      this.screenPassPlays_P2 = playDataOffList8;
      this.playActionPassPlays_P2 = playDataOffList9;
      this.goalLinePassPlays_P2 = playDataOffList11;
      this.goalLineRunPlays_P2 = playDataOffList10;
    }
  }

  public List<PlayDataOff> GetPlayCategory_InsideRun(bool playerOne) => !playerOne ? this.insideRunPlays_P2 : this.insideRunPlays_P1;

  public List<PlayDataOff> GetPlayCategory_OutsideRun(bool playerOne) => !playerOne ? this.outsideRunPlays_P2 : this.outsideRunPlays_P1;

  public List<PlayDataOff> GetPlayCategory_QbKeeper(bool playerOne) => !playerOne ? this.qbKeeperPlays_P2 : this.qbKeeperPlays_P1;

  public List<PlayDataOff> GetPlayCategory_ReadOption(bool playerOne) => !playerOne ? this.readOptionPlays_P2 : this.readOptionPlays_P1;

  public List<PlayDataOff> GetPlayCategory_ShortPass(bool playerOne) => !playerOne ? this.shortPassPlays_P2 : this.shortPassPlays_P1;

  public List<PlayDataOff> GetPlayCategory_MidPass(bool playerOne) => !playerOne ? this.midPassPlays_P2 : this.midPassPlays_P1;

  public List<PlayDataOff> GetPlayCategory_DeepPass(bool playerOne) => !playerOne ? this.deepPassPlays_P2 : this.deepPassPlays_P1;

  public List<PlayDataOff> GetPlayCategory_ScreenPass(bool playerOne) => !playerOne ? this.screenPassPlays_P2 : this.screenPassPlays_P1;

  public List<PlayDataOff> GetPlayCategory_PlayAction(bool playerOne) => !playerOne ? this.playActionPassPlays_P2 : this.playActionPassPlays_P1;

  public List<PlayDataOff> GetPlayCategory_GoalLineRun(bool playerOne) => !playerOne ? this.goalLineRunPlays_P2 : this.goalLineRunPlays_P1;

  public List<PlayDataOff> GetPlayCategory_GoalLinePass(bool playerOne) => !playerOne ? this.goalLinePassPlays_P2 : this.goalLinePassPlays_P1;

  private void SetPlayCategories_Defense(bool isPlayerOne)
  {
    List<PlayDataDef> playDataDefList1 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList2 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList3 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList4 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList5 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList6 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList7 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList8 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList9 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList10 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList11 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList12 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList13 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList14 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList15 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList16 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList17 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList18 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList19 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList20 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList21 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList22 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList23 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList24 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList25 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList26 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList27 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList28 = new List<PlayDataDef>();
    List<PlayDataDef> playDataDefList29 = new List<PlayDataDef>();
    Dictionary<PlayConcept, List<PlayDataDef>> dictionary1 = new Dictionary<PlayConcept, List<PlayDataDef>>();
    Dictionary<BaseFormation, List<PlayDataDef>> dictionary2 = new Dictionary<BaseFormation, List<PlayDataDef>>();
    foreach (PlayConcept key in Enum.GetValues(typeof (PlayConcept)))
      dictionary1[key] = new List<PlayDataDef>();
    foreach (BaseFormation key in Enum.GetValues(typeof (BaseFormation)))
      dictionary2[key] = new List<PlayDataDef>();
    List<FormationData> formationDataList = isPlayerOne ? this.defPlaybookP1 : this.defPlaybookP2;
    int num = 25;
    for (int index = 0; index < formationDataList.Count; ++index)
    {
      FormationPositions formationPositions = formationDataList[index].GetFormationPositions();
      int playsInFormation = formationDataList[index].GetNumberOfPlaysInFormation();
      int linemenInFormation = formationPositions.GetDefensiveLinemenInFormation();
      int linebackersInFormation = formationPositions.GetLinebackersInFormation();
      int backsInFormation = formationPositions.GetDefensiveBacksInFormation();
      BaseFormation baseFormation = formationPositions.GetBaseFormation();
      for (int i1 = 0; i1 < playsInFormation && i1 < num; ++i1)
      {
        PlayDataDef play = (PlayDataDef) formationDataList[index].GetPlay(i1);
        PlayConcept playConcept = play.GetPlayConcept();
        dictionary1[playConcept].Add(play);
        dictionary2[play.GetFormation().GetBaseFormation()].Add(play);
        switch (playConcept)
        {
          case PlayConcept.Man_Coverage:
            switch (baseFormation)
            {
              case BaseFormation.Six_Two:
                playDataDefList1.Add(play);
                break;
              case BaseFormation.Five_Three:
                playDataDefList2.Add(play);
                break;
              default:
                if (linemenInFormation + linebackersInFormation == 7)
                {
                  playDataDefList3.Add(play);
                  break;
                }
                playDataDefList4.Add(play);
                break;
            }
            break;
          case PlayConcept.Man_Zone_Double:
            switch (baseFormation)
            {
              case BaseFormation.Six_Two:
                playDataDefList5.Add(play);
                break;
              case BaseFormation.Five_Three:
                playDataDefList6.Add(play);
                break;
              default:
                if (linemenInFormation + linebackersInFormation == 7)
                {
                  playDataDefList7.Add(play);
                  break;
                }
                playDataDefList8.Add(play);
                break;
            }
            break;
          case PlayConcept.Cover_Two:
            switch (baseFormation)
            {
              case BaseFormation.Six_Two:
                playDataDefList9.Add(play);
                break;
              case BaseFormation.Five_Three:
                playDataDefList10.Add(play);
                break;
              default:
                if (linemenInFormation + linebackersInFormation == 7)
                {
                  playDataDefList11.Add(play);
                  break;
                }
                playDataDefList12.Add(play);
                break;
            }
            break;
          case PlayConcept.Cover_Three:
            switch (baseFormation)
            {
              case BaseFormation.Six_Two:
                playDataDefList13.Add(play);
                break;
              case BaseFormation.Five_Three:
                playDataDefList14.Add(play);
                break;
              default:
                if (linemenInFormation + linebackersInFormation == 7)
                {
                  playDataDefList15.Add(play);
                  break;
                }
                playDataDefList16.Add(play);
                break;
            }
            break;
          case PlayConcept.Cover_Four:
            switch (baseFormation)
            {
              case BaseFormation.Six_Two:
                playDataDefList17.Add(play);
                break;
              case BaseFormation.Five_Three:
                playDataDefList18.Add(play);
                break;
              default:
                if (linemenInFormation + linebackersInFormation == 7)
                {
                  playDataDefList19.Add(play);
                  break;
                }
                playDataDefList20.Add(play);
                break;
            }
            break;
          case PlayConcept.Man_Blitz:
            switch (baseFormation)
            {
              case BaseFormation.Six_Two:
                playDataDefList21.Add(play);
                break;
              case BaseFormation.Five_Three:
                playDataDefList22.Add(play);
                break;
              default:
                if (linemenInFormation + linebackersInFormation == 7)
                {
                  playDataDefList23.Add(play);
                  break;
                }
                playDataDefList24.Add(play);
                break;
            }
            break;
          case PlayConcept.Zone_Blitz:
            switch (baseFormation)
            {
              case BaseFormation.Six_Two:
                playDataDefList25.Add(play);
                break;
              case BaseFormation.Five_Three:
                playDataDefList26.Add(play);
                break;
              default:
                if (linemenInFormation + linebackersInFormation == 7)
                {
                  playDataDefList27.Add(play);
                  break;
                }
                playDataDefList28.Add(play);
                break;
            }
            break;
          default:
            if (linemenInFormation < 6)
            {
              Debug.Log((object) string.Format("Did not find a category for play: {0} - {1} => Concept = {2}, DL = {3}, LB = {4}, DB = {5}", (object) formationDataList[index].GetName(), (object) play.GetPlayName(), (object) playConcept, (object) linemenInFormation, (object) linebackersInFormation, (object) backsInFormation));
              break;
            }
            break;
        }
        for (int i2 = 0; i2 < 11; ++i2)
        {
          PlayAssignment routeData = play.GetRouteData(i2);
          if (routeData is ManDefenseAssignment && (double) routeData.GetRoutePoints()[0] == 0.0)
          {
            playDataDefList29.Add(play);
            break;
          }
        }
      }
    }
    if (playDataDefList1.Count == 0)
      playDataDefList1.Add((PlayDataDef) this.fiveThreePlays.GetPlay(0));
    if (playDataDefList2.Count == 0)
      playDataDefList2.Add((PlayDataDef) this.fourFourPlays.GetPlay(0));
    if (playDataDefList3.Count == 0)
      playDataDefList3.Add((PlayDataDef) this.nickelPlays.GetPlay(0));
    if (playDataDefList4.Count == 0)
      playDataDefList4.Add((PlayDataDef) this.dimePlays.GetPlay(0));
    if (playDataDefList5.Count == 0)
      playDataDefList5.Add((PlayDataDef) this.fiveThreePlays.GetPlay(7));
    if (playDataDefList6.Count == 0)
      playDataDefList6.Add((PlayDataDef) this.fourFourPlays.GetPlay(1));
    if (playDataDefList7.Count == 0)
      playDataDefList7.Add((PlayDataDef) this.nickelPlays.GetPlay(6));
    if (playDataDefList8.Count == 0)
      playDataDefList8.Add((PlayDataDef) this.dimePlays.GetPlay(11));
    if (playDataDefList9.Count == 0)
      playDataDefList9.Add((PlayDataDef) this.fiveThreePlays.GetPlay(3));
    if (playDataDefList10.Count == 0)
      playDataDefList10.Add((PlayDataDef) this.fourFourPlays.GetPlay(1));
    if (playDataDefList11.Count == 0)
      playDataDefList11.Add((PlayDataDef) this.nickelPlays.GetPlay(0));
    if (playDataDefList12.Count == 0)
      playDataDefList12.Add((PlayDataDef) this.dimePlays.GetPlay(8));
    if (playDataDefList13.Count == 0)
      playDataDefList13.Add((PlayDataDef) this.sixTwoPlays.GetPlay(4));
    if (playDataDefList14.Count == 0)
      playDataDefList14.Add((PlayDataDef) this.fourFourPlays.GetPlay(2));
    if (playDataDefList15.Count == 0)
      playDataDefList15.Add((PlayDataDef) this.nickelPlays.GetPlay(1));
    if (playDataDefList16.Count == 0)
      playDataDefList16.Add((PlayDataDef) this.dimePlays.GetPlay(2));
    if (playDataDefList17.Count == 0)
      playDataDefList17.Add((PlayDataDef) this.fiveThreePlays.GetPlay(8));
    if (playDataDefList18.Count == 0)
      playDataDefList18.Add((PlayDataDef) this.fourFourPlays.GetPlay(2));
    if (playDataDefList19.Count == 0)
      playDataDefList19.Add((PlayDataDef) this.nickelPlays.GetPlay(3));
    if (playDataDefList20.Count == 0)
      playDataDefList20.Add((PlayDataDef) this.dimePlays.GetPlay(6));
    if (playDataDefList21.Count == 0)
      playDataDefList21.Add((PlayDataDef) this.fiveThreePlays.GetPlay(15));
    if (playDataDefList22.Count == 0)
      playDataDefList22.Add((PlayDataDef) this.fourFourPlays.GetPlay(15));
    if (playDataDefList23.Count == 0)
      playDataDefList23.Add((PlayDataDef) this.nickelPlays.GetPlay(16));
    if (playDataDefList24.Count == 0)
      playDataDefList24.Add((PlayDataDef) this.dimePlays.GetPlay(4));
    if (playDataDefList25.Count == 0)
      playDataDefList25.Add((PlayDataDef) this.fiveThreePlays.GetPlay(1));
    if (playDataDefList26.Count == 0)
      playDataDefList26.Add((PlayDataDef) this.fourFourPlays.GetPlay(1));
    if (playDataDefList27.Count == 0)
      playDataDefList27.Add((PlayDataDef) this.nickelPlays.GetPlay(2));
    if (playDataDefList28.Count == 0)
      playDataDefList28.Add((PlayDataDef) this.dimePlays.GetPlay(3));
    if (isPlayerOne)
    {
      this.manCoverage_Close_P1 = playDataDefList1;
      this.manCoverage_Short_P1 = playDataDefList2;
      this.manCoverage_Medium_P1 = playDataDefList3;
      this.manCoverage_Long_P1 = playDataDefList4;
      this.manZoneDouble_Close_P1 = playDataDefList5;
      this.manZoneDouble_Short_P1 = playDataDefList6;
      this.manZoneDouble_Medium_P1 = playDataDefList7;
      this.manZoneDouble_Long_P1 = playDataDefList8;
      this.cover2_Close_P1 = playDataDefList9;
      this.cover2_Short_P1 = playDataDefList10;
      this.cover2_Medium_P1 = playDataDefList11;
      this.cover2_Long_P1 = playDataDefList12;
      this.cover3_Close_P1 = playDataDefList13;
      this.cover3_Short_P1 = playDataDefList14;
      this.cover3_Medium_P1 = playDataDefList15;
      this.cover3_Long_P1 = playDataDefList16;
      this.cover4_Close_P1 = playDataDefList17;
      this.cover4_Short_P1 = playDataDefList18;
      this.cover4_Medium_P1 = playDataDefList19;
      this.cover4_Long_P1 = playDataDefList20;
      this.manBlitz_Close_P1 = playDataDefList21;
      this.manBlitz_Short_P1 = playDataDefList22;
      this.manBlitz_Medium_P1 = playDataDefList23;
      this.manBlitz_Long_P1 = playDataDefList24;
      this.zoneBlitz_Close_P1 = playDataDefList25;
      this.zoneBlitz_Short_P1 = playDataDefList26;
      this.zoneBlitz_Medium_P1 = playDataDefList27;
      this.zoneBlitz_Long_P1 = playDataDefList28;
      this.qbSpy_P1 = playDataDefList29;
      this.defPlaysByConcept_P1 = dictionary1;
      this.defPlaysByFormation_P1 = dictionary2;
    }
    else
    {
      this.manCoverage_Close_P2 = playDataDefList1;
      this.manCoverage_Short_P2 = playDataDefList2;
      this.manCoverage_Medium_P2 = playDataDefList3;
      this.manCoverage_Long_P2 = playDataDefList4;
      this.manZoneDouble_Close_P2 = playDataDefList5;
      this.manZoneDouble_Short_P2 = playDataDefList6;
      this.manZoneDouble_Medium_P2 = playDataDefList7;
      this.manZoneDouble_Long_P2 = playDataDefList8;
      this.cover2_Close_P2 = playDataDefList9;
      this.cover2_Short_P2 = playDataDefList10;
      this.cover2_Medium_P2 = playDataDefList11;
      this.cover2_Long_P2 = playDataDefList12;
      this.cover3_Close_P2 = playDataDefList13;
      this.cover3_Short_P2 = playDataDefList14;
      this.cover3_Medium_P2 = playDataDefList15;
      this.cover3_Long_P2 = playDataDefList16;
      this.cover4_Close_P2 = playDataDefList17;
      this.cover4_Short_P2 = playDataDefList18;
      this.cover4_Medium_P2 = playDataDefList19;
      this.cover4_Long_P2 = playDataDefList20;
      this.manBlitz_Close_P2 = playDataDefList21;
      this.manBlitz_Short_P2 = playDataDefList22;
      this.manBlitz_Medium_P2 = playDataDefList23;
      this.manBlitz_Long_P2 = playDataDefList24;
      this.zoneBlitz_Close_P2 = playDataDefList25;
      this.zoneBlitz_Short_P2 = playDataDefList26;
      this.zoneBlitz_Medium_P2 = playDataDefList27;
      this.zoneBlitz_Long_P2 = playDataDefList28;
      this.qbSpy_P2 = playDataDefList29;
      this.defPlaysByConcept_P2 = dictionary1;
      this.defPlaysByFormation_P2 = dictionary2;
    }
  }

  public List<PlayDataDef> GetPlayCategory_ManCoverage_Close(bool playerOne) => !playerOne ? this.manCoverage_Close_P2 : this.manCoverage_Close_P1;

  public List<PlayDataDef> GetPlayCategory_ManCoverage_Short(bool playerOne) => !playerOne ? this.manCoverage_Short_P2 : this.manCoverage_Short_P1;

  public List<PlayDataDef> GetPlayCategory_ManCoverage_Medium(bool playerOne) => !playerOne ? this.manCoverage_Medium_P2 : this.manCoverage_Medium_P1;

  public List<PlayDataDef> GetPlayCategory_ManCoverage_Long(bool playerOne) => !playerOne ? this.manCoverage_Long_P2 : this.manCoverage_Long_P1;

  public List<PlayDataDef> GetPlayCategory_ManZoneDouble_Close(bool playerOne) => !playerOne ? this.manZoneDouble_Close_P2 : this.manZoneDouble_Close_P1;

  public List<PlayDataDef> GetPlayCategory_ManZoneDouble_Short(bool playerOne) => !playerOne ? this.manZoneDouble_Short_P2 : this.manZoneDouble_Short_P1;

  public List<PlayDataDef> GetPlayCategory_ManZoneDouble_Medium(bool playerOne) => !playerOne ? this.manZoneDouble_Medium_P2 : this.manZoneDouble_Medium_P1;

  public List<PlayDataDef> GetPlayCategory_ManZoneDouble_Long(bool playerOne) => !playerOne ? this.manZoneDouble_Long_P2 : this.manZoneDouble_Long_P1;

  public List<PlayDataDef> GetPlayCategory_Cover2_Close(bool playerOne) => !playerOne ? this.cover2_Close_P2 : this.cover2_Close_P1;

  public List<PlayDataDef> GetPlayCategory_Cover2_Short(bool playerOne) => !playerOne ? this.cover2_Short_P2 : this.cover2_Short_P1;

  public List<PlayDataDef> GetPlayCategory_Cover2_Medium(bool playerOne) => !playerOne ? this.cover2_Medium_P2 : this.cover2_Medium_P1;

  public List<PlayDataDef> GetPlayCategory_Cover2_Long(bool playerOne) => !playerOne ? this.cover2_Long_P2 : this.cover2_Long_P1;

  public List<PlayDataDef> GetPlayCategory_Cover3_Close(bool playerOne) => !playerOne ? this.cover3_Close_P2 : this.cover3_Close_P1;

  public List<PlayDataDef> GetPlayCategory_Cover3_Short(bool playerOne) => !playerOne ? this.cover3_Short_P2 : this.cover3_Short_P1;

  public List<PlayDataDef> GetPlayCategory_Cover3_Medium(bool playerOne) => !playerOne ? this.cover3_Medium_P2 : this.cover3_Medium_P1;

  public List<PlayDataDef> GetPlayCategory_Cover3_Long(bool playerOne) => !playerOne ? this.cover3_Long_P2 : this.cover3_Long_P1;

  public List<PlayDataDef> GetPlayCategory_Cover4_Close(bool playerOne) => !playerOne ? this.cover4_Close_P2 : this.cover4_Close_P1;

  public List<PlayDataDef> GetPlayCategory_Cover4_Short(bool playerOne) => !playerOne ? this.cover4_Short_P2 : this.cover4_Short_P1;

  public List<PlayDataDef> GetPlayCategory_Cover4_Medium(bool playerOne) => !playerOne ? this.cover4_Medium_P2 : this.cover4_Medium_P1;

  public List<PlayDataDef> GetPlayCategory_Cover4_Long(bool playerOne) => !playerOne ? this.cover4_Long_P2 : this.cover4_Long_P1;

  public List<PlayDataDef> GetPlayCategory_ManBlitz_Close(bool playerOne) => !playerOne ? this.manBlitz_Close_P2 : this.manBlitz_Close_P1;

  public List<PlayDataDef> GetPlayCategory_ManBlitz_Short(bool playerOne) => !playerOne ? this.manBlitz_Short_P2 : this.manBlitz_Short_P1;

  public List<PlayDataDef> GetPlayCategory_ManBlitz_Medium(bool playerOne) => !playerOne ? this.manBlitz_Medium_P2 : this.manBlitz_Medium_P1;

  public List<PlayDataDef> GetPlayCategory_ManBlitz_Long(bool playerOne) => !playerOne ? this.manBlitz_Long_P2 : this.manBlitz_Long_P1;

  public List<PlayDataDef> GetPlayCategory_ZoneBlitz_Close(bool playerOne) => !playerOne ? this.zoneBlitz_Close_P2 : this.zoneBlitz_Close_P1;

  public List<PlayDataDef> GetPlayCategory_ZoneBlitz_Short(bool playerOne) => !playerOne ? this.zoneBlitz_Short_P2 : this.zoneBlitz_Short_P1;

  public List<PlayDataDef> GetPlayCategory_ZoneBlitz_Medium(bool playerOne) => !playerOne ? this.zoneBlitz_Medium_P2 : this.zoneBlitz_Medium_P1;

  public List<PlayDataDef> GetPlayCategory_ZoneBlitz_Long(bool playerOne) => !playerOne ? this.zoneBlitz_Long_P2 : this.zoneBlitz_Long_P1;

  public List<PlayDataDef> GetPlayCategory_QbSpy(bool playerOne) => !playerOne ? this.qbSpy_P2 : this.qbSpy_P1;

  public List<PlayDataDef> GetAllDefensivePlaysByConcept(PlayConcept concept, bool playerOne) => !playerOne ? this.defPlaysByConcept_P2[concept] : this.defPlaysByConcept_P1[concept];

  public List<PlayDataDef> GetAllDefensivePlaysByFormation(BaseFormation formation, bool playerOne) => !playerOne ? this.defPlaysByFormation_P2[formation] : this.defPlaysByFormation_P1[formation];

  public List<FormationData> GetOffPlaybookForPlayer(Player player) => player != Player.One ? this.offPlaybookP2 : this.offPlaybookP1;

  public List<FormationData> GetDefPlaybookForPlayer(Player player) => player != Player.One ? this.defPlaybookP2 : this.defPlaybookP1;

  public FormationData GetOffFormDataFromPlay(bool isUserOff, PlayData p)
  {
    List<FormationData> formationList = isUserOff ? this.offPlaybookP1 : this.offPlaybookP2;
    if (p.GetPlayConcept() == PlayConcept.Clock_Control)
      return PlayManager.GetFormationByName(Plays.CLOCK_CONTROL_FORMATION_GROUP_NAME, formationList);
    foreach (FormationData formDataFromPlay in formationList)
    {
      if (p.GetFormation().GetBaseFormation() == formDataFromPlay.GetFormationPositions().GetBaseFormation() && p.GetFormation().GetSubFormation() == formDataFromPlay.GetFormationPositions().GetSubFormation())
        return formDataFromPlay;
    }
    return (FormationData) null;
  }

  private void RunPlayValidation_Off()
  {
    MonoBehaviour.print((object) "Running Offensive Play Vadiation---------------------------------");
    List<string> stringList = new List<string>();
    for (int index1 = 0; index1 < this.playbook_AllOffPlays.Count; ++index1)
    {
      FormationData playbookAllOffPlay = this.playbook_AllOffPlays[index1];
      int playsInFormation = playbookAllOffPlay.GetNumberOfPlaysInFormation();
      string upper = playbookAllOffPlay.GetName().ToUpper();
      stringList.Clear();
      for (int i = 0; i < playsInFormation; ++i)
      {
        PlayDataOff play = (PlayDataOff) playbookAllOffPlay.GetPlay(i);
        PlayType playType = play.GetPlayType();
        PlayTypeSpecific playTypeSpecific = play.GetPlayTypeSpecific();
        DropbackType dropbackType = play.GetDropbackType();
        HandoffType handoffType = play.GetHandoffType();
        PlayConcept playConcept = play.GetPlayConcept();
        int handoffTarget = play.GetHandoffTarget();
        PlayAssignment routeData1 = play.GetRouteData(handoffTarget);
        PlayAssignment routeData2 = play.GetRouteData(play.GetPrimaryReceiver());
        PlayAssignment routeData3 = play.GetRouteData(5);
        bool flag = handoffType == HandoffType.GunTossLeft || handoffType == HandoffType.GunTossRight || handoffType == HandoffType.UnderCenterTossLeft || handoffType == HandoffType.UnderCenterTossRight;
        int runnerHoleOffset = play.GetRunnerHoleOffset();
        stringList.Add(play.GetPlayName());
        if (playType == PlayType.Pass)
        {
          if (playTypeSpecific == PlayTypeSpecific.NormalRun)
            this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "PLAY TYPE / PLAY TYPE SPECIFIC MISMATCH");
        }
        else if (playType == PlayType.Run && (playTypeSpecific == PlayTypeSpecific.NormalPass || playTypeSpecific == PlayTypeSpecific.PlayAction))
          this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "PLAY TYPE / PLAY TYPE SPECIFIC MISMATCH");
        if (playType == PlayType.Pass)
        {
          if (playTypeSpecific == PlayTypeSpecific.PlayAction)
          {
            if (dropbackType != DropbackType.OneStep)
              this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "PLAY ACTION PLAYS SHOULD HAVE DROPBACKTYPE.NONE");
          }
          else if (dropbackType != DropbackType.ThreeStep && dropbackType != DropbackType.FiveStep && dropbackType != DropbackType.Shotgun)
            this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "PASS PLAY DOES NOT HAVE A VALID DROPBACK TYPE");
        }
        else if (playType == PlayType.Run && dropbackType != DropbackType.OneStep)
          this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "RUN PLAYS SHOULD HAVE DROPBACKTYPE.NONE");
        if (playType == PlayType.Run)
        {
          if (handoffType == HandoffType.None)
            this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "NO HANDOFF TYPE SET FOR THIS RUN PLAY");
          if (flag)
          {
            if (routeData1 == this.rbTossIn)
            {
              if (handoffType != HandoffType.UnderCenterTossRight)
                this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "HANDOFF TYPE SHOULD BE TOSS RIGHT");
            }
            else if (routeData1 == this.rbTossOut)
            {
              if (handoffType != HandoffType.UnderCenterTossLeft)
                this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "HANDOFF TYPE SHOULD BE TOSS LEFT");
            }
            else if (routeData1 == this.rbGunToss && handoffType != HandoffType.GunTossRight)
              this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "HANDOFF TYPE SHOULD BE GunTossRight");
          }
        }
        if (playType == PlayType.Run)
        {
          if (flag)
          {
            if (routeData3 != this.qbToss)
              this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "QB ROUTEDATA SHOULD BE qbToss");
            if (upper == "SHOTGUN")
            {
              if (routeData1 != this.rbGunToss)
                this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "RUNNER ROUTEDATA SHOULD BE rbGunToss");
            }
            else if (routeData1 != this.rbTossIn && routeData1 != this.rbTossOut)
              this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "RUNNER ROUTEDATA SHOULD BE rbTossIn OR rbTossOut");
          }
          else if (routeData3 == this.qbFBDiveIn && routeData1 != this.fbDiveIn && routeData1 != this.fbDive_OffsetRight && routeData1 != this.fbDive_OffsetLeft)
            this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "QB / RUNNER ROUTEDATA MISMATCH");
          else if (routeData3 == this.qbFBDiveOut && routeData1 != this.fbDiveOut)
            this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "QB / RUNNER ROUTEDATA MISMATCH");
          else if (routeData3 == this.qbRBDiveIn && routeData1 != this.rbDiveIn)
            this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "QB / RUNNER ROUTEDATA MISMATCH");
          else if (routeData3 == this.qbRBDiveOut && routeData1 != this.rbDiveOut)
            this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "QB / RUNNER ROUTEDATA MISMATCH");
          else if (routeData3 == this.qbIsoIn && routeData1 != this.rbIsoIn)
            this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "QB / RUNNER ROUTEDATA MISMATCH");
          else if (routeData3 == this.qbIsoOut && routeData1 != this.rbIsoOut)
            this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "QB / RUNNER ROUTEDATA MISMATCH");
          else if (routeData3 == this.qbOffTackleIn && routeData1 != this.rbOffTackleIn)
            this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "QB / RUNNER ROUTEDATA MISMATCH");
          else if (routeData3 == this.qbOffTackleOut && routeData1 != this.rbOffTackleOut)
            this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "QB / RUNNER ROUTEDATA MISMATCH");
        }
        if (playType == PlayType.Pass)
        {
          if (playTypeSpecific != PlayTypeSpecific.PlayAction && (routeData2 == this.teScreenRoute || routeData2 == this.rbScreenIn || routeData2 == this.rbScreenOut))
          {
            if (playConcept != PlayConcept.Screen_Pass)
              this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "INVALID PLAY CONCEPT - SHOULD BE SCREEN PASS");
          }
          else if (playTypeSpecific == PlayTypeSpecific.PlayAction)
          {
            if (playConcept != PlayConcept.Play_Action)
              this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "INVALID PLAY CONCEPT - SHOULD BE PLAY ACTION");
          }
          else if (playConcept != PlayConcept.Deep_Pass && playConcept != PlayConcept.Mid_Pass && playConcept != PlayConcept.Short_Pass && playConcept != PlayConcept.Screen_Pass)
            this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "INVALID PLAY CONCEPT");
        }
        else if (playType == PlayType.Run)
        {
          if (routeData3.GetAssignmentType() == EPlayAssignmentId.RunToEndZone && playConcept != PlayConcept.QB_Keeper)
            this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "INVALID PLAY CONCEPT - SHOULD BE QB KEEPER");
          if (routeData1 == this.rbDiveIn || routeData1 == this.rbDiveOut || routeData1 == this.fbDiveIn || routeData1 == this.fbDiveOut || routeData1 == this.fbDive_OffsetLeft || routeData1 == this.fbDive_OffsetRight || routeData1 == this.rbIsoIn || routeData1 == this.rbIsoOut)
          {
            if (playConcept != PlayConcept.Inside_Run)
              this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "INVALID PLAY CONCEPT - SHOULD BE INSIDE RUN");
          }
          else if (routeData1 == this.rbOffTackleIn || routeData1 == this.rbOffTackleOut || routeData1 == this.rbTossIn || routeData1 == this.rbTossOut || routeData1 == this.rbGunToss || routeData1 == this.rbGunHOAcross)
          {
            if (playConcept != PlayConcept.Outside_Run)
              this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "INVALID PLAY CONCEPT - SHOULD BE OUTSIDE RUN");
          }
          else if (routeData1 == this.rbCounterIn || routeData1 == this.rbCounterOut)
          {
            if (playConcept != PlayConcept.Misdirection)
              this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "INVALID PLAY CONCEPT - SHOULD BE MISDIRECTION");
          }
          else if ((routeData1 == this.slotAround || routeData1 == this.teAround || routeData1 == this.teAroundGun) && playConcept != PlayConcept.Reverse)
            this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "INVALID PLAY CONCEPT - SHOULD BE REVERSE");
        }
        if (playType == PlayType.Run)
        {
          if (routeData1 == this.rbDiveIn || routeData1 == this.rbDiveOut || routeData1 == this.fbDiveIn || routeData1 == this.fbDiveOut || routeData1 == this.fbDive_OffsetLeft || routeData1 == this.fbDive_OffsetRight || routeData1 == this.rbIsoIn || routeData1 == this.rbIsoOut)
          {
            if (runnerHoleOffset != this.runPos_actualPosition)
              this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "INVALID RUN POSITION - SHOULD BE runPos_actualPosition");
          }
          else if (routeData1 == this.rbCounterOut || routeData1 == this.rbOffTackleOut)
          {
            if (runnerHoleOffset != this.runPos_offTackleLeft)
              this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "INVALID RUN POSITION - SHOULD BE runPos_offTackleLeft");
          }
          else if (routeData1 == this.rbCounterIn || routeData1 == this.rbOffTackleIn)
          {
            if (runnerHoleOffset != this.runPos_offTackleRight)
              this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "INVALID RUN POSITION - SHOULD BE runPos_offTackleRight");
          }
          else if (routeData1 == this.rbTossOut)
          {
            if (runnerHoleOffset != this.runPos_sweepLeft)
              this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "INVALID RUN POSITION - SHOULD BE runPos_sweepLeft");
          }
          else if (routeData1 == this.rbTossIn)
          {
            if (runnerHoleOffset != this.runPos_sweepRight)
              this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "INVALID RUN POSITION - SHOULD BE runPos_sweepRight");
          }
          else if ((routeData1 == this.slotAround || routeData1 == this.teAround || routeData1 == this.teAroundGun) && runnerHoleOffset != this.runPos_reverseToLeft && runnerHoleOffset != this.runPos_reverseToRight)
            this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "INVALID RUN POSITION - SHOULD BE runPos_reverseLeft/Right");
        }
        else if (playType == PlayType.Pass && (routeData2 == this.teScreenRoute || routeData2 == this.rbScreenIn || routeData2 == this.rbScreenOut || routeData2 == this.underIn) && runnerHoleOffset != this.runPos_screenLeft && runnerHoleOffset != this.runPos_screenRight)
          this.ShowErrorWithPlay(playbookAllOffPlay, (PlayData) play, "INVALID RUN POSITION - SHOULD BE runPos_screenLeft/Right");
      }
      for (int index2 = 0; index2 < stringList.Count - 1; ++index2)
      {
        string str = stringList[index2];
        for (int index3 = index2 + 1; index3 < stringList.Count; ++index3)
        {
          if (str == stringList[index3])
            this.ShowErrorWithFormation(playbookAllOffPlay, "DUPLICATE PLAY NAME: " + str);
        }
      }
    }
    MonoBehaviour.print((object) "Finished Running Offensive Play Vadiation---------------------------------");
  }

  private void ShowErrorWithFormation(FormationData f, string message) => MonoBehaviour.print((object) ("There is an error with: " + f.GetName() + "   " + f.GetSubFormationName() + "\n     " + message));

  private void ShowErrorWithPlay(FormationData f, PlayData p, string message) => MonoBehaviour.print((object) ("There is an error with: " + f.GetName() + "   " + f.GetSubFormationName() + "  -  " + p.GetPlayName() + "\n     " + message));

  private void SetRouteDataVariables()
  {
    this.yard_1 = 0.9144f;
    this.yards_2 = this.yard_1 * 2f;
    this.yards_3 = this.yard_1 * 3f;
    this.yards_4 = this.yard_1 * 4f;
    this.yards_5 = this.yard_1 * 5f;
    this.yards_6 = this.yard_1 * 6f;
    this.yards_7 = this.yard_1 * 7f;
    this.yards_8 = this.yard_1 * 8f;
    this.yards_9 = this.yard_1 * 9f;
    this.yards_10 = this.yard_1 * 10f;
    this.yards_11 = this.yard_1 * 11f;
    this.yards_12 = this.yard_1 * 12f;
    this.yards_13 = this.yard_1 * 13f;
    this.yards_14 = this.yard_1 * 14f;
    this.yards_15 = this.yard_1 * 15f;
    this.yards_16 = this.yard_1 * 16f;
    this.yards_17 = this.yard_1 * 17f;
    this.yards_18 = this.yard_1 * 18f;
    this.yards_19 = this.yard_1 * 19f;
    this.yards_20 = this.yard_1 * 20f;
    this.yards_21 = this.yard_1 * 21f;
    this.yards_22 = this.yard_1 * 22f;
    this.yards_23 = this.yard_1 * 23f;
    this.yards_24 = this.yard_1 * 24f;
    this.yards_25 = this.yard_1 * 25f;
    this.yards_26 = this.yard_1 * 26f;
    this.yards_27 = this.yard_1 * 27f;
    this.yards_28 = this.yard_1 * 28f;
    this.yards_29 = this.yard_1 * 29f;
    this.yards_30 = this.yard_1 * 30f;
    this.yards_31 = this.yard_1 * 31f;
    this.yards_32 = this.yard_1 * 32f;
    this.yards_33 = this.yard_1 * 33f;
    this.yards_34 = this.yard_1 * 34f;
    this.yards_35 = this.yard_1 * 35f;
    this.yards_36 = this.yard_1 * 36f;
    this.yards_37 = this.yard_1 * 37f;
    this.yards_38 = this.yard_1 * 38f;
    this.yards_39 = this.yard_1 * 39f;
    this.yards_40 = this.yard_1 * 40f;
    this.yards_41 = this.yard_1 * 41f;
    this.yards_42 = this.yard_1 * 42f;
    this.yards_43 = this.yard_1 * 43f;
    this.yards_44 = this.yard_1 * 44f;
    this.yards_45 = this.yard_1 * 45f;
    this.yards_46 = this.yard_1 * 46f;
    this.yards_47 = this.yard_1 * 47f;
    this.yards_48 = this.yard_1 * 48f;
    this.yards_49 = this.yard_1 * 49f;
    this.yards_50 = this.yard_1 * 50f;
    this.yards_60 = this.yard_1 * 60f;
    this.yards_70 = this.yard_1 * 70f;
    this.yards_80 = this.yard_1 * 80f;
    this.yards_90 = this.yard_1 * 90f;
    this.yards_100 = this.yard_1 * 100f;
    this.speed_0 = 0.0f;
    this.speed_1 = 1f;
    this.speed_2 = 2f;
    this.speed_3 = 3f;
    this.speed_4 = 4f;
    this.speed_5 = 5f;
  }

  private void SetRouteData_Offense()
  {
    int num1 = 0;
    int num2 = 1;
    int num3 = 3;
    this.runBlockT = (PlayAssignment) new RunBlockAssignment(this.rgd_runBlock, new float[4]
    {
      (float) num2,
      0.0f,
      1.5f,
      this.speed_0
    });
    this.runBlockG = (PlayAssignment) new RunBlockAssignment(this.rgd_runBlock, new float[4]
    {
      (float) num2,
      0.0f,
      1.5f,
      this.speed_0
    });
    this.runBlockC = (PlayAssignment) new RunBlockAssignment(this.rgd_runBlock, new float[4]
    {
      (float) num2,
      0.0f,
      1.5f,
      this.speed_0
    });
    this.runBlockTE = (PlayAssignment) new RunBlockAssignment(this.rgd_runBlock, new float[4]
    {
      (float) num2,
      0.0f,
      1.5f,
      this.speed_0
    });
    this.runBlockWR = (PlayAssignment) new RunBlockAssignment(this.rgd_runBlock, new float[4]
    {
      (float) num2,
      0.0f,
      this.yards_2,
      this.speed_0
    });
    this.runBlockRB = (PlayAssignment) new RunBlockAssignment(this.rgd_runBlock, new float[4]
    {
      (float) num2,
      0.0f,
      this.yards_2,
      this.speed_0
    });
    this.passBlockC_Shotgun = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.0f,
      -this.yards_3,
      this.speed_0
    });
    this.passBlockC = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.0f,
      -this.yards_3,
      this.speed_0
    });
    this.passBlockG = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.0f,
      -this.yards_4,
      this.speed_0
    });
    this.passBlockT = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.0f,
      -this.yards_6,
      this.speed_0
    });
    this.passBlockTE = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      this.yard_1,
      -this.yards_4,
      this.speed_0
    });
    this.passBlockRB = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlockRB, new float[4]
    {
      (float) num3,
      -1.25f,
      -this.yards_5,
      this.speed_0
    });
    this.passBlockRB_Singleback = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlockRB, new float[4]
    {
      (float) num3,
      1.25f,
      -5f,
      this.speed_0
    });
    this.passBlockRB_Shotgun_Right = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlockRB, new float[4]
    {
      (float) num3,
      1.25f,
      -5f,
      this.speed_0
    });
    this.passBlockRB_Shotgun_Left = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlockRB, new float[4]
    {
      (float) num3,
      -1.25f,
      -5f,
      this.speed_0
    });
    this.passBlockFB_GoLeft = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlockRB, new float[4]
    {
      (float) num3,
      -1.25f,
      -3f,
      this.speed_0
    });
    this.passBlockLT_Short = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.0f,
      -3.95f,
      this.speed_0
    });
    this.passBlockLG_Short = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.0f,
      -2.95f,
      this.speed_0
    });
    this.passBlockC_Short = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.0f,
      -2f,
      this.speed_0
    });
    this.passBlockRG_Short = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.0f,
      -2.95f,
      this.speed_0
    });
    this.passBlockRT_Short = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.0f,
      -3.95f,
      this.speed_0
    });
    this.passBlockLT_Mid = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      -0.5f,
      -4.95f,
      this.speed_0
    });
    this.passBlockLG_Mid = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      -0.25f,
      -3.95f,
      this.speed_0
    });
    this.passBlockC_Mid = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.0f,
      -3.33f,
      this.speed_0
    });
    this.passBlockRG_Mid = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.25f,
      -3.95f,
      this.speed_0
    });
    this.passBlockRT_Mid = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.5f,
      -4.95f,
      this.speed_0
    });
    this.passBlockLT_Deep = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      -1f,
      -5.95f,
      this.speed_0
    });
    this.passBlockLG_Deep = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      -0.5f,
      -4.95f,
      this.speed_0
    });
    this.passBlockC_Deep = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.0f,
      -4.33f,
      this.speed_0
    });
    this.passBlockRG_Deep = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.5f,
      -4.95f,
      this.speed_0
    });
    this.passBlockRT_Deep = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      1f,
      -5.95f,
      this.speed_0
    });
    this.passBlockLT_PA_Left = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      -4f,
      -0.5f,
      this.speed_0
    });
    this.passBlockLG_PA_Left = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      -3f,
      -0.5f,
      this.speed_0
    });
    this.passBlockC_PA_Left = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      -1.5f,
      -0.5f,
      this.speed_0
    });
    this.passBlockRG_PA_Left = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      -0.5f,
      -0.5f,
      this.speed_0
    });
    this.passBlockRT_PA_Left = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      -1.5f,
      -0.5f,
      this.speed_0
    });
    this.passBlockLT_PA_Right = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      1.5f,
      -0.5f,
      this.speed_0
    });
    this.passBlockLG_PA_Right = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.5f,
      -0.5f,
      this.speed_0
    });
    this.passBlockC_PA_Right = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      1.5f,
      -0.5f,
      this.speed_0
    });
    this.passBlockRG_PA_Right = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      3f,
      -0.5f,
      this.speed_0
    });
    this.passBlockRT_PA_Right = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      4f,
      -0.5f,
      this.speed_0
    });
    this.passBlockT_PA = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.0f,
      -2.5f,
      this.speed_0
    });
    this.passBlockG_PA = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.0f,
      -2f,
      this.speed_0
    });
    this.passBlockC_PA = (PlayAssignment) new PassBlockAssignment(this.rgd_passBlock, new float[4]
    {
      (float) num3,
      0.0f,
      -2.5f,
      this.speed_0
    });
    this.pullBlockOut = (PlayAssignment) new RunBlockAssignment(this.rgd_pullBlockOut, new float[7]
    {
      (float) num2,
      -this.yard_1,
      -this.yards_2,
      this.speed_0,
      -this.yards_6,
      -this.yards_2,
      this.speed_0
    });
    this.pullBlockIn = (PlayAssignment) new RunBlockAssignment(this.rgd_pullBlockIn, new float[7]
    {
      (float) num2,
      this.yard_1,
      -this.yards_2,
      this.speed_0,
      this.yards_6,
      -this.yards_2,
      this.speed_0
    });
    this.isoBlockIn = (PlayAssignment) new RunBlockAssignment(this.rgd_isoBlockIn, new float[4]
    {
      (float) num2,
      this.yard_1,
      -this.yards_5,
      this.speed_0
    });
    this.isoBlockOut = (PlayAssignment) new RunBlockAssignment(this.rgd_isoBlockOut, new float[4]
    {
      (float) num2,
      -this.yard_1,
      -this.yards_5,
      this.speed_0
    });
    this.diveBlockIn = (PlayAssignment) new RunBlockAssignment(this.rgd_diveBlockIn, new float[4]
    {
      (float) num2,
      this.yards_2,
      -this.yards_2,
      this.speed_0
    });
    this.diveBlockOut = (PlayAssignment) new RunBlockAssignment(this.rgd_diveBlockOut, new float[4]
    {
      (float) num2,
      -this.yards_2,
      -this.yards_2,
      this.speed_0
    });
    this.rbSweepBlockIn = (PlayAssignment) new RunBlockAssignment(this.rgd_sweepBlockIn, new float[4]
    {
      (float) num2,
      this.yards_9,
      -this.yards_2,
      this.speed_0
    });
    this.rbSweepBlockOut = (PlayAssignment) new RunBlockAssignment(this.rgd_sweepBlockOut, new float[4]
    {
      (float) num2,
      -this.yards_9,
      -this.yards_2,
      this.speed_0
    });
    this.screenBlockOLIn = (PlayAssignment) new RunBlockAssignment(this.rgd_olScreenBlockIn, new float[10]
    {
      (float) num2,
      this.yards_3,
      -this.yards_2,
      this.speed_1,
      this.yards_11,
      0.0f,
      this.speed_1,
      this.yards_12,
      0.0f,
      -this.speed_5
    });
    this.screenBlockOLOut = (PlayAssignment) new RunBlockAssignment(this.rgd_olScreenBlockOut, new float[10]
    {
      (float) num2,
      -this.yards_3,
      -this.yards_2,
      this.speed_1,
      -this.yards_11,
      0.0f,
      this.speed_1,
      -this.yards_12,
      0.0f,
      -this.speed_5
    });
    this.screenBlockRBIn = (PlayAssignment) new RunBlockAssignment(this.rgd_rbScreenBlockIn, new float[4]
    {
      (float) num2,
      this.yards_12,
      -this.yards_3,
      this.speed_0
    });
    this.screenBlockRBOut = (PlayAssignment) new RunBlockAssignment(this.rgd_rbScreenBlockOut, new float[4]
    {
      (float) num2,
      -this.yards_12,
      -this.yards_3,
      this.speed_0
    });
    this.rbGunPABlock = (PlayAssignment) new RunBlockAssignment(this.rgd_isoBlockIn, new float[4]
    {
      (float) num2,
      this.yards_3,
      -this.yards_3,
      this.speed_0
    });
    this.kickoffMid = new PlayAssignment(EPlayAssignmentId.RunPath, this.rgd_kickoffMid, new float[4]
    {
      (float) num1,
      0.0f,
      this.yards_33,
      -this.speed_2
    });
    this.kickoffLeft = new PlayAssignment(EPlayAssignmentId.RunPath, this.rgd_kickoffLeft, new float[4]
    {
      (float) num1,
      0.0f,
      this.yards_40,
      -this.speed_1
    });
    this.kickoffIn = new PlayAssignment(EPlayAssignmentId.RunPath, this.rgd_kickoffIn, new float[4]
    {
      (float) num1,
      0.0f,
      this.yards_25,
      -this.speed_3
    });
    this.kickoffKicker = new PlayAssignment(EPlayAssignmentId.RunPath, this.empty, new float[4]
    {
      (float) num1,
      0.0f,
      this.yards_20,
      -this.speed_5
    });
    this.puntGunner = new PlayAssignment(EPlayAssignmentId.RunPath, this.empty, new float[4]
    {
      (float) num1,
      -this.yard_1,
      this.yards_35,
      this.speed_0
    });
    this.qbPassPlay = new PlayAssignment(EPlayAssignmentId.BlockGo, this.empty, new float[4]
    {
      (float) num2,
      0.0f,
      -this.yards_6,
      this.speed_0
    });
    this.qbKneel = new PlayAssignment(EPlayAssignmentId.BlockGo, this.empty, new float[4]
    {
      (float) num2,
      0.0f,
      this.yard_1,
      this.speed_0
    });
    this.qbRBDiveOut = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      -this.yard_1,
      -this.yards_16,
      -this.speed_1
    });
    this.qbRBDiveIn = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      this.yard_1,
      -this.yards_17,
      -this.speed_1
    });
    this.qbIsoOut = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      -this.yards_3,
      -this.yards_14,
      this.speed_0
    });
    this.qbIsoIn = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      this.yards_3,
      -this.yards_16,
      this.speed_0
    });
    this.qbOffTackleOut = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      -this.yards_6,
      -this.yards_16,
      this.speed_0
    });
    this.qbOffTackleIn = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      this.yards_6,
      -this.yards_16,
      this.speed_0
    });
    this.qbPistolOffTackleOut = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      -this.yards_4,
      -this.yards_9,
      -this.speed_1
    });
    this.qbPistolOffTackleIn = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      this.yards_4,
      -this.yards_7,
      -this.speed_1
    });
    this.qbPistolPAOut = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      -this.yards_4,
      -this.yards_9,
      -this.speed_1
    });
    this.qbPistolPAIn = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      this.yards_5,
      -this.yards_8,
      -this.speed_1
    });
    this.qbFBDiveOut = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      -this.yard_1,
      -this.yards_5,
      this.speed_0
    });
    this.qbFBDiveIn = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      this.yard_1,
      -this.yards_5,
      this.speed_0
    });
    this.qbToss = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      0.0f,
      0.0f,
      this.speed_0
    });
    this.qbCounterOut = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      -this.yard_1,
      -this.yards_6,
      -this.speed_2
    });
    this.qbCounterIn = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      this.yard_1,
      -this.yards_6,
      -this.speed_2
    });
    this.qbEndAroundToLeft = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[7]
    {
      (float) num2,
      this.yards_2,
      -this.yards_2,
      this.speed_0,
      this.yards_7,
      -this.yards_3,
      this.speed_0
    });
    this.qbEndAroundToRight = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[7]
    {
      (float) num2,
      -this.yards_2,
      -this.yards_2,
      this.speed_0,
      -this.yards_7,
      -this.yards_3,
      this.speed_0
    });
    this.qbSlotAroundToLeft_GL = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[7]
    {
      (float) num2,
      this.yards_2,
      -this.yards_2,
      this.speed_0,
      this.yards_7,
      -this.yards_5,
      this.speed_0
    });
    this.qbSlotAroundToRight_GL = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[7]
    {
      (float) num2,
      -this.yards_2,
      -this.yards_2,
      this.speed_0,
      -this.yards_7,
      -this.yards_5,
      this.speed_0
    });
    this.qbSlotAroundToLeft_SB = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[7]
    {
      (float) num2,
      this.yards_4,
      -this.yards_3,
      this.speed_0,
      this.yards_7,
      -this.yards_6,
      this.speed_0
    });
    this.qbSlotAroundToRight_SB = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[7]
    {
      (float) num2,
      -this.yards_4,
      -this.yards_3,
      this.speed_0,
      -this.yards_7,
      -this.yards_6,
      this.speed_0
    });
    this.qbGunHOAcross = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      0.0f,
      -this.yards_5,
      -this.speed_3
    });
    this.qbGunSlotHOIn = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      this.yards_4,
      -this.yards_4,
      this.speed_0
    });
    this.qbGunSlotHOOut = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      -this.yards_4,
      -this.yards_5,
      this.speed_0
    });
    this.qbGunEndAround = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      this.yards_2,
      -this.yards_3,
      -this.speed_2
    });
    this.qbRHBAcrossIso = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      0.0f,
      -this.yards_5,
      -this.speed_1
    });
    this.qbLHBAcrossIso = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      0.0f,
      -this.yards_5,
      -this.speed_1
    });
    this.qbRHBDive = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      this.yards_3,
      -this.yards_8,
      -this.speed_1
    });
    this.qbLHBDive = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      -this.yards_3,
      -this.yards_8,
      -this.speed_1
    });
    this.qbSplitDiveOut = (PlayAssignment) new GiveHandoffAssignment(this.empty, new float[4]
    {
      (float) num2,
      -this.yards_4,
      -this.yards_8,
      -this.speed_1
    });
    this.qbKeeperRight = (PlayAssignment) new RunToEndZoneAssignment(this.rgd_qbRun, new float[4]
    {
      (float) num2,
      this.yard_1,
      0.0f,
      this.speed_1
    });
    this.qbKeeperLeft = (PlayAssignment) new RunToEndZoneAssignment(this.rgd_qbRun, new float[4]
    {
      (float) num2,
      -this.yard_1,
      0.0f,
      this.speed_1
    });
    this.qbReadOptionLeft = (PlayAssignment) new RunToEndZoneAssignment(this.empty, new float[4]
    {
      (float) num2,
      -this.yards_9,
      this.yards_5,
      this.speed_0
    });
    this.qbReadOptionRight = (PlayAssignment) new RunToEndZoneAssignment(this.empty, new float[4]
    {
      (float) num2,
      this.yards_9,
      this.yards_5,
      this.speed_0
    });
    this.rbDiveOut = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_diveOut, new float[4]
    {
      (float) num1,
      -this.yards_2,
      -this.yard_1,
      -this.speed_1
    });
    this.rbDiveIn = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_diveIn, new float[4]
    {
      (float) num1,
      this.yards_2,
      -this.yard_1,
      -this.speed_1
    });
    this.rbIsoIn = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_isoIn, new float[4]
    {
      (float) num1,
      this.yards_2,
      -this.yard_1,
      this.speed_1
    });
    this.rbIsoOut = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_isoOut, new float[4]
    {
      (float) num1,
      -this.yards_2,
      -this.yard_1,
      this.speed_1
    });
    this.rbOffTackleIn = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_offTackleIn, new float[4]
    {
      (float) num1,
      this.yards_2,
      -this.yard_1,
      this.speed_1
    });
    this.rbOffTackleOut = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_offTackleOut, new float[4]
    {
      (float) num1,
      -this.yards_2,
      -this.yard_1,
      this.speed_1
    });
    this.rbCounterOut = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_counterOut, new float[7]
    {
      (float) num1,
      this.yard_1,
      -this.yards_7,
      this.speed_2,
      -this.yard_1,
      -this.yard_1,
      this.speed_1
    });
    this.rbCounterIn = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_counterIn, new float[7]
    {
      (float) num1,
      -this.yard_1,
      -this.yards_7,
      this.speed_2,
      this.yard_1,
      -this.yard_1,
      this.speed_1
    });
    this.fbDiveIn = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_fbDiveIn, new float[4]
    {
      (float) num1,
      this.yards_9,
      this.yards_5,
      this.speed_0
    });
    this.fbDive_OffsetRight = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_fbDiveIn, new float[4]
    {
      (float) num1,
      this.yards_3,
      0.0f,
      this.speed_0
    });
    this.fbDive_OffsetLeft = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_fbDiveIn, new float[4]
    {
      (float) num1,
      this.yards_3,
      0.0f,
      this.speed_0
    });
    this.fbDiveOut = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_fbDiveOut, new float[4]
    {
      (float) num1,
      -this.yards_9,
      this.yards_5,
      this.speed_0
    });
    this.rbTossOut = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_tossOut, new float[4]
    {
      (float) num1,
      -this.yards_8,
      -this.yards_5,
      this.speed_0
    });
    this.rbTossIn = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_tossIn, new float[4]
    {
      (float) num1,
      this.yards_8,
      -this.yards_5,
      this.speed_0
    });
    this.rbGunHOAcross = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_gunAcross, new float[4]
    {
      (float) num1,
      this.yards_7,
      -this.yard_1,
      this.speed_0
    });
    this.rbGunToss = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_gunToss, new float[7]
    {
      (float) num1,
      -this.yards_3,
      -this.yards_6,
      this.speed_3,
      -this.yards_6,
      -this.yards_6,
      -this.speed_3
    });
    this.rbSplitDive = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_splitDive, new float[4]
    {
      (float) num1,
      0.0f,
      -this.yards_2,
      -this.speed_1
    });
    this.rhbSplitAcrossIso = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_splitAcross, new float[4]
    {
      (float) num1,
      this.yards_4,
      -this.yards_4,
      this.speed_1
    });
    this.lhbSplitAcrossIso = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_splitAcross, new float[4]
    {
      (float) num1,
      -this.yards_4,
      -this.yards_4,
      this.speed_1
    });
    this.teAround = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_reverse, new float[4]
    {
      (float) num1,
      this.yards_6,
      -this.yards_6,
      this.speed_0
    });
    this.teAroundGun = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_reverse, new float[4]
    {
      (float) num1,
      this.yards_6,
      -this.yards_3,
      this.speed_0
    });
    this.slotAround = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_slotReverse, new float[4]
    {
      (float) num1,
      this.yards_20,
      -this.yards_7,
      this.speed_2
    });
    this.wrZInsideRun = (PlayAssignment) new ReceiveHandoffAssignment(this.rgd_slotReverse, new float[4]
    {
      (float) num1,
      this.yards_20,
      -this.yards_5,
      this.speed_2
    });
    this.out5 = (PlayAssignment) new RunRouteAssignment(this.rgd_out5, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_5,
      this.speed_0,
      -this.yards_30,
      this.yards_5,
      this.speed_0
    });
    this.out10 = (PlayAssignment) new RunRouteAssignment(this.rgd_out10, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_10,
      this.speed_0,
      -this.yards_30,
      this.yards_10,
      this.speed_0
    });
    this.out15 = (PlayAssignment) new RunRouteAssignment(this.rgd_out15, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_15,
      this.speed_0,
      -this.yards_20,
      this.yards_15,
      this.speed_0
    });
    this.in5 = (PlayAssignment) new RunRouteAssignment(this.rgd_in5, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_5,
      this.speed_0,
      this.yards_60,
      this.yards_5,
      this.speed_0
    });
    this.in10 = (PlayAssignment) new RunRouteAssignment(this.rgd_in10, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_10,
      this.speed_0,
      this.yards_60,
      this.yards_10,
      this.speed_0
    });
    this.in15 = (PlayAssignment) new RunRouteAssignment(this.rgd_in15, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_15,
      this.speed_0,
      this.yards_60,
      this.yards_15,
      this.speed_0
    });
    this.slantIn = (PlayAssignment) new RunRouteAssignment(this.rgd_slantIn, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_2,
      this.speed_0,
      this.yards_15,
      this.yards_9,
      this.speed_0
    });
    this.slantOut = (PlayAssignment) new RunRouteAssignment(this.rgd_slantOut, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_2,
      this.speed_0,
      -this.yards_15,
      this.yards_9,
      this.speed_0
    });
    this.slantInHitch = (PlayAssignment) new RunRouteAssignment(this.rgd_slantInHitch, new float[7]
    {
      (float) num1,
      this.yards_10,
      this.yards_8,
      this.speed_0,
      this.yards_12,
      this.yards_6,
      this.speed_0
    });
    this.slantInUpHitchOut = (PlayAssignment) new RunRouteAssignment(this.rgd_slantInUpHitchOut, new float[10]
    {
      (float) num1,
      this.yards_6,
      this.yards_6,
      this.speed_0,
      this.yards_6,
      this.yards_12,
      this.speed_0,
      0.0f,
      this.yards_6,
      -this.speed_1
    });
    this.slantInUpCorner = (PlayAssignment) new RunRouteAssignment(this.rgd_slantInUpCorner, new float[10]
    {
      (float) num1,
      this.yards_6,
      this.yards_6,
      this.speed_0,
      this.yards_6,
      this.yards_12,
      this.speed_0,
      -this.yards_30,
      this.yards_50,
      this.speed_0
    });
    this.slantInUpPost = (PlayAssignment) new RunRouteAssignment(this.rgd_slantInUpPost, new float[10]
    {
      (float) num1,
      this.yards_6,
      this.yards_6,
      this.speed_0,
      this.yards_6,
      this.yards_12,
      this.speed_0,
      this.yards_30,
      this.yards_50,
      this.speed_0
    });
    this.slantInFly = (PlayAssignment) new RunRouteAssignment(this.rgd_slantInFly, new float[7]
    {
      (float) num1,
      this.yards_5,
      this.yards_4,
      this.speed_0,
      this.yards_5,
      this.yards_60,
      this.speed_0
    });
    this.slantOutFly = (PlayAssignment) new RunRouteAssignment(this.rgd_slantOutFly, new float[7]
    {
      (float) num1,
      -this.yards_5,
      this.yards_4,
      this.speed_0,
      -this.yards_5,
      this.yards_50,
      this.speed_0
    });
    this.slantInIn = (PlayAssignment) new RunRouteAssignment(this.rgd_slantInIn, new float[7]
    {
      (float) num1,
      this.yards_5,
      this.yards_4,
      this.speed_0,
      this.yards_30,
      this.yards_4,
      this.speed_0
    });
    this.slantInLowPost = (PlayAssignment) new RunRouteAssignment(this.rgd_slantInLowPost, new float[7]
    {
      (float) num1,
      this.yards_6,
      this.yards_3,
      this.speed_0,
      this.yards_50,
      this.yards_50,
      this.speed_0
    });
    this.slantInLowPostSkinny = (PlayAssignment) new RunRouteAssignment(this.rgd_slantInLowPostSkinny, new float[7]
    {
      (float) num1,
      this.yards_6,
      this.yards_3,
      this.speed_0,
      this.yards_30,
      this.yards_60,
      this.speed_0
    });
    this.slantInPostFlat = (PlayAssignment) new RunRouteAssignment(this.rgd_slantInPostFlat, new float[7]
    {
      (float) num1,
      this.yards_6,
      this.yards_6,
      this.speed_0,
      this.yards_50,
      this.yards_20,
      this.speed_0
    });
    this.slantInPostSkinny = (PlayAssignment) new RunRouteAssignment(this.rgd_slantInPostSkinny, new float[7]
    {
      (float) num1,
      this.yards_6,
      this.yards_6,
      this.speed_0,
      this.yards_30,
      this.yards_60,
      this.speed_0
    });
    this.slantInHighPost = (PlayAssignment) new RunRouteAssignment(this.rgd_slantInHighPost, new float[7]
    {
      (float) num1,
      this.yards_6,
      this.yards_10,
      this.speed_0,
      this.yards_50,
      this.yards_50,
      this.speed_0
    });
    this.slantInHighPostFlat = (PlayAssignment) new RunRouteAssignment(this.rgd_slantInHighPostFlat, new float[7]
    {
      (float) num1,
      this.yards_6,
      this.yards_10,
      this.speed_0,
      this.yards_50,
      this.yards_20,
      this.speed_0
    });
    this.post5 = (PlayAssignment) new RunRouteAssignment(this.rgd_post5, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_5,
      this.speed_0,
      this.yards_25,
      this.yards_50,
      this.speed_0
    });
    this.post10 = (PlayAssignment) new RunRouteAssignment(this.rgd_post10, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_10,
      this.speed_0,
      this.yards_25,
      this.yards_50,
      this.speed_0
    });
    this.post5flat = (PlayAssignment) new RunRouteAssignment(this.rgd_post5flat, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_5,
      this.speed_0,
      this.yards_40,
      this.yards_30,
      this.speed_0
    });
    this.post10flat = (PlayAssignment) new RunRouteAssignment(this.rgd_post10flat, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_10,
      this.speed_0,
      this.yards_40,
      this.yards_30,
      this.speed_0
    });
    this.post5skinny = (PlayAssignment) new RunRouteAssignment(this.rgd_post5skinny, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_5,
      this.speed_0,
      this.yards_10,
      this.yards_50,
      this.speed_0
    });
    this.post10skinny = (PlayAssignment) new RunRouteAssignment(this.rgd_post10skinny, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_10,
      this.speed_0,
      this.yards_10,
      this.yards_50,
      this.speed_0
    });
    this.corner5 = (PlayAssignment) new RunRouteAssignment(this.rgd_corner5, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_5,
      this.speed_0,
      -this.yards_30,
      this.yards_50,
      this.speed_0
    });
    this.corner10 = (PlayAssignment) new RunRouteAssignment(this.rgd_corner10, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_10,
      this.speed_0,
      -this.yards_30,
      this.yards_50,
      this.speed_0
    });
    this.corner5flat = (PlayAssignment) new RunRouteAssignment(this.rgd_corner5flat, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_5,
      this.speed_0,
      -this.yards_40,
      this.yards_30,
      this.speed_0
    });
    this.corner10flat = (PlayAssignment) new RunRouteAssignment(this.rgd_corner10flat, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_10,
      this.speed_0,
      -this.yards_40,
      this.yards_30,
      this.speed_0
    });
    this.corner5skinny = (PlayAssignment) new RunRouteAssignment(this.rgd_corner5skinny, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_5,
      this.speed_0,
      -this.yards_15,
      this.yards_50,
      this.speed_0
    });
    this.corner10skinny = (PlayAssignment) new RunRouteAssignment(this.rgd_corner10skinny, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_10,
      this.speed_0,
      -this.yards_15,
      this.yards_50,
      this.speed_0
    });
    this.fly = (PlayAssignment) new RunRouteAssignment(this.rgd_fly, new float[4]
    {
      (float) num1,
      -this.yards_2,
      this.yards_100,
      this.speed_0
    });
    this.underIn = (PlayAssignment) new RunRouteAssignment(this.rgd_underIn, new float[7]
    {
      (float) num1,
      this.yards_3,
      -this.yards_5,
      this.speed_0,
      this.yards_30,
      -this.yards_5,
      this.speed_0
    });
    this.underOut = (PlayAssignment) new RunRouteAssignment(this.rgd_underOut, new float[4]
    {
      (float) num1,
      -this.yards_30,
      -this.yards_2,
      this.speed_0
    });
    this.hitch5in = (PlayAssignment) new RunRouteAssignment(this.rgd_hitch5in, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_7,
      this.speed_0,
      this.yards_4,
      this.yards_3,
      -this.speed_5
    });
    this.hitch5out = (PlayAssignment) new RunRouteAssignment(this.rgd_hitch5out, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_7,
      this.speed_0,
      -this.yards_4,
      this.yards_3,
      -this.speed_5
    });
    this.hitch10in = (PlayAssignment) new RunRouteAssignment(this.rgd_hitch10in, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_12,
      this.speed_0,
      this.yards_4,
      this.yards_8,
      -this.speed_5
    });
    this.hitch10out = (PlayAssignment) new RunRouteAssignment(this.rgd_hitch10out, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_12,
      this.speed_0,
      -this.yards_4,
      this.yards_8,
      -this.speed_5
    });
    this.hitch15out = (PlayAssignment) new RunRouteAssignment(this.rgd_hitch10out, new float[7]
    {
      (float) num1,
      0.0f,
      this.yards_17,
      this.speed_0,
      -this.yards_4,
      this.yards_8,
      -this.speed_5
    });
    this.hitchInFly = (PlayAssignment) new RunRouteAssignment(this.rgd_hitchInFly, new float[10]
    {
      (float) num1,
      0.0f,
      this.yards_12,
      this.speed_0,
      this.yards_4,
      this.yards_8,
      -this.speed_3,
      this.yards_5,
      this.yards_60,
      this.speed_0
    });
    this.upCornerFly = (PlayAssignment) new RunRouteAssignment(this.rgd_upCornerFly, new float[10]
    {
      (float) num1,
      0.0f,
      this.yards_3,
      this.speed_0,
      -this.yards_6,
      this.yards_7,
      this.speed_0,
      -this.yards_6,
      this.yards_60,
      this.speed_0
    });
    this.underOutAndUp = (PlayAssignment) new RunRouteAssignment(this.rgd_underOutUp, new float[7]
    {
      (float) num1,
      -this.yards_5,
      0.0f,
      this.speed_0,
      -this.yards_5,
      this.yards_100,
      this.speed_0
    });
    this.underOutUpCorner = (PlayAssignment) new RunRouteAssignment(this.rgd_underOutUpCorner, new float[10]
    {
      (float) num1,
      -this.yards_5,
      0.0f,
      this.speed_0,
      -this.yards_5,
      this.yards_5,
      this.speed_0,
      -this.yards_60,
      this.yards_60,
      this.speed_0
    });
    this.underInUpPost = (PlayAssignment) new RunRouteAssignment(this.rgd_underInUpPost, new float[10]
    {
      (float) num1,
      this.yards_5,
      0.0f,
      this.speed_0,
      this.yards_5,
      this.yards_10,
      this.speed_0,
      this.yards_60,
      this.yards_60,
      this.speed_0
    });
    this.upPostHitch = (PlayAssignment) new RunRouteAssignment(this.rgd_upPostHitch, new float[10]
    {
      (float) num1,
      0.0f,
      this.yards_5,
      this.speed_0,
      this.yards_5,
      this.yards_10,
      this.speed_0,
      this.yards_8,
      this.yards_5,
      -this.speed_1
    });
    this.upPostIn = (PlayAssignment) new RunRouteAssignment(this.rgd_upPostIn, new float[10]
    {
      (float) num1,
      0.0f,
      this.yards_5,
      this.speed_0,
      this.yards_5,
      this.yards_10,
      this.speed_0,
      this.yards_60,
      this.yards_10,
      this.speed_0
    });
    this.upPostOut = (PlayAssignment) new RunRouteAssignment(this.rgd_upPostOut, new float[10]
    {
      (float) num1,
      0.0f,
      this.yards_5,
      this.speed_0,
      this.yards_5,
      this.yards_10,
      this.speed_0,
      -this.yards_60,
      this.yards_10,
      this.speed_0
    });
    this.upOutFly = (PlayAssignment) new RunRouteAssignment(this.rgd_upOutFly, new float[10]
    {
      (float) num1,
      0.0f,
      this.yards_5,
      this.speed_0,
      -this.yards_5,
      this.yards_5,
      this.speed_0,
      -this.yards_5,
      this.yards_60,
      this.speed_0
    });
    this.upCornerHitch = (PlayAssignment) new RunRouteAssignment(this.rgd_upCornerHitch, new float[10]
    {
      (float) num1,
      0.0f,
      this.yards_5,
      this.speed_0,
      -this.yards_10,
      this.yards_15,
      this.speed_0,
      -this.yards_10,
      this.yards_5,
      -this.speed_1
    });
    this.upInPost = (PlayAssignment) new RunRouteAssignment(this.rgd_upInPost, new float[10]
    {
      (float) num1,
      0.0f,
      this.yards_5,
      this.speed_0,
      this.yards_10,
      this.yards_5,
      this.speed_0,
      this.yards_60,
      this.yards_20,
      this.speed_0
    });
    this.dragIn = (PlayAssignment) new RunRouteAssignment(this.rgd_dragIn, new float[7]
    {
      (float) num1,
      this.yards_3,
      this.yards_3,
      this.speed_0,
      this.yards_30,
      this.yards_3,
      -this.speed_2
    });
    this.dragInFromSlot = (PlayAssignment) new RunRouteAssignment(this.rgd_dragInFromSlot, new float[10]
    {
      (float) num1,
      this.yards_3,
      this.yards_2,
      this.speed_0,
      this.yards_6,
      this.yards_4,
      -this.speed_2,
      this.yards_30,
      this.yards_6,
      -this.speed_2
    });
    this.dragOut = (PlayAssignment) new RunRouteAssignment(this.rgd_dragOut, new float[7]
    {
      (float) num1,
      -this.yards_3,
      this.yards_3,
      this.speed_0,
      -this.yards_30,
      this.yards_2,
      -this.speed_2
    });
    this.dragOutFromSlot = (PlayAssignment) new RunRouteAssignment(this.rgd_dragOutFromSlot, new float[10]
    {
      (float) num1,
      0.0f,
      0.0f,
      this.speed_0,
      -this.yards_3,
      this.yards_3,
      -this.speed_2,
      -this.yards_30,
      this.yards_3,
      -this.speed_2
    });
    this.post5Corner = (PlayAssignment) new RunRouteAssignment(this.rgd_post5corner, new float[10]
    {
      (float) num1,
      0.0f,
      this.yards_5,
      this.speed_0,
      this.yards_5,
      this.yards_12,
      this.speed_0,
      -this.yards_60,
      this.yards_50,
      this.speed_0
    });
    this.post5Out = (PlayAssignment) new RunRouteAssignment(this.rgd_post5out, new float[10]
    {
      (float) num1,
      0.0f,
      this.yards_5,
      this.speed_0,
      this.yards_5,
      this.yards_10,
      this.speed_0,
      -this.yards_60,
      this.yards_10,
      this.speed_0
    });
    this.rbFlatIn = (PlayAssignment) new RunRouteAssignment(this.rgd_rbFlatIn, new float[7]
    {
      (float) num1,
      this.yards_11,
      this.yard_1,
      -this.speed_1,
      this.yards_22,
      this.yards_2,
      -this.speed_1
    });
    this.rbFlatOut = (PlayAssignment) new RunRouteAssignment(this.rgd_rbFlatOut, new float[7]
    {
      (float) num1,
      -this.yards_11,
      this.yard_1,
      -this.speed_1,
      -this.yards_22,
      this.yards_2,
      -this.speed_1
    });
    this.rbFlatOut_Fly = (PlayAssignment) new RunRouteAssignment(this.rgd_rbFlatOut_Fly, new float[10]
    {
      (float) num1,
      -this.yards_11,
      -this.yards_6,
      -this.speed_1,
      -this.yards_11,
      this.yards_4,
      -this.speed_1,
      -this.yards_15,
      this.yards_40,
      this.speed_0
    });
    this.rbFlatIn_Fly = (PlayAssignment) new RunRouteAssignment(this.rgd_rbFlatIn_Fly, new float[10]
    {
      (float) num1,
      this.yards_11,
      -this.yards_6,
      -this.speed_1,
      this.yards_11,
      this.yards_4,
      -this.speed_1,
      this.yards_15,
      this.yards_40,
      this.speed_0
    });
    this.rbDiveIn_Out = (PlayAssignment) new RunRouteAssignment(this.rgd_rbDiveIn_Out, new float[7]
    {
      (float) num1,
      this.yards_11,
      this.yards_2,
      this.speed_0,
      -this.yards_22,
      this.yards_5,
      this.speed_0
    });
    this.rbDiveIn_In = (PlayAssignment) new RunRouteAssignment(this.rgd_rbDiveIn_In, new float[7]
    {
      (float) num1,
      this.yards_11,
      this.yards_2,
      this.speed_0,
      this.yards_10,
      this.yards_5,
      this.speed_0
    });
    this.rbIsoIn_HitchIn = (PlayAssignment) new RunRouteAssignment(this.rgd_rbIsoIn_HitchIn, new float[10]
    {
      (float) num1,
      this.yards_11,
      this.yards_2,
      this.speed_0,
      0.0f,
      this.yards_7,
      this.speed_0,
      -this.yards_2,
      -this.yards_2,
      -this.speed_2
    });
    this.rbIsoIn_Out = (PlayAssignment) new RunRouteAssignment(this.rgd_PAIsoIn_Out, new float[7]
    {
      (float) num1,
      this.yards_11,
      this.yards_3,
      this.speed_0,
      -this.yards_22,
      this.yards_4,
      this.speed_0
    });
    this.rbIsoIn_In = (PlayAssignment) new RunRouteAssignment(this.rgd_PAIsoIn_In, new float[7]
    {
      (float) num1,
      this.yards_11,
      this.yards_3,
      this.speed_0,
      this.yards_22,
      this.yards_4,
      this.speed_0
    });
    this.rbIsoOut_Out = (PlayAssignment) new RunRouteAssignment(this.rgd_PAIsoOut_Out, new float[7]
    {
      (float) num1,
      -this.yards_11,
      this.yards_3,
      this.speed_0,
      -this.yards_22,
      this.yards_4,
      this.speed_0
    });
    this.rbIsoOut_In = (PlayAssignment) new RunRouteAssignment(this.rgd_PAIsoOut_In, new float[7]
    {
      (float) num1,
      -this.yards_11,
      this.yards_3,
      this.speed_0,
      this.yards_22,
      this.yards_4,
      this.speed_0
    });
    this.rbIsoIn_Fly = (PlayAssignment) new RunRouteAssignment(this.rgd_isoIn, new float[7]
    {
      (float) num1,
      this.yards_11,
      this.yards_3,
      this.speed_0,
      this.yards_5,
      this.yards_45,
      this.speed_0
    });
    this.rbIsoOut_Fly = (PlayAssignment) new RunRouteAssignment(this.rgd_isoOut, new float[7]
    {
      (float) num1,
      -this.yards_11,
      this.yards_3,
      this.speed_0,
      -this.yards_5,
      this.yards_45,
      this.speed_0
    });
    this.rbOffTackleOut_In = (PlayAssignment) new RunRouteAssignment(this.rgd_rbOffTackleOut_In, new float[10]
    {
      (float) num1,
      -this.yards_11,
      -this.yards_2,
      this.speed_0,
      -this.yards_10,
      this.yards_5,
      this.speed_0,
      this.yards_15,
      this.yards_5,
      this.speed_0
    });
    this.rbOffTackleIn_In = (PlayAssignment) new RunRouteAssignment(this.rgd_rbOffTackleIn_In, new float[10]
    {
      (float) num1,
      this.yards_11,
      -this.yard_1,
      this.speed_0,
      this.yards_7,
      this.yards_5,
      this.speed_0,
      -this.yards_15,
      this.yards_5,
      this.speed_0
    });
    this.rbScreenIn = (PlayAssignment) new RunRouteAssignment(this.rgd_rbScreenIn, new float[4]
    {
      (float) num1,
      this.yards_13,
      -this.yards_6,
      this.speed_0
    });
    this.rbScreenOut = (PlayAssignment) new RunRouteAssignment(this.rgd_rbScreenOut, new float[4]
    {
      (float) num1,
      -this.yards_13,
      -this.yards_6,
      this.speed_0
    });
    this.fbDiveIn_Out = (PlayAssignment) new RunRouteAssignment(this.rgd_fbDiveIn_Out, new float[7]
    {
      (float) num1,
      this.yard_1,
      this.yards_3,
      this.speed_0,
      -this.yards_60,
      this.yards_3,
      this.speed_0
    });
    this.fbDiveIn_In = (PlayAssignment) new RunRouteAssignment(this.rgd_fbDiveIn_In, new float[7]
    {
      (float) num1,
      this.yard_1,
      this.yards_3,
      this.speed_0,
      this.yards_60,
      this.yards_3,
      this.speed_0
    });
    this.splitBackIso_Out = (PlayAssignment) new RunRouteAssignment(this.rgd_PAIsoOut_Out, new float[7]
    {
      (float) num1,
      -this.yards_11,
      this.yard_1,
      -this.speed_1,
      -this.yards_22,
      this.yards_2,
      -this.speed_1
    });
    this.splitBackIso_In = (PlayAssignment) new RunRouteAssignment(this.rgd_PAIsoOut_In, new float[7]
    {
      (float) num1,
      this.yards_11,
      this.yard_1,
      -this.speed_1,
      this.yards_22,
      this.yards_2,
      -this.speed_1
    });
    this.rbGunOffTackleIn_Fly = (PlayAssignment) new RunRouteAssignment(this.rgd_rbFlatIn_Fly, new float[7]
    {
      (float) num1,
      this.yards_11,
      -this.yard_1,
      this.speed_0,
      this.yards_10,
      this.yards_50,
      this.speed_0
    });
    this.rbGunOffTackleIn_Out = (PlayAssignment) new RunRouteAssignment(this.rgd_rbGunOffTackleIn_Out, new float[10]
    {
      (float) num1,
      this.yards_11,
      -this.yard_1,
      this.speed_0,
      this.yards_7,
      this.yards_5,
      this.speed_0,
      this.yards_30,
      this.yards_5,
      this.speed_0
    });
    this.rbGunOffTackleIn_In = (PlayAssignment) new RunRouteAssignment(this.rgd_rbGunOffTackleIn_In, new float[10]
    {
      (float) num1,
      this.yards_11,
      -this.yard_1,
      this.speed_0,
      0.0f,
      this.yards_7,
      this.speed_0,
      0.0f,
      this.yards_5,
      this.speed_0
    });
    this.rbGunOffTackleIn_Post = (PlayAssignment) new RunRouteAssignment(this.rgd_rbGunOffTackleIn_Post, new float[10]
    {
      (float) num1,
      this.yards_11,
      -this.yard_1,
      this.speed_0,
      this.yards_7,
      this.yards_5,
      this.speed_0,
      -this.yards_60,
      this.yards_60,
      this.speed_0
    });
    this.teScreenRoute = (PlayAssignment) new RunRouteAssignment(this.rgd_underOut, new float[4]
    {
      (float) num1,
      -this.yards_9,
      -this.yards_7,
      this.speed_0
    });
    this.wrScreenRoute = (PlayAssignment) new RunRouteAssignment(this.rgd_wrScreen, new float[4]
    {
      (float) num1,
      -this.yards_4,
      -this.yards_6,
      -this.speed_3
    });
  }

  private void SetRouteData_Defense()
  {
    float num1 = 0.0f;
    float num2 = 1f;
    float num3 = 2f;
    float num4 = 3f;
    float num5 = 4f;
    float num6 = 5f;
    this.manCoverage = (PlayAssignment) new ManDefenseAssignment(this.empty, new float[1]
    {
      num2
    });
    this.manCoverageInsideOver = (PlayAssignment) new ManDefenseAssignment(this.empty, new float[1]
    {
      num2
    }, techniqueParam: ManDefenseAssignment.EManCoverTypeTechnique.Over);
    this.manCoverageOutsideUnder = (PlayAssignment) new ManDefenseAssignment(this.empty, new float[1]
    {
      num2
    }, leverageParam: ManDefenseAssignment.EManCoverTypeLeverage.Outside);
    this.manCoverageOutsideOver = (PlayAssignment) new ManDefenseAssignment(this.empty, new float[1]
    {
      num2
    }, leverageParam: ManDefenseAssignment.EManCoverTypeLeverage.Outside, techniqueParam: ManDefenseAssignment.EManCoverTypeTechnique.Over);
    this.spyQB = (PlayAssignment) new ManDefenseAssignment(this.empty, new float[1]
    {
      num1
    }, true);
    ZoneCoverageConfig zoneCoverageConfig = Game.ZoneCoverageConfig;
    ZoneCoverageParameters zoneParams1;
    zoneParams1.primaryPrecedence = ZoneCoveragePrecedence.High;
    zoneParams1.secondaryPrecedence = ZoneCoveragePrecedence.Closest;
    zoneParams1.shouldTurnAndChaseReceiver = true;
    zoneParams1.shouldStayDeeperThanDeepestReceiver = true;
    zoneParams1.shouldChargeQBIfOutsidePocket = false;
    zoneParams1.maxDistanceFromBackOfEndZone = zoneCoverageConfig.MinDistanceFromBackOfEndzoneDeep;
    this.deepZone1of1 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_deep1of1, new float[5]
    {
      num3,
      this.yards_70,
      -this.yards_26,
      this.yards_13,
      this.yards_26
    }, new Vector3(0.0f, 0.0f, this.yards_25), zoneParams1);
    this.deepZone1of2 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_deep1of2, new float[5]
    {
      num3,
      this.yards_70,
      -this.yards_26,
      this.yards_13,
      0.0f
    }, new Vector3(-this.yards_13, 0.0f, this.yards_24), zoneParams1);
    this.deepZone2of2 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_deep2of2, new float[5]
    {
      num3,
      this.yards_70,
      0.0f,
      this.yards_13,
      this.yards_26
    }, new Vector3(this.yards_13, 0.0f, this.yards_23), zoneParams1);
    this.deepZone1of3 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_deep1of3, new float[5]
    {
      num3,
      this.yards_70,
      -this.yards_26,
      this.yards_13,
      -this.yards_9
    }, new Vector3(-this.yards_17, 0.0f, this.yards_21), zoneParams1);
    this.deepZone2of3 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_deep2of3, new float[5]
    {
      num3,
      this.yards_70,
      -this.yards_9,
      this.yards_13,
      this.yards_9
    }, new Vector3(0.0f, 0.0f, this.yards_22), zoneParams1);
    this.deepZone3of3 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_deep3of3, new float[5]
    {
      num3,
      this.yards_70,
      this.yards_9,
      this.yards_13,
      this.yards_26
    }, new Vector3(this.yards_17, 0.0f, this.yards_21), zoneParams1);
    this.deepZone1of4 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_deep1of4, new float[5]
    {
      num3,
      this.yards_70,
      -this.yards_26,
      this.yards_13,
      -this.yards_13
    }, new Vector3(-this.yards_19, 0.0f, this.yards_21), zoneParams1);
    this.deepZone2of4 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_deep2of4, new float[5]
    {
      num3,
      this.yards_70,
      -this.yards_13,
      this.yards_13,
      0.0f
    }, new Vector3(-this.yards_6, 0.0f, this.yards_22), zoneParams1);
    this.deepZone3of4 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_deep3of4, new float[5]
    {
      num3,
      this.yards_70,
      0.0f,
      this.yards_13,
      this.yards_13
    }, new Vector3(this.yards_6, 0.0f, this.yards_22), zoneParams1);
    this.deepZone4of4 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_deep4of4, new float[5]
    {
      num3,
      this.yards_70,
      this.yards_13,
      this.yards_13,
      this.yards_26
    }, new Vector3(this.yards_19, 0.0f, this.yards_21), zoneParams1);
    ZoneCoverageParameters zoneParams2;
    zoneParams2.primaryPrecedence = ZoneCoveragePrecedence.High;
    zoneParams2.secondaryPrecedence = ZoneCoveragePrecedence.Closest;
    zoneParams2.shouldTurnAndChaseReceiver = false;
    zoneParams2.shouldStayDeeperThanDeepestReceiver = false;
    zoneParams2.shouldChargeQBIfOutsidePocket = true;
    zoneParams2.maxDistanceFromBackOfEndZone = zoneCoverageConfig.MinDistanceFromBackOfEndzoneShort;
    this.midZone1of2 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_mid1of2, new float[5]
    {
      num3,
      this.yards_7,
      -this.yards_13,
      this.yards_2,
      0.0f
    }, new Vector3(-this.yards_6, 0.0f, this.yards_6), zoneParams2);
    this.midZone2of2 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_mid2of2, new float[5]
    {
      num3,
      this.yards_7,
      -this.yards_2,
      this.yards_2,
      this.yards_13
    }, new Vector3(this.yards_6, 0.0f, this.yards_7), zoneParams2);
    this.midZone1of3 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_mid1of3, new float[5]
    {
      num3,
      this.yards_13,
      -this.yards_20,
      this.yards_2,
      -this.yards_6
    }, new Vector3(-this.yards_10, 0.0f, this.yards_7), zoneParams2);
    this.midZone2of3 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_mid2of3, new float[5]
    {
      num3,
      this.yards_18,
      -this.yards_5,
      this.yards_2,
      this.yards_5
    }, new Vector3(0.0f, 0.0f, this.yards_8), zoneParams2);
    this.midZone3of3 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_mid3of3, new float[5]
    {
      num3,
      this.yards_13,
      this.yards_6,
      this.yards_2,
      this.yards_20
    }, new Vector3(this.yards_10, 0.0f, this.yards_7), zoneParams2);
    this.midZone1of4 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_mid1of4, new float[5]
    {
      num3,
      this.yards_13,
      -this.yards_26,
      this.yards_2,
      -this.yards_6
    }, new Vector3(-this.yards_19, 0.0f, this.yards_7), zoneParams2);
    this.midZone2of4 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_mid2of4, new float[5]
    {
      num3,
      this.yards_13,
      -this.yards_6,
      this.yards_2,
      0.0f
    }, new Vector3(-this.yards_6, 0.0f, this.yards_9), zoneParams2);
    this.midZone3of4 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_mid3of4, new float[5]
    {
      num3,
      this.yards_13,
      0.0f,
      this.yards_2,
      this.yards_6
    }, new Vector3(this.yards_6, 0.0f, this.yards_8), zoneParams2);
    this.midZone4of4 = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_mid4of4, new float[5]
    {
      num3,
      this.yards_13,
      this.yards_6,
      this.yards_2,
      this.yards_26
    }, new Vector3(this.yards_19, 0.0f, this.yards_7), zoneParams2);
    ZoneCoverageParameters zoneParams3;
    zoneParams3.primaryPrecedence = ZoneCoveragePrecedence.Low;
    zoneParams3.secondaryPrecedence = ZoneCoveragePrecedence.Closest;
    zoneParams3.shouldTurnAndChaseReceiver = false;
    zoneParams3.shouldStayDeeperThanDeepestReceiver = false;
    zoneParams3.shouldChargeQBIfOutsidePocket = true;
    zoneParams3.maxDistanceFromBackOfEndZone = zoneCoverageConfig.MinDistanceFromBackOfEndzoneShort;
    this.flatZoneLeft = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_flatLeft, new float[5]
    {
      num3,
      this.yards_11,
      -this.yards_26,
      -this.yard_1,
      -this.yards_16
    }, new Vector3(-this.yards_20, 0.0f, this.yards_5), zoneParams3);
    this.flatZoneRight = (PlayAssignment) new ZoneDefenseAssignment(this.rgd_flatRight, new float[5]
    {
      num3,
      this.yards_11,
      this.yards_16,
      -this.yard_1,
      this.yards_26
    }, new Vector3(this.yards_20, 0.0f, this.yards_5), zoneParams3);
    this.blitzHole0 = (PlayAssignment) new BlitzAssignment(this.rgd_blitz0, new float[4]
    {
      num4,
      0.0f,
      -1.75f,
      -this.speed_1
    });
    this.blitzHole2 = (PlayAssignment) new BlitzAssignment(this.rgd_blitz2, new float[4]
    {
      num4,
      this.yard_1,
      -1.75f,
      -this.speed_1
    });
    this.blitzHole4 = (PlayAssignment) new BlitzAssignment(this.rgd_blitz4, new float[4]
    {
      num4,
      this.yards_2,
      -1.75f,
      -this.speed_1
    });
    this.blitzHole6 = (PlayAssignment) new BlitzAssignment(this.rgd_blitz6, new float[4]
    {
      num4,
      this.yards_4,
      -1.75f,
      -this.speed_1
    });
    this.blitzHole8 = (PlayAssignment) new BlitzAssignment(this.rgd_blitz8, new float[4]
    {
      num4,
      this.yards_6,
      -1.75f,
      -this.speed_1
    });
    this.blitzHole10 = (PlayAssignment) new BlitzAssignment(this.rgd_blitz10, new float[4]
    {
      num4,
      this.yards_8,
      -1.75f,
      -this.speed_1
    });
    this.blitzHole1 = (PlayAssignment) new BlitzAssignment(this.rgd_blitz1, new float[4]
    {
      num4,
      -this.yard_1,
      -1.75f,
      -this.speed_1
    });
    this.blitzHole3 = (PlayAssignment) new BlitzAssignment(this.rgd_blitz3, new float[4]
    {
      num4,
      -this.yards_2,
      -1.75f,
      -this.speed_1
    });
    this.blitzHole5 = (PlayAssignment) new BlitzAssignment(this.rgd_blitz5, new float[4]
    {
      num4,
      -this.yards_4,
      -1.75f,
      -this.speed_1
    });
    this.blitzHole7 = (PlayAssignment) new BlitzAssignment(this.rgd_blitz7, new float[4]
    {
      num4,
      -this.yards_6,
      -1.75f,
      -this.speed_1
    });
    this.blitzHole9 = (PlayAssignment) new BlitzAssignment(this.rgd_blitz9, new float[4]
    {
      num4,
      -this.yards_8,
      -1.75f,
      -this.speed_1
    });
    this.linemanHole9 = (PlayAssignment) new BlitzAssignment(this.rgd_lineman, new float[4]
    {
      num4,
      -this.yards_8,
      -1.75f,
      -this.speed_1
    });
    this.linemanHole7 = (PlayAssignment) new BlitzAssignment(this.rgd_lineman, new float[4]
    {
      num4,
      -this.yards_6,
      -1.75f,
      -this.speed_1
    });
    this.linemanHole5 = (PlayAssignment) new BlitzAssignment(this.rgd_lineman, new float[4]
    {
      num4,
      -this.yards_3,
      -1.75f,
      -this.speed_1
    });
    this.linemanHole3 = (PlayAssignment) new BlitzAssignment(this.rgd_lineman, new float[4]
    {
      num4,
      -this.yards_2,
      -1.75f,
      -this.speed_1
    });
    this.linemanHole1 = (PlayAssignment) new BlitzAssignment(this.rgd_lineman, new float[4]
    {
      num4,
      -this.yard_1,
      -1.75f,
      -this.speed_1
    });
    this.linemanHole0 = (PlayAssignment) new BlitzAssignment(this.rgd_lineman, new float[4]
    {
      num4,
      0.0f,
      -1.75f,
      -this.speed_1
    });
    this.linemanHole2 = (PlayAssignment) new BlitzAssignment(this.rgd_lineman, new float[4]
    {
      num4,
      this.yard_1,
      -1.75f,
      -this.speed_1
    });
    this.linemanHole4 = (PlayAssignment) new BlitzAssignment(this.rgd_lineman, new float[4]
    {
      num4,
      this.yards_2,
      -1.75f,
      -this.speed_1
    });
    this.linemanHole6 = (PlayAssignment) new BlitzAssignment(this.rgd_lineman, new float[4]
    {
      num4,
      this.yards_3,
      -1.75f,
      -this.speed_1
    });
    this.linemanHole8 = (PlayAssignment) new BlitzAssignment(this.rgd_lineman, new float[4]
    {
      num4,
      this.yards_6,
      -1.75f,
      -this.speed_1
    });
    this.linemanHole10 = (PlayAssignment) new BlitzAssignment(this.rgd_lineman, new float[4]
    {
      num4,
      this.yards_8,
      -1.75f,
      -this.speed_1
    });
    this.stuntLeftOut = (PlayAssignment) new BlitzAssignment(this.rgd_stuntOut, new float[4]
    {
      num4,
      -this.yards_6,
      0.0f,
      -this.speed_1
    });
    this.stuntRightOut = (PlayAssignment) new BlitzAssignment(this.rgd_stuntOut, new float[4]
    {
      num4,
      this.yards_6,
      0.0f,
      -this.speed_1
    });
    this.stuntLeftIn = (PlayAssignment) new BlitzAssignment(this.rgd_stuntIn, new float[4]
    {
      num4,
      -this.yards_2,
      this.yard_1,
      -this.speed_1
    });
    this.stuntRightIn = (PlayAssignment) new BlitzAssignment(this.rgd_stuntIn, new float[4]
    {
      num4,
      this.yards_2,
      this.yard_1,
      -this.speed_1
    });
    this.kickRetBlockerIn = (PlayAssignment) new KickRetBlockingAssignment(this.rgd_kickRetBlockIn, new float[4]
    {
      num6,
      -this.yards_20,
      this.yards_20,
      -this.speed_3
    }, EKickRetBlockerType.FrontLineBlocker);
    this.kickRetBlockerOut = (PlayAssignment) new KickRetBlockingAssignment(this.rgd_kickRetBlockOut, new float[4]
    {
      num6,
      -this.yards_20,
      this.yards_20,
      -this.speed_3
    }, EKickRetBlockerType.FrontLineBlocker);
    this.kickRetBlockerBack = (PlayAssignment) new KickRetBlockingAssignment(this.rgd_kickRetBlockBack, new float[4]
    {
      num6,
      -this.yards_20,
      this.yards_20,
      -this.speed_3
    }, EKickRetBlockerType.FrontLineBlocker);
    this.kickRetBlockerBackRT = (PlayAssignment) new KickRetBlockingAssignment(this.rgd_kickRetBlockBackRT, new float[4]
    {
      num6,
      this.yards_10,
      this.yards_20,
      this.speed_0
    }, EKickRetBlockerType.FrontLineBlocker);
    this.kickRetBlockerBackRG = (PlayAssignment) new KickRetBlockingAssignment(this.rgd_kickRetBlockBackRG, new float[4]
    {
      num6,
      this.yards_5,
      this.yards_20,
      this.speed_0
    }, EKickRetBlockerType.FrontLineBlocker);
    this.kickRetBlockerBackC = (PlayAssignment) new KickRetBlockingAssignment(this.rgd_kickRetBlockBackC, new float[4]
    {
      num6,
      0.0f,
      this.yards_20,
      this.speed_0
    }, EKickRetBlockerType.FrontLineBlocker);
    this.kickRetBlockerBackLG = (PlayAssignment) new KickRetBlockingAssignment(this.rgd_kickRetBlockBackLG, new float[4]
    {
      num6,
      -this.yards_5,
      this.yards_20,
      this.speed_0
    }, EKickRetBlockerType.FrontLineBlocker);
    this.kickRetBlockerBackLT = (PlayAssignment) new KickRetBlockingAssignment(this.rgd_kickRetBlockBackLT, new float[4]
    {
      num6,
      -this.yards_10,
      this.yards_20,
      this.speed_0
    }, EKickRetBlockerType.FrontLineBlocker);
    this.upBlocker1 = (PlayAssignment) new KickRetBlockingAssignment(this.empty, new float[4]
    {
      num6,
      -this.yards_10,
      this.yards_35,
      -this.speed_1
    }, EKickRetBlockerType.UpBlocker);
    this.upBlocker2 = (PlayAssignment) new KickRetBlockingAssignment(this.empty, new float[4]
    {
      num6,
      -this.yards_4,
      this.yards_35,
      -this.speed_1
    }, EKickRetBlockerType.UpBlocker);
    this.upBlocker3 = (PlayAssignment) new KickRetBlockingAssignment(this.empty, new float[4]
    {
      num6,
      this.yards_10,
      this.yards_35,
      -this.speed_1
    }, EKickRetBlockerType.UpBlocker);
    this.upBlocker4 = (PlayAssignment) new KickRetBlockingAssignment(this.empty, new float[4]
    {
      num6,
      this.yards_4,
      this.yards_35,
      -this.speed_1
    }, EKickRetBlockerType.UpBlocker);
    this.onsideKickRet = (PlayAssignment) new KickRetBlockingAssignment(this.rgd_onsideKickRet, new float[4]
    {
      num6,
      0.0f,
      0.0f,
      this.speed_0
    }, EKickRetBlockerType.OnsideBlocker);
    this.kickReturn = new PlayAssignment(EPlayAssignmentId.PuntKickReceive, this.empty, new float[4]
    {
      num5,
      -this.yards_6,
      this.yards_40,
      this.speed_0
    });
    this.puntReturn = new PlayAssignment(EPlayAssignmentId.PuntKickReceive, this.empty, new float[4]
    {
      num5,
      0.0f,
      this.yards_30,
      this.speed_0
    });
    this.puntRetBlockerIn = (PlayAssignment) new KickRetBlockingAssignment(this.rgd_kickRetBlockIn, new float[4]
    {
      num6,
      this.yards_15,
      this.yards_30,
      -this.speed_1
    }, EKickRetBlockerType.FrontLineBlocker);
    this.puntRetBlockerOut = (PlayAssignment) new KickRetBlockingAssignment(this.rgd_kickRetBlockOut, new float[4]
    {
      num6,
      -this.yards_15,
      this.yards_30,
      -this.speed_1
    }, EKickRetBlockerType.FrontLineBlocker);
    this.puntRetBlockerCB = (PlayAssignment) new KickRetBlockingAssignment(this.empty, new float[4]
    {
      num6,
      0.0f,
      this.yards_30,
      -this.speed_2
    }, EKickRetBlockerType.PuntJammer);
  }

  private void SetRouteGraphicData_Offense()
  {
    this.rgd_kickoffMid = new RouteGraphicData(30, 0, LineEndType.Arrow);
    this.rgd_kickoffIn = new RouteGraphicData(40, 0, 10, -45, LineEndType.Arrow);
    this.rgd_kickoffLeft = new RouteGraphicData(40, 0, 10, 45, LineEndType.Arrow);
    this.rgd_runBlock = new RouteGraphicData(7, 0, LineEndType.Block);
    this.rgd_passBlock = new RouteGraphicData(7, 180, LineEndType.Block);
    this.rgd_passBlockRB = new RouteGraphicData(7, 0, LineEndType.Block);
    this.rgd_pullBlockIn = new RouteGraphicData(15, -135, 30, 45, LineEndType.Block);
    this.rgd_pullBlockOut = new RouteGraphicData(15, 135, 30, -45, LineEndType.Block);
    this.rgd_diveBlockIn = new RouteGraphicData(20, -30, LineEndType.Block);
    this.rgd_diveBlockOut = new RouteGraphicData(20, 30, LineEndType.Block);
    this.rgd_isoBlockIn = new RouteGraphicData(20, -45, LineEndType.Block);
    this.rgd_isoBlockOut = new RouteGraphicData(20, 45, LineEndType.Block);
    this.rgd_sweepBlockIn = new RouteGraphicData(25, -60, LineEndType.Block);
    this.rgd_sweepBlockOut = new RouteGraphicData(25, 60, LineEndType.Block);
    this.rgd_rbScreenBlockIn = new RouteGraphicData(20, -90, 15, 45, LineEndType.Block);
    this.rgd_rbScreenBlockOut = new RouteGraphicData(20, 90, 15, -45, LineEndType.Block);
    this.rgd_olScreenBlockIn = new RouteGraphicData(15, -135, 30, 45, LineEndType.Block);
    this.rgd_olScreenBlockOut = new RouteGraphicData(15, 135, 30, -45, LineEndType.Block);
    this.rgd_counterOut = new RouteGraphicData(12, -45, 35, 120, LineEndType.Arrow);
    this.rgd_counterIn = new RouteGraphicData(12, 45, 35, -120, LineEndType.Arrow);
    this.rgd_diveIn = new RouteGraphicData(15, -45, 10, 45, LineEndType.Arrow);
    this.rgd_diveOut = new RouteGraphicData(15, 45, 10, -45, LineEndType.Arrow);
    this.rgd_isoIn = new RouteGraphicData(20, -45, 10, 45, LineEndType.Arrow);
    this.rgd_isoOut = new RouteGraphicData(20, 45, 10, -45, LineEndType.Arrow);
    this.rgd_offTackleIn = new RouteGraphicData(40, -45, 10, 45, LineEndType.Arrow);
    this.rgd_offTackleOut = new RouteGraphicData(40, 45, 10, -45, LineEndType.Arrow);
    this.rgd_tossIn = new RouteGraphicData(25, -60, LineEndType.Arrow);
    this.rgd_tossOut = new RouteGraphicData(25, 60, LineEndType.Arrow);
    this.rgd_gunAcross = new RouteGraphicData(20, -90, LineEndType.Arrow);
    this.rgd_gunToss = new RouteGraphicData(15, 60, LineEndType.Arrow);
    this.rgd_splitDive = new RouteGraphicData(15, 0, LineEndType.Arrow);
    this.rgd_splitAcross = new RouteGraphicData(20, -60, LineEndType.Arrow);
    this.rgd_reverse = new RouteGraphicData(15, -135, 40, 45, LineEndType.Arrow);
    this.rgd_slotReverse = new RouteGraphicData(50, -90, LineEndType.Arrow);
    this.rgd_fbDiveIn = new RouteGraphicData(15, -45, 10, 45, LineEndType.Arrow);
    this.rgd_fbDiveOut = new RouteGraphicData(15, 45, 10, -45, LineEndType.Arrow);
    this.rgd_out5 = new RouteGraphicData(25, 0, 25, 90, LineEndType.Arrow);
    this.rgd_out10 = new RouteGraphicData(40, 0, 25, 90, LineEndType.Arrow);
    this.rgd_out15 = new RouteGraphicData(55, 0, 25, 90, LineEndType.Arrow);
    this.rgd_in5 = new RouteGraphicData(25, 0, 40, -90, LineEndType.Arrow);
    this.rgd_in10 = new RouteGraphicData(40, 0, 40, -90, LineEndType.Arrow);
    this.rgd_in15 = new RouteGraphicData(55, 0, 40, -90, LineEndType.Arrow);
    this.rgd_slantIn = new RouteGraphicData(40, -45, LineEndType.Arrow);
    this.rgd_slantInHitch = new RouteGraphicData(45, -45, 15, -135, LineEndType.Arrow);
    this.rgd_slantOut = new RouteGraphicData(40, 45, LineEndType.Arrow);
    this.rgd_post5 = new RouteGraphicData(25, 0, 35, -45, LineEndType.Arrow);
    this.rgd_post10 = new RouteGraphicData(40, 0, 35, -45, LineEndType.Arrow);
    this.rgd_post5flat = new RouteGraphicData(25, 0, 35, -60, LineEndType.Arrow);
    this.rgd_post10flat = new RouteGraphicData(40, 0, 35, -60, LineEndType.Arrow);
    this.rgd_post5skinny = new RouteGraphicData(25, 0, 35, -30, LineEndType.Arrow);
    this.rgd_post10skinny = new RouteGraphicData(40, 0, 35, -30, LineEndType.Arrow);
    this.rgd_corner5 = new RouteGraphicData(25, 0, 35, 45, LineEndType.Arrow);
    this.rgd_corner10 = new RouteGraphicData(40, 0, 35, 45, LineEndType.Arrow);
    this.rgd_corner5flat = new RouteGraphicData(25, 0, 35, 60, LineEndType.Arrow);
    this.rgd_corner10flat = new RouteGraphicData(40, 0, 35, 60, LineEndType.Arrow);
    this.rgd_corner5skinny = new RouteGraphicData(25, 0, 35, 30, LineEndType.Arrow);
    this.rgd_corner10skinny = new RouteGraphicData(40, 0, 35, 30, LineEndType.Arrow);
    this.rgd_fly = new RouteGraphicData(70, 0, LineEndType.Arrow);
    this.rgd_rbFlatIn = new RouteGraphicData(25, -60, LineEndType.Arrow);
    this.rgd_rbFlatOut = new RouteGraphicData(25, 60, LineEndType.Arrow);
    this.rgd_rbFlatOut_Fly = new RouteGraphicData(60, 60, 10, -60, LineEndType.Arrow);
    this.rgd_rbFlatIn_Fly = new RouteGraphicData(60, -60, 10, 60, LineEndType.Arrow);
    this.rgd_rbOffTackleOut_In = new RouteGraphicData(50, 45, 50, -45, 15, -90, LineEndType.Arrow);
    this.rgd_rbDiveIn_In = new RouteGraphicData(10, -45, 70, 45, 15, -90, LineEndType.Arrow);
    this.rgd_rbDiveIn_Out = new RouteGraphicData(10, -45, 70, 45, 15, 90, LineEndType.Arrow);
    this.rgd_rbOffTackleIn_In = new RouteGraphicData(50, -45, 40, 45, 15, 90, LineEndType.Arrow);
    this.rgd_rbScreenIn = new RouteGraphicData(40, -90, LineEndType.Arrow);
    this.rgd_rbScreenOut = new RouteGraphicData(40, 90, LineEndType.Arrow);
    this.rgd_fbDiveIn_In = new RouteGraphicData(10, -45, 45, 45, 15, -90, LineEndType.Arrow);
    this.rgd_fbDiveIn_Out = new RouteGraphicData(10, -45, 45, 45, 15, 90, LineEndType.Arrow);
    this.rgd_PAIsoOut_Out = new RouteGraphicData(10, 45, 70, -45, 15, 90, LineEndType.Arrow);
    this.rgd_PAIsoOut_In = new RouteGraphicData(10, 45, 70, -45, 15, -90, LineEndType.Arrow);
    this.rgd_PAIsoIn_Out = new RouteGraphicData(10, -45, 70, 45, 15, 90, LineEndType.Arrow);
    this.rgd_PAIsoIn_In = new RouteGraphicData(10, -45, 70, 45, 15, -90, LineEndType.Arrow);
    this.rgd_rbGunOffTackleIn_Post = new RouteGraphicData(60, -60, 25, 60, 20, 45, LineEndType.Arrow);
    this.rgd_rbGunOffTackleIn_Out = new RouteGraphicData(60, -60, 25, 60, 15, -90, LineEndType.Arrow);
    this.rgd_rbGunOffTackleIn_In = new RouteGraphicData(60, -60, 25, 60, 15, 90, LineEndType.Arrow);
    this.rgd_wrScreen = new RouteGraphicData(25, -180, LineEndType.Arrow);
    this.rgd_underIn = new RouteGraphicData(20, -135, 8, 45, LineEndType.Arrow);
    this.rgd_underOut = new RouteGraphicData(20, 135, 8, -45, LineEndType.Arrow);
    this.rgd_hitch5in = new RouteGraphicData(25, 0, 15, -135, LineEndType.Arrow);
    this.rgd_hitch5out = new RouteGraphicData(25, 0, 15, 135, LineEndType.Arrow);
    this.rgd_hitch10in = new RouteGraphicData(40, 0, 15, -135, LineEndType.Arrow);
    this.rgd_hitchInFly = new RouteGraphicData(40, 0, 15, -135, 25, 135, LineEndType.Arrow);
    this.rgd_hitch10out = new RouteGraphicData(40, 0, 15, 135, LineEndType.Arrow);
    this.rgd_upCornerFly = new RouteGraphicData(25, 0, 25, 45, 20, -45, LineEndType.Arrow);
    this.rgd_underOutUp = new RouteGraphicData(15, 90, 35, -90, LineEndType.Arrow);
    this.rgd_underOutUpCorner = new RouteGraphicData(10, 90, 30, -90, 20, 45, LineEndType.Arrow);
    this.rgd_underInUpPost = new RouteGraphicData(20, -90, 30, 90, 20, -45, LineEndType.Arrow);
    this.rgd_rbIsoIn_HitchIn = new RouteGraphicData(10, -45, 80, 45, 10, 135, LineEndType.Arrow);
    this.rgd_dragIn = new RouteGraphicData(15, -45, 20, -45, LineEndType.Arrow);
    this.rgd_dragInFromSlot = new RouteGraphicData(15, 0, 20, -45, 40, -45, LineEndType.Arrow);
    this.rgd_dragOut = new RouteGraphicData(15, 45, 20, 45, LineEndType.Arrow);
    this.rgd_dragOutFromSlot = new RouteGraphicData(15, 0, 20, 45, 20, 45, LineEndType.Arrow);
    this.rgd_upPostHitch = new RouteGraphicData(25, 0, 30, -45, 15, -90, LineEndType.Arrow);
    this.rgd_upPostIn = new RouteGraphicData(25, 0, 30, -45, 15, -45, LineEndType.Arrow);
    this.rgd_upPostOut = new RouteGraphicData(25, 0, 30, -45, 15, 135, LineEndType.Arrow);
    this.rgd_upOutFly = new RouteGraphicData(30, 0, 20, 90, 25, -90, LineEndType.Arrow);
    this.rgd_upCornerHitch = new RouteGraphicData(25, 0, 30, 45, 15, 90, LineEndType.Arrow);
    this.rgd_upInPost = new RouteGraphicData(25, 0, 25, -90, 20, 45, LineEndType.Arrow);
    this.rgd_post5corner = new RouteGraphicData(25, 0, 30, -45, 20, 90, LineEndType.Arrow);
    this.rgd_post5out = new RouteGraphicData(25, 0, 30, -45, 20, 135, LineEndType.Arrow);
    this.rgd_slantInIn = new RouteGraphicData(40, -45, 20, -45, LineEndType.Arrow);
    this.rgd_slantInFly = new RouteGraphicData(40, -45, 25, 45, LineEndType.Arrow);
    this.rgd_slantOutFly = new RouteGraphicData(40, 45, 35, -45, LineEndType.Arrow);
    this.rgd_slantInUpHitchOut = new RouteGraphicData(40, -45, 25, 45, 10, 135, LineEndType.Arrow);
    this.rgd_slantInUpCorner = new RouteGraphicData(40, -45, 25, 45, 30, 45, LineEndType.Arrow);
    this.rgd_slantInUpPost = new RouteGraphicData(40, -45, 25, 45, 30, -45, LineEndType.Arrow);
    this.rgd_slantInLowPost = new RouteGraphicData(40, -60, 35, 15, LineEndType.Arrow);
    this.rgd_slantInLowPostSkinny = new RouteGraphicData(40, -60, 35, 45, LineEndType.Arrow);
    this.rgd_slantInPostFlat = new RouteGraphicData(40, -45, 35, -15, LineEndType.Arrow);
    this.rgd_slantInPostSkinny = new RouteGraphicData(40, -45, 35, 15, LineEndType.Arrow);
    this.rgd_slantInHighPost = new RouteGraphicData(40, -30, 35, -15, LineEndType.Arrow);
    this.rgd_slantInHighPostFlat = new RouteGraphicData(40, -30, 35, -30, LineEndType.Arrow);
    this.rgd_qbRun = new RouteGraphicData(25, 0, LineEndType.Arrow);
  }

  private void SetRouteGraphicData_Defense()
  {
    this.empty = new RouteGraphicData();
    this.rgd_deep1of1 = new RouteGraphicData(ZoneType.Deep2of3);
    this.rgd_deep1of2 = new RouteGraphicData(ZoneType.Deep1of2);
    this.rgd_deep2of2 = new RouteGraphicData(ZoneType.Deep2of2);
    this.rgd_deep1of3 = new RouteGraphicData(ZoneType.Deep1of3);
    this.rgd_deep2of3 = new RouteGraphicData(ZoneType.Deep2of3);
    this.rgd_deep3of3 = new RouteGraphicData(ZoneType.Deep3of3);
    this.rgd_deep1of4 = new RouteGraphicData(ZoneType.Deep1of4);
    this.rgd_deep2of4 = new RouteGraphicData(ZoneType.Deep2of4);
    this.rgd_deep3of4 = new RouteGraphicData(ZoneType.Deep3of4);
    this.rgd_deep4of4 = new RouteGraphicData(ZoneType.Deep4of4);
    this.rgd_mid1of2 = new RouteGraphicData(ZoneType.Mid1of2);
    this.rgd_mid2of2 = new RouteGraphicData(ZoneType.Mid2of2);
    this.rgd_mid1of3 = new RouteGraphicData(ZoneType.Mid1of3);
    this.rgd_mid2of3 = new RouteGraphicData(ZoneType.Mid2of3);
    this.rgd_mid3of3 = new RouteGraphicData(ZoneType.Mid3of3);
    this.rgd_mid1of4 = new RouteGraphicData(ZoneType.Mid1of4);
    this.rgd_mid2of4 = new RouteGraphicData(ZoneType.Mid2of4);
    this.rgd_mid3of4 = new RouteGraphicData(ZoneType.Mid3of4);
    this.rgd_mid4of4 = new RouteGraphicData(ZoneType.Mid4of4);
    this.rgd_flatLeft = new RouteGraphicData(ZoneType.FlatLeft);
    this.rgd_flatRight = new RouteGraphicData(ZoneType.FlatRight);
    this.rgd_blitz9 = new RouteGraphicData(BlitzType.Hole9);
    this.rgd_blitz7 = new RouteGraphicData(BlitzType.Hole7);
    this.rgd_blitz5 = new RouteGraphicData(BlitzType.Hole5);
    this.rgd_blitz3 = new RouteGraphicData(BlitzType.Hole3);
    this.rgd_blitz1 = new RouteGraphicData(BlitzType.Hole1);
    this.rgd_blitz0 = new RouteGraphicData(BlitzType.Hole0);
    this.rgd_blitz2 = new RouteGraphicData(BlitzType.Hole2);
    this.rgd_blitz4 = new RouteGraphicData(BlitzType.Hole4);
    this.rgd_blitz6 = new RouteGraphicData(BlitzType.Hole6);
    this.rgd_blitz8 = new RouteGraphicData(BlitzType.Hole8);
    this.rgd_blitz10 = new RouteGraphicData(BlitzType.Hole10);
    this.rgd_kickRetBlockIn = new RouteGraphicData(25, -45, LineEndType.Block);
    this.rgd_kickRetBlockOut = new RouteGraphicData(25, 45, LineEndType.Block);
    this.rgd_kickRetBlockBack = new RouteGraphicData(25, 0, LineEndType.Block);
    this.rgd_onsideKickRet = new RouteGraphicData();
    this.rgd_lineman = new RouteGraphicData(10, 180, LineEndType.Arrow);
    this.rgd_lineman.blitzType = BlitzType.Lineman;
    this.rgd_stuntOut = new RouteGraphicData(15, 45, 5, 45, LineEndType.Arrow);
    this.rgd_stuntOut.blitzType = BlitzType.Lineman;
    this.rgd_stuntIn = new RouteGraphicData(15, -45, 5, -45, LineEndType.Arrow);
    this.rgd_stuntIn.blitzType = BlitzType.Lineman;
  }
}
